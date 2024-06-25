using Labs.Janelas.LabsEstoque;
using Labs.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Labs.Janelas.LabsPDV.Dependencias
{
    /// <summary>
    /// Lógica interna para JanelaDePagamentoWPF.xaml
    /// </summary>
    public partial class JanelaDePagamentoWPF : Window
    {
        //Eventos
        public delegate void PagamentoFinalizado(JanelaDePagamentoWPF Janela);
        public event PagamentoFinalizado OnPagamentoFinalizado = null!;
        //
        //
        MeiosPagamento MeiosPagamento { get; set; } = null!;
        //
        List<PagamentoEfetuado> PagamentosEfetuados { get; set; } = new();
        //
        List<Produto> Produtos { get; set; } = new();
        //
        LabsPDVWPF LabsPDV { get; set; } = null!; // referencia a janela de pdv que requisitou a janela de pagamento
                                               //
                                               //Variáveis de Construtor
        private double ValorTotal = 0;
        private double ValorTotalComDesconto = 0;
        private double ValorTotalRecebido = 0;
        private double ValorDescontoPorcentagem = 0;
        private double ValorTroco = 0;
        private double FaltaReceber = 0;
        //
        public JanelaDePagamentoWPF()
        {
            InitializeComponent();
            this.Closed += LimpaEstaJanela;
            // Carrega a lista de Modos de Pagamento presente na DataBase;
        }

        void GetMeios()
        {
            if (LabsPDV != null)
            {
                MeiosPagamento = LabsPDV.CaixaLabs.Meios;
                //
                MeioDePagamentoComboBox.Items.Clear();
                foreach (var Meio in MeiosPagamento.Meios)
                {
                    MeioDePagamentoComboBox.Items.Add(Meio.Item1);
                }
            }
        }
        //--------------------------//
        //		   METODOS
        //--------------------------//
        //
        private void Reset()
        {
            MeioDePagamentoComboBox.Text = null;
            PagamentoBoxInput.Text = null;
            //
            ListaPagamentosEfetuados.Items.Clear();
            PagamentosEfetuados.Clear();
            Produtos.Clear();
            //
            ResetFocus();
        }
        double getPorcentagem()
        {
            return ValorDescontoPorcentagem * 0.01f;
        }
        void UpdateInterface()
        {
            //
            ValorRecebidoBox.Text = $"R$ {ValorTotalRecebido}";
            ValorTotalComDescontoBox.Text = $"R$ {ValorTotalComDesconto}";
            FaltaReceberValorBox.Text = $"R$ {FaltaReceber}";
            TrocoBox.Text = $"R$ {this.ValorTroco}";
        }
        private void ResetFocus()
        {
            PagamentoBoxInput.Focus();
        }
        private void LimpaEstaJanela(object? sender, EventArgs e)
        {
            ValorTotalBox.Text = null;
        }
        //
        private void SetPagamentoTotalBox(double valor) // Atualiza o texto do pagamento de maneira padronizada.
        {
            ValorTotalBox.Text = $"R$: {valor}";
        }
        /// <summary>
        /// Inicia a tela de pagamento usando os parâmetros Fornecidos
        /// </summary>
        /// <param name="ValorTotal">Valor total dos itens</param>
        /// <param name="Produtos">Produtos Da venda (Quantidade etc)</param>
        /// <param name="LabsPDV">O Ponto PDV que requisitou a janela (Será usado no futuro para pontos remotos)</param>
        public void IniciarTelaDePagamento(double ValorTotal, List<Produto> Produtos, LabsPDVWPF LabsPDV)
        {
            //Garante que todos os campos estejam limpos para receber novos valores
            Reset();
            //
            // seta o valor total visualmente pro operador de caixa
            SetPagamentoTotalBox(ValorTotal);
            //
            this.ValorTotal = ValorTotal;
            this.LabsPDV = LabsPDV;
            Produtos.ForEach(this.Produtos.Add);
            //
            DescontoBox.Text = "0";
            //
            //
            GetMeios();
            //
            RealizarCalculos(0, 0);
            //
            UpdateInterface();
        }
        //
        async void Finalizar()
        {
            //Previne que a venda seja finalizada sem receber o valor total do pagamento.
            //
            if (FaltaReceber > 0) { Modais.MostrarInfo($"Ainda Falta Receber R$: {FaltaReceber} !"); return; }
            // Adiciona o valor Recebido ao meio correspondente
            if (LabsPDV != null)
            {
                foreach (var pagEfet in PagamentosEfetuados)
                {
                    var index = pagEfet.ID;
                    double valorR = Math.Round(pagEfet.Valor - pagEfet.ValorTroco,2);
                    // ValorR é o cálculo de quanto foi recebido de fato através desse pagamento (para evitar problemas na contabilidade final do caixa)
                    LabsPDV.CaixaLabs.AdicionarCapitalAoMeio(index, valorR);
                }
                //
                LabsPDV.CaixaLabs.AtualizarCaixa(); // Atualizar é importante para termos controle dos Valores Recebidos!
                //
                // o ID da venda é literalmente o horario e a data do ano em que foi realizada (o que impede de ter ID's repetidos :D)
                string IDVenda = $"{DateTime.Now:ddMMyyyyy}{DateTime.Now:HHmmss}";
                // Geramos o objeto de venda e salvamos no banco de dados
                VendaRealizada venda = new()
                {
                    Desconto = ValorDescontoPorcentagem,
                    Produtos = [.. Produtos], // Repassa pra array
                    Total = ValorTotal,
                    TotalComDesconto = ValorTotalComDesconto,
                    ValorPago = ValorTotalRecebido,
                    Troco = ValorTroco,
                    PagamentosEfetuados = [.. PagamentosEfetuados],//Repassa pra array
                    IDVenda = IDVenda
                };
                await CloudDataBase.RegisterLocalAsync(Collections.Vendas, venda);
                //
                // Aqui faz a impressão do cupom fiscal (ou não fiscal)
                using (var PM = new PrintManager())
                {
                    MessageBoxResult re = Modais.MostrarPergunta("Imprimir Via do Estabelecimento?");
                    if(re == MessageBoxResult.Yes)
                    {
                        PM.ImprimirCupomNaoFiscalLoja(PrintManager.ImpressoraDefault, venda);
                    }
                    //
                    //
                    MessageBoxResult r = Modais.MostrarPergunta("Imprimir Via do Cliente?");
                    if (r == MessageBoxResult.Yes)
                    {
                        PM.ImprimirCupomNaoFiscalCliente(PrintManager.ImpressoraDefault, venda);
                    }
                }
                // Sinaliza que a venda foi finalizada com sucesso
                //
                await LabsEstoqueWPF.AbaterProdutosEmEstoqueAsync(Produtos);
                Modais.MostrarInfo("Venda Finalizada com Sucesso!");
                OnPagamentoFinalizado?.Invoke(this);
                Reset();
                //
                this.Close();
            }
        }
        //Ao Cancelar, somente voltamos para a tela de PDV (Vai que o cliente esqueceu de comprar algo né)
        void Cancelar()
        {
            MessageBoxResult r = Modais.MostrarPergunta("Você Deseja Retornar Para a Tela do PDV?");
            //
            if (r == MessageBoxResult.Yes)
            {
                //
                Reset();
                this.Close();
            }
        }
        //
        /// <summary>
        /// Realiza os Cálculos de troco, falta receber e valor recebido, junto com valor de desconto
        /// </summary>
        /// <param name="ValorPago">Valor do pagamento efetuado</param>
        /// <param name="descontoPorcentagem">Desconto aplicado na venda (Esse valor é constante para todos os pagamentos (não afeta o pagamento, somente o valor total))</param>
        void RealizarCalculos(double ValorPago, double descontoPorcentagem)
        {
            ValorTotalComDesconto = Math.Round((ValorTotal - (ValorTotal * descontoPorcentagem)), 2); // Já calculamos esse primeiro já que será usado no resto
                                                                                                      //
            ValorTotalRecebido += Math.Round(ValorPago, 2);
            FaltaReceber = Math.Round(ValorTotalComDesconto - ValorTotalRecebido, 2);
            ValorTroco = 0;
            if (ValorTotalRecebido > ValorTotalComDesconto) { ValorTroco = Math.Round(ValorTotalRecebido - ValorTotalComDesconto, 2); FaltaReceber = 0; }
            //
            UpdateInterface();
        }
        //
        void AdicionarPagamento()
        {
            if (MeioDePagamentoComboBox.Items.Count < 1) { Modais.MostrarErro("Nenhum Meio de Pagamento Registrado!"); return; }
            if (MeioDePagamentoComboBox.Text.IsNullOrEmpty()) { Modais.MostrarAviso("Selecione um Meio de Pagamento!"); return; }
            //
            string valorSTR = PagamentoBoxInput.Text;
            string descSTR = DescontoBox.Text;
            bool SLDV = MeiosPagamento.Meios[MeioDePagamentoComboBox.SelectedIndex].Item2; // Item2 é SLDV
                                                                                           //
            if (FaltaReceber > 0)
            {
                //
                if (!Utils.TryParseToDouble(valorSTR, out double valorPag)) { Modais.MostrarAviso("Insira um Valor de Pagamento Válido!"); return; }
                if (!Utils.TryParseToDouble(descSTR, out ValorDescontoPorcentagem)) { Modais.MostrarAviso("Insira um Valor de Desconto Válido"); return; }
                if (!SLDV)
                {
                    if (valorPag > FaltaReceber) { valorPag = FaltaReceber; }
                }
                //
                RealizarCalculos(valorPag, getPorcentagem());
                // é importante que o pagamento efetuado e a lista de pagamento sejam atualizados juntos para manter o mesmo index
                PagamentoEfetuado pagEfet = new(MeioDePagamentoComboBox.SelectedIndex, MeioDePagamentoComboBox.Text, Math.Round(valorPag, 2),ValorTroco);
                PagamentosEfetuados.Add(pagEfet);
                //
                ListaPagamentosEfetuados.Items.Add(pagEfet);
                //
                PagamentoBoxInput.Text = null!;
            }
        }
        //
        void ExcluirUmPagamento()
        {
            PagamentoEfetuado? pagEfet = ListaPagamentosEfetuados.SelectedItem as PagamentoEfetuado;
            if (pagEfet == null) { Modais.MostrarAviso("Você Deve Selecionar um Pagamento da lista Para Removêlo"); return; }
            //
            MessageBoxResult r = Modais.MostrarPergunta("Você Deseja Remover o Pagamento Selecionado?");
            if (r == MessageBoxResult.Yes)
            {
                try
                {
                                                                                //
                    ValorTotalRecebido -= pagEfet.Valor;
                    //
                    PagamentosEfetuados.Remove(pagEfet);
                    //
                    ListaPagamentosEfetuados.Items.Remove(pagEfet);
                    //
                    Modais.MostrarInfo("Pagamento Removido com Sucesso!");
                    //
                    RealizarCalculos(0, getPorcentagem());
                    UpdateInterface();
                }
                catch (Exception ex)
                {
                    Modais.MostrarAviso($"Não foi Possível Remover o Pagamento Selecionado \n{ex.Message}");
                    throw;
                }
            }
        }
        //--------------------------//
        //		   EVENTOS
        //--------------------------//
        private void OnJanelaDePagamentoKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    PagamentoBoxInput.Focus();
                    break;
                case Key.F2:
                    Finalizar();
                    break;
                case Key.F3:
                    Cancelar();
                    break;
                case Key.F4:
                    DescontoBox.Focus();
                    break;
                case Key.F5:
                    ExcluirUmPagamento();
                    break;
            }
        }

        private void OnBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender == DescontoBox)
                {
                    if (!Utils.TryParseToDouble(DescontoBox.Text, out ValorDescontoPorcentagem)) { Modais.MostrarAviso("Insira um Valor de Desconto Válido"); return; }
                    RealizarCalculos(0, getPorcentagem()); // chamamos o realizar calculo aqui para atualizar o desconto
                }
                if (sender == PagamentoBoxInput) { AdicionarPagamento(); }
            }
        }
        //
        private void ExcluirPagamento_Click(object sender, RoutedEventArgs e)
        {
            ExcluirUmPagamento();
        }
        //
        private void FinalizarButton_Click(object sender, RoutedEventArgs e)
        {
            Finalizar();
        }

        private void CancelarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Cancelar();
        }
    }
}

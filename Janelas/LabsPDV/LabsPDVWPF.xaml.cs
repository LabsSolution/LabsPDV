using Labs.Janelas.LabsPDV.Dependencias;
using Labs.LABS_PDV;
using MongoDB.Driver;
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
using static Labs.LABS_PDV.Modelos;
using Color = System.Windows.Media.Color;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Labs.Janelas.LabsPDV
{
    /// <summary>
    /// Lógica interna para LabsPDV_WPF.xaml
    /// </summary>
    public partial class LabsPDVWPF : Window
    {
        //Constantes porque precisamos que não usem outro endereçamento de memória
        //Coisa que o static faz a cada instância
        const string CaixaAberto = "CAIXA ABERTO";
        const string CaixaFechado = "CAIXA FECHADO";
        const string AbrirCaixaText = "ABRIR CAIXA";
        const string FecharCaixaText = "FECHAR CAIXA";
        //
        private static bool EstaAberto { get; set; } = false; // deixar isso como false por padrão
        private static bool RealizandoVenda { get; set; } = false;
        //
        private protected List<Produto> Produtos = []; // Usado para o gerenciamento do valor total
        private protected double PagamentoTotal = 0.0; // registro do pagamento total
                                                       //
        public CaixaLabs CaixaLabs { get; private set; } = null!;
        public OperadorCaixa Operador { get; private set; } = null!;
        public EstadoCaixa EstadoCaixa { get; private set; } = null!;
        //
        internal class ProdutoVisual(string Numero, string Descricao, string CodBarras, string Unidade, string Preco, string Quantidade, string TotalItem, string Desconto) 
        {
            public string Numero { get; set; } = Numero;
            public string Descricao { get; set; } = Descricao;
            public string CodBarras { get; set; } = CodBarras;
            public string Unidade { get; set; } = Unidade;
            public string Preco { get; set; } = Preco;
            public string Quantidade { get; set; } = Quantidade;
            public string TotalItem { get; set; } = TotalItem;
            public string Desconto { get; set; } = Desconto;
        }
        //
        public LabsPDVWPF()
        {
            InitializeComponent();
        }
        private void LabsPDVLoaded(object sender, RoutedEventArgs e)
        {
            FoiEncerradoInesperadamente();
        }
        //
        private async void FoiEncerradoInesperadamente()
        {
            EstadoCaixa = await CloudDataBase.GetLocalAsync<EstadoCaixa>(Collections.EstadoCaixa, _ => true);
            if (EstadoCaixa != null)
            {
                MessageBoxResult r = Modais.MostrarPergunta("O Sistema foi encerrado de maneira inesperada\nDeseja Retornar ao Caixa Salvo?");
                if (r == MessageBoxResult.Yes)
                {
                    Operador = EstadoCaixa.OperadorCaixa;
                    CaixaLabs = EstadoCaixa.CaixaLabs;
                    EstaAberto = EstadoCaixa.CaixaAberto;
                    RealizandoVenda = EstadoCaixa.RealizandoVenda;
                    //Por últimos atualizamos os produtos
                    foreach (Produto produto in EstadoCaixa.Produtos)
                    {
                        AddProduto(produto, out double TotalItem);
                        // Ao Adicionar o produto na lista, limpamos o código de barras e resetamos a quantidade para somente 1 (para evitar de replicar a quantidade anterior);
                        PagamentoTotal += TotalItem;
                        //
                    }
                    PagamentoTotal = Math.Round(PagamentoTotal, 2);
                    SetPagamentoTotalBox();
                    //
                    AbrirCaixaVisual();
                    //
                    Modais.MostrarInfo("Caixa Recuperado Com Sucesso!");
                }
            }
        }
        //
        public void AbrirCaixa()
        {
            //
            //FAZER JANELINHA DE ABERTURA DE CAIXA // Gestão de Fluxo
            var JDAC = LABS_PDV_MAIN_WPF.IniciarDependencia<JanelaAberturaDeCaixaWPF>(App =>
            {
                App.onJDACClose += RealizarAbertura;
            }, true, false);
        }
        public void FecharCaixa()
        {
            if (RealizandoVenda) { Modais.MostrarAviso("Você não pode fechar o caixa com uma venda em progresso!"); return; }
            //
            var JFC = LABS_PDV_MAIN_WPF.IniciarDependencia<JanelaFechamentoDeCaixaWPF>(App =>
            {
                App.InicializarFechamento(CaixaLabs);
                App.onJFCClose += RealizarFechamento;
                //
            }, true, false);
        }
        //
        public void AbrirCaixaVisual()
        {
            //Atualizamos a parte visual
            EstaAberto = true;
            CaixaStateLabel.Content = CaixaAberto;
            AbrirFecharCaixaButtonLabel.Text = FecharCaixaText;
            AbrirFecharCaixaButton.Background = new SolidColorBrush(Color.FromArgb(255,200,80,80));
            CaixaStateColor.Background = new SolidColorBrush(Color.FromArgb(255,80,255,80));
            //
            QuantidadeInput.IsEnabled = true;
            CodBarrasInput.IsEnabled = true;
            //
            QuantidadeInput.Text = "1";
            CodBarrasInput.Focus();
        }
        public void FecharCaixaVisual()
        {
            //
            EstaAberto = false;
            CaixaStateLabel.Content = CaixaFechado;
            AbrirFecharCaixaButtonLabel.Text = AbrirCaixaText;
            AbrirFecharCaixaButton.Background = new SolidColorBrush(Color.FromArgb(255, 80, 255, 80));
            CaixaStateColor.Background = new SolidColorBrush(Color.FromArgb(255, 255, 80, 80));
            QuantidadeInput.IsEnabled = false;
            CodBarrasInput.IsEnabled = false;
            //
            QuantidadeInput.Text = null;
        }
        private async void UpdateEstadoCaixa()
        {
            await CloudDataBase.UpdateOneLocalAsync(Collections.EstadoCaixa, EstadoCaixa, Builders<EstadoCaixa>.Filter.Eq("ID", EstadoCaixa.ID));
        }
        //
        /// <summary>
        /// Chamado quando a janela de abertura de caixa é "finalizada";
        /// </summary>
        /// <param name="ValorDeAbertura">Valor total com que o caixa está abrindo</param>
        /// <param name="Janela">Retorno da Própria janela</param>
        private async void RealizarAbertura(double ValorDeAbertura, JanelaAberturaDeCaixaWPF Janela)
        {
            //Iniciamos o CaixaLabs, se não conseguirmos lançamos um erro
            Operador = new("Operador Teste", "User", "Pass");
            //
            CaixaLabs = new(ValorDeAbertura, Operador); // Aqui estamos iniciando na maluquice
                                                        //
            if (CaixaLabs == null) { Modais.MostrarErro("ERRO CRÍTICO!\nA Comunicação com um módulo interno foi Interrompida!"); return; }
            //
            CaixaLabs.RealizarAbertura();
            //
            Janela.Close();
            AbrirCaixaVisual();
            Modais.MostrarInfo("Caixa Aberto com Sucesso! \nBOAS VENDAS!");
            //
            if (EstadoCaixa == null)
            {
                EstadoCaixa = new()
                {
                    CaixaLabs = CaixaLabs,
                    RealizandoVenda = RealizandoVenda,
                    CaixaAberto = EstaAberto,
                    Produtos = Produtos,
                    OperadorCaixa = Operador
                };
                await CloudDataBase.RegisterLocalAsync(Collections.EstadoCaixa, EstadoCaixa);
            }
        }
        //
        public async void RealizarFechamento(JanelaFechamentoDeCaixaWPF Janela)
        {
            FecharCaixaVisual();
            //
            await CloudDataBase.RemoveLocalAsync<EstadoCaixa>(Collections.EstadoCaixa, _ => true); // ao realizar o fechamento do caixa, não precisamos mais do monitoramento
                                                                                             //
            Janela.Close();
            Modais.MostrarInfo("Caixa Fechado com Sucesso!");
        }
        //----------------------------//
        //			METODOS
        //----------------------------//
        //
        private void ResetarFoco() { CodBarrasInput.Focus(); } // Essa função faz com que o foco do cursor seja o Input de código de barras
        private void ResetarInterface() // função auto explicativa (eu acho)
        {
            //Resetamos os campos Principais que é o valor de pagamento, a lista visual e a lista de produtos;
            PagamentoTotal = 0;
            ListaDeVenda.Items.Clear();
            Produtos.Clear();
            //
            RealizandoVenda = false;
            DescricaoProdutoBox.Text = "Descrição do Produto";
            PagamentoTotalBox.Text = "Total a Pagar R$: 0,00";
            CodBarrasInput.Text = null;
            QuantidadeBox.Text = "1";
            //
            QuantidadeInput.Text = "1";
            //
            PrecoUnitarioBox.Text = "R$: 0,00";
            SubTotalBox.Text = "R$: 0,00";
            //
            CodBarrasInput.Focus();
            //
            EstadoCaixa.RealizandoVenda = false;
            EstadoCaixa.Produtos = Produtos;
            UpdateEstadoCaixa();
        }
        //
        private void SetPagamentoTotalBox() // Atualiza o texto do pagamento de maneira padronizada.
        {
            PagamentoTotalBox.Text = $"Total a Pagar R$: {PagamentoTotal}";
        }
        //
        private void AddProduto(Produto produto, out double TotalItem) // Adiciona um produto na lista de venda
        {
            if (!RealizandoVenda)
            {
                RealizandoVenda = true;
                EstadoCaixa.RealizandoVenda = true;
                UpdateEstadoCaixa();
            }
            // assim que o primeiro produto é registrado, setamos que uma venda está sendo realizada
            // Atualizamos também no estado caixa
            //
            DescricaoProdutoBox.Text = produto.Descricao; // pegamos a descrição do produto
                                                          //// Geramos o numero apresentado na tela (Começando por 1 já que a maioria não iria entender se começar por 0).
            string Numero = (ListaDeVenda.Items.Count + 1).ToString();
            //
            TotalItem = Math.Round(produto.Quantidade * produto.Preco, 2); ; // geramos o total do item fazendo Quant * valor.
            //Adicionamos o item na lista visual
            ListaDeVenda.Items.Add(new ProdutoVisual(Numero,produto.Descricao,produto.CodBarras,"UN",$"R$: {produto.Preco}", $"{produto.Quantidade}",$"{TotalItem}","N/A"));
            ListaDeVenda.ScrollIntoView(ListaDeVenda.Items[^1]);
            //Adicionamos o produto na lista de produtos da venda
            Produtos.Add(produto);
        }
        /// <summary>
        /// Remove um produto da lista de venda. (Caso esteja sendo realizado alguma venda no momento)
        /// </summary>
        private void RemoverProduto()
        {
            if (!EstaAberto) { return; }
            if (!RealizandoVenda) { Modais.MostrarAviso("Você não está realizando nenhuma venda no momento!"); return; }
            if (ListaDeVenda.SelectedItem is not ProdutoVisual pv) { Modais.MostrarAviso("Selecione um Produto da Lista Primeiro!"); ResetarFoco(); return; }
            //
            string nomeProduto = pv.Descricao; // pegamos o nome do produto

            //Perguntamos se o Usuário quer realmente Realizar a operação
            MessageBoxResult res = Modais.MostrarPergunta($"Deseja Remover o Produto: {nomeProduto}?\nESTA OPERAÇÃO NÃO PODE SER DESFEITA!");

            //Após informado dos riscos seguimos em frente
            if (res == MessageBoxResult.No) { Modais.MostrarInfo("Exclusão do Item Cancelada"); ResetarFoco(); return; }
            //
            int pIndex = ListaDeVenda.Items.IndexOf(pv); // pegamos o index do item
            Produtos.RemoveAt(pIndex); // removemos da lista de produtos no index
            ListaDeVenda.Items.Remove(pv); // removemos também da lista visual

            // Logo após isso atualizamos as listas e a tela de venda
            double TotalHolder = 0.0;
            foreach (ProdutoVisual produto in ListaDeVenda.Items)
            {
                int index = ListaDeVenda.Items.IndexOf(produto);
                produto.Numero = $"{index + 1}";
                //
                var p = Produtos[index];
                //
                TotalHolder += p.Quantidade * p.Preco;
            }
            // Atualizamos o pagamento total
            PagamentoTotal = Math.Round(TotalHolder, 2);
            PagamentoTotalBox.Text = PagamentoTotal.ToString();
            SetPagamentoTotalBox(); // Essa função apenas atualiza o visor de preço pro cliente
                                    //Atualizamos o Estado Caixa (Reflexão)
                                    //
            EstadoCaixa.Produtos = Produtos;
            UpdateEstadoCaixa();
            // resetamos o foco novamente para o Input de Cod de Barras
            ResetarFoco();
        }
        //	if (!EstaAberto) { return; } -- Usado para checar se o caixa está aberto, caso contrário, simplesmente cancelamos a execução
        // dos métodos abaixo
        private void Pagamento()
        {
            if (!EstaAberto) { Modais.MostrarAviso("Realize a Abertura do Caixa Primeiro!"); return; }
            if (!RealizandoVenda) { Modais.MostrarAviso("Você não está realizando nenhuma venda no momento!"); return; }

            //AS ETAPAS ACIMA DEVEM SER DESABILITADAS PARA AGILIZAR O PROCESSO DE DEV
            //Mostra a janela de conclusão de venda e atrela o evento de fechamento da janela com a finalização da venda
            JanelaDePagamentoWPF janelaDePagamento = LABS_PDV_MAIN_WPF.IniciarDependencia<JanelaDePagamentoWPF>(app =>
            {
                //Atrelamos o evento para a finalização
                app.OnPagamentoFinalizado += OnPagamentoFinalizado;
                app.IniciarTelaDePagamento(PagamentoTotal, Produtos, this);
            });
        }
        //
        private void OnPagamentoFinalizado(JanelaDePagamentoWPF Janela)
        {
            //Desatrelamos o evento para prevenir vazamento de memória e resetamos a interface
            Janela.OnPagamentoFinalizado -= OnPagamentoFinalizado;
            ResetarInterface();
        }
        //
        private void CancelarVenda()
        {
            if (!EstaAberto) { return; }
            if (!RealizandoVenda) { Modais.MostrarAviso("Você não está realizando nenhuma venda no momento!"); return; }
            //Ao Cancelar a venda, simplesmente descartamos todos os items e resetamos os campos;
            MessageBoxResult r = Modais.MostrarPergunta("Você Realmente Deseja Cancelar esta Venda?\nESTA AÇÃO NÃO PODE SER DESFEITA!");
            if(r == MessageBoxResult.Yes)
            {
                ResetarInterface();
            }
        }
        //---EVENTOS--//
        //Chamado quando pressiona alguma tecla na tela de PDV
        private void OnPDVKeyUp(object sender, KeyEventArgs e)
        {
            //Usamos SwitchCase por questão de performance (Endereçamento direto)
            switch (e.Key)
            {
                case Key.F1:
                    Pagamento();
                    break;
                case Key.F2:
                    CancelarVenda();
                    break;
                case Key.F3:
                    RemoverProduto();
                    break;
                case Key.F4:
                    QuantidadeInput.Focus();
                    break;
            }
        }
        //Chamado quando alguma tecla é pressionada na área de Quantidade
        private void OnQuantidadeKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) { return; } // se não for enter não seguimos
            string Quantidade = QuantidadeInput.Text;
            //
            if (Utils.TryParseToInt(Quantidade, out int QTD))
            {
                if(QTD == 0) { QuantidadeBox.Text = "1"; }
                else { QuantidadeBox.Text = QTD.ToString(); }
                QuantidadeInput.Text = "1";
                ResetarFoco();
            }
            else { Modais.MostrarErro("Somente Números!"); }
        }
        //Chamado quando alguma tecla é pressionada na área de cód de barras
        //
        private async void OnAddProduto()
        {
            //
            if (QuantidadeBox.Text.Length < 1) { QuantidadeBox.Text = "1"; }
            //
            if (Utils.IsValidBarCode(CodBarrasInput.Text))
            {
                Produto produto = await Utils.GetProdutoByCode(CodBarrasInput.Text);
                if (produto != null)
                {
                    // só prosseguimos com a adição na lista de venda, caso o produto exista
                    // no banco de dados
                    //
                    // Alteramos a quantidade porque não queremos vender o estoque inteiro de uma vez só KKK
                    int QTD = int.Parse(QuantidadeBox.Text);
                    if (QTD > produto.Quantidade) { Modais.MostrarAviso("A quantidade passada é maior que a contida no estoque!"); return; } // caso a quantidade que estamos passando for maior que a disponível no estoque;
                                                                                                                                             //
                    produto.Quantidade = int.Parse(QuantidadeBox.Text);
                    AddProduto(produto, out double TotalItem);
                    // Ao Adicionar o produto na lista, limpamos o código de barras e resetamos a quantidade para somente 1 (para evitar de replicar a quantidade anterior);
                    CodBarrasInput.Text = null;
                    QuantidadeBox.Text = "1";
                    //
                    PrecoUnitarioBox.Text = $"{produto.Preco}";
                    SubTotalBox.Text = TotalItem.ToString();
                    //
                    PagamentoTotal += TotalItem;
                    PagamentoTotal = Math.Round(PagamentoTotal, 2);
                    //
                    SetPagamentoTotalBox();
                    //
                    EstadoCaixa.Produtos = Produtos; // Aqui refletimos
                                                     // Atualizamos o estado Caixa do Banco de dados
                    UpdateEstadoCaixa();
                }
                else
                {
                    Modais.MostrarAviso("Produto Não Cadastrado no Estoque!");
                }
            }
        }
        private void OnCodBarrasKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) { return; } // se não for enter não seguimos
            OnAddProduto();
            //
        }
        //
        private void PagamentoButton_Click(object sender, RoutedEventArgs e)
        {
            Pagamento();
        }
        //
        private void CancelarVendaButton_Click(object sender, RoutedEventArgs e)
        {
            CancelarVenda();
        }
        //
        private void ExcluirItemButton_Click(object sender, RoutedEventArgs e)
        {
            RemoverProduto();
        }
        //
        private void DevolucaoButton_Click(object sender, RoutedEventArgs e)
        {
            LABS_PDV_MAIN_WPF.IniciarDependencia<JanelaDeDevolucaoWPF>(null!,true,false);
        }
        //
        private void VoltarButton_Click(object sender, RoutedEventArgs e)
        {
            // Bem simples, se o caixa estiver aberto, caso o cliente queira voltar pra tela anterior, ele será mantido rodando
            // Caso contrário simplesmente fechamos para não consumir memória desnecessária;
            if (EstaAberto) { this.Visibility = Visibility.Hidden; return; }
            if (!EstaAberto) { this.Close(); }
        }

        private void AbrirFecharCaixaButton_Click(object sender, RoutedEventArgs e)
        {
            //Fazemos verificação para só abrir o caixa com a senha do usuário atual;
            //Por enquanto deixamos sem para DEV
            if (EstaAberto) { FecharCaixa(); return; }
            if (!EstaAberto) { AbrirCaixa(); return; }
        }
    }
}

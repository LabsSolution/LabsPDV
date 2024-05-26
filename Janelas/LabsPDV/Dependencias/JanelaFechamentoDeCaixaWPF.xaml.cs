using Labs.LABS_PDV;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Labs.Janelas.LabsPDV.Dependencias
{
    /// <summary>
    /// Lógica interna para JanelaFechamentoDeCaixaWPF.xaml
    /// </summary>
    public partial class JanelaFechamentoDeCaixaWPF : Window
    {
        public delegate void OnJFCClose(JanelaFechamentoDeCaixaWPF Janela);
        public event OnJFCClose onJFCClose = null!;
        //
        ObservableCollection<ValorFechamento> ValoresParaAferimentoMeio = [];
        ObservableCollection<ValorFechamento> ValoresParaAferimentoGeral = [];
        //
        //
        FechamentoDeCaixa Fechamento = null!;
        //
        CaixaLabs CaixaLabs { get; set; } = null!;
        //
        public JanelaFechamentoDeCaixaWPF()
        {
            InitializeComponent();
            //
            ListaAferimentoMeios.ItemsSource = ValoresParaAferimentoMeio;
            //
            ListaAferimentoGeral.ItemsSource = ValoresParaAferimentoGeral;
        }
        //
        private void FecharJanela()
        {
            if (onJFCClose != null) { onJFCClose(this); }
        }
        private void OnCellChanged(object sender, EventArgs e)
        {
            if (sender == ListaAferimentoMeios) 
            {
                if (ListaAferimentoMeios.SelectedItem is ValorFechamento a)
                {
                    UpdateAferimentoMeios();
                }
            }
        }
        //
        private void UpdateAferimentoMeios()
        {
            double gT = 0;
            for (int i = 0; i < ValoresParaAferimentoMeio.Count-1; i++)
            {
                gT += ValoresParaAferimentoMeio[i].ValorAferido;
            }
            ValorFechamento lvf = ValoresParaAferimentoMeio[^1];
            ValoresParaAferimentoMeio[^1] = new ValorFechamento(lvf.Nome,gT,lvf.ValorAferido);
        }
        //
        public void InicializarFechamento(CaixaLabs CaixaLabs)
        {
            //Limpamos a lista de controle para aferimento
            this.CaixaLabs = CaixaLabs;
            //
            if (this.CaixaLabs == null) { return; }
            //
            ValoresParaAferimentoGeral.Clear();
            ValoresParaAferimentoMeio.Clear();
            //
            DataLabel.Content = $"DATA: {DateTime.Now:dd/MM/yyyy} | HORA: {DateTime.Now:HH:mm}";
            //
            ValoresParaAferimentoGeral.Add(new("Valor de Abertura", CaixaLabs.ValorDeAbertura, 0));
            //
            ValoresParaAferimentoGeral.Add(new("Fundo de Caixa", CaixaLabs.FundoDeCaixa, 0));
            //
            //Aqui adicionamos os meios
            for (int i = 0; i < this.CaixaLabs.RegistroInternoDePagamentos.Count; i++)
            {
                RIDP registroInterno = this.CaixaLabs.RegistroInternoDePagamentos[i];
                //
                ValoresParaAferimentoMeio.Add(new(registroInterno.NomeDoRegistro, registroInterno.CapitalDeGiro, 0));
                //
            }
            ValoresParaAferimentoMeio.Add(new("Ganhos Totais", this.CaixaLabs.GanhosTotais, 0));
            //
        }
        //
        //EVENTOS
        private void RealizarFechamentoButton_Click(object sender, RoutedEventArgs e)
        {
            List<ValorFechado> ValoresFechadosMeio = [];
            foreach  (ValorFechamento vpafm in ValoresParaAferimentoMeio) { ValoresFechadosMeio.Add(new(vpafm.Nome,vpafm.ValorSistema,vpafm.ValorAferido)); }
            //
            List<ValorFechado> ValoresFechadosGeral = [];
            foreach  (ValorFechamento vpafg in ValoresParaAferimentoGeral) { ValoresFechadosGeral.Add(new(vpafg.Nome,vpafg.ValorSistema,vpafg.ValorAferido)); }
            //Os campos acima são de extrema importância para a geração de notas de fechamento após o fechamento do caixa e para gestão quando for realizado pesquisas
            //Criamos um novo objeto de fechamento de caixa
            Fechamento = new()
            {
                FechamentoID = $"{DateTime.Now:ddMMyyyy}{DateTime.Now:HHmmss}",
                FundoDeCaixa = CaixaLabs.FundoDeCaixa,
                GanhosTotais = CaixaLabs.GanhosTotais,
                ValorDeAbertura = CaixaLabs.ValorDeAbertura,
                Recebimentos = [.. CaixaLabs.RegistroInternoDePagamentos], // repassa para array
                ValoresFechadosMeio = ValoresFechadosMeio, // Salva a lista de aferimentos Meio
                ValoresFechadosGeral = ValoresFechadosGeral, // Salva a lista de aferimentos Geral
                Sangrias = [], // Não implementado ainda
                Suprimentos = [], // Não implementado ainda
                ItensDevolvidos = 0,
                ItensVendidos = 0,
            };
            // Registramos na database local
            CloudDataBase.RegisterLocalAsync(Collections.Fechamentos, Fechamento);
            // Caso o cliente tenha a assinatura cloud, refletimos para a database cloud
            //Realizar espelhamento aqui.
            //Logo após imprimimos o cupom de fechamento
            DialogResult r = Modais.MostrarPergunta("Deseja Imprimir a Nota de Fechamento?");
            if (r == System.Windows.Forms.DialogResult.Yes)
            {
                using (var PM = new PrintManager())
                {
                    PM.ImprimirCupomFechamentoDeCaixa(PrintManager.ImpressoraDefault, Fechamento);
                }
            }
            //
            //
            FecharJanela();
        }
        //
        private void VoltarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

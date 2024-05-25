using Labs.LABS_PDV;
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

namespace Labs.Janelas.LabsPDV.Dependencias
{
    /// <summary>
    /// Lógica interna para JanelaAberturaDeCaixaWPF.xaml
    /// </summary>
    public partial class JanelaAberturaDeCaixaWPF : Window
    {
        public delegate void OnJDACClose(double CapitalDeAbertura, JanelaAberturaDeCaixaWPF Janela);
        /// <summary>
        /// Evento Para Fechamento da Janela (Essa Janela é uma dependência interna de Alto Nível, Ou seja a janela que chamou deve fechá-la)
        /// </summary>
        public event OnJDACClose onJDACClose = null!;
        //
        public double CapitalEmCaixa { get; set; } = 0;
        public double CapitalTotal { get; set; } = 0;
        //
        internal class ValorDeAbertura(string Nome, double Valor)
        {
            public string Nome { get; set; } = Nome;
            public double Valor { get; set; } = Valor;
        }
        //
        private List<ValorDeAbertura> ValoresDeAbertura { get; set; } = new();
        //
        public JanelaAberturaDeCaixaWPF()
        {
            //Não remover esse Campo; Contrutor do WPF
            InitializeComponent();
            //
            if (LabsCripto.Decript("CAIXA", out string Decripted))
            {
                if (Utils.TryParseToDouble(Decripted, out double Value))
                {
                    CapitalEmCaixa = Value;
                }
            }
            //Calcula
            Calc();
            //Realiza a parte visual
            UpdateVisual();
        }
        private void Calc()
        {
            double total = 0;
            foreach (ValorDeAbertura vda in ValoresDeAbertura)
            {
                total += vda.Valor;
                total = Math.Round(total, 2);
            }
            CapitalTotal = CapitalEmCaixa + total;
        }
        private void UpdateVisual()
        {
            CapitalEmCaixaBox.Text = $"R$ {CapitalEmCaixa}";
            CapitalTotalBox.Text = $"R$ {CapitalTotal}";
        }
        //
        /// <summary>
        /// Adiciona Capital ao Caixa
        /// </summary>
        private void AdicionarCapital()
        {
            if (Utils.TryParseToDouble(AdicionarCapitalInputBox.Text, out double value))
            {
                //
                ValorDeAbertura item = new("DINHEIRO", value);
                // Adicionamos na lista visual e na lista de controle (Essencialmente os itens vão ter o mesmo index)
                ValoresDeAbertura.Add(item);
                ListaCapitaisAdicionados.Items.Add(item);
                //Logo Após Adicionarmos limpamos para evitar duplo clique acidental
                AdicionarCapitalInputBox.Text = null!;// --> o Null! é só pra indicar que o valor pode ser nulo e não nulo de fato ;d
                //Depois disso tudo, calculamos e atualizamos
                Calc();
                UpdateVisual();
            }
            else { Modais.MostrarAviso("Somente Números!"); }
        }

        private void AdicionarCapitalButton_Click(object sender, RoutedEventArgs e)
        {
            AdicionarCapital();
        }

        private void RemoverCapitalButton_Click(object sender, RoutedEventArgs e)
        {
            //Realizamos Quick return por comodidade
            ValorDeAbertura? vda = ListaCapitaisAdicionados.SelectedItem as ValorDeAbertura;
            if (vda == null) { Modais.MostrarAviso("Selecione o valor que deseja remover!"); return; }
            //Garantimos que esse Item existe... até porquê não queremos erros ;D
            if (ListaCapitaisAdicionados.Items.Contains(vda) && ValoresDeAbertura.Contains(vda))
            {
                ListaCapitaisAdicionados.Items.Remove(vda);
                ValoresDeAbertura.Remove(vda);
            }
            //Após isso tudo, recalculamos e atualizamos
            Calc();
            UpdateVisual();
        }

        private void AdicionarCapitalInputBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AdicionarCapital();
            }
        }

        private void AbrirCaixaButton_Click(object sender, RoutedEventArgs e)
        {
            if (onJDACClose != null) // se tiver alguém ouvindo o evento, disparamos
            {
                if (CapitalTotal == 0) { Modais.MostrarAviso("Não é Possível Prosseguir sem Fundo de Caixa!"); return; }
                //
                onJDACClose(CapitalTotal, this);
            }
        }
    }
}

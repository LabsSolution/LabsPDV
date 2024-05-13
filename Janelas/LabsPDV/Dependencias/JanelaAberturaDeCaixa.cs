using Labs.LABS_PDV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Janelas.LabsPDV.Dependencias
{
    public partial class JanelaAberturaDeCaixa : Form
    {

        public delegate void OnJDACClose(double CapitalDeAbertura,JanelaAberturaDeCaixa Janela);
        /// <summary>
        /// Evento Para Fechamento da Janela (Essa Janela é uma dependência interna de Alto Nível, Ou seja a janela que chamou deve fechá-la)
        /// </summary>
        public event OnJDACClose onJDACClose = null!;
        //
        public double CapitalEmCaixa { get; set; } = 0;
        public double CapitalTotal { get; set; } = 0;
        //
        private List<double> ValoresDeAbertura { get; set; } = new();
        //

        public JanelaAberturaDeCaixa()
        {
            //Não remover esse Campo; Contrutor do form
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
        //
        private void Calc()
        {
            double total = 0;
            foreach (var valor in ValoresDeAbertura)
            {
                total += valor;
                total = Math.Round(total, 2);
            }
            CapitalTotal = total;
        }
        //
        private void UpdateVisual()
        {
            CapitalEmCaixaBox.Text = $"R$ {CapitalEmCaixa}";
            CapitalTotalBox.Text = $"R$ {CapitalTotal}";
        }
        /// <summary>
        /// Adiciona Capital ao Caixa
        /// </summary>
        private void AdicionarCapital()
        {
            if (Utils.TryParseToDouble(AdicionarCapitalInputBox.Text, out double value))
            {
                //
                ListViewItem item = new(["DINHEIRO", $"R$ {value}"]);
                // Adicionamos na lista visual e na lista de controle (Essencialmente os itens vão ter o mesmo index)
                ValoresDeAbertura.Add(value);
                ListaCapitaisAdicionados.Items.Add(item);
                //Logo Após Adicionarmos limpamos para evitar duplo clique acidental
                AdicionarCapitalInputBox.Text = null!;// --> o Null! é só pra indicar que o valor pode ser nulo e não nulo de fato ;d
                //Depois disso tudo, calculamos e atualizamos
                Calc();
                UpdateVisual();
            }
            else { Modais.MostrarAviso("Somente Números!"); }
        }
        //
        private void AdicionarCapitalButton_Click(object sender, EventArgs e)
        {
            AdicionarCapital();
        }
        //
        private void RemoverCapitalButton_Click(object sender, EventArgs e)
        {
            //Realizamos Quick return por comodidade
            if (ListaCapitaisAdicionados.SelectedItems.Count == 0) { Modais.MostrarAviso("Selecione o valor que deseja remover!"); return; }
            //
            var index = ListaCapitaisAdicionados.SelectedItems[0].Index;
            //Garantimos que esse index existe... até porquê não queremos erros ;D
            if (ListaCapitaisAdicionados.Items.Count > 0 && ValoresDeAbertura.Count > 0)
            {
                ListaCapitaisAdicionados.Items.RemoveAt(index);
                ValoresDeAbertura.RemoveAt(index);
            }
            //Após isso tudo, recalculamos e atualizamos
            Calc();
            UpdateVisual();
        }

        private void OnAdicionarCapitalInputBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AdicionarCapital();
            }
        }
        private void AbrirCaixaButton_Click(object sender, EventArgs e)
        {
            if(onJDACClose != null) // se tiver alguém ouvindo o evento, disparamos
            {
                onJDACClose(CapitalTotal,this);
            }
        }
    }
}

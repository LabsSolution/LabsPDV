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
    public partial class JanelaFechamentoDeCaixa : Form
    {
        public delegate void OnJFCClose(JanelaFechamentoDeCaixa Janela);
        public event OnJFCClose onJFCClose = null!;
        //
        internal class ValorFechamento
        {
            public string Nome { get; set; } = null!;
            public double ValorSistema { get; set; } = 0;
            public double ValorAferido { get; set; } = 0;
            public ValorFechamento(string Nome, double ValorSistema, double ValorAferido) 
            {
                this.Nome = Nome;
                this.ValorAferido = ValorAferido;
                this.ValorSistema = ValorSistema;
            }
            //
            public bool AferirValor() { return this.ValorSistema == this.ValorAferido; }
        }

        //

        Dictionary<string,double[]> ValoresParaAferimento = [];
        //
        public JanelaFechamentoDeCaixa()
        {
            InitializeComponent();
        }
        //
        private void OnCellChanged(object? sender, DataGridViewCellEventArgs e)
        {
            var rIndex = e.RowIndex;
            var cIndex = e.ColumnIndex;
            //
            //
            if (sender == ListaAferimentoMeios) 
            {
                if(cIndex == 2)
                {
                    var cell = ListaAferimentoMeios.Rows[rIndex].Cells[cIndex];
                    if(Utils.TryParseToDouble($"{cell.Value}",out double value))
                    {
                        ValoresParaAferimento[$"{ListaAferimentoMeios.Rows[rIndex].Cells[0].Value}"].Item2 = value;
                    }
                    else { Modais.MostrarAviso($"Aferimento do Meio:{ListaAferimentoMeios.Rows[rIndex].Cells[0].Value}\nValor Inválido Inserido!"); return; }
                }
                //
                UpdateAferimentoMeios(); 
            }
        }
        //
        public void InicializarFechamento(CaixaLabs caixaLabs)
        {
            //Limpamos a lista de controle para aferimento
            ValoresParaAferimento.Clear();
            //
            LabelDataHora.Text = $"DATA: {DateTime.Now:dd/MM/yyyy} | HORA: {DateTime.Now:HH:mm}";
            //
            CreateRowGeral("Valor de Abertura", caixaLabs.ValorDeAbertura);
            ValoresParaAferimento.Add("Valor de Abertura",new(caixaLabs.ValorDeAbertura,0));
            //
            CreateRowGeral("Fundo de Caixa", caixaLabs.FundoDeCaixa);
            ValoresParaAferimento.Add("Fundo de Caixa",new(caixaLabs.FundoDeCaixa,0));
            //
            //Aqui adicionamos os meios
            for (int i = 0; i < caixaLabs.RegistroInternoDePagamentos.Count; i++)
            {
                RIDP registroInterno = caixaLabs.RegistroInternoDePagamentos[i];
                CreateRowMeio(registroInterno.NomeDoRegistro, registroInterno.CapitalDeGiro);
                //
                ValoresParaAferimento.Add(registroInterno.NomeDoRegistro,new (registroInterno.CapitalDeGiro,0));
                //
            }
            CreateRowMeio("Ganhos Totais", caixaLabs.GanhosTotais);
            ValoresParaAferimento.Add("Ganhos Totais",new (caixaLabs.GanhosTotais,0));
            //Atribuimos o evento
            //Por último adicionamos os ganhos totais (No Aferimento de meios)
            //
            ListaAferimentoMeios.CellValueChanged += OnCellChanged;
        }
        //
        private void VerificarAferimentos()
        {
            //
            bool Proceed = true;
            foreach (var VPA in ValoresParaAferimento)
            {

            }
        }
        //
        private void UpdateAferimentoMeios()
        {
            double gT = 0;
            int lastIndex = ListaAferimentoMeios.Rows.Count - 1;
            for (int i = 0; i < ListaAferimentoMeios.Rows.Count - 1; i++) // o -1 é para evitar que atualize a ultima linha
            {
                DataGridViewRow rowMeio = ListaAferimentoMeios.Rows[i];
                if (Utils.TryParseToDouble($"{rowMeio.Cells[2].Value}", out double value))
                {
                    gT += value;
                }
            }
            ListaAferimentoMeios.Rows[lastIndex].Cells[1].Value = $"R$: {Utils.FormatarValor(gT)}";
        }
        //PRIVADOS
        private void CreateRowMeio(string Descricao, double ValorAferidoSistema)
        {
            var index = ListaAferimentoMeios.Rows.Count;
            DataGridViewRow rowMeio = new();
            ListaAferimentoMeios.Rows.Insert(index, rowMeio);
            ListaAferimentoMeios.Rows[index].SetValues(Descricao, $"R$: {Utils.FormatarValor(ValorAferidoSistema)}", 0);
        }
        //
        private void CreateRowGeral(string Descricao, double ValorAferidoSistema)
        {
            var index = ListaAferimentoGeral.Rows.Count;
            DataGridViewRow rowGeral = new();
            ListaAferimentoGeral.Rows.Insert(index, rowGeral);
            ListaAferimentoGeral.Rows[index].SetValues(Descricao, $"R$: {Utils.FormatarValor(ValorAferidoSistema)}", 0);
        }

        //
        private void FecharJanela()
        {
            if (onJFCClose != null) { onJFCClose(this); }
        }

        private void VoltarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RealizarFechamentoButton_Click(object sender, EventArgs e)
        {

        }
    }
}

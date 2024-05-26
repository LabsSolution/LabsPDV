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
using static Labs.LABS_PDV.Modelos;

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
        List<ValorFechamento> ValoresParaAferimento = [];
        FechamentoDeCaixa Fechamento = null!;
        //
        CaixaLabs CaixaLabs { get; set; } = null!;
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
                        string? NomeMeio = $"{ListaAferimentoMeios.Rows[rIndex].Cells[0].Value}";
                        foreach (ValorFechamento vf in ValoresParaAferimento) { if(vf.Nome == NomeMeio) { vf.ValorAferido = value; } }
                    }
                    else { Modais.MostrarAviso($"Aferimento do Meio:{ListaAferimentoMeios.Rows[rIndex].Cells[0].Value}\nValor Inválido Inserido!"); return; }
                }
                //
                UpdateAferimentoMeios(); 
            }
            if (sender == ListaAferimentoGeral) 
            {
                if(cIndex == 2)
                {
                    var cell = ListaAferimentoGeral.Rows[rIndex].Cells[cIndex];
                    if(Utils.TryParseToDouble($"{cell.Value}",out double value))
                    {
                        string? NomeMeio = $"{ListaAferimentoGeral.Rows[rIndex].Cells[0].Value}";
                        foreach (ValorFechamento vf in ValoresParaAferimento) { if(vf.Nome == NomeMeio) { vf.ValorAferido = value; } }
                    }
                    else { Modais.MostrarAviso($"Aferimento do Meio:{ListaAferimentoGeral.Rows[rIndex].Cells[0].Value}\nValor Inválido Inserido!"); return; }
                }
                //
                UpdateAferimentoMeios(); 
            }
        }
        //
        public void InicializarFechamento(CaixaLabs CaixaLabs)
        {
            //Limpamos a lista de controle para aferimento
            this.CaixaLabs = CaixaLabs;
            //
            if(this.CaixaLabs == null) { return; }
            //
            ValoresParaAferimento.Clear();
            //
            LabelDataHora.Text = $"DATA: {DateTime.Now:dd/MM/yyyy} | HORA: {DateTime.Now:HH:mm}";
            //
            CreateRowGeral("Valor de Abertura", this.CaixaLabs.ValorDeAbertura);
            ValoresParaAferimento.Add(new("Valor de Abertura", CaixaLabs.ValorDeAbertura, 0));
            //
            CreateRowGeral("Fundo de Caixa", this.CaixaLabs.FundoDeCaixa);
            ValoresParaAferimento.Add(new("Fundo de Caixa", CaixaLabs.FundoDeCaixa, 0));
            //
            //Aqui adicionamos os meios
            for (int i = 0; i < this.CaixaLabs.RegistroInternoDePagamentos.Count; i++)
            {
                RIDP registroInterno = this.CaixaLabs.RegistroInternoDePagamentos[i];
                CreateRowMeio(registroInterno.NomeDoRegistro, registroInterno.CapitalDeGiro);
                //
                ValoresParaAferimento.Add(new(registroInterno.NomeDoRegistro, registroInterno.CapitalDeGiro, 0));
                //
            }
            CreateRowMeio("Ganhos Totais", this.CaixaLabs.GanhosTotais);
            ValoresParaAferimento.Add(new("Ganhos Totais", this.CaixaLabs.GanhosTotais, 0));
            //Atribuimos o evento
            //Por último adicionamos os ganhos totais (No Aferimento de meios)
            //
            ListaAferimentoMeios.CellValueChanged += OnCellChanged;
            ListaAferimentoGeral.CellValueChanged += OnCellChanged;
            //
        }
        //
        private bool VerificarAferimentos()
        {
            //
            foreach (var VPA in ValoresParaAferimento)
            {
                if(VPA.ValorSistema != VPA.ValorAferido) { Modais.MostrarAviso($"Aferimento do Meio:{VPA.Nome}\nOs valores não Coincidem!"); return false; }
            }
            return true;
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
            ListaAferimentoMeios.CellValueChanged -= OnCellChanged;
            ListaAferimentoGeral.CellValueChanged -= OnCellChanged;
        }

        private void RealizarFechamentoButton_Click(object sender, EventArgs e)
        {
            if (VerificarAferimentos())
            {

                Fechamento = new()
                {
                    FechamentoID = $"{DateTime.Now:ddMMyyyy}{DateTime.Now:HHmmss}",
                    FundoDeCaixa = CaixaLabs.FundoDeCaixa,
                    GanhosTotais = CaixaLabs.GanhosTotais,
                    ValorDeAbertura = CaixaLabs.ValorDeAbertura,
                    Recebimentos = [.. CaixaLabs.RegistroInternoDePagamentos], // repassa para array
                    Sangrias = [], // Não implementado ainda
                    Suprimentos = [], // Não implementado ainda
                    ItensDevolvidos = 0,
                    ItensVendidos = 0,
                };
                //
                CloudDataBase.RegisterLocalAsync(Collections.Fechamentos,Fechamento);
                //Realizar espelhamento aqui.
                //Logo após imprimimos o cupom de fechamento
                DialogResult r = Modais.MostrarPergunta("Deseja Imprimir a Nota de Fechamento?");
                if(r == DialogResult.Yes)
                {
                    using (var PM = new PrintManager())
                    {
                        PM.ImprimirCupomFechamentoDeCaixa(PrintManager.ImpressoraDefault,Fechamento);
                    }
                }
                //
                //
                FecharJanela();
            }
        }
    }
}

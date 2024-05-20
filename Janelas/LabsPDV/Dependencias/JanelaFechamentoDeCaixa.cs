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
        public JanelaFechamentoDeCaixa()
        {
            InitializeComponent();
        }
        //
        private void OnCellChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if(sender == ListaAferimentoMeios) { UpdateAferimentoMeios(); }
        }
        //
        public void InicializarFechamento(CaixaLabs caixaLabs)
        {
            LabelDataHora.Text = $"DATA: {DateTime.Now:dd/MM/yyyy} | HORA: {DateTime.Now:HH:mm}";
            //
            CreateRowGeral("Valor de Abertura",caixaLabs.ValorDeAbertura);
            CreateRowGeral("Fundo de Caixa",caixaLabs.FundoDeCaixa);
            //Aqui adicionamos os meios
            for (int i = 0; i < caixaLabs.RegistroInternoDePagamentos.Count; i++)
            {
                RIDP registroInterno = caixaLabs.RegistroInternoDePagamentos[i];
                CreateRowMeio(registroInterno.NomeDoRegistro, registroInterno.CapitalDeGiro);
            }
            CreateRowMeio("Ganhos Totais",caixaLabs.GanhosTotais);
            //Atribuimos o evento
            //Por último adicionamos os ganhos totais (No Aferimento de meios)
            //
            ListaAferimentoMeios.CellValueChanged += OnCellChanged;
        }
        //

        private void UpdateAferimentoMeios()
        {
            double gT = 0;
            int lastIndex = ListaAferimentoMeios.Rows.Count-1;
            for (int i = 0; i < ListaAferimentoMeios.Rows.Count-1; i++) // o -1 é para evitar que atualize a ultima linha
            {
                DataGridViewRow rowMeio = ListaAferimentoMeios.Rows[i];
                if (Utils.TryParseToDouble($"{rowMeio.Cells[2].Value}",out double value))
                {
                    gT += value;
                }
                else { Modais.MostrarAviso($"Aferimento do Meio:{rowMeio.Cells[0].Value}\nValor Inválido Inserido!"); return; }
            }
            ListaAferimentoMeios.Rows[lastIndex].Cells[1].Value = $"R$: {Utils.FormatarValor(gT)}";
        }
        //PRIVADOS
        private void CreateRowMeio(string Descricao, double ValorAferidoSistema)
        {
            var index = ListaAferimentoMeios.Rows.Count;
            DataGridViewRow rowMeio = new ();
            ListaAferimentoMeios.Rows.Insert(index,rowMeio);
            ListaAferimentoMeios.Rows[index].SetValues(Descricao,$"R$: {Utils.FormatarValor(ValorAferidoSistema)}",0);
        }
        //
        private void CreateRowGeral(string Descricao, double ValorAferidoSistema)
        {
            var index = ListaAferimentoGeral.Rows.Count;
            DataGridViewRow rowGeral = new();
            ListaAferimentoGeral.Rows.Insert(index, rowGeral);
            ListaAferimentoGeral.Rows[index].SetValues(Descricao, $"R$: {Utils.FormatarValor(ValorAferidoSistema)}",0);
        }

        //
        private void FecharJanela()
        {
            if (onJFCClose != null) { onJFCClose(this); }
        }
    }
}

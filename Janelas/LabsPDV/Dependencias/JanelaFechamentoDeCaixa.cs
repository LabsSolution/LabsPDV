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





        public JanelaFechamentoDeCaixa()
        {
            InitializeComponent();
        }

        public void InicializarFechamento(CaixaLabs caixaLabs)
        {

        }
        //




        //PRIVADOS
        private void CreateRowMeio(string Descricao, double ValorAferidoSistema)
        {
            var index = ListaAferimentoMeios.Rows.Count;
            DataGridViewRow rowMeio = new ();
            ListaAferimentoMeios.Rows.Insert(index,rowMeio);
            ListaAferimentoMeios.Rows[index].SetValues(Descricao,$"R$ {Utils.FormatarValor(ValorAferidoSistema)}");
        }
        //
        private void CreateRowGeral(string Descricao, double ValorAferidoSistema)
        {
            var index = ListaAferimentoGeral.Rows.Count;
            DataGridViewRow rowMeio = new();
            ListaAferimentoGeral.Rows.Insert(index, rowMeio);
            ListaAferimentoGeral.Rows[index].SetValues(Descricao, $"R$ {Utils.FormatarValor(ValorAferidoSistema)}");
        }

        //
        private void FecharJanela()
        {
            if (onJFCClose != null) { onJFCClose(this); }
        }
    }
}

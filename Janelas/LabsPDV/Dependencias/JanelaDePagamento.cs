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
	public partial class JanelaDePagamento : Form
	{
		public JanelaDePagamento()
		{
			InitializeComponent();
			this.FormClosed += LimpaEstaJanela;
		}

		private void LimpaEstaJanela(object? sender, FormClosedEventArgs e)
		{
			ListaProdutosPagamento.Items.Clear();
			PagamentoTotalBox.Text = null;
		}

		//
		private void SetPagamentoTotalBox(double valor) // Atualiza o texto do pagamento de maneira padronizada.
		{
			PagamentoTotalBox.Text = $"Total a Pagar R$: {valor}";
		}
		//
		public void IniciarTelaDePagamento(List<Produto> produtos,double PagamentoTotal)
		{
			//Por garantia, limpamos a lista
			ListaProdutosPagamento.Items.Clear();
			foreach (Produto produto in produtos)
			{
				//
				//
				double total = Math.Round(produto.Quantidade * produto.GetPrecoAsDouble(), 2);
				ListaProdutosPagamento.Items.Add(new ListViewItem([produto.Descricao, "UN", $"R$ {produto.Preco}",$"{produto.Quantidade}",$"{total}"]));
				//
				SetPagamentoTotalBox(PagamentoTotal);
			}
		}
	}
}

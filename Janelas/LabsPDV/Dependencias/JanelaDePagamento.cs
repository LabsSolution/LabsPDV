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
	public partial class JanelaDePagamento : Form
	{
		//
		public const string NomeArquivoConfig = "ModosDePagamento";
		//
		List<ModoDePagamento> ModosDePagamento = new();
		//
		public JanelaDePagamento()
		{
			InitializeComponent();
			this.FormClosed += LimpaEstaJanela;
			// Carrega a lista de Modos de Pagamento presente nas configs;
			if (!JsonManager.ChecarConfig(NomeArquivoConfig))
			{
				Modais.MostrarAviso("Não foi Encontrado o Arquivo Contendo os Modos de Pagamento!");
			}
			// Caso seja encontrado, carrega o arquivo
			else { ModosDePagamento = JsonManager.CarregarConfig<List<ModoDePagamento>>(NomeArquivoConfig); }
			//
		}
		//
		private void LimpaEstaJanela(object? sender, FormClosedEventArgs e)
		{
			ValorTotalBox.Text = null;
		}
		//
		private void SetPagamentoTotalBox(double valor) // Atualiza o texto do pagamento de maneira padronizada.
		{
			ValorTotalBox.Text = $"R$: {valor}";
		}
		//
		public void IniciarTelaDePagamento(double ValorTotal)
		{
			//
			SetPagamentoTotalBox(ValorTotal);
			//Lista os modos de pagamento
			foreach (ModoDePagamento modo in ModosDePagamento)
			{
				//
				ListViewItem item = new([modo.Modo,$"%{modo.Taxa}"]);
				//
				if (modo.Ativo) { ListaMeioDePagamento.Items.Add(item); }
			}
			//
		}
		//--------------------------//
		//		   EVENTOS
		//--------------------------//
		private void OnJanelaDePagamentoKeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:

					break;
				case Keys.F2:

					break;
				case Keys.F3:

					break;
				case Keys.F4:

					break;
			}
		}
	}
}

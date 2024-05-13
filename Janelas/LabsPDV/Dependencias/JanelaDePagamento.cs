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
		MeiosPagamento MeiosPagamento { get; set; } = null!;
		//
		List<PagamentoEfetuado> PagamentosEfetuados = new();
		//
		List<Produto> Produtos = new();
		//
		LabsPDV LabsPDV { get; set; } = null!; // referencia a janela de pdv que requisitou a janela de pagamento
		//
		//Variáveis de Construtor
		private double ValorTotal = 0;
		private double ValorTotalComDesconto = 0;
		private double ValorTotalRecebido = 0;
		private double ValorDescontoPorcentagem = 0;
		private double ValorTroco = 0;
		private double FaltaReceber = 0;
		public JanelaDePagamento()
		{
			InitializeComponent();
			this.FormClosed += LimpaEstaJanela;
			// Carrega a lista de Modos de Pagamento presente na DataBase;
		}

		void GetMeios()
		{
			if(LabsPDV != null)
			{
                MeiosPagamento = LabsPDV.CaixaLabs.Meios;
				//
				MeioDePagamentoComboBox.Items.Clear();
				foreach (var Meio in MeiosPagamento.Meios)
				{
					MeioDePagamentoComboBox.Items.Add(Meio.Item1);
				}
			}
		}
		//--------------------------//
		//		   METODOS
		//--------------------------//
		//
		private void Reset()
		{
			MeioDePagamentoComboBox.Text = null;
			PagamentoBoxInput.Text = null;
			//
			ListaPagamentosEfetuados.Items.Clear();
			PagamentosEfetuados.Clear();
			Produtos.Clear();
			//
			ResetFocus();
		}
		double getPorcentagem()
		{
			return ValorDescontoPorcentagem * 0.01f;
		}
		void UpdateInterface()
		{
			//
			ValorRecebidoBox.Text = $"R$ {ValorTotalRecebido}";
			ValorTotalComDescontoBox.Text = $"R$ {ValorTotalComDesconto}";
			FaltaReceberValorBox.Text = $"R$ {FaltaReceber}";
			TrocoBox.Text = $"R$ {this.ValorTroco}";
		}
		private void ResetFocus()
		{
			PagamentoBoxInput.Focus();
		}
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
		public void IniciarTelaDePagamento(double ValorTotal,List<Produto> Produtos,LabsPDV LabsPDV)
		{
			//
			SetPagamentoTotalBox(ValorTotal);
			this.ValorTotal = ValorTotal;
			this.LabsPDV = LabsPDV;
			this.Produtos = Produtos;
			//
			DescontoBoxInput.Text = "0";
			//
			Reset();
			GetMeios();
			RealizarCalculos(0,0);
			UpdateInterface();
		}
		//
		async void Finalizar()
		{
			await CloudDataBase.AbaterProdutosEmEstoqueAsync(Produtos);
			Reset();
			this.Close();
		}
		void Cancelar()
		{
			Reset();
			this.Close();
		}
		//
		void RealizarCalculos(double ValorPago, double descontoPorcentagem)
		{
			ValorTotalComDesconto = Math.Round((ValorTotal - (ValorTotal * descontoPorcentagem)),2); // Já calculamos esse primeiro já que será usado no resto
			//
			ValorTotalRecebido += Math.Round(ValorPago,2);
			FaltaReceber = Math.Round(ValorTotalComDesconto - ValorTotalRecebido,2);
			ValorTroco = 0;
			if(ValorTotalRecebido > ValorTotalComDesconto) { ValorTroco = Math.Round(ValorTotalRecebido - ValorTotalComDesconto,2); FaltaReceber = 0; }
			//
			UpdateInterface();
		}
		//
		void AdicionarPagamento()
		{
			if (MeioDePagamentoComboBox.Items.Count < 1) { Modais.MostrarErro("Nenhum Meio de Pagamento Registrado!"); return; }
			if (MeioDePagamentoComboBox.Text.IsNullOrEmpty()) { Modais.MostrarAviso("Selecione um Meio de Pagamento!"); return; }
			//
			string valorSTR = PagamentoBoxInput.Text;
			string descSTR = DescontoBoxInput.Text;
			bool SLDV = MeiosPagamento.Meios[MeioDePagamentoComboBox.SelectedIndex].Item2; // Item2 é SLDV
			//
			if(FaltaReceber > 0)
			{
				//
				if (!Utils.TryParseToDouble(valorSTR,out double valorPag)) { Modais.MostrarAviso("Insira um Valor de Pagamento Válido!"); return; }
				if (!Utils.TryParseToDouble(descSTR,out ValorDescontoPorcentagem)) { Modais.MostrarAviso("Insira um Valor de Desconto Válido"); return; }
				if (!SLDV)
				{
					if(valorPag > FaltaReceber) { valorPag = FaltaReceber; }
				}
				//
                RealizarCalculos(valorPag, getPorcentagem());
                //
                PagamentoEfetuado pagEfet = new(MeioDePagamentoComboBox.Text, Math.Round(valorPag));
                PagamentosEfetuados.Add(pagEfet);
                //
                ListaPagamentosEfetuados.Items.Add(new ListViewItem([MeioDePagamentoComboBox.Text, $"R$ {Math.Round(valorPag, 2)}"]));
                //
                PagamentoBoxInput.Text = null!;
                DescontoBoxInput.Text = "0";
            }
		}
		void AtualizarPagamento()
		{
			
		}
		void ExcluirUmPagamento()
		{

		}
		//--------------------------//
		//		   EVENTOS
		//--------------------------//
		private void OnJanelaDePagamentoKeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					PagamentoBoxInput.Focus();
					break;
				case Keys.F2:
					Finalizar();
					break;
				case Keys.F3:
					Cancelar();
					break;
				case Keys.F4:
					DescontoBoxInput.Focus();
					break;
				case Keys.F5:
					AtualizarPagamento();
					break;
				case Keys.F6:
					ExcluirUmPagamento();
					break;
			}
		}

		private void OnMeioDePagamentoSelect(object sender, EventArgs e)
		{
			
		}

		private void OnBoxKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (sender == DescontoBoxInput) { ResetFocus(); }
				if (sender == PagamentoBoxInput) { AdicionarPagamento(); }
			}
		}
		private void AtualizarPagamentoButton_Click(object sender, EventArgs e)
		{
			AtualizarPagamento();
		}

		private void ExcluirPagamento_Click(object sender, EventArgs e)
		{
			ExcluirUmPagamento();
		}

		private void CancelarButton_Click(object sender, EventArgs e)
		{
			Cancelar();
		}

		private void FinalizarButton_Clicks(object sender, EventArgs e)
		{
			Finalizar();
		}
	}
}

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
		List<MeioDePagamento> MeiosDePagamento = new();
		//
		List<PagamentoEfetuado> PagamentosEfetuados = new();
		//
		List<Produto> Produtos = new();
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
			//
		}

		async void GetMeios()
		{
			MeiosDePagamento = await CloudDataBase.GetMeiosDePagamentoAsync();
			//Lista os modos de pagamento
			MeioDePagamentoComboBox.Items.Clear();
			foreach (MeioDePagamento Meio in MeiosDePagamento)
			{
				MeioDePagamentoComboBox.Items.Add(Meio.Meio);
			}
		}
		//--------------------------//
		//		   METODOS
		//--------------------------//
		//
		private void Reset()
		{
			MeioDePagamentoComboBox.Text = null;
			ModoDePagamentoComboBox.Text = null;
			//
			BandeiraComboBox.Text = null;
			ParcelasComboBox.Text = null;
			//
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
			this.ValorTotalComDesconto = this.ValorTotal - (this.ValorTotal * getPorcentagem());
			//Aqui atualizamos os valores antes de por diretamente na interface
			this.FaltaReceber = this.ValorTotalComDesconto - this.ValorTotalRecebido;
			this.ValorTroco = 0;
			if (FaltaReceber < 0) { this.ValorTroco = this.ValorTotalRecebido - ValorTotalComDesconto; FaltaReceber = 0; }
			this.ValorTroco = Math.Round(this.ValorTroco,2);
			this.FaltaReceber = Math.Round(this.FaltaReceber,2);
			this.ValorTotalComDesconto = Math.Round(this.ValorTotalComDesconto,2);

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
		public void IniciarTelaDePagamento(double ValorTotal,List<Produto> produtos)
		{
			//
			SetPagamentoTotalBox(ValorTotal);
			this.ValorTotal = ValorTotal;
			this.Produtos = produtos;
			//
			Reset();
			GetMeios();
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
		void AdicionarPagamento()
		{
			string PagamentoEfetuado = null!;
			//
			if (MeioDePagamentoComboBox.Text.Length == 0) { Modais.MostrarAviso("Você Deve Selecionar um Meio de Pagameto!"); MeioDePagamentoComboBox.Focus(); return; }
			else
			{
				//
				int index = MeioDePagamentoComboBox.SelectedIndex;
				MeioDePagamento meio = MeiosDePagamento[index];
				ModoDePagamento modo = default!;
				//
				PagamentoEfetuado = $"{MeioDePagamentoComboBox.Text} ";
				//
				if (meio.PossuiModos && ModoDePagamentoComboBox.Text.Length == 0) { Modais.MostrarAviso("Selecione um Modo Para o Meio de Pagamento Selecionado!"); ModoDePagamentoComboBox.Focus(); return; }
				if (meio.PossuiModos) { modo = meio.Modos[ModoDePagamentoComboBox.SelectedIndex]; }
				//
				else { PagamentoEfetuado += $"{ModoDePagamentoComboBox.Text} "; }
				if (modo != null)
				{
					if (modo.PossuiBandeira && BandeiraComboBox.Text.Length == 0) { Modais.MostrarAviso("Selecione uma Bandeira Para o Modo de Pagamento Selecionado!"); BandeiraComboBox.Focus(); return; }
					//
					else { PagamentoEfetuado += $"{BandeiraComboBox.Text}"; }
					if (modo.PossuiParcelas && ParcelasComboBox.Text.Length == 0) { Modais.MostrarAviso("Selecione a Quantidade de Parcelas!"); ParcelasComboBox.Focus(); return; }
				}
				//
				if (Utils.TryParseToDouble(PagamentoBoxInput.Text, out double value))
				{
					// Se o produto não pode ultrapassar o valor total a gente trava ele na trave
					if (!meio.PodeUltrapassarOValorTotal) { if (value > this.FaltaReceber) { value = this.FaltaReceber; } }
					if (this.FaltaReceber > 0)
					{
						if (value > 0)
						{
							//
							ListViewItem item = new([PagamentoEfetuado, $"R$ {value}"]);
							ListaPagamentosEfetuados.Items.Add(item);
							item.EnsureVisible();
							//
							PagamentosEfetuados.Add(new PagamentoEfetuado(PagamentoEfetuado, value));
							//
							ValorTotalRecebido += value;
							//
							UpdateInterface();
							//
						}
					}
				}
			}
		}
		void AtualizarPagamento()
		{
			if (ListaPagamentosEfetuados.SelectedItems.Count == 0) { Modais.MostrarAviso("Selecione o Pagamento que Deseja Atualizar"); return; }
			string PagamentoEfetuado = null!;
			int indexPE = ListaPagamentosEfetuados.SelectedIndices[0];
			double value = PagamentosEfetuados[indexPE].Valor;
			//
			if (MeioDePagamentoComboBox.Text.Length == 0) { Modais.MostrarAviso("Você Deve Selecionar um Meio de Pagameto!"); MeioDePagamentoComboBox.Focus(); return; }
			else
			{
				//
				int index = MeioDePagamentoComboBox.SelectedIndex;
				MeioDePagamento meio = MeiosDePagamento[index];
				ModoDePagamento modo = default!;
				//
				PagamentoEfetuado = $"{MeioDePagamentoComboBox.Text} ";
				//
				if (meio.PossuiModos && ModoDePagamentoComboBox.Text.Length == 0) { Modais.MostrarAviso("Selecione um Modo Para o Meio de Pagamento Selecionado!"); ModoDePagamentoComboBox.Focus(); return; }
				if (meio.PossuiModos) { modo = meio.Modos[ModoDePagamentoComboBox.SelectedIndex]; }
				//
				else { PagamentoEfetuado += $"{ModoDePagamentoComboBox.Text} "; }
				if (modo != null)
				{
					if (modo.PossuiBandeira && BandeiraComboBox.Text.Length == 0) { Modais.MostrarAviso("Selecione uma Bandeira Para o Modo de Pagamento Selecionado!"); BandeiraComboBox.Focus(); return; }
					//
					else { PagamentoEfetuado += $"{BandeiraComboBox.Text}"; }
					if (modo.PossuiParcelas && ParcelasComboBox.Text.Length == 0) { Modais.MostrarAviso("Selecione a Quantidade de Parcelas!"); ParcelasComboBox.Focus(); return; }
				}
				//
				if (PagamentoBoxInput.Text.Length == 0) { Modais.MostrarAviso("Você Deve Informar o Valor Para a Atualização"); ResetFocus(); return; }
				//
				ListViewItem item = new([PagamentoEfetuado, $"R$ {value}"]);
				ListaPagamentosEfetuados.Items.RemoveAt(indexPE);
				ListaPagamentosEfetuados.Items.Add(item);
				item.EnsureVisible();
				//
				PagamentosEfetuados.RemoveAt(indexPE);
				PagamentosEfetuados.Add(new PagamentoEfetuado(PagamentoEfetuado, value));
				//
				UpdateInterface();
				//
				Reset();
			}
		}
		void ExcluirUmPagamento()
		{
			if (ListaPagamentosEfetuados.SelectedItems.Count == 0) { Modais.MostrarAviso("Selecione o Pagamento que Deseja Excluir"); return; }
			int indexPE = ListaPagamentosEfetuados.SelectedIndices[0];
			double value = PagamentosEfetuados[indexPE].Valor;
			PagamentosEfetuados.RemoveAt(indexPE);
			ListaPagamentosEfetuados.Items.RemoveAt(indexPE);
			//
			ValorTotalRecebido -= value;
			UpdateInterface();
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
			//Primeiramente garantimos que a lista vai estar vazia e habilitamos a janela de seleção de modos
			if (MeioDePagamentoComboBox.SelectedIndex > -1)
			{
				ModoDePagamentoComboBox.Items.Clear();
				int index = MeioDePagamentoComboBox.SelectedIndex;
				MeioDePagamento meio = MeiosDePagamento[index];
				if (meio.PossuiModos)
				{
					ModoDePagamentoComboBox.Enabled = true;
					foreach (var Modo in meio.Modos)
					{
						ModoDePagamentoComboBox.Items.Add(Modo.Modo);
					}
				}
			}
		}

		private void OnModoDePagamentoSelect(object sender, EventArgs e)
		{
			//Mais uma vez, Para Garantir limpamos as listas e deixamos desabilitadas
			BandeiraComboBox.Items.Clear(); BandeiraComboBox.Text = null; BandeiraComboBox.Enabled = false;
			ParcelasComboBox.Items.Clear(); ParcelasComboBox.Text = null; ParcelasComboBox.Enabled = false;
			//Após dizemos se tem bandeira ou não
			int index = ModoDePagamentoComboBox.SelectedIndex;
			if (index > -1)
			{
				// Aqui selecionamos o modo requisitado
				ModoDePagamento modo = MeiosDePagamento[MeioDePagamentoComboBox.SelectedIndex].Modos[index];
				if (modo.PossuiBandeira)
				{
					BandeiraComboBox.Enabled = true;
					foreach (string Bandeira in modo.Bandeiras)
					{
						BandeiraComboBox.Items.Add(Bandeira);
					}
				}
				//
				if (modo.PossuiParcelas)
				{
					ParcelasComboBox.Enabled = true;
					//
					for (int i = 1; i <= modo.Parcelas; i++)
					{
						if(i == 1) { ParcelasComboBox.Items.Add($"{i} Vez"); }
						else { ParcelasComboBox.Items.Add($"{i} Vezes"); }
					}
				}
				//
			}
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

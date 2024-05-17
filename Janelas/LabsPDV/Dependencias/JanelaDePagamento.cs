﻿using Labs.LABS_PDV;
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
//
namespace Labs.Janelas.LabsPDV.Dependencias
{
	public partial class JanelaDePagamento : Form
	{
		//
		public const string NomeArquivoConfig = "ModosDePagamento";
		//
		MeiosPagamento MeiosPagamento { get; set; } = null!;
		//
		List<PagamentoEfetuado> PagamentosEfetuados { get; set; } = new();
		//
		List<Produto> Produtos { get; set; } = new();
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
		/// <summary>
		/// Inicia a tela de pagamento usando os parâmetros Fornecidos
		/// </summary>
		/// <param name="ValorTotal">Valor total dos itens</param>
		/// <param name="Produtos">Produtos Da venda (Quantidade etc)</param>
		/// <param name="LabsPDV">O Ponto PDV que requisitou a janela (Será usado no futuro para pontos remotos)</param>
		public void IniciarTelaDePagamento(double ValorTotal,List<Produto> Produtos,LabsPDV LabsPDV)
		{
			//Garante que todos os campos estejam limpos para receber novos valores
			Reset();
			//
			// seta o valor total visualmente pro operador de caixa
			SetPagamentoTotalBox(ValorTotal);
			//
			this.ValorTotal = ValorTotal;
			this.LabsPDV = LabsPDV;
			this.Produtos = Produtos;
			Modais.MostrarInfo($"Produtos: {this.Produtos.Count}");
			//
			DescontoBoxInput.Text = "0";
			//
			//
			GetMeios();
			//
			RealizarCalculos(0,0);
			//
			UpdateInterface();
		}
		//
		async void Finalizar()
		{
			//Previne que a venda seja finalizada sem receber o valor total do pagamento.
			//
			if(FaltaReceber > 0) { Modais.MostrarInfo($"Ainda Falta Receber R$: {FaltaReceber} !"); return; }
            //Substituir essa parte por funções genéricas para espelhamento (As funções genéricas já suportam Update Massivo)
            Modais.MostrarInfo($"Produtos: {this.Produtos.Count}");
            await CloudDataBase.AbaterProdutosEmEstoqueAsync(Produtos);
			// Adiciona o valor Recebido ao meio correspondente
			if(LabsPDV != null)
			{
				var index = MeioDePagamentoComboBox.SelectedIndex;
				var meioPagamento = MeioDePagamentoComboBox.Text;
				double valor = ValorTotalRecebido - ValorTroco;
				//
				LabsPDV.CaixaLabs.AdicionarCapitalAoMeio(index,valor);
				LabsPDV.CaixaLabs.AtualizarCaixa(); // Atualizar é importante para termos controle dos Valores Recebidos!
				//
				// Aqui faz a impressão do cupom fiscal (ou não fiscal)
				using (var PM = new PrintManager())
				{
					PM.ImprimirCupomNaoFiscal(PrintManager.ImpressoraDefault,Produtos,ValorTotalComDesconto,ValorTotal,ValorTotalRecebido,ValorDescontoPorcentagem,ValorTroco,meioPagamento);
				}
				// Sinaliza que a venda foi finalizada com sucesso
				//
				Modais.MostrarInfo("Venda Finalizada com Sucesso!");
				Reset();
				//
				this.Close();
			}
		}
		//Ao Cancelar, somente voltamos para a tela de PDV (Vai que o cliente esqueceu de comprar algo né)
		void Cancelar()
		{
			DialogResult r = Modais.MostrarPergunta("Você Deseja Retornar Para a Tela do PDV?");
			//
			if(r == DialogResult.Yes)
			{
				//
				Reset();
				this.Close();
			}
		}
		//
		/// <summary>
		/// Realiza os Cálculos de troco, falta receber e valor recebido, junto com valor de desconto
		/// </summary>
		/// <param name="ValorPago">Valor do pagamento efetuado</param>
		/// <param name="descontoPorcentagem">Desconto aplicado na venda (Esse valor é constante para todos os pagamentos (não afeta o pagamento, somente o valor total))</param>
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
                // é importante que o pagamento efetuado e a lista de pagamento sejam atualizados juntos para manter o mesmo index
                PagamentoEfetuado pagEfet = new(MeioDePagamentoComboBox.Text, Math.Round(valorPag));
                PagamentosEfetuados.Add(pagEfet);
                //
                ListaPagamentosEfetuados.Items.Add(new ListViewItem([MeioDePagamentoComboBox.Text, $"R$ {Math.Round(valorPag, 2)}"]));
                //
                PagamentoBoxInput.Text = null!;
            }
		}
		//
		void ExcluirUmPagamento()
		{
			if(ListaPagamentosEfetuados.SelectedIndices.Count < 1) { Modais.MostrarAviso("Você Deve Selecionar um Pagamento da lista Para Removêlo"); return; }
			var index = ListaPagamentosEfetuados.SelectedIndices[0];
			//
			DialogResult r = Modais.MostrarPergunta("Você Deseja Remover o Pagamento Selecionado?");
			if (r == DialogResult.Yes)
			{
				try
				{
					PagamentoEfetuado PagEfetuado = PagamentosEfetuados[index]; // se pegou o pagamento na lista é porque existe
					//
					ValorTotalRecebido -= PagEfetuado.Valor;
					//
					PagamentosEfetuados.Remove(PagEfetuado);
					//
					ListaPagamentosEfetuados.Items.RemoveAt(index);
					//
					Modais.MostrarInfo("Pagamento Removido com Sucesso!");
					//
					RealizarCalculos(0,getPorcentagem());
					UpdateInterface();
				}
				catch (Exception ex)
				{
					Modais.MostrarAviso($"Não foi Possível Remover o Pagamento Selecionado \n{ex.Message}");
					throw;
				}
			}
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
				if (sender == DescontoBoxInput) 
				{
                    if (!Utils.TryParseToDouble(DescontoBoxInput.Text, out ValorDescontoPorcentagem)) { Modais.MostrarAviso("Insira um Valor de Desconto Válido"); return; }
                    RealizarCalculos(0,getPorcentagem()); // chamamos o realizar calculo aqui para atualizar o desconto
                }
				if (sender == PagamentoBoxInput) { AdicionarPagamento(); }
			}
		}
		//
		private void ExcluirPagamento_Click(object sender, EventArgs e)
		{
			ExcluirUmPagamento();
		}
		//
		private void CancelarButton_Click(object sender, EventArgs e)
		{
			Cancelar();
		}
		//
		private void FinalizarButton_Clicks(object sender, EventArgs e)
		{
			Finalizar();
		}
	}
}

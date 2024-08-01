﻿using Labs.Janelas.LabsEstoque.Dependencias;
using Labs.Janelas.LabsPDV.Dependencias;
using Labs.Main;
using Lucene.Net.Util.Mutable;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Labs.Janelas.LabsEstoque
{
    /// <summary>
    /// Lógica interna para LabsEstoqueWPF.xaml
    /// </summary>
    //Classe Privativa
    public partial class LabsEstoqueWPF : Window
    {

        List<Produto> Produtos = [];
		//
		List<Fornecedor> Fornecedores = [];
		//
        List<Devolucao> Devolucoes = [];
		//
		List<EntradaDeProduto> EntradaDeProdutos = [];
		//
		List<VendaRealizada> SaidaDeProdutos = [];
        //
        public LabsEstoqueWPF()
        {
            InitializeComponent();
            LoadFromDataBase();
			
        }
		#region Métodos Estáticos

		//-----------------------------------------//
		//-------------MÉTODOS ESTÁTICOS!!!
		//-----------------------------------------//
		public static async Task AbaterProdutosEmEstoqueAsync(List<Produto> Produtos)
        {
            try
            {
                //Pega os Produtos do Estoque
                List<Produto> toUpdateList = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos, x => Produtos.Exists(y => y.ID == x.ID));
                //
                foreach (var produto in Produtos)
                {
                    Produto? prod = toUpdateList.Find(x => x.ID == produto.ID);
                    //
                    if (prod != null)
                    {
                        prod.Quantidade -= produto.Quantidade;
                        await CloudDataBase.UpdateOneLocalAsync(Collections.Produtos, prod, Builders<Produto>.Filter.Eq("ID", prod.ID));
                    }
                }
                // Logo após Chamamos o UpdateMany
                //
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw;
            }
        }
		#endregion
		//
		#region Métodos Globais

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.F5)
			{
				LoadFromDataBase();
			}
		}
		//
		private void AddProdutoToList(Produto produto)
		{
			var item = new ListViewItem { Content = produto };
			//
			double Threshold = (produto.QuantidadeMin * 0.5f) + produto.QuantidadeMin;
			//
			item.Foreground = Brushes.Black;
			item.FontSize = 15;
			item.FontFamily = new FontFamily("Segoe UI");
			item.Selected += Item_Selected;
			item.Unselected += Item_Unselected;
			//
			if (produto.Quantidade > Threshold) { item.Background = Brushes.LightGreen; produto.Status = "Ok"; }
			if (produto.Quantidade <= Threshold) { item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF80")); produto.Status = "Chegando no Estoque Mínimo"; }
			if (produto.Quantidade < produto.QuantidadeMin) { item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f08080")); produto.Status = "Produto em Baixa"; }
			//
			ListaProdutosCadastrados.Items.Add(item);
		}
		//
		private void UpdateContent(ListViewItem item)
		{
			if(item.Content is not Produto produto) { return; }
			double Threshold = (produto.QuantidadeMin * 0.5f) + produto.QuantidadeMin;
			//
			item.Foreground = Brushes.Black;
			//
			if (produto.Quantidade > Threshold) { item.Background = Brushes.LightGreen; produto.Status = "Ok"; }
			if (produto.Quantidade <= Threshold) { item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF80")); produto.Status = "Chegando no Estoque Mínimo"; }
			if (produto.Quantidade < produto.QuantidadeMin) { item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f08080")); produto.Status = "Produto em Baixa"; }
		}
		//
		private void Item_Selected(object sender, RoutedEventArgs e)
		{
			if(sender is ListViewItem item)
			{
				item.Background = new SolidColorBrush(Color.FromArgb(150,40,40,255));
			}
		}
		private void Item_Unselected(object sender, RoutedEventArgs e)
		{
			if(sender is ListViewItem item)
			{
				UpdateContent(item);
			}
		}
		//
		private async void LoadFromDataBase()
        {
            //Garantimos que todos os itens são limpos para evitar consumo de memória excessiva
            Produtos.Clear();
			Fornecedores.Clear();
            Devolucoes.Clear();
			EntradaDeProdutos.Clear();
			SaidaDeProdutos.Clear();
            //
            ListaProdutosCadastrados.Items.Clear();
            ListaProdutosDevolvidos.Items.Clear();
			ListaFornecedores.Items.Clear();
			ListaRegistroDeEntradas.Items.Clear();
			ListaRegistroDeSaidas.Items.Clear();
            //
            Produtos = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos,_ => true);
            Devolucoes = await CloudDataBase.GetManyLocalAsync<Devolucao>(Collections.Devolucoes, _ => true);
			Fornecedores = await CloudDataBase.GetManyLocalAsync<Fornecedor>(Collections.Fornecedores, _ => true);
			EntradaDeProdutos = await CloudDataBase.GetManyLocalAsync<EntradaDeProduto>(Collections.Entradas, _ => true);
			SaidaDeProdutos = await CloudDataBase.GetManyLocalAsync<VendaRealizada>(Collections.Vendas,_ => true);
			//
			//
			//Adicionamos direto na lista visual já que os itens apresentados são os próprios produtos
			// Aqui fazemos uma iteração básica para definir a cor de cada produto baseado na quantidade
			foreach (var produto in Produtos) { AddProdutoToList(produto); }
			//
            foreach (Devolucao devolucao in Devolucoes) { ListaProdutosDevolvidos.Items.Add(devolucao); }
            //
			foreach (Fornecedor fornecedor in Fornecedores) { ListaFornecedores.Items.Add(fornecedor); }
			//
			foreach(EntradaDeProduto entrada in EntradaDeProdutos) { ListaRegistroDeEntradas.Items.Add(entrada); }
			//
			foreach(VendaRealizada venda in SaidaDeProdutos) { ListaRegistroDeSaidas.Items.Add(venda); }
			//
            ComboBox_Todos.IsSelected = true;
            ComboBox_Descricao.IsSelected = true;
            //
        }
		#endregion
		//
		#region Eventos Estoque

		//EVENTOS Estoque
		private void UpdateByEvent(object? sender, EventArgs e)
		{
			LoadFromDataBase();
			//
			if (sender is Window window) { window.Closed -= UpdateByEvent; }
		}
		private void CadastrarProdutoButton_Click(object sender, RoutedEventArgs e)
		{
			if(Fornecedores.Count == 0) { Modais.MostrarAviso("Você Não Possui Fornecedores Cadastrados!"); return; }
			//
			LabsMain.IniciarDependencia<CadastrarProdutoWPF>(app =>
			{
				app.Closed += UpdateByEvent;
			}, true);
		}
		//
		private void AtualizarProdutoButton_Click(object sender, RoutedEventArgs e)
		{
			if (ListaProdutosCadastrados.SelectedItem is not ListViewItem Itemproduto) { Modais.MostrarAviso("Você deve selecionar o produto que deseja atualizar!"); return; }
			//
			if(Itemproduto.Content is not Produto produto){ Modais.MostrarAviso("Algo deu errado ao chamar o editor"); return; }
			//
			LabsMain.IniciarDependencia<AtualizarProdutoWPF>(app =>
			{
				app.CarregarAtributos(produto);
				app.Closed += UpdateByEvent;
			});
		}
		//
		private async void RemoverProdutoButton_Click(object sender, RoutedEventArgs e)
		{
			if (ListaProdutosCadastrados.SelectedItem is not ListViewItem Itemproduto) { Modais.MostrarAviso("Você deve selecionar o produto que deseja remover!"); return; }
			//
			if (Itemproduto.Content is not Produto produto) { Modais.MostrarAviso("Algo deu errado ao realizar o comando de remoção"); return; }
			MessageBoxResult r = Modais.MostrarPergunta($"Você deseja remover o Produto: {produto.Descricao}?\nESTA OPERAÇÃO NÃO PODE SER DESFEITA!");
			if (r == MessageBoxResult.Yes)
			{
				//
				ListaProdutosCadastrados.Items.Remove(produto);
				//
				if (LabsMain.Cliente.PossuiPlanoCloud)
				{
					await CloudDataBase.RemoveCloudAsync<Produto>(Collections.Produtos, x => x.ID == produto.ID);
				}
				//
				await CloudDataBase.RemoveLocalAsync<Produto>(Collections.Produtos, x => x.ID == produto.ID);
				//
				LabsMain.MotorDeBusca.RemoverItemIndexado(Collections.Produtos,"ID",produto.ID);
				//
				Modais.MostrarInfo($"Produto: {produto.Descricao}\nRemovido com Sucesso!");
				//
				LoadFromDataBase();
			}
		}
		//
		private void RegistrarEntradaButton_Click(object sender, RoutedEventArgs e)
		{
			if(ListaProdutosCadastrados.SelectedItem is not ListViewItem ItemProduto) { Modais.MostrarAviso("Você deve selecionar um produto!"); return; }
			if(ItemProduto.Content is not Produto produto) { Modais.MostrarAviso("Algo deu errado ao chamar a janela."); return; }
			LabsMain.IniciarDependencia<RegistroDeEntradaWPF>(app =>
			{
				app.SetarProduto(produto);
				app.Closed += UpdateByEvent;
			});
		}
		//
		private void VoltarButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void OnSearchBarKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter) { AtualizarEstoqueVisual(CaixaDePesquisa.Text); }
		}

		private void PesquisarButton_Click(object sender, RoutedEventArgs e)
		{
			AtualizarEstoqueVisual(CaixaDePesquisa.Text);
		}
		private void LimparFiltrosButton_Click(object sender, RoutedEventArgs e)
		{
			ComboBox_Descricao.IsSelected = true;
			CaixaDePesquisa.Text = null!;
			LoadFromDataBase();
		}
		private void ListarSelection_Changed(object sender, SelectionChangedEventArgs e)
		{
			FiltrarPorCategoria();
		}
		#endregion
		//
		#region Aba Estoque

		// Métodos Estoque
		private async void AtualizarEstoqueVisual(string searchFilter)
		{
			//Aqui a gente retorna se não tiver nada até porque não faz sentido gastar processamento atoa
			if (searchFilter.IsNullOrEmpty() && ListaProdutosCadastrados.Items.Count == Produtos.Count) { return; }
			//
			ListaProdutosCadastrados.Items.Clear();
			//
			//
			if (searchFilter.IsNullOrEmpty()) { Produtos.ForEach(AddProdutoToList); return; }
			//
			else
			{
				if (ComboBox_Descricao.IsSelected)
				{
					var filteredIDs = await LabsMain.MotorDeBusca.ProcurarItem(Collections.Produtos,"ID","Descricao",searchFilter);
					//
					Produtos.ForEach((x) =>
					{
						if (filteredIDs.Contains(x.ID)) { AddProdutoToList(x); }
					});
				}
				if (ComboBox_Fornecedor.IsSelected)
				{
					var filteredIDs = await LabsMain.MotorDeBusca.ProcurarItem(Collections.Produtos, "ID", "Fornecedor", searchFilter);
					//
					Produtos.ForEach((x) =>
					{
						if (filteredIDs.Contains(x.ID)) { AddProdutoToList(x); }
					});
				}
				if (ComboBox_PrecoMaiorQue.IsSelected)
				{
					if(!Utils.TryParseToDouble(searchFilter,out double valor)) { Modais.MostrarAviso("Insira um valor Válido!"); return; }
					//
					Produtos.ForEach((x) =>
					{
						if(x.Preco >= valor) { AddProdutoToList(x); }
					});
				}
				if (ComboBox_PrecoMenorQue.IsSelected)
				{
					if (!Utils.TryParseToDouble(searchFilter, out double valor)) { Modais.MostrarAviso("Insira um valor Válido!"); return; }
					//
					Produtos.ForEach((x) =>
					{
						if (x.Preco <= valor) { AddProdutoToList(x); }
					});
				}
			}
			//
		}
		//Filtro de produtos
		private void FiltrarPorCategoria()
		{
			if (ListaProdutosCadastrados == null) { return; }
			ListaProdutosCadastrados.Items.Clear();
			//
			if (ComboBox_Todos.IsSelected) { LoadFromDataBase(); }
			//
			if (ComboBox_EmBaixa.IsSelected)
			{
				Produtos.ForEach(x => { if (x.Quantidade <= x.QuantidadeMin) { AddProdutoToList(x); } });
			}
			//
		}
		//
		#endregion
		//
		#region Aba de Devolução

		private void RegistrarDevolucao_Click(object sender, RoutedEventArgs e)
		{
			LabsMain.IniciarDependencia<JanelaDeDevolucaoWPF>((app) => { app.Closed += JanelaDeDevolucao_Closed; }, true, false);
			RegistrarDevolucaoButton.IsEnabled = false;
			RemoverDevolucaoButton.IsEnabled = false;
		}

		private void JanelaDeDevolucao_Closed(object? sender, EventArgs e)
		{
			if(sender is JanelaDeDevolucaoWPF app)
			{
				app.Closed -= JanelaDeDevolucao_Closed;
				LoadFromDataBase();
				RegistrarDevolucaoButton.IsEnabled = true;
				RemoverDevolucaoButton.IsEnabled = true;
			}
		}

		private async void RemoverDevolucao_Click(object sender, RoutedEventArgs e)
		{
			if (ListaProdutosDevolvidos.SelectedItem is not Devolucao devolucao) { Modais.MostrarAviso("Você precisa selecionar o produto que deseja remover do estoque!"); return; }
			var r = Modais.MostrarPergunta($"Deseja realmente remover o registro de devolução do Produto ({devolucao.Descricao})\n ESTA AÇÃO NÃO PODE SER DESFEITA!");
			if(r == MessageBoxResult.No) { return; }
			//
			if (ListaProdutosDevolvidos.Items.Contains(devolucao)) { ListaProdutosDevolvidos.Items.Remove(devolucao); }
			Devolucoes.Remove(devolucao);
			//
			//TENTAR ENTENDER OQ TA ACONTECENDO AQ
			if (LabsMain.Cliente.PossuiPlanoCloud)
			{
				await CloudDataBase.RemoveCloudAsync<Devolucao>(Collections.Devolucoes,x => x.ID == devolucao.ID);
			}
			await CloudDataBase.RemoveLocalAsync<Devolucao>(Collections.Devolucoes,x => x.ID == devolucao.ID);
		}
		#endregion
		//
		#region Aba Fornecedores

		private void RegistrarFornecedorButton_Click(object sender, RoutedEventArgs e)
		{
			LabsMain.IniciarDependencia<CadastrarFornecedorWPF>(null!,true,false);
		}
		//
		private async void RemoverFornecedor()
		{
			if(ListaFornecedores.SelectedItem is not Fornecedor fornecedor) { Modais.MostrarAviso("Você Deve Selecionar um fornecedor"); return; }
			var r = Modais.MostrarPergunta($"Você Deseja Remover o Fornecedor {fornecedor.NomeEmpresa}?\nESTA AÇÃO NÃO PODE SER DESFEITA!");
			//
			if(r == MessageBoxResult.No) { return; }
			// Tentar descobrir porque essa porcaria não deleta no cloud
			if (LabsMain.Cliente.PossuiPlanoCloud)
			{
				await CloudDataBase.RemoveCloudAsync<Fornecedor>(Collections.Fornecedores,x => x.ID == fornecedor.ID);
			}
			await CloudDataBase.RemoveLocalAsync<Fornecedor>(Collections.Fornecedores,x => x.ID == fornecedor.ID);
			//
			ListaFornecedores.Items.Remove(fornecedor);
			Modais.MostrarInfo($"Fornecedor {fornecedor.NomeEmpresa} Removido com Sucesso!");
		}
		//
		private void RemoverFornecedorButton_Click(object sender, RoutedEventArgs e)
		{
			RemoverFornecedor();
		}

		#endregion
		//
		#region Aba Saidas
		//
		// Métodos Estoque
		private void AtualizarVendasVisual(string searchFilter,bool lp = false)
		{
			//Aqui a gente retorna se não tiver nada até porque não faz sentido gastar processamento atoa
			if (searchFilter.IsNullOrEmpty() && ListaRegistroDeSaidas.Items.Count == SaidaDeProdutos.Count) 
			{
				if (lp)
				{
					ListaRegistroDeSaidas.Items.Clear();
					SaidaDeProdutos.ForEach(x =>
					{
						if(Utils.IsToday(x.DataVenda)) { ListaRegistroDeSaidas.Items.Add(x); }
					});
					return;
				}
				return;
			}
			//
			//
			ListaRegistroDeSaidas.Items.Clear();
			//
			//
			if (searchFilter.IsNullOrEmpty()) { SaidaDeProdutos.ForEach(x => ListaRegistroDeSaidas.Items.Add(x)); return; }
			//
			else
			{
				if (ComboBox_IDVenda.IsSelected)
				{
					SaidaDeProdutos.ForEach((x) =>
					{
						if (SaidaDeProdutos.Find(x => x.IDVenda.Contains(searchFilter,StringComparison.OrdinalIgnoreCase)) != null) { ListaRegistroDeSaidas.Items.Add(x); }
					});
				}
				if (ComboBox_Data.IsSelected)
				{
					SaidaDeProdutos.ForEach((x) =>
					{
						if (SaidaDeProdutos.Find(x => $"{x.DataVenda}-{x.HoraVenda}".Contains(searchFilter)) != null) { ListaRegistroDeSaidas.Items.Add(x); }
					});
				}
				if (ComboBox_PagMaiorQue.IsSelected)
				{
					if (!Utils.TryParseToDouble(searchFilter, out double valor)) { Modais.MostrarAviso("Insira um valor Válido!"); return; }
					//
					SaidaDeProdutos.ForEach((x) =>
					{
						if (x.TotalComDesconto >= valor) { ListaRegistroDeSaidas.Items.Add(x); }
					});
				}
				if (ComboBox_PrecoMenorQue.IsSelected)
				{
					if (!Utils.TryParseToDouble(searchFilter, out double valor)) { Modais.MostrarAviso("Insira um valor Válido!"); return; }
					//
					SaidaDeProdutos.ForEach((x) =>
					{
						if (x.TotalComDesconto <= valor) { ListaRegistroDeSaidas.Items.Add(x); }
					});
				}
			}
			//
		}

		private void CaixaDePesquisaVenda_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
			{
				AtualizarVendasVisual(CaixaDePesquisaVenda.Text, (bool)ListarVendasRealizadasHoje.IsChecked!);
			}
		}

		private void PesquisarVendaButton_Click(object sender, RoutedEventArgs e)
		{
			AtualizarVendasVisual(CaixaDePesquisaVenda.Text,(bool)ListarVendasRealizadasHoje.IsChecked!);
		}

		private void LimparFiltrosVendaButton_Click(object sender, RoutedEventArgs e)
		{
			ComboBox_IDVenda.IsSelected = true;
			LoadFromDataBase();
		}

		private void ImprimirComprovanteClienteButton_Click(object sender, RoutedEventArgs e)
		{
			var r = Modais.MostrarPergunta("Deseja Imprimir a 2° Via do Comprovante de Venda do Cliente para a Venda Selecionada?");
			if (r == MessageBoxResult.No) { return; }
			//
			if(ListaRegistroDeSaidas.SelectedItem is not VendaRealizada Venda) { Modais.MostrarAviso("Você deve selecionar uma venda para realizar a impressão!"); return; }
			//
			using (var Pm = new PrintManager())
			{
				Pm.ImprimirComprovanteDeVendaCliente(LabsMainAppWPF.ImpressoraTermica,Venda,Venda.ClienteLoja);
			}
		}
		//
		private async static void ImprimirDANFE(VendaRealizada Venda)
		{
			bool parcelado = false;
			List<Produto> nFiscProds = [];
			//
			foreach (PagamentoEfetuado pe in Venda.PagamentosEfetuados) { if(pe.Parcelas > 1) { parcelado = true; } }
			//
			foreach (Produto prod in Venda.Produtos) { if (prod.PossuiInfosFiscais == false) { Modais.MostrarInfo($"PRODUTO: {prod.Descricao} POSSUI?: {prod.PossuiInfosFiscais}"); nFiscProds.Add(prod); } }
			//
			if (nFiscProds.Count > 0)
			{ 
				Modais.MostrarAviso("Não foi possível imprimir a DANFE!\nUm ou mais produtos não estão com suas informações fiscais registradas!");
				var r = Modais.MostrarPergunta("Deseja Corrigir as Informações para a Impressão?");
				if(r == MessageBoxResult.No) { return; }

				LabsMain.IniciarDependencia<CadastrarInfosFiscais>(app => 
				{
					app.InitMany(nFiscProds);
					app.OnInfosApplied += UpdateProdutos; // Atrelamos a função local de atualização
				});
				//Função local disparada pelo Evento da janela de cadastro
				async void UpdateProdutos(CadastrarInfosFiscais Janela, Produto produto, List<Produto> Produtos)
				{
					Janela.OnInfosApplied -= UpdateProdutos;
					//
					if (Produtos == null) { return; }
					//
					for (int i = 0; i < Venda.Produtos.Length; i++)
					{
						Produto? match = Produtos.Find(x => x.ID == Venda.Produtos[i].ID);
						if (match != null) { Venda.Produtos[i] = match; }
					}
					// Atualizamos os produtos da venda
					// Disparamos a nota fiscal com os produtos atualizados
					await LabsNFe.EmitirNotaFiscalDeConsumidorEletronicaAsync("Venda para consumidor final.", Venda.IDVenda, Venda.Troco, parcelado, [.. Venda.Produtos], [.. Venda.PagamentosEfetuados]);
				}
				//
				return; 
			}
			//
			await LabsNFe.EmitirNotaFiscalDeConsumidorEletronicaAsync("Venda para consumidor final.",Venda.IDVenda,Venda.Troco,parcelado, [.. Venda.Produtos], [.. Venda.PagamentosEfetuados]);
		}
		//
		private void ImprimirNotaFiscalButton_Click(object sender, RoutedEventArgs e)
		{
			var r = Modais.MostrarPergunta("Deseja Imprimir a DANFE da Venda Selecionada?");
			if(r == MessageBoxResult.No) { return; }
			//
			if(ListaRegistroDeSaidas.SelectedItem is not VendaRealizada Venda) { Modais.MostrarAviso("Você deve selecionar uma venda para realizar a Impressão!"); return; }
			//
			ImprimirDANFE(Venda);
		}

		private void ImprimirComprovanteLojaButton_Click(object sender, RoutedEventArgs e)
		{
			var r = Modais.MostrarPergunta("Deseja Imprimir a 2° Via da Nota de Controle Interno (NCI) da Venda Selecionada?");
			if (r == MessageBoxResult.No) { return; }
			//
			if (ListaRegistroDeSaidas.SelectedItem is not VendaRealizada Venda) { Modais.MostrarAviso("Você deve selecionar uma venda para realizar a impressão!"); return; }
			//
			using (var Pm = new PrintManager())
			{
				Pm.ImprimirComprovanteDeVendaLoja(LabsMainAppWPF.ImpressoraTermica, Venda);
			}
		}

		private void ImprimirAuxiliarParcelaButton_Click(object sender, RoutedEventArgs e)
		{
			var r = Modais.MostrarPergunta("Deseja Imprimir a 2° Via da Nota Auxiliar de Parcelamento?");
			if(r == MessageBoxResult.No) { return; }
			//
			if(ListaRegistroDeSaidas.SelectedItem is not VendaRealizada Venda) { Modais.MostrarAviso("Você deve selecionar uma venda para realizar a impressão!"); return; }
			//
			using( var Pm = new PrintManager()) 
			{
				Pm.ImprimirCupomAuxiliarDeParcelamento(LabsMainAppWPF.ImpressoraTermica,[..Venda.PagamentosEfetuados]);
			}
		}
		private void ListarVendasRealizadasHoje_Checked(object sender, RoutedEventArgs e)
		{
			bool? lvrh = ListarVendasRealizadasHoje.IsChecked;
			if (lvrh.HasValue)
			{
				if(lvrh.Value == true) { AtualizarVendasVisual(CaixaDePesquisaVenda.Text, true); return; }
				AtualizarVendasVisual(CaixaDePesquisaVenda.Text,false);
			}
		}
		//
		#endregion


	}
}

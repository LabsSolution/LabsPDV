using Labs.Janelas.LabsEstoque.Dependencias;
using Labs.Janelas.LabsPDV.Dependencias;
using Labs.LABS_PDV;
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
using static Labs.LABS_PDV.Modelos;
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
		List<Produto> ProdutosComDefeito = [];
        //
        List<Devolucao> Devolucoes = [];
        //
        public LabsEstoqueWPF()
        {
            InitializeComponent();
            LoadFromDataBase();
        }
        //
        private void UpdateQuantidadeLabel() 
		{ 
			if(ListaProdutosCadastrados == null) { return; }
			if(ListaProdutosComDefeito == null) { return; }
			if(ListaProdutosDevolvidos == null) { return; }
			if(Estoque_Tab?.IsSelected == true)
			{
				QuantidadeProdutosLabel.Content = $"Mostrando {ListaProdutosCadastrados.Items.Count} Produtos"; 
			}
			if(Devolucoes_Tab?.IsSelected == true)
			{
				QuantidadeProdutosLabel.Content = $"Mostrando {ListaProdutosDevolvidos.Items.Count} Produtos"; 
			}
			if(Defeituosos_Tab?.IsSelected == true)
			{
				QuantidadeProdutosLabel.Content = $"Mostrando {ListaProdutosComDefeito.Items.Count} Produtos";
			}
		}
		//
        private async void LoadFromDataBase()
        {
            //Garantimos que todos os itens são limpos para evitar consumo de memória excessiva
            Produtos.Clear();
			ProdutosComDefeito.Clear();
            Devolucoes.Clear();
            //
            ListaProdutosCadastrados.Items.Clear();
            ListaProdutosDevolvidos.Items.Clear();
			ListaProdutosComDefeito.Items.Clear();
            //
            Produtos = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos,_ => true);
			ProdutosComDefeito = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.ProdutosComDefeito,_ => true);
            Devolucoes = await CloudDataBase.GetManyLocalAsync<Devolucao>(Collections.Devolucoes, _ => true);
			//
			//Adicionamos direto na lista visual já que os itens apresentados são os próprios produtos
			foreach (Produto produto in Produtos) { ListaProdutosCadastrados.Items.Add(produto); }
			//
			foreach (Produto produto in ProdutosComDefeito) { ListaProdutosComDefeito.Items.Add(produto); }
            //
            foreach (Devolucao devolucao in Devolucoes) { ListaProdutosDevolvidos.Items.Add(devolucao); }
            //
            ComboBox_Todos.IsSelected = true;
            ComboBox_Descricao.IsSelected = true;
            //
            UpdateQuantidadeLabel();
        }
		//
		private void UpdateByEvent(object? sender, EventArgs e)
		{
			LoadFromDataBase();
			//
			if (sender is Window window) { window.Closed -= UpdateByEvent; }
		}
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
		//EVENTOS
		private void CadastrarProdutoButton_Click(object sender, RoutedEventArgs e)
		{
			LabsMain.IniciarDependencia<CadastrarProdutoWPF>(app =>
			{
				app.Closed += UpdateByEvent;
			}, true);
		}

		//
		private void AtualizarProdutoButton_Click(object sender, RoutedEventArgs e)
		{
			if (ListaProdutosCadastrados.SelectedItem is not Produto produto) { Modais.MostrarAviso("Você deve selecionar o produto que deseja atualizar!"); return; }
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
			if (ListaProdutosCadastrados.SelectedItem is not Produto produto) { Modais.MostrarAviso("Você precisa selecionar o produto que deseja remover do estoque!"); return; }
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
				Modais.MostrarInfo($"Produto: {produto.Descricao}\nRemovido com Sucesso!");
				//
				LoadFromDataBase();
			}
		}
		//
		private void VoltarButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void OnSearchBarKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter) { AtualizarListaProdutosVisual(CaixaDePesquisa.Text); }
		}

		private void PesquisarButton_Click(object sender, RoutedEventArgs e)
		{
			AtualizarListaProdutosVisual(CaixaDePesquisa.Text);
		}
		// Métodos de UTILIDADE
		private void AtualizarListaProdutosVisual(string filter)
		{
			//Aqui a gente retorna se não tiver nada até porque não faz sentido gastar processamento atoa
			if (filter.IsNullOrEmpty() && ListaProdutosCadastrados.Items.Count == Produtos.Count) { UpdateQuantidadeLabel(); return; }
			//
			ListaProdutosCadastrados.Items.Clear();
			ListaProdutosComDefeito.Items.Clear();
			ListaProdutosDevolvidos.Items.Clear();
			//
			//
			if (filter.IsNullOrEmpty()) { Produtos.ForEach(x => ListaProdutosCadastrados.Items.Add(x)); UpdateQuantidadeLabel(); return; }
			else
			{
				if (ComboBox_Descricao.IsSelected)
				{
					//
					Produtos.ForEach((x) =>
					{
						if (x.Descricao.Contains(filter, StringComparison.OrdinalIgnoreCase)) { ListaProdutosCadastrados.Items.Add(x); }
					});
					//
					ProdutosComDefeito.ForEach(x => 
					{
						if (x.Descricao.Contains(filter, StringComparison.OrdinalIgnoreCase)) { ListaProdutosComDefeito.Items.Add(x); }
					});
					//
					Devolucoes.ForEach(x => 
					{ 
						if(x.Descricao.Contains(filter, StringComparison.OrdinalIgnoreCase)) { ListaProdutosDevolvidos.Items.Add(x); }
					});
				}
				//
				if (ComboBox_Codigo.IsSelected)
				{
					Produtos.ForEach((x) =>
					{
						if (x.CodBarras.Contains(filter, StringComparison.OrdinalIgnoreCase)) { ListaProdutosCadastrados.Items.Add(x); }
					});
				}
				//
				if (ComboBox_ID.IsSelected)
				{
					Produtos.ForEach((x) =>
					{
						if (x.ID.Contains(filter, StringComparison.OrdinalIgnoreCase)) { ListaProdutosCadastrados.Items.Add(x); }
					});
				}
				//
				if (ComboBox_Preco.IsSelected)
				{
					Produtos.ForEach((x) =>
					{
						if (Utils.FormatarValor(x.Preco).Contains(filter, StringComparison.OrdinalIgnoreCase)) { ListaProdutosCadastrados.Items.Add(x); }
					});
				}
				//
				UpdateQuantidadeLabel();
			}
			//
		}

		private void LimparFiltrosButton_Click(object sender, RoutedEventArgs e)
		{
			ComboBox_Descricao.IsSelected = true;
			CaixaDePesquisa.Text = null!;
			LoadFromDataBase();
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
				Produtos.ForEach(x => { if (x.Quantidade <= LabsMainAppWPF.QMDP) { ListaProdutosCadastrados.Items.Add(x); } });
			}
			//
			UpdateQuantidadeLabel();
		}
		//

		private void ListarSelection_Changed(object sender, SelectionChangedEventArgs e)
		{
			FiltrarPorCategoria();
		}

		private void ListasTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(sender == ListasTabControl)
			{
				UpdateQuantidadeLabel();
			}
		}

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

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.F5)
			{
				LoadFromDataBase();
			}
		}
	}
}

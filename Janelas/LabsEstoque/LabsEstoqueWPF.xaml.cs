using Labs.Janelas.LabsEstoque.Dependencias;
using Labs.Janelas.LabsPDV.Dependencias;
using Labs.Main;
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
using static Labs.Main.Modelos;
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
		List<Produto> ProdutosComDefeito = [];
        //
        List<Devolucao> Devolucoes = [];
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
        private async void LoadFromDataBase()
        {
            //Garantimos que todos os itens são limpos para evitar consumo de memória excessiva
            Produtos.Clear();
			ProdutosComDefeito.Clear();
			Fornecedores.Clear();
            Devolucoes.Clear();
            //
            ListaProdutosCadastrados.Items.Clear();
            ListaProdutosDevolvidos.Items.Clear();
			ListaProdutosComDefeito.Items.Clear();
			ListaFornecedores.Items.Clear();
            //
            Produtos = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos,_ => true);
			ProdutosComDefeito = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.ProdutosComDefeito,_ => true);
            Devolucoes = await CloudDataBase.GetManyLocalAsync<Devolucao>(Collections.Devolucoes, _ => true);
			Fornecedores = await CloudDataBase.GetManyLocalAsync<Fornecedor>(Collections.Fornecedores, _ => true);
			//
			//Adicionamos direto na lista visual já que os itens apresentados são os próprios produtos
			foreach (Produto produto in Produtos) { ListaProdutosCadastrados.Items.Add(produto); }
			//
			foreach (Produto produto in ProdutosComDefeito) { ListaProdutosComDefeito.Items.Add(produto); }
            //
            foreach (Devolucao devolucao in Devolucoes) { ListaProdutosDevolvidos.Items.Add(devolucao); }
            //
			foreach (Fornecedor fornecedor in Fornecedores) { ListaFornecedores.Items.Add(fornecedor); }
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
			if(Fornecedores.Count == 0) { var r = Modais.MostrarPergunta("Você Não Possui Fornecedores Cadastrados!, Deseja continuar?"); if (r == MessageBoxResult.No) { return; } }
			//
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
		private void AtualizarEstoqueVisual(string filter)
		{
			//Aqui a gente retorna se não tiver nada até porque não faz sentido gastar processamento atoa
			if (filter.IsNullOrEmpty() && ListaProdutosCadastrados.Items.Count == Produtos.Count) { return; }
			//
			ListaProdutosCadastrados.Items.Clear();
			//
			//
			if (filter.IsNullOrEmpty()) { Produtos.ForEach(x => ListaProdutosCadastrados.Items.Add(x)); return; }
			else
			{
				if (ComboBox_Descricao.IsSelected)
				{
					//
					Produtos.ForEach((x) =>
					{
						if (x.Descricao.Contains(filter, StringComparison.OrdinalIgnoreCase)) { ListaProdutosCadastrados.Items.Add(x); }
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
				Produtos.ForEach(x => { if (x.Quantidade <= LabsMainAppWPF.QMDP) { ListaProdutosCadastrados.Items.Add(x); } });
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


	}
}

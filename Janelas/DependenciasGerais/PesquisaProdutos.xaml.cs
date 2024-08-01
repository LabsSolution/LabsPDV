using Labs.Main;
using MongoDB.Driver.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Labs.Janelas.DependenciasGerais
{
	/// <summary>
	/// Lógica interna para PesquisaProdutos.xaml
	/// </summary>
	public partial class PesquisaProdutos : Window
	{
		//
		List<Produto> Produtos = [];
		//
		public delegate void ProdutoSelecionado(string ID,PesquisaProdutos janela);
		/// <summary>
		/// Esse Evento Retorna o Código de Banco de Dados do produto selecionado. "ID".
		/// </summary>
		public event ProdutoSelecionado OnProdutoSelect = null!;
		//
		public PesquisaProdutos()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Faz a listagem inicial de todos os produtos disponíveis para estoque
		/// </summary>
		public async void ListarProdutos()
		{
			Produtos = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos, _ => true);
			//
			Produtos.ForEach(x => { ListaProdutosCadastrados.Items.Add(x); });
			//
			PesquisaInputBox.Focus();
		}
		//
		private void RetornarProdutoSelecionado()
		{
			if (ListaProdutosCadastrados.SelectedItem is not Produto prod) { return; }
			//Invocamos o evento baseado no produto Selecionado.
			OnProdutoSelect?.Invoke(prod.ID,this);
			//Após invocarmos o evento, simplesmente fechamos
			this.Close();
		}
		//
		private void SairButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
		//
		private void ListaProdutosCadastrados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			RetornarProdutoSelecionado();
		}

		private void Item_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
			{
				if (ListaProdutosCadastrados.SelectedItem is not Produto prod) { AtualizarLista(); return; }
				RetornarProdutoSelecionado();
			}
		}

		public async void AtualizarLista()
		{
			ListaProdutosCadastrados.Items.Clear();
			if (PesquisaInputBox.Text.IsNullOrEmpty()) { Produtos.ForEach(x=> { ListaProdutosCadastrados.Items.Add(x); }); return; }
			//Caso Tenha algo, Pesquisamos
			var Cods = await LabsMain.MotorDeBusca.ProcurarItem(Collections.Produtos,"ID","Descricao",PesquisaInputBox.Text);
			//
			Produtos.ForEach(prod => 
			{
				if (Cods.Contains(prod.ID)) { ListaProdutosCadastrados.Items.Add(prod); }
			});
			//
		}

		private void PesquisaInputBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			AtualizarLista();
		}

		private void Selecionar_Click(object sender, RoutedEventArgs e)
		{
			RetornarProdutoSelecionado();
		}
	}
}

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
	public partial class PesquisaClientes : Window
	{
		//
		List<ClienteLoja> Clientes = [];
		//
		public delegate void ProdutoSelecionado(ClienteLoja cliente,PesquisaClientes janela);
		/// <summary>
		/// Esse Evento Retorna o Código de Banco de Dados do produto selecionado. "ID".
		/// </summary>
		public event ProdutoSelecionado OnClienteSelect = null!;
		//
		public PesquisaClientes()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Faz a listagem inicial de todos os produtos disponíveis para estoque
		/// </summary>
		public async void ListarClientes()
		{
			Clientes = await CloudDataBase.GetManyLocalAsync<ClienteLoja>(Collections.Clientes, _ => true);
			//
			Clientes.ForEach(x => { ListaClientesCadastrados.Items.Add(x); });
			//
			PesquisaInputBox.Focus();
		}
		//
		private void RetornarClienteSelecionado()
		{
			if (ListaClientesCadastrados.SelectedItem is not ClienteLoja cliente) { return; }
			//Invocamos o evento baseado no produto Selecionado.
			OnClienteSelect?.Invoke(cliente,this);
			//Após invocarmos o evento, simplesmente fechamos
			this.Close();
		}
		//
		private void SairButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
		//
		private void ListaClientesCadastrados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			RetornarClienteSelecionado();
		}

		private void Item_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
			{
				if (ListaClientesCadastrados.SelectedItem is not ClienteLoja cliente) { AtualizarLista(); return; }
				RetornarClienteSelecionado();
			}
		}

		public async void AtualizarLista()
		{
			ListaClientesCadastrados.Items.Clear();
			if (PesquisaInputBox.Text.IsNullOrEmpty()) { Clientes.ForEach(x=> { ListaClientesCadastrados.Items.Add(x); }); return; }
			//Caso Tenha algo, Pesquisamos
			var Cods = await LabsMain.MotorDeBusca.ProcurarItem(Collections.Clientes,"ID","Nome",PesquisaInputBox.Text);
			//
			Clientes.ForEach(cl => 
			{
				if (Cods.Contains(cl.ID)) { ListaClientesCadastrados.Items.Add(cl); }
			});
			//
		}

		private void PesquisaInputBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			AtualizarLista();
		}

		private void Selecionar_Click(object sender, RoutedEventArgs e)
		{
			RetornarClienteSelecionado();
		}
	}
}

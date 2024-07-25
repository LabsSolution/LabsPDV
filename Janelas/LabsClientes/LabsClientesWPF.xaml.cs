using Labs.Janelas.LabsClientes.Dependencias;
using Labs.Main;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using Unimake.Business.DFe.Xml.SNCM;

namespace Labs.Janelas.LabsClientes
{
	/// <summary>
	/// Lógica interna para LabsClientesWPF.xaml
	/// </summary>
	public partial class LabsClientesWPF : Window
	{
		private List<ClienteLoja> Clientes = [];
		//Cores
		Brush VerdeClaro = Brushes.LightGreen;
		Color Salmao = Color.FromArgb(200,255,100,100);
		Color AmareloClaro = Color.FromArgb(220,255,255,80);
		Color CinzaClaro = Color.FromArgb(255,160,160,160);
		Color RoxoClaro = Color.FromArgb(255,120,120,255);
		public LabsClientesWPF()
		{
			InitializeComponent();
			LoadFromDatabase();
		}
		//
		private void OnItemSelect(object sender, RoutedEventArgs e)
		{
			if(sender is ListViewItem item)
			{
				item.Background = new SolidColorBrush(RoxoClaro);
			}
		}
		private void OnItemUnselect(object sender, RoutedEventArgs e)
		{
			if(sender is ListViewItem item)
			{
				if(item.Content is ClienteLoja cliente)
				{
					item.Background = new SolidColorBrush(CinzaClaro); cliente.Status = "Cliente Recente";
					if (cliente.DataUltimaCompra != null)
					{
						DateTime UltimaCompra = DateTime.ParseExact(cliente.DataUltimaCompra, "dd/MM/yyyy", CultureInfo.InvariantCulture);
						TimeSpan Dif = DateTime.Now.Subtract(UltimaCompra);
						//
						item.Background = VerdeClaro; cliente.Status = "Cliente Ativo";
						//Aqui utilizamos o numero de dias na parte de configuraçoes gerais
						if (Dif.TotalDays >= 15) { item.Background = new SolidColorBrush(AmareloClaro); cliente.Status = "Inativo a mais de 15 dias"; }
						if (Dif.TotalDays >= 30) { item.Background = new SolidColorBrush(Salmao); cliente.Status = "Inativo a mais de 30 dias"; }
					}
				}
			}
		}
		//
		private void AddClienteToList(ClienteLoja cliente)
		{
			var item = new ListViewItem { Content = cliente };
			item.Foreground = Brushes.Black;
			item.FontSize = 15;
			item.FontFamily = new FontFamily("Segoe UI");
			item.Background = new SolidColorBrush(CinzaClaro); cliente.Status = "Cliente Recente";
			if(cliente.DataUltimaCompra != null)
			{
				DateTime UltimaCompra = DateTime.ParseExact(cliente.DataUltimaCompra,"dd/MM/yyyy",CultureInfo.InvariantCulture);
				TimeSpan Dif = DateTime.Now.Subtract(UltimaCompra);
				//
				item.Background = VerdeClaro; cliente.Status = "Cliente Ativo";
				//Aqui utilizamos o numero de dias na parte de configuraçoes gerais
				if(Dif.TotalDays >= 15) { item.Background = new SolidColorBrush(AmareloClaro); cliente.Status = "Inativo a mais de 15 dias"; }
				if(Dif.TotalDays >= 30) { item.Background = new SolidColorBrush(Salmao); cliente.Status = "Inativo a mais de 30 dias"; }
			}
			//
			item.Selected += OnItemSelect;
			item.Unselected += OnItemUnselect;
			ListaClientesCadastrados.Items.Add(item);
		}
		//
		#region Metodos Publicos
		//
		public async void LoadFromDatabase()
		{
			//Garantimos que a lista esteja vazia
			Clientes.Clear();
			ListaClientesCadastrados.Items.Clear();
			//
			Clientes = await CloudDataBase.GetManyLocalAsync<ClienteLoja>(Collections.Clientes, _ => true);
			//
			Clientes.ForEach(AddClienteToList);
		}
		#endregion

		#region Eventos

		private void UpdateByEvent(object? sender, EventArgs e)
		{
			LoadFromDatabase();
		}
		//Evento de saida da página
		private void VoltarButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void CadastrarClienteButton_Click(object sender, RoutedEventArgs e)
		{
			LabsMain.IniciarDependencia<CadastrarClienteWPF>(app =>
			{
				app.Closed += UpdateByEvent;
			});
		}

		private void EditarClienteButton_Click(object sender, RoutedEventArgs e)
		{
			if (ListaClientesCadastrados.SelectedItem is not ListViewItem ItemCliente) { return; }
			if (ItemCliente.Content is not ClienteLoja cliente) { Modais.MostrarAviso("Você deve selecionar um cliente!"); return; }
			LabsMain.IniciarDependencia<AtualizarClienteWPF>(app =>
			{
				app.SetarCliente(cliente);
				app.Closed += UpdateByEvent;
			});
		}

		private async void RemoverClienteButton_Click(object sender, RoutedEventArgs e)
		{
			if(ListaClientesCadastrados.SelectedItem is not ListViewItem ItemCliente) { return; }
			if(ItemCliente.Content is not ClienteLoja Cliente) { Modais.MostrarAviso("Você deve selecionar um cliente!"); return; }
			//
			var r = Modais.MostrarPergunta($"Você deseja remover o Cliente ({Cliente.Nome}) ?\n\nTodos os dados referentes a este cliente serão deletados!\n\n(Esta ação não pode ser desfeita)");
			if(r == MessageBoxResult.No) { return; }
			//
			if(LabsMainAppWPF.IsConnectedToInternet && LabsMain.Cliente.PossuiPlanoCloud)
			{
				await CloudDataBase.RemoveCloudAsync<ClienteLoja>(Collections.Clientes,x => x.ID == Cliente.ID);
			}
			await CloudDataBase.RemoveLocalAsync<ClienteLoja>(Collections.Clientes,x => x.ID == Cliente.ID);
			Modais.MostrarInfo("Cliente Removido com Sucesso!");
			LoadFromDatabase();
		}

		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.F5:
					LoadFromDatabase();
					break;
			}
		}
		#endregion

		#region Barra de Pesquisa
		private void CaixaDePesquisa_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter) { Pesquisar(); }
		}
		private void PesquisarButton_Click(object sender, RoutedEventArgs e)
		{
			Pesquisar();
		}
		private void LimparFiltrosButton_Click(object sender, RoutedEventArgs e)
		{
			ComboBox_Nome.IsSelected = true;
			CaixaDePesquisa.Text = null!;
			LoadFromDatabase();
		}
		private async void Pesquisar()
		{
			if (CaixaDePesquisa.Text.IsNullOrEmpty()) { LoadFromDatabase(); return; }
			//
			var searchFilter = CaixaDePesquisa.Text;
			ListaClientesCadastrados.Items.Clear();
			//
			if (ComboBox_Nome.IsSelected)
			{
				var filteredIDs = await LabsMain.MotorDeBusca.ProcurarItem(Collections.Clientes,"ID","Nome",searchFilter);
				Clientes.ForEach(x => 
				{ 
					if (filteredIDs.Contains(x.ID)) { AddClienteToList(x); }
				});
			}
			//
			if (ComboBox_CPF.IsSelected)
			{
				var filteredIDs = await LabsMain.MotorDeBusca.ProcurarItem(Collections.Clientes,"ID","CPF",searchFilter);
				Clientes.ForEach(x => 
				{
					if (filteredIDs.Contains(x.ID)) { AddClienteToList(x); }
				});
			}
			//
			if (ComboBox_CNPJ.IsSelected)
			{
				var filteredIDs = await LabsMain.MotorDeBusca.ProcurarItem(Collections.Clientes,"ID","CNPJ",searchFilter);
				Clientes.ForEach(x => 
				{
					if (filteredIDs.Contains(x.ID)) { AddClienteToList(x); }
				});
			}
			//
			if (ComboBox_EMail.IsSelected)
			{
				var filteredIDs = await LabsMain.MotorDeBusca.ProcurarItem(Collections.Clientes,"ID","Email",searchFilter);
				Clientes.ForEach(x => 
				{
					if (filteredIDs.Contains(x.ID)) { AddClienteToList(x); }
				});

			}
		}
		#endregion
	}
}

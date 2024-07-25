using Labs.Main;
using MongoDB.Driver;
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

namespace Labs.Janelas.LabsClientes.Dependencias
{
	/// <summary>
	/// Lógica interna para AtualizarClienteWPF.xaml
	/// </summary>
	public partial class AtualizarClienteWPF : Window
	{
		private ClienteLoja Cliente = null!;
		public AtualizarClienteWPF()
		{
			InitializeComponent();
		}
		//
		public void SetarCliente(ClienteLoja cliente)
		{
			this.Cliente = cliente;
			NomeInputBox.Text = Cliente.Nome;
			CPFInputBox.Text = Cliente.CPF;
			CNPJInputBox.Text = Cliente.CNPJ;
			TelefoneInputBox.Text = Cliente.Fone;
			EmailInputBox.Text = Cliente.Email;
			//
		}
		//
		private async void AtualizarClienteButton_Click(object sender, RoutedEventArgs e)
		{
			if(Cliente == null) { return; }
			if (NomeInputBox.Text.IsNullOrEmpty()) { Modais.MostrarAviso("Você Deve Informar o Nome do Cliente!"); return; }
			Cliente.Nome = NomeInputBox.Text;
			Cliente.CPF = CPFInputBox.Text.IsNullOrEmpty() ? "NAO INFORMADO" : CPFInputBox.Text;
			Cliente.CNPJ = CNPJInputBox.Text.IsNullOrEmpty() ? "NAO INFORMADO" : CNPJInputBox.Text;
			Cliente.Fone = TelefoneInputBox.Text.IsNullOrEmpty()? "NAO INFORMADO" : TelefoneInputBox.Text;
			Cliente.Email = EmailInputBox.Text.IsNullOrEmpty()? "NAO INFORMADO" : EmailInputBox.Text;
			//
			if(LabsMain.Cliente.PossuiPlanoCloud && LabsMainAppWPF.IsConnectedToInternet)
			{
				await CloudDataBase.RegisterCloudAsync(Collections.Clientes,Cliente, Builders<ClienteLoja>.Filter.Eq("ID", Cliente.ID));
			}
			await CloudDataBase.RegisterLocalAsync(Collections.Clientes,Cliente,Builders<ClienteLoja>.Filter.Eq("ID",Cliente.ID));
			//
			LabsMainAppWPF.IndexarClientes();
			//
			Modais.MostrarInfo("Cliente Atualizado com Sucesso!");
		}
		//
		private void SairButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

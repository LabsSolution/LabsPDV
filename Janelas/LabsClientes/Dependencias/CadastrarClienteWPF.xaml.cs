using Labs.Main;
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
	/// Lógica interna para CadastrarClienteWPF.xaml
	/// </summary>
	public partial class CadastrarClienteWPF : Window
	{
		public CadastrarClienteWPF()
		{
			InitializeComponent();
		}
		private void LimparCampos()
		{
			NomeInputBox.Text = null!;
			CPFInputBox.Text = null!;
			CNPJInputBox.Text = null!;
			TelefoneInputBox.Text = null!;
			EmailInputBox.Text = null!;
		}
		//
		private async void CadastrarClienteButton_Click(object sender, RoutedEventArgs e)
		{
			if (NomeInputBox.Text.IsNullOrEmpty()) { Modais.MostrarAviso("Você Deve Informar o Nome do Cliente!"); return; }
			var cpf = CPFInputBox.Text.IsNullOrEmpty() ? "NAO INFORMADO" : CPFInputBox.Text;
			var cnpj = CNPJInputBox.Text.IsNullOrEmpty() ? "NAO INFORMADO" : CNPJInputBox.Text;
			var fone = TelefoneInputBox.Text.IsNullOrEmpty()? "NAO INFORMADO" : TelefoneInputBox.Text;
			var email = EmailInputBox.Text.IsNullOrEmpty()? "NAO INFORMADO" : EmailInputBox.Text;
			//
			var clienteLoja = new ClienteLoja(NomeInputBox.Text,cpf,cnpj,fone,email,[]);
			//
			if(LabsMain.Cliente.PossuiPlanoCloud && LabsMainAppWPF.IsConnectedToInternet)
			{
				await CloudDataBase.RegisterCloudAsync(Collections.Clientes,clienteLoja);
			}
			await CloudDataBase.RegisterLocalAsync(Collections.Clientes,clienteLoja);
			//
			LabsMainAppWPF.IndexarClientes();
			//
			Modais.MostrarInfo("Cliente Registrado com Sucesso!");
		}
		//
		private void LimparTudoButtom_Click(object sender, RoutedEventArgs e)
		{
			LimparCampos();
		}
		//
		private void SairButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

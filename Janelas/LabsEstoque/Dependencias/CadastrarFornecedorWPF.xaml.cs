using Labs.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

namespace Labs.Janelas.LabsEstoque.Dependencias
{
	/// <summary>
	/// Lógica interna para CadastrarFornecedorWPF.xaml
	/// </summary>
	public partial class CadastrarFornecedorWPF : Window
	{
		ViaCepClient ViaCepClient { get; } = new();
		public CadastrarFornecedorWPF()
		{
			InitializeComponent();
		}
		private bool VerificarCampos()
		{
			if (NomeEmpresaInputBox.Text.IsNullOrEmpty()) { Modais.MostrarAviso("Por Favor, Preencha o Nome do Fornecedor!"); return false; }
			if (BairroInputBox.Text.IsNullOrEmpty()) { BairroInputBox.Text = "N/A"; }
			if (CidadeInputBox.Text.IsNullOrEmpty()) { CidadeInputBox.Text = "N/A"; }
			if (EnderecoInputBox.Text.IsNullOrEmpty()) { EnderecoInputBox.Text = "N/A"; }
			if (ComplementoInputBox.Text.IsNullOrEmpty()) { ComplementoInputBox.Text = "N/A"; }
			if (ContatoInputBox.Text.IsNullOrEmpty()) { ContatoInputBox.Text = "N/A"; }
			if (EmailInputBox.Text.IsNullOrEmpty()) { EmailInputBox.Text = "N/A"; }
			if (CNPJInputBox.Text.IsNullOrEmpty()) { CNPJInputBox.Text = "N/A"; }
			return true;
		}
		//
		private async void BuscaEndereco(string cep)
		{
			try
			{
				Endereco endereco = await ViaCepClient.GetEnderecoAsync(cep);
				if (endereco != null)
				{
					BairroInputBox.Text = endereco.Bairro;
					CidadeInputBox.Text = $"{endereco.Localidade}-{endereco.Uf}";
					EnderecoInputBox.Text = $"{endereco.Logradouro}";
					ComplementoInputBox.Text = $"{endereco.Complemento}";
				}
			}
			catch
			{
				Modais.MostrarAviso("Não Foi Possível Realizar a Pesquisa.");
			}
		}

		private void CepInputBox_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
			{
				if (CepInputBox.Text.IsNullOrEmpty() || !Utils.TryParseToInt(CepInputBox.Text,out int _)) { Modais.MostrarAviso("Informe um Cep Válido"); return; }
				//
				BuscaEndereco(CepInputBox.Text);
			}
		}
		//
		private async void RegistrarFornecedor()
		{

			Endereco endereco;
			try { endereco = await ViaCepClient.GetEnderecoAsync(CepInputBox.Text); } catch { endereco = new Endereco
			{
				Bairro = "N/A",
				Cep = "N/A",
				Complemento = "N/A",
				Localidade = "N/A",
				Uf = "N/A",
				Logradouro = "N/A"
			}; }
			//
			Fornecedor fornecedor = new(CNPJInputBox.Text, NomeEmpresaInputBox.Text, ContatoInputBox.Text, EmailInputBox.Text, endereco);
			//
			if (LabsMain.Cliente.PossuiPlanoCloud)
			{
				await CloudDataBase.RegisterCloudAsync(Collections.Fornecedores, fornecedor);
			}
			//
			await CloudDataBase.RegisterLocalAsync(Collections.Fornecedores, fornecedor);
			//
			Modais.MostrarInfo("Fornecedor Cadastrado com Sucesso!");
		}
		//
		private void RegistrarFornecedorButton_Click(object sender, RoutedEventArgs e)
		{
			if (!VerificarCampos()) { Modais.MostrarAviso("Preencha os Campos Obrigatórios"); return; }
			// caso esteja tudo ok, registramos a Empresa
			RegistrarFornecedor();

		}
		private void CancelarRegistroButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

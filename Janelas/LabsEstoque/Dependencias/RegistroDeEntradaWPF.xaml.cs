using Labs.Main;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
	/// Lógica interna para RegistroDeEntradaWPF.xaml
	/// </summary>
	public partial class RegistroDeEntradaWPF : Window
	{
		private Produto Produto { get; set; } = null!;
		double ValorTotal = 0;

		public RegistroDeEntradaWPF()
		{
			InitializeComponent();
		}
		//
		
		//
		private void Calc()
		{
			bool isQTD = Utils.TryParseToInt(QuantidadeInputBox.Text,out int Quantidade);
			bool isCusto = Utils.TryParseToDouble(CustoUnitarioInputBox.Text,out double Custo);
			if(!isQTD && !isCusto) { return; }
			//
			ValorTotal = Quantidade * Custo;
			ValorTotalLabel.Content = Utils.FormatarValor(ValorTotal);
		}
		private void CalcLucro()
		{
			bool isCusto = Utils.TryParseToDouble(CustoUnitarioInputBox.Text, out double Custo);
			bool isPreco = Utils.TryParseToDouble(PrecoDeVendaInputBox.Text, out double Preco);
			//
			if (!isCusto && !isPreco) { return; }
			//
			MargemDeLucroLabel.Content = $"{Utils.FormatarValor(Utils.GetInverseRelativeValue(Preco, 0, Custo)*100)}%";
		}
		//
		private bool VerificarCampos()
		{
			if (DescricaoProdutoLabel.Content.IsNullOrEmpty()) { return false; }
			if (FornecedorLabel.Content.IsNullOrEmpty()) { return false; }
			if (QuantidadeInputBox.Text.IsNullOrEmpty()) { return false; }
			if (PrecoDeVendaInputBox.Text.IsNullOrEmpty()) { return false; }
			if (TabelaDeDatas.Text.IsNullOrEmpty()) { return false; }
			if(TabelaDeDatas.SelectedDate == null!) { return false; }
			return true;
		}
		//
		private void ResetInterface()
		{
			DescricaoProdutoLabel.Content = null!;
			FornecedorLabel.Content = null!;
			CustoUnitarioInputBox.Text = null!;
			QuantidadeInputBox.Text = null!;
			PrecoDeVendaInputBox.Text = null!;
			TabelaDeDatas.Text = null!;
			TabelaDeDatas.SelectedDate = null!;
			ValorTotalLabel.Content = null!;
			MargemDeLucroLabel.Content = null!;
		}
		//
		//
		public void SetarProduto(Produto Produto)
		{
			this.Produto = Produto;
			//
			DescricaoProdutoLabel.Content = Produto.Descricao;
			FornecedorLabel.Content = Produto.Fornecedor.NomeEmpresa;
			//
		}

		private void CheckInputBox(object sender, TextCompositionEventArgs e)
		{
			if (sender is TextBox textBox)
			{
				var match = Utils.OnlyNumber().Match(textBox.Text);
				if (match.Value.Length > 0) { textBox.Text = textBox.Text.Replace(match.Value, ""); }
				e.Handled = match.Success ;
				if(match.Success) { Modais.MostrarAviso("Somente Números!"); }
			}
		}
		private void CheckInputBoxMonetary(object sender, TextCompositionEventArgs e)
		{
			if (sender is TextBox textBox)
			{
				var match = Utils.OnlyMonetary().Match(textBox.Text);
				if (match.Value.Length > 0) { textBox.Text = textBox.Text.Replace(match.Value, ""); }
				e.Handled = match.Success ;
				if(match.Success) { Modais.MostrarAviso("Somente Números!"); }
				//
			}
			Calc();
			CalcLucro();
		}
		//
		//
		private async void RealizarEntradaDeProduto()
		{
			if (!VerificarCampos()) { Modais.MostrarAviso("Por Favor Preencha todos os Campos Corretamente!"); return; }
			//
			string? data = TabelaDeDatas?.SelectedDate!.Value.ToString("dd/MM/yyyy");
			if(data == null) { Modais.MostrarAviso("Insira uma Data válida!"); return; }
			//
			bool isQTD = Utils.TryParseToInt(QuantidadeInputBox.Text,out int QTD);
			if (!isQTD) { Modais.MostrarAviso("Insira uma Quantidade válida"); return; }
			//
			bool isCusto = Utils.TryParseToDouble(CustoUnitarioInputBox.Text,out double Custo);
			if (!isCusto) { Modais.MostrarAviso("Insira um Custo Válido"); return; }
			//
			bool isPreco = Utils.TryParseToDouble(PrecoDeVendaInputBox.Text, out double Preco);
			if (!isPreco) { Modais.MostrarAviso("Insira um Preco Válido"); return; }
			// se está tudo OK continuamos, procuramos o produto, atualizamos a sua quantidade e inserimos novamente na database
			var total = Math.Round(QTD * Custo, 2);
			//
			Fornecedor fornecedor = await CloudDataBase.GetLocalAsync<Fornecedor>(Collections.Fornecedores,x => x.ID == Produto.Fornecedor.ID);
			fornecedor.TotalComprado += total;
			//Atualizamos o fornecedor com o valor atualizado
			//Se o cliente tem plano cloud, atualizamos na cloud tbm
			if (LabsMain.Cliente.PossuiPlanoCloud) { await CloudDataBase.RegisterLocalAsync(Collections.Fornecedores, fornecedor, Builders<Fornecedor>.Filter.Eq("ID", fornecedor.ID)); }
			//
			await CloudDataBase.RegisterLocalAsync(Collections.Fornecedores,fornecedor,Builders<Fornecedor>.Filter.Eq("ID",fornecedor.ID));
			//
			Produto.Quantidade += QTD;
			Produto.Fornecedor = fornecedor;
			Produto.Preco = Preco;
			Produto.Custo = Custo;
			//Atualizamos o produto
			if (LabsMain.Cliente.PossuiPlanoCloud) { await CloudDataBase.RegisterCloudAsync(Collections.Produtos,Produto,Builders<Produto>.Filter.Eq("ID",Produto.ID)); }
			await CloudDataBase.RegisterLocalAsync(Collections.Produtos, Produto, Builders<Produto>.Filter.Eq("ID", Produto.ID));
			//
			EntradaDeProduto entrada = new(data,Produto,Produto.Fornecedor,QTD,Custo,Preco,total);
			//
			// Logo após setamos para que se o cliente tiver o plano cloud contratado, setamos o produto na sua respectiva nuvem cloud
			if (LabsMain.Cliente.PossuiPlanoCloud)
			{
				await CloudDataBase.RegisterCloudAsync(Collections.Entradas,entrada);
			}
			await CloudDataBase.RegisterLocalAsync(Collections.Entradas,entrada);
			//
			Modais.MostrarInfo("Entrada Registrada com Sucesso!");
			ResetInterface();
		}
		//
		private void RegistrarEntradaButton_Click(object sender, RoutedEventArgs e)
		{
			var r = Modais.MostrarPergunta("Deseja Confirmar a Entrada?, Confira se todos os campos estão corretos!");
			if(r == MessageBoxResult.Yes)
			{
				RealizarEntradaDeProduto();
			}
		}

		private void Cancelar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

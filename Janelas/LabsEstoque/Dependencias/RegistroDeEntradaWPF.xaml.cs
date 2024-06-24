using Labs.Main;
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
using static Labs.Main.Modelos;

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
		private void ResetInterface()
		{
			DescricaoProdutoLabel.Content = null!;
			FornecedorLabel.Content = null!;
			QuantidadeInputBox.Text = null!;
			PrecoDeVendaInputBox.Text = null!;
			TabelaDeDatas.Text = null!;
			TabelaDeDatas.SelectedDate = null!;
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
	}
}

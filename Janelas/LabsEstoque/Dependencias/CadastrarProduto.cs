using Labs.LABS_PDV;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Labs.LABS_PDV.Modelos;

namespace Labs.Janelas.LabsEstoque.Dependencias
{
	public partial class CadastrarProduto : Form
	{
		public CadastrarProduto()
		{
			InitializeComponent();
		}
		//Chamado quando pressionado enter
		private void DescricaoManualInput_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (Utils.IsValidBarCode(DescricaoManualInput.Text)) { MessageBox.Show("Não é Permitido a insersão de Código de barras na descrição", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
				DescricaoProdutoOutput.Text = DescricaoManualInput.Text;
				DescricaoManualInput.Text = null;
			}
		}
		//Metodos Privados
		void LimparCampos()
		{
			DescricaoManualInput.Text = null;
			DescricaoProdutoOutput.Text = null;
			QuantEstoqueInput.Text = null;
			PrecoInput.Text = null;
			CodBarras.Text = null;
		}
		//

		private void CadastrarButton_Click(object sender, EventArgs e)
		{
			string Descricao, QuantEstoque, Preco, Cod;
			Descricao = DescricaoProdutoOutput.Text;
			QuantEstoque = QuantEstoqueInput.Text;
			Preco = PrecoInput.Text;
			Cod = CodBarras.Text;
			//Se todos os Parâmetros são validos
			if (Descricao.Length > 0 && Utils.TryParseToInt(QuantEstoque, out int Qtd) && Utils.TryParseToDouble(Preco,out double dPreco) && Cod.Length > 0)
			{
				Produto produto = new()
				{
					Descricao = Descricao,
					Quantidade = Qtd,
					Preco = Math.Round(dPreco,2),
					CodBarras = Cod,
				};
				//Depois de ter o produto Pronto, registramos ele :D
				CloudDataBase.RegisterLocalAsync(Collections.Produtos,produto);
				//
				Modais.MostrarInfo("Produto Cadastrado Com Sucesso!");
				//
				LimparCampos();
			}
		}
		//
		private void LimparButton_Click(object sender, EventArgs e)
		{
			LimparCampos();
		}
		//
		private void SairButton_Click(object sender, EventArgs e)
		{
			//Não tem muuuito o que fazer além de fechar kk
			this.Close();
		}
		//
		private void CadastrarProduto_Load(object sender, EventArgs e)
		{

		}
	}
}

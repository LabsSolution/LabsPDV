using Labs.LABS_PDV;
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

	public partial class AtualizarProduto : Form
	{
		public AtualizarProduto()
		{
			InitializeComponent();
		}
		//
		public void SetarProduto(Produto produto)
		{
			IDProduto.Text = produto.ID;
			DescricaoProdutoOutput.Text = produto.Descricao;
			QuantEstoqueInput.Text = produto.Quantidade.ToString();
			PrecoInput.Text = produto.Preco.ToString();
			CodBarras.Text = produto.CodBarras;
		}
		//
		private void AtualizarButton_Click(object sender, EventArgs e)
		{
			string ID, Descricao, QuantEstoque, Preco, Cod; // Geramos as variaveis locais necessárias
															//
			ID = IDProduto.Text;
			Descricao = DescricaoProdutoOutput.Text;
			QuantEstoque = QuantEstoqueInput.Text;
			Preco = PrecoInput.Text;
			Cod = CodBarras.Text;
			//
			//Se todos os Parâmetros são validos
			if (Descricao.Length > 0 && Utils.TryParseToInt(QuantEstoque, out int Qtd) && Utils.TryParseToDouble(Preco,out double dPreco) && Cod.Length > 0)
			{
				//Montamos o produto atribuindo o ID para a busca
				Produto produto = new()
				{
					ID = IDProduto.Text,
					Descricao = Descricao,
					Quantidade = Qtd,
					Preco = Math.Round(dPreco,2),
					CodBarras = Cod,
				};
				//Informamos para o Usuário que o Produto será atualizado
				DialogResult r = Modais.MostrarPergunta($"Deseja Atualizar o Produto de ID ({ID})? \n\n" +

					$"Descrição Anterior: {Descricao} | Agora: {produto.Descricao}\n\n" +

					$"Quant. Estoque Anterior: {QuantEstoque} | Agora: {produto.Quantidade}\n\n" +

					$"Preço Anterior: {Preco} | Agora: {produto.Preco}\n\n" +

					$"Cod. Barras Anterior: {Cod} | Agora: {produto.CodBarras}");
				//
				//Caso o Usuário Confirme a Ação, Seguimos em Frente
				if (r == DialogResult.Yes)
				{
					CloudDataBase.UpdateProdutoAsync(produto);
					Modais.MostrarInfo("Produto Atualizado Com Sucesso!");
				}//
				else { Modais.MostrarInfo("Atualização Cancelada"); }
			}
		}
		//
		private void SairButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void OnDescManualKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (Utils.TryParseToInt(DescricaoManualInput.Text, out _)) { MessageBox.Show("Não é Permitido a insersão de Código de barras na descrição", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
				DescricaoProdutoOutput.Text = DescricaoManualInput.Text;
				DescricaoManualInput.Text = null;
			}
		}
		//

	}
}

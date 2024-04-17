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
			IDProduto.Text = produto.ID.ToString();
			DescricaoProdutoOutput.Text = produto.Descricao;
			QuantEstoqueInput.Text = produto.Quantidade.ToString();
			PrecoInput.Text = produto.Preco;
			CodBarras.Text = produto.CodBarras;
		}
		//
		public void AtualizaProduto(Produto produto)
		{

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
			if (Utils.TryParseToInt(ID,out int ID_Produto) && Descricao.Length > 0 && Utils.TryParseToInt(QuantEstoque, out int Qtd) && Preco.Length > 0 && Cod.Length > 0)
			{
				//Montamos o produto atribuindo o ID para a busca
				Produto produto = new()
				{
					ID = ID_Produto,
					Descricao = Descricao,
					Quantidade = Qtd,
					Preco = PrecoInput.Text,
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
				if(r == DialogResult.Yes)
				{
					DataBase.UpdateProduto(produto);
					Modais.MostrarInfo("Produto Atualizado Com Sucesso!");
				}//
				else{ Modais.MostrarInfo("Atualização Cancelada"); }
			}
		}
		//
		private void SairButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		//

	}
}

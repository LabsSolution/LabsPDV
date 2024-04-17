using Labs.Janelas.LabsEstoque.Dependencias;
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

namespace Labs.Janelas.LabsEstoque
{
	public partial class LabsEstoque : Form
	{
		public LabsEstoque()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Adiciona um produto a lista de produtos no estoque (Chamado somente quando Iniciado)
		/// </summary>
		/// <param name="ID">ID No Banco de Dados</param>
		/// <param name="Desc">Descrição do produto</param>
		/// <param name="QtdEstoque">Quantidade no estoque</param>
		/// <param name="Preco">Preco Unitário</param>
		/// <param name="CodBarras">Código de Barras</param>
		private void InserirProdutoNaLista(string ID, string Desc, string QtdEstoque, string Preco, string CodBarras)
		{
			ListViewItem row = new([ID, Desc, QtdEstoque, $"R$ {Preco}", CodBarras]);
			ListaProdutosEstoque.Items.Insert(ListaProdutosEstoque.Items.Count, row);
		}


		/// <summary>
		/// Chamado quando a Janela de Estoque é Carregada
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLabsEstoqueLoad(object sender, EventArgs e)
		{
			//
			ListaProdutosEstoque.Items.Clear(); // Limpamos a lista antes de carregar
												//
			List<Produto> Produtos = DataBase.GetProdutos(); // Pegados todos os produtos cadastrados
			foreach (Produto produto in Produtos) //Iteramos e adicionamos a lista
			{
				InserirProdutoNaLista(produto.ID.ToString(), produto.Descricao, produto.Quantidade.ToString(), produto.Preco, produto.CodBarras);
			}
		}
		//------------------------//
		//Eventos
		//------------------------//
		private void CadastrarButton_Click(object sender, EventArgs e)
		{
			LABS_PDV_MAIN.IniciarDependencia<CadastrarProduto>();
		}

		private void AtualizarButton_Click(object sender, EventArgs e)
		{
			if(ListaProdutosEstoque.SelectedItems.Count == 0) { Modais.MostrarAviso("Você Precisa Selecionar um Produto da Lista Para Atualizar os Dados!"); return; }
			LABS_PDV_MAIN.IniciarDependencia<AtualizarProduto>();
		}

		private void RemoverButton_Click(object sender, EventArgs e)
		{

		}
	}
}

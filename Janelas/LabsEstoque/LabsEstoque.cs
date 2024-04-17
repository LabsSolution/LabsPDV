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
		//---------------//
		//    METODOS
		//---------------//
		//
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
		/// Atualiza a lista de produtos do estoque
		/// </summary>
		private void AtualizarLista()
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

		private void UpdateByEvent(object? sender, FormClosedEventArgs e)
		{
			AtualizarLista();
			var form = sender as Form;
			if (form != null) { form.FormClosed -= UpdateByEvent; }
		}

		/// <summary>
		/// Chamado quando a Janela de Estoque é Carregada
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLabsEstoqueLoad(object sender, EventArgs e)
		{
			AtualizarLista();
		}
		//------------------------//
		//Eventos
		//------------------------//
		private void CadastrarButton_Click(object sender, EventArgs e)
		{
			LABS_PDV_MAIN.IniciarDependencia<CadastrarProduto>();
		}
		//
		private void AtualizarButton_Click(object sender, EventArgs e)
		{
			if (ListaProdutosEstoque.SelectedItems.Count == 0) { Modais.MostrarAviso("Você Precisa Selecionar um Produto da Lista Para Atualizar os Dados!"); return; }
			//
			ListViewItem item = ListaProdutosEstoque.SelectedItems[0]; // Retorna o produto que está selecionado na lista
																	   //
																	   //Pegamos o produto pelo código e retornamos através da database local (poderia ser da lista, mas to com preguiça) *Além da DataBase ser mais confiável*.
			string Cod = item.SubItems[(int)ColunaEstoqueBD.CodBarras].Text;
			Produto produto = Utils.GetProdutoByCode(Cod);
			//
			AtualizarProduto attProd = LABS_PDV_MAIN.IniciarDependencia<AtualizarProduto>();
			//
			//
			attProd.SetarProduto(produto);
			attProd.FormClosed += UpdateByEvent;
		}

		private void RemoverButton_Click(object sender, EventArgs e)
		{

		}

		private void OnLabsEstoqueKeyUp(object sender, KeyEventArgs e)
		{
			//Se o Usuário pressionar F5, Atualiza a lista
			if (e.KeyCode == Keys.F5) { AtualizarLista(); }
		}

		private void VoltarButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}

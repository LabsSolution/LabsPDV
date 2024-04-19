﻿using Labs.Janelas.LabsEstoque.Dependencias;
using Labs.LABS_PDV;
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
			CadastrarProduto app = LABS_PDV_MAIN.IniciarDependencia<CadastrarProduto>();
			app.FormClosed += UpdateByEvent;
		}
		//
		private void AtualizarButton_Click(object sender, EventArgs e)
		{
			if (ListaProdutosEstoque.SelectedItems.Count == 0) { Modais.MostrarAviso("Você Precisa Selecionar um Produto da Lista Para Atualizar os Dados!"); return; }
			//
			ListViewItem item = ListaProdutosEstoque.SelectedItems[0]; // Retorna o produto que está selecionado na lista
			//
			//Pegamos o produto pelo código e retornamos através da database local (poderia ser da lista, mas to com preguiça) *Além da DataBase ser mais confiável*.
			string Cod = item.SubItems[ColunaCodBarras.Index].Text;
			if (Utils.GetProdutoByCode(Cod, out Produto produto))//se conseguirmos achar o produto, prosseguimos
			{
				//
				AtualizarProduto attProd = LABS_PDV_MAIN.IniciarDependencia<AtualizarProduto>(app => 
				{ 
					app.SetarProduto(produto); 
					app.FormClosed += UpdateByEvent; 
				}) ;
				//
			}
		}

		//
		private void RemoverButton_Click(object sender, EventArgs e)
		{
			if(ListaProdutosEstoque.SelectedItems.Count == 0) { Modais.MostrarAviso("Você Precisa Selecionar um Produto da Lista Para Remover os Dados!"); return; }
			//
			ListViewItem item = ListaProdutosEstoque.SelectedItems[0];
			string Cod = item.SubItems[ColunaCodBarras.Index].Text;
			if(Utils.GetProdutoByCode(Cod,out Produto produto)) // se o produto existe seguimos
			{
				//
				DataBase.RemoveProduto(produto); // Chamamos a DataBase para a remoção do item desejado
				//Fazemos essa proteção para que não haja Bugs ou travamentos caso não seja encontrado nada na database
				//O que pode acontecer caso haja alguma falha no espelhamento da lista com a database.
			}
			//Agora que deletamos da database, podemos remover da lista.
			if (ListaProdutosEstoque.Items.Contains(item))
			{
				ListaProdutosEstoque.Items.Remove(item);
			}
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
		//
		//PREVENÇÃO DE MOVIMENTO DE JANELA // Qualquer janela que tiver a propriedade de prevenção de movimento deve herdar esse Método
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x0112;
			const int SC_MOVE = 0xF010;

			switch (m.Msg)
			{
				case WM_SYSCOMMAND:
					int command = m.WParam.ToInt32() & 0xfff0;
					if (command == SC_MOVE)
						return;
					break;
			}

			base.WndProc(ref m);
		}
	}
}

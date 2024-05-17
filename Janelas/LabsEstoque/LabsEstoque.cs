using Labs.Janelas.LabsEstoque.Dependencias;
using Labs.LABS_PDV;
using MongoDB.Bson;
using MongoDB.Driver;
using static Labs.LABS_PDV.Modelos;

namespace Labs.Janelas.LabsEstoque
{
	public partial class Labs_Estoque : Form
	{
		public Labs_Estoque()
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
		/// <param name="EmFalta">Indicador se o Produto está quase acabando</param>
		private void InserirProdutoNaLista(string ID, string Desc, string QtdEstoque, string Preco, string CodBarras, bool EmFalta = false)
		{
			var item = new ListViewItem([ID, Desc, QtdEstoque, $"R$ {Preco}", CodBarras]);
			if (EmFalta) { item.BackColor = Color.IndianRed; item.ForeColor = Color.Black; } else {  item.BackColor = Color.LightGreen; item.ForeColor = Color.Black; }
			ListaProdutosEstoque.Items.Insert(ListaProdutosEstoque.Items.Count, item);
		}
		/// <summary>
		/// Atualiza a lista de produtos do estoque
		/// </summary>
		private async void AtualizarLista()
		{
			//
			ListaProdutosEstoque.Items.Clear(); // Limpamos a lista antes de carregar
			//
			List<Produto> Produtos = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos, _ => true); // Pega todos os produtos cadastrados
			
			foreach (Produto produto in Produtos) //Iteramos e adicionamos a lista
			{
				InserirProdutoNaLista(produto.ID.ToString(), produto.Descricao, produto.Quantidade.ToString(), Utils.FormatarValor(produto.Preco), produto.CodBarras,produto.Quantidade <= LabsMainApp.QMDP);
				await Task.Delay(1);
			}
		}
		//
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
		//-----------------------------------------//
		//-------------MÉTODOS ESTÁTICOS!!!
		//-----------------------------------------//
		public static async Task AbaterProdutosEmEstoqueAsync(List<Produto> Produtos)
		{
            try
            {
				//Pega os Produtos do Estoque
				List<Produto> toUpdateList = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos, x => Produtos.Exists(y => y.ID == x.ID));
				//
				foreach (var produto in Produtos)
				{
					Produto? prod = toUpdateList.Find(x => x.ID == produto.ID);
					//
					if(prod != null)
					{
						prod.Quantidade -= produto.Quantidade;
						CloudDataBase.UpdateOneLocalAsync(Collections.Produtos,prod,Builders<Produto>.Filter.Eq("ID",prod.ID));
					}
				}
				// Logo após Chamamos o UpdateMany
				//
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw;
            }
        }
		//------------------------//
		//Eventos
		//------------------------//
		private void CadastrarButton_Click(object sender, EventArgs e)
		{
			CadastrarProduto app = LABS_PDV_MAIN.IniciarDependencia<CadastrarProduto>(app =>
			{
				app.FormClosed += UpdateByEvent;
			});
		}
		//
		private async void AtualizarProduto()
		{
			if (ListaProdutosEstoque.SelectedItems.Count == 0) { Modais.MostrarAviso("Você Precisa Selecionar um Produto da Lista Para Atualizar os Dados!"); return; }
			//
			ListViewItem item = ListaProdutosEstoque.SelectedItems[0]; // Retorna o produto que está selecionado na lista
																	   //
																	   //Pegamos o produto pelo código e retornamos através da database local (poderia ser da lista, mas to com preguiça) *Além da DataBase ser mais confiável*.
			string Cod = item.SubItems[ColunaCodBarras.Index].Text;
			Produto produto = await Utils.GetProdutoByCode(Cod);
			if (produto != null)//se conseguirmos achar o produto, prosseguimos
			{
				//
				AtualizarProduto attProd = LABS_PDV_MAIN.IniciarDependencia<AtualizarProduto>(app =>
				{
					app.SetarProduto(produto);
					app.FormClosed += UpdateByEvent;
				});
				//
			}
		}
		private async void RemoverProduto()
		{
			if (ListaProdutosEstoque.SelectedItems.Count == 0) { Modais.MostrarAviso("Você Precisa Selecionar um Produto da Lista Para Remover os Dados!"); return; }
			//
			ListViewItem item = ListaProdutosEstoque.SelectedItems[0];
			string Cod = item.SubItems[ColunaCodBarras.Index].Text;
			Produto produto = await Utils.GetProdutoByCode(Cod);
			if (produto != null) // se o produto existe seguimos
			{
				//
				var r = Modais.MostrarPergunta("Deseja Remover o Produto Selecionado?\n\n" +
											   "ESSA OPERAÇÃO NÃO PODE SER DESFEITA!");
				if(r == DialogResult.Yes)
				{
					CloudDataBase.RemoveLocalAsync<Produto>(Collections.Produtos,x => x.ID == produto.ID);
					// Chamamos a DataBase para a remoção do item desejado
					//Fazemos essa proteção para que não haja Bugs ou travamentos caso não seja encontrado nada na database
					//O que pode acontecer caso haja alguma falha no espelhamento da lista com a database.
					//Agora que deletamos da database, podemos remover da lista.
					if (ListaProdutosEstoque.Items.Contains(item))
					{
						ListaProdutosEstoque.Items.Remove(item);
					}
					Modais.MostrarInfo("Produto Removido Com Sucesso!");
				}
			}
			
		}
		private void AtualizarButton_Click(object sender, EventArgs e)
		{
			AtualizarProduto();
		}

		//
		private void RemoverButton_Click(object sender, EventArgs e)
		{
			RemoverProduto();
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

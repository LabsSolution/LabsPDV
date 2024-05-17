using Labs.Janelas.LabsEstoque;
using Labs.Janelas.LabsPDV.Dependencias;
using Labs.LABS_PDV;
using Unimake.Business.DFe.Xml.EFDReinf;
using static Labs.LABS_PDV.Modelos;

namespace Labs.Janelas.LabsPDV
{
	public partial class LabsPDV : Form
	{
		//Constantes porque precisamos que não usem outro endereçamento de memória
		//Coisa que o static faz a cada instância
		const string CaixaAberto = "CAIXA ABERTO";
		const string CaixaFechado = "CAIXA FECHADO";
		const string AbrirCaixaText = "ABRIR CAIXA";
		const string FecharCaixaText = "FECHAR CAIXA";
		//
		private static bool EstaAberto { get; set; } = false; // deixar isso como false por padrão
		private static bool RealizandoVenda { get; set; } = false;
		//
		private protected List<Produto> Produtos = []; // Usado para o gerenciamento do valor total
		private protected double PagamentoTotal = 0.0; // registro do pagamento total
		//
		public CaixaLabs CaixaLabs { get; private set; } = null!;
		//
		public LabsPDV()
		{
			InitializeComponent();
			if (!EstaAberto) { FecharCaixa(); } else { ResetarFoco(); }
			//
		}
		//
        public void AbrirCaixa()
        {
            //
            //FAZER JANELINHA DE ABERTURA DE CAIXA // Gestão de Fluxo
            var JDAC = LABS_PDV_MAIN.IniciarDependencia<JanelaAberturaDeCaixa>(App =>
            {
                App.onJDACClose += RealizarAbertura;
            }, true, false);
        }
		//
        /// <summary>
        /// Chamado quando a janela de abertura de caixa é "finalizada";
        /// </summary>
        /// <param name="ValorDeAbertura">Valor total com que o caixa está abrindo</param>
        /// <param name="Janela">Retorno da Própria janela</param>
        private void RealizarAbertura(double ValorDeAbertura,JanelaAberturaDeCaixa Janela)
		{
			//Iniciamos o CaixaLabs, se não conseguirmos lançamos um erro
			CaixaLabs = new(ValorDeAbertura,new OperadorCaixa("Operador Teste","User","Pass")); // Aqui estamos iniciando na maluquice
			//
			if(CaixaLabs == null) { Modais.MostrarErro("ERRO CRÍTICO!\nA Comunicação com um módulo interno foi Interrompida!"); return; }
			CaixaLabs.RealizarAbertura();
			Janela.Close();
            //
            //
            EstaAberto = true;
            CaixaStateLabel.Text = CaixaAberto;
            AbrirFecharCaixaButton.Text = FecharCaixaText;
            AbrirFecharCaixaButton.BackColor = Color.DarkSalmon;
            CaixaStateLabel.BackColor = Color.LightGreen;
            //
            QuantidadeInput.Enabled = true;
            CodBarrasInput.Enabled = true;
            //
            QuantidadeInput.Text = "1";
            CodBarrasInput.Focus();
            //
            Modais.MostrarInfo("Caixa Aberto com Sucesso! \nBOAS VENDAS!");
        }
		//
		
		//
		public void FecharCaixa()
		{
			if (RealizandoVenda) { Modais.MostrarAviso("Você Deve (Finalizar ou Cancelar) a Venda Atual Para Poder Fechar o Caixa!"); return; }
			//
			EstaAberto = false;
			CaixaStateLabel.Text = CaixaFechado;
			AbrirFecharCaixaButton.Text = AbrirCaixaText;
			AbrirFecharCaixaButton.BackColor = Color.LightGreen;
			CaixaStateLabel.BackColor = Color.DarkSalmon;
			QuantidadeInput.Enabled = false;
			CodBarrasInput.Enabled = false;
			//
			QuantidadeInput.Text = null;
			//
			//Fazer Janelinha de Fechamento de caixa
			//Modais.MostrarInfo("Caixa Fechado com Sucesso!");
		}
		//----------------------------//
		//			METODOS
		//----------------------------//
		//
		private void ResetarFoco() { CodBarrasInput.Focus(); } // Essa função faz com que o foco do cursor seja o Input de código de barras
		private void ResetarInterface() // função auto explicativa (eu acho)
		{
			//Resetamos os campos Principais que é o valor de pagamento, a lista visual e a lista de produtos;
			PagamentoTotal = 0;
			ListaDeVenda.Items.Clear();
			Produtos.Clear();
			//
			RealizandoVenda = false;
			DescricaoProdutoBox.Text = null;
			PagamentoTotalBox.Text = null;
			CodBarrasInput.Text = null;
			QuantidadeBox.Text = "1";
			//
			QuantidadeInput.Text = "1";
			//
			PrecoUnitarioBox.Text = null;
			SubTotalBox.Text = null;
			//
			CodBarrasInput.Focus();
		}
		//
		private void SetPagamentoTotalBox() // Atualiza o texto do pagamento de maneira padronizada.
		{
			PagamentoTotalBox.Text = $"Total a Pagar R$: {PagamentoTotal}";
		}
		//
		private void AddProduto(Produto produto, out double TotalItem) // Adiciona um produto na lista de venda
		{
			if (!RealizandoVenda) { RealizandoVenda = true; } // assim que o primeiro produto é registrado, setamos que uma venda está sendo realizada
			//
			DescricaoProdutoBox.Text = produto.Descricao; // pegamos a descrição do produto
			//// Geramos o numero apresentado na tela (Começando por 1 já que a maioria não iria entender se começar por 0).
			string Numero = (ListaDeVenda.Items.Count + 1).ToString();
			//
			TotalItem = Math.Round(produto.Quantidade * produto.Preco, 2); ; // geramos o total do item fazendo Quant * valor.
			//Colocamos os valores em suas respectivas colunas
			ListViewItem item = new(
			[   Numero,
				produto.Descricao,
				produto.CodBarras,
				"UN",
				$"R$: {produto.Preco}",
				$"{produto.Quantidade}",
				$"R$: {TotalItem}",
				"N/A"
			]);
			//Adicionamos o item na lista visual
			ListaDeVenda.Items.Add(item);
			item.EnsureVisible();
			//Adicionamos o produto na lista de produtos da venda
			Produtos.Add(produto); 
		}
		/// <summary>
		/// Remove um produto da lista de venda. (Caso esteja sendo realizado alguma venda no momento)
		/// </summary>
		private void RemoverProduto()
		{
			if (!EstaAberto) { return; }
			if (!RealizandoVenda) { Modais.MostrarAviso("Você não está realizando nenhuma venda no momento!"); return; }
			if (ListaDeVenda.SelectedItems.Count < 1) { Modais.MostrarAviso("Selecione um Produto da Lista Primeiro!"); ResetarFoco(); return; }
			//
			ListViewItem item = ListaDeVenda.SelectedItems[0]; // pegamos o item selecionado
			string nomeProduto = item.SubItems[ColunaDescricao.Index].Text; // pegamos o nome do produto

			//Perguntamos se o Usuário quer realmente Realizar a operação
			DialogResult res = Modais.MostrarPergunta($"Deseja Remover o Produto: {nomeProduto}?\nESTA OPERAÇÃO NÃO PODE SER DESFEITA!");

			//Após informado dos riscos seguimos em frente
			if (res == DialogResult.No) { Modais.MostrarInfo("Exclusão do Item Cancelada"); ResetarFoco(); return; }
			//
			int pIndex = item.Index; // pegamos o index do item
			Produtos.RemoveAt(pIndex); // removemos da lista de produtos no index
			ListaDeVenda.Items.Remove(item); // removemos também da lista visual

			// Logo após isso atualizamos as listas e a tela de venda
			double TotalHolder = 0.0;
			foreach (ListViewItem produto in ListaDeVenda.Items)
			{
				produto.SubItems[ColunaItemID.Index].Text = (produto.Index + 1).ToString();
				//
				var p = Produtos[produto.Index];
				//
				TotalHolder += p.Quantidade * p.Preco;
			}
			// Atualizamos o pagamento total
			PagamentoTotal = Math.Round(TotalHolder, 2);
			PagamentoTotalBox.Text = PagamentoTotal.ToString();
			SetPagamentoTotalBox(); // Essa função apenas atualiza o visor de preço pro cliente
			// resetamos o foco novamente para o Input de Cod de Barras
			ResetarFoco();
		}
		//	if (!EstaAberto) { return; } -- Usado para checar se o caixa está aberto, caso contrário, simplesmente cancelamos a execução
		// dos métodos abaixo
		private void Pagamento()
		{
			if (!EstaAberto) { Modais.MostrarAviso("Realize a Abertura do Caixa Primeiro!"); return; }
			if (!RealizandoVenda) { Modais.MostrarAviso("Você não está realizando nenhuma venda no momento!"); return; }

			//AS ETAPAS ACIMA DEVEM SER DESABILITADAS PARA AGILIZAR O PROCESSO DE DEV
			//Mostra a janela de conclusão de venda e atrela o evento de fechamento da janela com a finalização da venda
			JanelaDePagamento janelaDePagamento = LABS_PDV_MAIN.IniciarDependencia<JanelaDePagamento>(app=>
			{
				//Atrelamos o evento para a finalização
				app.FormClosed += JanelaDePagamento_FormClosed;
				app.IniciarTelaDePagamento(PagamentoTotal,Produtos,this);
			});
		}

		private void JanelaDePagamento_FormClosed(object? sender, FormClosedEventArgs e)
		{
			if (sender is JanelaDePagamento app)
			{
				//Desatrelamos o evento para prevenir vazamento de memória e resetamos a interface
				app.FormClosed -= JanelaDePagamento_FormClosed;
				//PARA DEBUG!!!
				//PARA DEBUG!!!
				//PARA DEBUG!!!
				DebugLabel.Text = $"{CaixaLabs.ValorTotal}";
				//PARA DEBUG!!!
				//PARA DEBUG!!!
				//PARA DEBUG!!!
				ResetarInterface();
			}
		}
		//
		private void CancelarVenda()
		{
			if (!EstaAberto) { return; }
			if (!RealizandoVenda) { Modais.MostrarAviso("Você não está realizando nenhuma venda no momento!"); return; }
			//Ao Cancelar a venda, simplesmente descartamos todos os items e resetamos os campos;
			ResetarInterface();
			RealizandoVenda = false;
		}
		//---EVENTOS--//
		//Chamado quando pressiona alguma tecla na tela de PDV
		private void OnPDVKeyUp(object sender, KeyEventArgs e)
		{
			//Usamos SwitchCase por questão de performance (Endereçamento direto)
			switch (e.KeyCode)
			{
				case Keys.F1:
					Pagamento();
					break;
				case Keys.F2:
					CancelarVenda();
					break;
				case Keys.F3:
					RemoverProduto();
					break;
				case Keys.F4:
					QuantidadeInput.Focus();
					break;
			}
		}
		//Chamado quando alguma tecla é pressionada na área de Quantidade
		private void OnQuantidadeKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter) { return; } // se não for enter não seguimos
			string Quantidade = QuantidadeInput.Text;
			//
			if (Utils.TryParseToInt(Quantidade, out int QTD))
			{
				QuantidadeBox.Text = QTD.ToString();//
				QuantidadeInput.Text = "1";
				ResetarFoco();
			}
			else { Modais.MostrarErro("Somente Números!"); }
		}
		//Chamado quando alguma tecla é pressionada na área de cód de barras
		private async void OnAddCodBarras()
		{
			//
			if (QuantidadeBox.TextLength < 1) { QuantidadeBox.Text = "1"; }
			//
			if (Utils.IsValidBarCode(CodBarrasInput.Text))
			{
				Produto produto = await Utils.GetProdutoByCode(CodBarrasInput.Text);
				if (produto != null)
				{
					// só prosseguimos com a adição na lista de venda, caso o produto exista
					// no banco de dados
					//
					// Alteramos a quantidade porque não queremos vender o estoque inteiro de uma vez só KKK
					int QTD = int.Parse(QuantidadeBox.Text);
					if(QTD > produto.Quantidade) { Modais.MostrarAviso("Produto Esgotado!"); return; } // caso a quantidade que estamos passando for maior que a disponível no estoque;
					//
					produto.Quantidade = int.Parse(QuantidadeBox.Text);
					AddProduto(produto, out double TotalItem);
					// Ao Adicionar o produto na lista, limpamos o código de barras e resetamos a quantidade para somente 1 (para evitar de replicar a quantidade anterior);
					CodBarrasInput.Text = null;
					QuantidadeBox.Text = "1";
					//
					PrecoUnitarioBox.Text = $"{produto.Preco}";
					SubTotalBox.Text = TotalItem.ToString();
					//
					PagamentoTotal += TotalItem;
					PagamentoTotal = Math.Round(PagamentoTotal, 2);
					//
					SetPagamentoTotalBox();
				}
				else
				{
					Modais.MostrarAviso("Produto Não Cadastrado no Estoque!");
				}
			}
		}
		private void OnCodBarrasKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter) { return; } // se não for enter não seguimos
			OnAddCodBarras();
			//
		}
		//
		private void PagamentoButton_Click(object sender, EventArgs e)
		{
			Pagamento();
		}
		//
		private void CancelarVendaButton_Click(object sender, EventArgs e)
		{
			CancelarVenda();
		}
		//
		private void ExcluirItemButton_Click(object sender, EventArgs e)
		{
			RemoverProduto();
		}
		//
		private void VoltarButton_Click(object sender, EventArgs e)
		{
			// Bem simples, se o caixa estiver aberto, caso o cliente queira voltar pra tela anterior, ele será mantido rodando
			// Caso contrário simplesmente fechamos para não consumir memória desnecessária;
			if (EstaAberto) { this.Hide(); return; }
			this.Close();
		}

		private void AbrirFecharCaixaButton_Click(object sender, EventArgs e)
		{
			//Fazemos verificação para só abrir o caixa com a senha do usuário atual;
			//Por enquanto deixamos sem para DEV
			if (EstaAberto) { FecharCaixa(); return; }
			if (!EstaAberto) { AbrirCaixa(); return; }
		}
	}
}

using LabsPDV.LABS_PDV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LabsPDV.LABS_PDV.Modelos;

namespace LabsPDV
{
	public partial class RDP : Form
	{
		public RDP()
		{
			InitializeComponent();
			//Atualizamos a lista de produtos quanod inicializado
			UpdateListaProdutos();
		}

		private void UpdateListaProdutos()
		{
			List<Produto> produtos = DataBase.GetProdutos();
			//Verificamos se a lista é valida (Prevenção de erros)
			if(produtos != null)
			{
				ListaProdutos.Items.Clear();
				foreach (Produto produto in produtos)
				{
					ListaProdutos.Items.Add($"{produto.Quantidade} | {produto.Nome} | {produto.Preco} | {produto.CodBarras}");
				}
			}
		}



		private void RegistrarButton_Click(object sender, EventArgs e)
		{
			// Aqui Pegamos as tentativas de conversão para Assegurar
			// que o programa não crashe e evite bugs
			//
			bool isQtd = Utils.TryParseToInt(QuantidadeInput.Text,out int Qtd);
			bool isCod = Utils.TryParseToInt(CodigoInput.Text,out int Cod);
			//
			//O preço é tratado como double já que possui valores decimais
			//
			bool isPreco = Utils.TryParseToDouble(PrecoInput.Text,out double Prec);
			// se todos os requisitos forem cumpridos passamos
			if(isQtd && isPreco && isCod)
			{
				//Preenchemos os dados do produto com base nas informações do input
				//ID é -1 porque ele só é atribuido em chamada.
				Produto produto = new()
				{
					Nome = NomeInput.Text,
					Quantidade = Qtd,
					Preco = Prec,
					CodBarras = Cod
				};
				//Após criarmos o produto chamamos a API para Guardar na Database
				DataBase.RegisterProduto(produto);
				UpdateListaProdutos();
			}
		}
	}
}

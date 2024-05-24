using Labs.Janelas;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labs.LABS_PDV.Modelos;

namespace Labs.LABS_PDV
{
	public class GerenciadorPDV
	{
		/// <summary>
		/// Quantidade Minima de Produtos
		/// </summary>
		/// <param name="QMDP">Valor de quantidade minima</param>
		/// <returns>Retorna Aguardável (Task) </returns>
		public static async Task VerificarEstoque(int QMDP)
		{
            var JDC = LABS_PDV_MAIN_WPF.IniciarApp<JanelaCarregamentoWPF>(true,false,false);
            //
            var PBFDS = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos, _ => true); // Pega todos os produtos
            int ProdutosEmBaixa = 0;
            //
            JDC.SetTextoFrontEnd("VERIFICANDO ESTOQUE");
			await Task.Delay(100);
            JDC.ConfigBarraDeCarregamento(0, PBFDS.Count);
			//
			for (int i = 0; i < PBFDS.Count; i++)
			{
				if (PBFDS[i].Quantidade <= QMDP) { ProdutosEmBaixa++; }
                JDC.SetTextoFrontEnd($"VERIFICANDO ESTOQUE\n({i}/{PBFDS.Count})");
                JDC.AumentarBarraDeCarregamento(1);
				await Task.Delay(1);
            }
            //
            if (ProdutosEmBaixa > 0) { Modais.MostrarAviso("UM OU MAIS PRODUTOS ESTÃO EM BAIXA NO ESTOQUE!"); }
            //
            JDC.Close();
        }

		public static async Task EspelhamentoParaCloud()
		{
			var JDC = LABS_PDV_MAIN.IniciarApp<JanelaCarregamento>(true);
			//Pega tudo que ta na nuvem e espelha para o local.
			var Produtos = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos, _ => true); //Retorna todos os produtos
			JDC.SetTextoFrontEnd("Realizando Espelhamento de Estoque");
			JDC.ConfigBarraDeCarregamento(0,Produtos.Count);
			for (int i = 0; i < Produtos.Count; i++)
			{
				var produto = Produtos[i];
				CloudDataBase.RegisterCloudAsync(Collections.Produtos,produto,Builders<Produto>.Filter.Eq("CodBarras",produto.CodBarras));
				JDC.SetTextoFrontEnd($"Realizando Espelhamento de Estoque\n({i}/{Produtos.Count})");
				//
				JDC.SetarValor(i+1);
				await Task.Delay(1);
			}
			//
			JDC.Close();
		}
	}
}

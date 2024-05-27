using Labs.Janelas;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            JDC.SetTextoFrontEnd("VERIFICANDO ESTOQUE");
            //
            var PBFDS = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos, _ => true); // Pega todos os produtos
            int ProdutosEmBaixa = 0;
            //
			await Task.Delay(100);
            JDC.ConfigBarraDeCarregamento(0, PBFDS.Count);
			//
			for (int i = 0; i < PBFDS.Count; i++)
			{
				if (PBFDS[i].Quantidade <= QMDP) { ProdutosEmBaixa++; }
                JDC.SetTextoFrontEnd($"VERIFICANDO ESTOQUE\n({i}/{PBFDS.Count})");
                JDC.AumentarBarraDeCarregamento(1);
            }
            //
            if (ProdutosEmBaixa > 0) { Modais.MostrarAviso("UM OU MAIS PRODUTOS ESTÃO EM BAIXA NO ESTOQUE!"); }
            //
            JDC.Close();
        }

		public static async Task EspelhamentoParaCloud()
		{
			// Se o cliente não possui plano cloud não é necessário fazer o espelhamento, então retornamos
			if (!LABS_PDV_MAIN_WPF.Cliente.PossuiPlanoCloud){ await Task.Delay(1); return; }
			//
			var JDC = LABS_PDV_MAIN_WPF.IniciarApp<JanelaCarregamentoWPF>(true,false,false);
			JDC.SetTextoFrontEnd("Realizando Espelhamento de Estoque");
			await Task.Delay(1000); // Aguarda 1 seg
			//
			var ProdutosLocais = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos, _ => true); //Retorna todos os produtos locais
			var ProdutosCloud = await CloudDataBase.GetManyCloudAsync<Produto>(Collections.Produtos, _ => true);
			//
			JDC.SetTextoFrontEnd("Iniciando Processo de Comparação");
			await Task.Delay(1000); // Aguarda 1 seg
			JDC.SetTextoFrontEnd("Realizando Processo de Comparação");
			//
			JDC.ConfigBarraDeCarregamento(0,ProdutosLocais.Count);
			//
			List<Produto> produtosEspelhados = [];
			List<Produto> produtosParaEspelhar = [];
			//
			foreach (var pl in ProdutosLocais)
			{
				JDC.SetTextoFrontEnd($"Comparando Produto\n({pl.ID})");
				if (ProdutosCloud.Find(xC => //Operação Lambda para pesquisa determinante
                                             // Realizamos todos os comparativos já que precisamos que o espelhamento dos produtos sejam perfeitos
											 //Futuramente esse espelhamento será realizado de forma mais profunda (quando tivermos comunicação com a receita)
                (xC.CodBarras == pl.CodBarras && xC.Quantidade == pl.Quantidade && xC.Descricao == pl.Descricao && xC.Preco == pl.Preco)) != default) 
				{ 
					produtosEspelhados.Add(pl);
				}
				else { produtosParaEspelhar.Add(pl); }
				//
				JDC.AumentarBarraDeCarregamento(1); // aumenta a barra em 1 unidade
				//
				await Task.Delay(0);
			}
			if(produtosParaEspelhar.Count <= 0)
			{
				JDC.SetTextoFrontEnd($"Processo de Comparação Finalizado.\nFechando Automaticamente em 3 Seg...");
				await Task.Delay(3000);
				JDC.Close();
				return;
			}
			JDC.SetTextoFrontEnd($"Processo de Comparação Finalizado.\n{produtosParaEspelhar.Count} Itens de {ProdutosLocais.Count} Itens, Não Estão Espelhados.");
			await Task.Delay(3000);
			var r = Modais.MostrarPergunta($"Atenção! ({produtosParaEspelhar.Count} Itens de {ProdutosLocais.Count} Itens Não Estão Espelhados)\n" +
				$"Deseja Realizar o Espelhamento Agora? \nLeve em consideração que o tempo de espelhamento vai depender exclusivamente da velocidade de sua conexão!");
			//
			if(r == MessageBoxResult.No) { Modais.MostrarInfo("Proceso de Espelhamento Cancelado com Sucesso!"); JDC.Close(); return; }
			//
			JDC.SetTextoFrontEnd($"Iniciando Processo de Espelhamento\n (Isso Pode Demorar um Pouco)");
			JDC.ConfigBarraDeCarregamento(0,produtosParaEspelhar.Count);
			JDC.SetarValor(0);
			//
			await Task.Delay(3000);
			//
			for (int i = 0; i < produtosParaEspelhar.Count; i++)
			{
                JDC.SetTextoFrontEnd($"Espelhando Itens\n({i}/{produtosParaEspelhar.Count})");
                await CloudDataBase.RegisterCloudAsync(Collections.Produtos, produtosParaEspelhar[i], Builders<Produto>.Filter.Eq("CodBarras", produtosParaEspelhar[i].CodBarras));
                JDC.SetarValor(i+1);
			}
			JDC.SetTextoFrontEnd($"Processo de Espelhamento Finalizado!\nFechando Automaticamente em 3 Seg...");
			//
			await Task.Delay(3000);
			//
			JDC.Close();
		}
	}
}

using Labs.Janelas;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Labs.LABS_PDV.Modelos;

namespace Labs.LABS_PDV
{
	public class GerenciadorPDV
	{
		public static async Task Terminate(JanelaCarregamentoWPF JDC)
		{
            JDC.SetTextoFrontEnd("Fechando em 3 Seg...");
			await Task.Delay(3000);
			JDC.Close();
		}
		public static JanelaCarregamentoWPF Initiate(string text)
		{
			var JDC = LabsMain.IniciarDependencia<JanelaCarregamentoWPF>(x => { x.SetTextoFrontEnd(text); },true,false);
			return JDC;
		}
		/// <summary>
		/// Quantidade Minima de Produtos
		/// </summary>
		/// <param name="QMDP">Valor de quantidade minima</param>
		/// <returns>Retorna Aguardável (Task) </returns>
		/// 
		public static async Task VerificarEstoque(int QMDP,JanelaCarregamentoWPF JDC)
		{
			await Task.Delay(3000);
            //
            var PBFDS = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos, _ => true); // Pega todos os produtos
            int ProdutosEmBaixa = 0;
			//
			foreach (var produto in PBFDS)
			{
				produto.Status = "OK";
				//
				if (produto.Quantidade <= QMDP) { produto.Status = "Produto em Baixa"; ProdutosEmBaixa++; }
                //
				await Task.Delay(1);
            }
            //
            if (ProdutosEmBaixa > 0) { Modais.MostrarAviso("UM OU MAIS PRODUTOS ESTÃO EM BAIXA NO ESTOQUE!"); }
			//
			JDC.SetTextoFrontEnd("Comparando Produtos...");
			JDC.ConfigBarraDeCarregamento(0, PBFDS.Count);
			await Task.Delay(3000); // aguarda 3s
			foreach (var produto in PBFDS)
			{
				await CloudDataBase.RegisterLocalAsync(Collections.Produtos,produto,Builders<Produto>.Filter.Eq("ID",produto.ID));
				JDC.AumentarBarraDeCarregamento(1);
			}
			JDC.SetTextoFrontEnd("Verificação Concluída");
			await Task.Delay(3000);
		}
		/// <summary>
		/// Busca os Produtos No Estoque em Nuvem e Replica para o servidor Local
		/// </summary>
		/// <returns>Aguardável Task</returns>
		public static async Task BuscarEstoqueRemoto(JanelaCarregamentoWPF JDC)
		{
			if (!LabsMain.Cliente.PossuiPlanoCloud) { await Task.Delay(1); return; }
			//
			await Task.Delay(1000);
			//
			var ProdutosLocais = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos,_ => true);
			var ProdutosCloud = await CloudDataBase.GetManyCloudAsync<Produto>(Collections.Produtos,_ => true);
			//
			List<Produto> produtosEspelhados = [];
			List<Produto> produtosParaEspelhar = [];
			//
			JDC.SetTextoFrontEnd("Comparando busca com Estoque...");
			await Task.Delay(3000);
			//
			produtosEspelhados = ProdutosLocais.Except(ProdutosCloud).ToList();
			// Pega os produtos que estão no local, mas não na nuvem

			produtosParaEspelhar = ProdutosCloud.Except(ProdutosLocais).ToList();
			// Pega os produtos que estão na nuvem, mas não no local
			//
			//
			if (produtosParaEspelhar.Count <= 0 && ProdutosLocais.Count > 0)
			{
				await Task.Delay(1000);
				return;
			}
			else
			{
				JDC.SetTextoFrontEnd("Busca Finalizada.\nIniciando Processo de Registro...");
				JDC.ConfigBarraDeCarregamento(0,produtosParaEspelhar.Count);
				await Task.Delay(3000);
				for (int i = 0; i < produtosParaEspelhar.Count; i++)
				{
					JDC.SetTextoFrontEnd($"Registrando Produtos...\n{i}/{produtosParaEspelhar.Count}");
					await CloudDataBase.RegisterLocalAsync(Collections.Produtos, produtosParaEspelhar[i], Builders<Produto>.Filter.Eq("ID", produtosParaEspelhar[i].ID));
					JDC.AumentarBarraDeCarregamento(1);
				}
				JDC.SetTextoFrontEnd("Registro Finalizado.");
				await Task.Delay(1000);
			}
		}

		/// <summary>
		/// Realiza o espelhamento do estoque local para o cloud
		/// </summary>
		/// <returns>Agardável Cloud</returns>
		public static async Task EspelhamentoParaCloud(JanelaCarregamentoWPF JDC)
		{
			// Se o cliente não possui plano cloud não é necessário fazer o espelhamento, então retornamos
			if (!LabsMain.Cliente.PossuiPlanoCloud){ await Task.Delay(1); return; }
			//
			await Task.Delay(1000);
			//
			var ProdutosLocais = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos, _ => true); //Retorna todos os produtos locais
			var ProdutosCloud = await CloudDataBase.GetManyCloudAsync<Produto>(Collections.Produtos, _ => true); //Retorna todos os produtos cloud
			//
			List<Produto> produtosEspelhados = [];
			//
			List<Produto> produtosParaEspelhar = [];
			//
			JDC.SetTextoFrontEnd("Comparando\n (Local => Nuvem)");
			await Task.Delay(3000); // aguarda 3s
			//
            produtosEspelhados = ProdutosCloud.Except(ProdutosLocais).ToList(); // Pega os produtos que já estão no cloud
            //
            produtosParaEspelhar = ProdutosLocais.Except(ProdutosCloud).ToList(); // Pega os produtos que não estão no cloud
			// após a comparação seguimos para a verificação
			if (produtosParaEspelhar.Count <= 0 && ProdutosCloud.Count > 0)
			{
				await Task.Delay(1000);
				return;
			}
			else
			{
				//
				await Task.Delay(3000);
				var r = Modais.MostrarPergunta($"Atenção! ({produtosParaEspelhar.Count} Itens de {ProdutosLocais.Count} Itens Não Estão Espelhados)\n" +
					$"Deseja Realizar o Espelhamento Agora? \nLeve em consideração que o tempo de espelhamento vai depender exclusivamente da velocidade de sua conexão!");
				//
				if (r == MessageBoxResult.No) 
				{
					Modais.MostrarInfo("Proceso de Espelhamento Cancelado com Sucesso!"); 
				}
				JDC.SetTextoFrontEnd("Iniciando Processo de Espelhamento...");
				JDC.ConfigBarraDeCarregamento(0, produtosParaEspelhar.Count);
				await Task.Delay(3000);
				//
				for (int i = 0; i < produtosParaEspelhar.Count; i++)
				{
					JDC.SetTextoFrontEnd($"Espelhando Itens...\n{i}/{produtosParaEspelhar.Count}");
					await CloudDataBase.RegisterCloudAsync(Collections.Produtos, produtosParaEspelhar[i], Builders<Produto>.Filter.Eq("ID", produtosParaEspelhar[i].ID));
					JDC.AumentarBarraDeCarregamento(1);
				}
				JDC.SetTextoFrontEnd("Espelhamento Finalizado.");
				await Task.Delay(3000);
			}
		}
	}
}

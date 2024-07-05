using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimake.Business.DFe.Servicos;

namespace Labs.Main.ReceitaFederal.Utilitarios
{
	internal class UtilitariosInternos
	{
		//Funções Utilitárias Internas para Cálculos de Modos de Operação de cada ICMS



		/// <summary>
		/// Retorna o Valor Referente ao modo de base de cálculo de ICMS
		/// </summary>
		/// <param name="PMVAST">Porcentagem do Valor Agregado ST</param>
		/// <param name="PrecoDeVendaDoProduto">Valor do Produto para base de cálculo</param>
		/// <param name="PrecoTabelado">Valor do Produto Tabelado para cálculo usado (pauta e valor máximo)</param>
		/// <returns>Retorna o Valor de Base De Cálculo i(0) e o Valor de ICMS i(1)</returns>
		public static double[] GetMBCICMS(ModalidadeBaseCalculoICMS Modalidade,double PICMS, double PMVAST, double PrecoTabelado, double PrecoDeVendaDoProduto)
		{
			// Inicio do Método
			double vBC = PrecoDeVendaDoProduto;
			double vICMS = 0;
			switch (Modalidade)
			{
				case ModalidadeBaseCalculoICMS.MargemValorAgregado:
					// Calculando a Base de Cálculo do ICMS Margem Valor Agregado
					vBC = Math.Round(PrecoDeVendaDoProduto * (1 + (PMVAST * 0.01)), 2);
					// Calculando o valor do ICMS
					vICMS = Math.Round(vBC * (PICMS * 0.01), 2);
					return [vBC, vICMS];
				case ModalidadeBaseCalculoICMS.Pauta:
					vBC = PrecoTabelado;
					vICMS = Math.Round(vBC * (PICMS * 0.01), 2);
					return [vBC, vICMS];
				case ModalidadeBaseCalculoICMS.PrecoTabeladoMaximo:
					vBC = PrecoTabelado;
					vICMS = Math.Round(vBC * (PICMS * 0.01), 2);
					return [vBC, vICMS];
				case ModalidadeBaseCalculoICMS.ValorOperacao:
					vICMS = Math.Round(vBC * (PICMS * 0.01), 2);
					return [vBC, vICMS];
			}
			return [0, 0];
		}




		/// <summary>
		/// Retorna o Valor Referente ao modo de base de cálculo de ICMS ST
		/// </summary>
		/// <param name="PICMSST">Porcentagem do ICMS ST</param>
		/// <param name="PMVAST">Porcentagem do Valor Agregado ST</param>
		/// <param name="PRedBCST">Porcentagem de Redução</param>
		/// <param name="PrecoDeVendaDoProduto">Valor do Produto para base de cálculo</param>
		/// <param name="PrecoTabelado">Valor do Produto Tabelado para cálculo usado (pauta e valor máximo)</param>
		/// <returns>Retorna o Valor de Base De Cálculo ST i(0) e o Valor de ICMSST i(1)</returns>
		public static double[] GetMBCICMSST(ModalidadeBaseCalculoICMSST ModalidadeDeCalculoST,
			double PICMSST = 0,
			double PMVAST = 0,
			double PRedBCST = 0,
			double PrecoDeVendaDoProduto = 0,
			double PrecoTabelado = 0)
		{
			// Inicio do Método
			double vBCSTAntesReducao = 0;
			double vBCST = PrecoDeVendaDoProduto;
			double vICMSST = 0;
			switch (ModalidadeDeCalculoST)
			{
				case ModalidadeBaseCalculoICMSST.MargemValorAgregado:
					// Calculando a Base de Cálculo do ICMS ST antes da redução
					vBCSTAntesReducao = Math.Round(PrecoDeVendaDoProduto * (1 + (PMVAST * 0.01)), 2);
					// Aplicando a redução da base de cálculo
					vBCST = Math.Round(vBCSTAntesReducao * (1 - (PRedBCST * 0.01)), 2);
					// Calculando o valor do ICMS ST
					vICMSST = Math.Round(vBCST * (PICMSST * 0.01), 2);
					return [vBCST, vICMSST];
				case ModalidadeBaseCalculoICMSST.PrecoTabeladoMaximoSugerido:
					vBCST = PrecoTabelado;
					vICMSST = Math.Round(vBCST * (PICMSST * 0.01), 2);
					return [vBCST, vICMSST];
				case ModalidadeBaseCalculoICMSST.Pauta:
					vBCST = PrecoTabelado;
					vICMSST = Math.Round(vBCST * (PICMSST * 0.01), 2);
					return [vBCST, vICMSST];
				case ModalidadeBaseCalculoICMSST.ValorOperacao:
					vBCST = Math.Round(PrecoDeVendaDoProduto * (1 - (PRedBCST * 0.01)), 2);
					vICMSST = Math.Round(vBCST * (PICMSST * 0.01), 2);
					return [vBCST, vICMSST];
				case ModalidadeBaseCalculoICMSST.ListaNeutra:
					vBCST = Math.Round(PrecoDeVendaDoProduto * (1 - (PRedBCST * 0.01)), 2);
					vICMSST = Math.Round(vBCST * (PICMSST * 0.01), 2);
					return [vBCST, vICMSST];
				case ModalidadeBaseCalculoICMSST.ListaPositiva: // Geralmente há um incremento da aliquota do ICMSST. mas a base de cálculo é a mesma
					vBCST = Math.Round(PrecoDeVendaDoProduto * (1 - (PRedBCST * 0.01)), 2);
					vICMSST = Math.Round(vBCST * (PICMSST * 0.01), 2);
					return [vBCST, vICMSST];
				case ModalidadeBaseCalculoICMSST.ListaNegativa: // Geralmente a Porentagem da aliquota do ICMSST é reduzida
					vBCST = Math.Round(PrecoDeVendaDoProduto * (1 - (PRedBCST * 0.01)), 2);
					vICMSST = Math.Round(vBCST * (PICMSST * 0.01), 2);
					return [vBCST, vICMSST];
			}
			return [0, 0];
		}
	}
}

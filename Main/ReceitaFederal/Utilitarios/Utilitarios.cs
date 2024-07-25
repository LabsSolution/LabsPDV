using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labs.Main;
using Unimake.Business.DFe.Servicos;
using Unimake.Business.DFe.Xml.NFe;

namespace Labs.Main.ReceitaFederal.Utilitarios
{
	public class Utilitarios
	{
		/// <summary>
		/// Essa função retorna o Tipo de ICMS refetente ao CST do produto, listado como 'A''B''B'
		/// </summary>
		/// <param name="CodCST"> Código CST do Produto, Ele Determina o tipo de ICMS a ser Pago</param>
		/// <param name="VBCOUT"> Valor de Base de Cálculo OUT</param>
		/// <param name="VFCPOUT"> Valor de Fundo de Combate a Pobreza OUT</param>
		/// <param name="VBCSTOUT"> Valor de Base de Cálculo ST OUT</param>
		/// <param name="VICMSOUT"> Valor do ICMS OUT</param>
		/// <param name="VICMSSTOUT"> Valor do ICMS ST OUT</param>
		/// <param name="VICMSDeson"> Valor do ICMS Desonerado</param>
		/// <param name="PICMS"> Porcentagem da Aliquota Integral do ICMS</param>
		/// <param name="PrecoDeVendaDoProduto">Preço de Venda do produto para os cálculos a serem realizados</param>
		/// <param name="ModalidadeDeCalculo">Modalidade De Cálculo de ICMS (Ver Função Para descrição de cada modo)</param>
		/// <param name="ModalidadeDeCalculoST">Modalidade De Cálculo de ICMS ST </param>
		/// <param name="MotivoDeson"> Motivo de Desoneração do ICMS </param>
		/// <param name="PFCP">Porcentagem da Aliquota Integral do FCP (Se Aplicável)</param>
		/// <param name="PICMSST">Porcentagem da Aliquota Integral do ICMS ST</param>
		/// <param name="PMVAST">Porcentagem da Aliquota de Margem de valor Agregado do ICMS ST</param>
		/// <param name="PRedBCST">Porcentagem da Aliquota de Redução de Base de Cálculo ST</param>
		/// <param name="PRedBC">Porcentagem da Aliquota de Redução de Base de Cálculo</param>
		/// <param name="PICMSDif">Porcentagem de Diferimento do ICMS (pDif ICMS 51)</param>
		/// <param name="PCredSN">Porcentagem da Aliquita de Crédito do ICMS 101,201,900</param>
		/// <returns>Retorna um Objeto de ICMS Respectivo aos valores Fornecidos</returns>
		public static ICMS GetICMS(string CodCST,
			out double VBCOUT,
			out double VFCPOUT,
			out double VBCSTOUT,
			out double VICMSOUT,
			out double VICMSSTOUT,
			double VICMSDeson,
			double PICMS,
			double PrecoDeVendaDoProduto,
			ModalidadeBaseCalculoICMS ModalidadeDeCalculo,
			ModalidadeBaseCalculoICMSST ModalidadeDeCalculoST,
			MotivoDesoneracaoICMS MotivoDeson,
			double PFCP = 0,
			double PICMSST = 0,
			double PMVAST = 0,
			double PRedBCST = 0,
			double PRedBC = 0,
			double PICMSDif = 0,
			double PCredSN= 0)
		{
			// Atribuição padrão para agregação posterior
			VBCOUT = 0; VFCPOUT = 0; VBCSTOUT = 0; VICMSOUT = 0; VICMSSTOUT = 0;
			// Atribuição padrão para agregação posterior
			//
			if (CodCST.Length > 4) { Modais.MostrarAviso("O Código CST Informado Tem Mais de 4 Dígitos"); return null!; }
			//
			//ModalidadeBaseCalculoICMS.MargemValorAgregado, // Usado em ICMSST
			//ModalidadeBaseCalculoICMS.PrecoTabeladoMaximo, // Usado quando o preço é tabulado, geralmente Combustível e Medicamento
			//ModalidadeBaseCalculoICMS.Pauta, // Geralmente Utilizado em produtos com grande variação de preço, como Fumo e Bebidas Alcoólicas
			//ModalidadeBaseCalculoICMS.ValorOperacao, // Normal, Utilizado na maioria das operações de venda.
			// O Cód CST pode ter no máximo 4 digitos
			OrigemMercadoria Origem = OrigemMercadoria.Nacional;
			// Repassamos o primeiro Digito Para pegar a Origem da Mercadoria.
			if (Utils.TryParseToInt($"{CodCST[0]}",out int dOrig)) { Origem = (OrigemMercadoria)dOrig; }
			// Se tiver 3 digitos é icms Regime Normal (Fora do Simples Nacional)
			if(CodCST.Length == 3)
			{
				string codICMS = $"{CodCST[1]}{CodCST[2]}";
				//
				double[] ValoresICMS = UtilitariosInternos.GetMBCICMS(ModalidadeDeCalculo,PICMS,PMVAST,0,PrecoDeVendaDoProduto);
				double[] ValoresICMSST = UtilitariosInternos.GetMBCICMSST(ModalidadeDeCalculoST, PICMSST, PMVAST, PRedBCST, PrecoDeVendaDoProduto,0);
				VBCSTOUT = ValoresICMSST[0]; VICMSSTOUT = ValoresICMSST[1]; // Atribuimos o cálculo prévio antes dos retornos abaixo //
				// Prevenção de Atribuição Errônea para os Seguintes Códigos
				var t = codICMS;
				if (t == "00" || t == "20" || t == "40" || t == "41" || t == "50" || t == "51" || t == "60" || t == "90") { VBCSTOUT = 0; VICMSSTOUT = 0; }
				//
				switch (codICMS)
				{
					case "00":
						var ICMS00 = new ICMS
						{
							ICMS00 = new ICMS00
							{
								Orig = Origem,
								ModBC = ModalidadeDeCalculo,
								VBC = ValoresICMS[0], // Valor total do produto
															 //Cálculo ICMS
								PICMS = PICMS, // Aliquota do icms
								VICMS = ValoresICMS[1], //Valor do icms = vProd * AliqIcms.
																							   //Cálculo FCP (Se Aplicável)
								PFCP = PFCP,
								VFCP = Math.Round(PrecoDeVendaDoProduto * (PFCP * 0.01), 2), // Valor do FCP = vProd * AliqFCP.
							}
						};
						VBCOUT = ICMS00.ICMS00.VBC;
						VFCPOUT = ICMS00.ICMS00.VFCP;
						VICMSOUT = ICMS00.ICMS00.VICMS;
						return ICMS00;
					case "10":
						var ICMS10 = new ICMS
						{
							ICMS10 = new ICMS10
							{
								Orig = Origem,
								ModBC = ModalidadeDeCalculo,
								//Valor Bruto do Produto Para Base de Cálculo
								VBC = ValoresICMS[0],
								//Cálculo ICMS
								PICMS = PICMS,
								VICMS = ValoresICMS[1], //Valor do ICMS = vProd * AliqIcms
																							   //Calculo FCP (Se Aplicável)
								PFCP = PFCP,
								VFCP = Math.Round(PrecoDeVendaDoProduto * (PFCP * 0.01), 2),
								//Cálculo ICMSST
								ModBCST = ModalidadeDeCalculoST, // Modalidade de determinação da base de cálculo do ICMS ST
								PMVAST = PMVAST == 0? null : PMVAST, // Percentual da margem de valor agregado ICMS ST (Preenchido Pelo Lojista)
								PRedBCST = PRedBCST, // Percentual de redução da base de cálculo ICMS ST (Preenchido Pelo Lojista)
								PICMSST = PICMSST, // Alíquota do ICMS ST (Preenchido Pelo Lojista)
								VBCST = ValoresICMSST[0], // Base de cálculo do ICMS ST (Retorno Auto)
								VICMSST = ValoresICMSST[1], // Valor do ICMS ST (Retorno Auto)
							}
						};
						VBCOUT = ICMS10.ICMS10.VBC;
						VFCPOUT = ICMS10.ICMS10.VFCP;
						VICMSOUT = ICMS10.ICMS10.VICMS;
						return ICMS10; 
					case "20":
						var ICMS20 = new ICMS
						{
							ICMS20 = new ICMS20
							{
								Orig = Origem,
								ModBC = ModalidadeDeCalculo,
								//Valor Bruto do Produto Para Base de Cálculo
								//Calculo FCP (Se Aplicável)
								PFCP = PFCP,
								VFCP = Math.Round(PrecoDeVendaDoProduto * (PFCP * 0.01), 2),
								//Cálculo ICMS
								PRedBC = PRedBC,
								PICMS = PICMS,
								//Esse é o unico com cálculo próprio
								VBC = Math.Round(PrecoDeVendaDoProduto * (1 - (PRedBC * 0.01)), 2), // Base de Cálculo do ICMS Reduzido
								VICMS = Math.Round(Math.Round(PrecoDeVendaDoProduto * (1 - (PRedBC * 0.01)), 2) * (PICMS * 0.01), 2), //Valor do ICMS Red = vProdRed * AliqIcms
								//
								MotDesICMS = MotivoDeson,
								VICMSDeson = VICMSDeson,
							}
						};
						VBCOUT = ICMS20.ICMS20.VBC;
						VFCPOUT = ICMS20.ICMS20.VFCP;
						VICMSOUT = ICMS20.ICMS20.VICMS;
						return ICMS20;
					case "30":
						var ICMS30 = new ICMS
						{
							ICMS30 = new ICMS30
							{
								Orig = Origem,
								ModBCST = ModalidadeDeCalculoST,
								PMVAST = PMVAST == 0 ? null : PMVAST,
								PRedBCST = PRedBCST,
								PICMSST = PICMSST,
								VBCST = ValoresICMSST[0],
								VICMSST = ValoresICMSST[1],
							}
						};
						return ICMS30;
					case "40": // Isento
						return new ICMS
						{
							ICMS40 = new ICMS40
							{
								Orig = Origem,
								CST = "40",
								VICMSDeson = VICMSDeson,
								MotDesICMS = MotivoDeson,
							}
						};
					case "41":
						return new ICMS
						{
							ICMS40 = new ICMS40
							{
								Orig = Origem,
								CST = "41",
							}
						};
					case "50":
						return new ICMS
						{
							ICMS40 = new ICMS40
							{
								Orig = Origem,
								CST = "50",
							}
						};
					case "51":
						var ICMS51 = new ICMS
						{
							ICMS51 = new ICMS51
							{
								Orig = Origem,
								ModBC = ModalidadeDeCalculo,
								//
								VBC = ValoresICMS[0],
								PICMS = PICMS,
								VICMSOp = ValoresICMS[1], // VBC * pICMS
								PDif = PICMSDif,
								VICMSDif = Math.Round(ValoresICMS[1] * (PICMSDif * 0.01), 2), // vICMSOP * pDif
							}
						};
						VBCOUT = (double)ICMS51.ICMS51.VBC;
						VICMSOUT = (double)ICMS51.ICMS51.VICMSOp;
						return ICMS51; 
					case "60":
						return new ICMS
						{
							ICMS60 = new ICMS60 //  Usado quando a venda for feita para um consumidor final, ou se a empresa compradora for do simples nacional
							{
								Orig = Origem,
							}
						};
					case "70":
						var ICMS70 = new ICMS
						{
							ICMS70 = new ICMS70
							{
								Orig = Origem,
								ModBC = ModalidadeDeCalculo,
								PRedBC = PRedBC,
								VBC = ValoresICMS[0],
								PICMS = PICMS,
								VICMS = ValoresICMS[1], // VBC * PICMS
								ModBCST = ModalidadeDeCalculoST,
								PMVAST = PMVAST == 0 ? null : PMVAST,
								PICMSST = PICMSST,
								VBCST = ValoresICMSST[0],
								VICMSST = ValoresICMSST[1],
								VICMSDeson = VICMSDeson,
								MotDesICMS = MotivoDeson,
							}
						};
						VBCOUT = ICMS70.ICMS70.VBC;
						VICMSOUT = ICMS70.ICMS70.VICMS;
						return ICMS70;
					case "90":
						return new ICMS
						{
							ICMS90 = new ICMS90
							{
								Orig = Origem,
							}
						};
				}
			}
			// Se tiver 4 digitos é icms Regine Simples Nacional
			if(CodCST.Length == 4)
			{
				string codICMS = $"{CodCST[1]}{CodCST[2]}{CodCST[3]}";

				//
				double[] ValoresICMS = UtilitariosInternos.GetMBCICMS(ModalidadeDeCalculo, PICMS, PMVAST, 0, PrecoDeVendaDoProduto);
				double[] ValoresICMSST = UtilitariosInternos.GetMBCICMSST(ModalidadeDeCalculoST, PICMSST, PMVAST, PRedBCST, PrecoDeVendaDoProduto);
				VBCSTOUT = ValoresICMSST[0]; VICMSSTOUT = ValoresICMSST[1]; // Atribuimos o cálculo prévio antes dos retornos abaixo //
				var t = codICMS;
				if(t == "101" || t == "102" || t == "103" || t == "300" || t == "400" || t == "500" || t == "900") { VBCSTOUT = 0; VICMSSTOUT = 0; }
				//
				switch (codICMS)
				{
					case "101":
						return new ICMS
						{
							ICMSSN101 = new ICMSSN101
							{
								Orig = Origem,
								PCredSN = PCredSN,
								VCredICMSSN = Math.Round(PrecoDeVendaDoProduto * (PCredSN * 0.01),2),
							}
						};
					case "102":
						return new ICMS
						{
							ICMSSN102 =  new ICMSSN102
							{
								Orig = Origem,
								CSOSN = "102",
							}
						};
					case "103":
						return new ICMS
						{
							ICMSSN102 = new ICMSSN102
							{
								Orig = Origem,
								CSOSN = "103",
							}
						};
					case "201":
						var ICMSSN201 = new ICMS
						{
							ICMSSN201 = new ICMSSN201
							{
								Orig = Origem,
								CSOSN = "201",
								ModBCST = ModalidadeDeCalculoST,
								PMVAST = PMVAST == 0? null : PMVAST,
								PRedBCST = PRedBCST == 0? null : PRedBCST,
								VBCST = ValoresICMSST[0],
								PICMSST = PICMSST,
								VICMSST = ValoresICMSST[1],
								PCredSN = PCredSN == 0? 0 : PCredSN, // setamos como 0 se for true pq n dá pra multiplicar Numero por Null
								VCredICMSSN = Math.Round(PrecoDeVendaDoProduto * (PCredSN * 0.01), 2),
							}
						};
						return ICMSSN201; 
					case "202":
						return new ICMS
						{
							ICMSSN202 = new ICMSSN202
							{
								Orig = Origem,
								CSOSN = "202",
								ModBCST = ModalidadeDeCalculoST,
								PMVAST = PMVAST == 0? null : PMVAST,
								PRedBCST = PRedBCST == 0? null : PRedBCST,
								VBCST = ValoresICMSST[0],
								PICMSST = PICMSST,
								VICMSST = ValoresICMSST[1],
							}
						};
					case "203":
						return new ICMS
						{
							ICMSSN202 = new ICMSSN202
							{
								Orig = Origem,
								CSOSN = "203",
								ModBCST = ModalidadeDeCalculoST,
								PMVAST = PMVAST == 0 ? null : PMVAST,
								PRedBCST = PRedBCST == 0 ? null : PRedBCST,
								VBCST = ValoresICMSST[0],
								PICMSST = PICMSST,
								VICMSST = ValoresICMSST[1],
							}
						};
					case "300":
						return new ICMS
						{
							ICMSSN102 = new ICMSSN102
							{
								Orig = Origem,
								CSOSN = "300",
							}
						};
					case "400":
						return new ICMS
						{
							ICMSSN102 = new ICMSSN102
							{
								Orig = Origem,
								CSOSN = "400",
							}
						};
					case "500":
						return new ICMS
						{
							ICMSSN500 = new ICMSSN500
							{
								Orig = Origem,
								CSOSN = "500",
							}
						};
					case "900":
						return new ICMS
						{
							ICMSSN900 = new ICMSSN900
							{
								//Aparentemente esse campo é permitido omitir tudo.
								Orig = Origem,
								CSOSN = "900",
							}
						};
				}
			}
			return null!;
		}
		//
		//Retorna o pagamento com os campos preenchidos de acordo com o tipo mencionado
		public static DetPag GetDetPag(MeioPagamento Meio, double ValorPagamento = 0, bool ComParcelas = false)
		{
			switch (Meio)
			{
				case MeioPagamento.Dinheiro:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento
					};
				case MeioPagamento.Cheque:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento
					};
				case MeioPagamento.CartaoCredito:
					return new DetPag
					{
						TPag = Meio,
						// CNPJPag -- CNPJ que representa a empresa TEF que está sendo a intermediadora.
						// UFPag -- Determina o estado em que a empresa TEF reside.
						IndPag = ComParcelas? IndicadorPagamento.PagamentoPrazo : IndicadorPagamento.PagamentoVista, // Aparentemente só serve pra informar se o valor passado foi parcelado ou não.
						DPag = DateTime.Now, // Data em que o pagamento foi realizado
						VPag = ValorPagamento,
						Card = new Card
						{
							// CAut -- Código de Autorização de Pagamento
							// CNPJ -- CNPJ da Credenciadora TEF
							IdTermPag = "0", // Id do terminal em que foi realizado o pagamento (Maquininha)
							CNPJReceb = "54781393000147", //-- CNPJ da empresa que está recebendo o pagamento. (Obrigatório)
							TpIntegra = TipoIntegracaoPagamento.PagamentoNaoIntegrado,
						}
					};
				case MeioPagamento.CartaoDebito:
					return new DetPag
					{
						TPag = Meio,
						// CNPJPag -- CNPJ que representa a empresa TEF que está sendo a intermediadora.
						// UFPag -- Determina o estado em que a empresa TEF reside.
						IndPag = IndicadorPagamento.PagamentoVista, // Deixa sempre como pagamento à vista já que débito n tem como parcelar kkk
						DPag = DateTime.Now, // Data em que o pagamento foi realizado
						VPag = ValorPagamento,
						Card = new Card
						{
							// CAut -- Código de Autorização de Pagamento
							// CNPJ -- CNPJ da Credenciadora TEF
							IdTermPag = "0", // Id do terminal em que foi realizado o pagamento (Maquininha)
							CNPJReceb = "54781393000147", // CNPJ da empresa que está recebendo o pagamento. (Obrigatório)
							TpIntegra = TipoIntegracaoPagamento.PagamentoNaoIntegrado,
						}
					};
				case MeioPagamento.CreditoLoja:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
					};
				case MeioPagamento.ValeAlimentacao:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
					};
				case MeioPagamento.ValeRefeicao:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
					};
				case MeioPagamento.ValePresente:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
					};
				case MeioPagamento.ValeCombustivel: // Não tem (Não por enquanto) (Mas deixamos habilitado porque... sim)
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
					};
				case MeioPagamento.DuplicataMercantil: // Não tem (Desabilitado pela SEFAZ)
					return null!;
				case MeioPagamento.BoletoBancario:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
					};
				case MeioPagamento.DepositoBancario:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
					};
				case MeioPagamento.PagamentoInstantaneo: // Pix
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
						DPag = DateTime.Now,
						Card = new Card
						{
							CNPJReceb = "54781393000147", // CNPJ de quem está recebendo o pagamento
							TpIntegra = TipoIntegracaoPagamento.PagamentoNaoIntegrado
						}
					};
				case MeioPagamento.TransferenciaBancaria:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
					};
				case MeioPagamento.ProgramaFidelidade:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
					};
				case MeioPagamento.PagamentoInstantaneoEstatico: // Por algum motivo não requer o CNPJ que recebeu o valor ?????
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
						DPag = DateTime.Now
					};
					
				case MeioPagamento.CreditoEmLoja:
					return new DetPag
					{
						TPag = Meio,
						VPag = ValorPagamento,
						DPag = DateTime.Now
					};
				case MeioPagamento.PagamentoEletronicoNaoInformado: // Não sei qual seria o motivo de utilizar esse campo, já que se o equipamento estiver com problema, o lojista vai usar outro meio...
					return null!;
				case MeioPagamento.SemPagamento: // ?? Pra que serve isso se não passa a nota
					return new DetPag 
					{ 
						DPag = DateTime.Now,
					};
				case MeioPagamento.Outros: // Aqui colocamos o meio customizado pelo lojista (Por enquanto fica desabilitado.)
					return null!;
			}
			return null!;
		}
	}
}

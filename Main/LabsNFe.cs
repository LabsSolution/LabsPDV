using Labs.Main.ReceitaFederal;
using Labs.Main.ReceitaFederal.Utilitarios;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Unimake.Business.DFe.Servicos;
using Unimake.Business.DFe.Servicos.CCG;
using Unimake.Business.DFe.Servicos.NFe;
using Unimake.Business.DFe.Xml.CCG;
using Unimake.Business.DFe.Xml.NFe;
using Unimake.Exceptions;
using Unimake.Unidanfe.Configurations;
using Unimake.Unidanfe;
using Unimake.Business.DFe.Utility;

namespace Labs.Main
{
	public class LabsNFe
	{

		public static void ConsultaGTIN()
		{
			var config = new Configuracao
			{
				TipoDFe = TipoDFe.CCG,
				CertificadoDigital = new X509Certificate2("C:\\Users\\Pc\\Desktop\\LabSolution.pfx", "solution2024")
			};
			//
			var xml = new ConsGTIN
			{
				Versao = "1.00",
				GTIN = "7898949356196",
			};
			//
			try
			{
				var consGTIN = new CcgConsGTIN(xml, config);
				consGTIN.Executar();
				Modais.MostrarInfo(consGTIN.RetornoWSString);
				//
				switch (consGTIN.Result.CStat)
				{
					case 9490: // Consulta Com Sucesso
						var r = consGTIN.Result;
						Modais.MostrarInfo($"GTIN: {r.GTIN}\n" +
							$"Resposta:{r.XMotivo}\n" +
							$"TipoGTIN: {r.TpGTIN}\n" +
							$"Nome: {r.XProd}\n" +
							$"NCM: {r.NCM}\n" +
							$"CEST's: {r.CEST.Count}\n");
						break;
					default:
						//Consulta Rejeitada
						break;
				}
			}
			catch (ValidarXMLException ex)
			{
				Modais.MostrarErro(ex.GetLastException().Message);
			}
			catch (ValidatorDFeException ex)
			{
				Modais.MostrarErro(ex.GetLastException().Message);
			}
			catch (CertificadoDigitalException ex)
			{
				Modais.MostrarErro(ex.GetLastException().Message);
			}
			catch (Exception ex)
			{
				Modais.MostrarErro(ex.GetLastException().Message);
			}
		}
		//
		//
		public static async void EmitirNotaFiscalEletronicaAsync(Produto Produto)
		{
			var xmlNFE = new EnviNFe
			{
				Versao = "4.00",
				IdLote = "0000000001",
				IndSinc = SimNao.Nao,
				NFe = new List<NFe>
				{
					new NFe
					{
						InfNFe = new List<InfNFe>
						{
							new InfNFe
							{
								// Esse ponto é padrão, Contendo a natureza de operação e informações do Estabelecimento
								Versao = "4.00",
								Ide =  new Ide // Objeto de Identificação da Nota
								{
									CUF = UFBrasil.RJ, // Estado Emitente
									NatOp = "VENDA TESTE DO ESTABELECIMENTO", // Natureza de Operação
									Mod = ModeloDFe.NFe, // Modelo do documento
									Serie = 1, // Numero de Série do documento
									NNF = 25, // Numero da Nota Fiscal (Incremental)
									DhEmi = DateTime.Now, // Data e Hora Emitida
									DhSaiEnt = DateTime.Now, // Data e Hora (Saída/Entrada)
									TpNF = TipoOperacao.Saida, // Tipo de nota fiscal, se é saida ou entrada de produto
									IdDest = DestinoOperacao.OperacaoInterna, // Destino de Operação, se é Interno(Estadual) ou Externo(InterEstadual)
									CMunFG = 3305505, // Código do Mun Emitente
									TpImp = FormatoImpressaoDANFE.Simplificado, // Tipo de Impressão de DANFE
									TpEmis = TipoEmissao.Normal, // Tipo de Emissão
									TpAmb = TipoAmbiente.Homologacao, // Tipo de Ambiente de Emissão
									FinNFe = FinalidadeNFe.Normal, // Finalidade da NFE
									IndFinal = SimNao.Sim, // Indica se a nota é pra consumidor Final
									IndPres = IndicadorPresenca.OperacaoPresencial, // Indica se a operação de venda é presencial ou nao
									ProcEmi = ProcessoEmissao.AplicativoContribuinte, // Indica se a nota está sendo emitida de um aplicativo Contribuinte (Nesse caso, SIM) 
									VerProc = "TESTE 1.00", // Versão do Procedimento
								},
								//Emitente da Nota fiscal eletrônica (Identifica o estabelecimento Emissor)
								Emit = new Emit
								{
									CNPJ = "54781393000147",
									XNome = "IGOR DOS SANTOS MOURA",
									XFant = "Lab Soluções",
									EnderEmit = new EnderEmit
									{
										XLgr = "RUA CARLOS LACERDA",
										Nro = "N/A",
										XBairro = "BOQUEIRAO",
										CMun = 3305505,
										XMun = "SAQUAREMA",
										UF = UFBrasil.RJ,
										CEP = "28990524",
										Fone = "22988041803",
									},
									IE = "14800271",
									CRT = CRT.RegimeNormal, // Se for simples nacional usa o ICMSSN102(ICMS Simples Nacional 102)
								},							// Se não será usado o cálculo de imposto diretamente na nota usando a aliquota do estado contribuinte.
								//Não precisa de Destinatário mas colocaremos por questão fiscal
								//Nesse caso o destinatário é o consumidor final (o que pagou a nota)
								//Caso não seja Informado, Ficará como "Consumidor Não Informado" no site da SEFAZ
								Dest = new Dest
								{
									XNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
									CPF = "15758383708",
									Email = "igordossantos.00@hotmail.com",
									IndIEDest = IndicadorIEDestinatario.NaoContribuinte,
									EnderDest = new EnderDest
									{
										XLgr = "RUA CARLOS LACERDA",
										Nro = "N/A",
										XBairro = "BOQUEIRAO",
										CMun = 3305505,
										XMun = "SAQUAREMA",
										UF = UFBrasil.RJ,
										CEP = "28990524",
										Fone = "22988041803",
									}
								},
								// Lista de produtos presentes na nota (Por algum motivo é uma lista de detalhes).
								// Aqui Constamos todos os itens e impostos residentes
								//Cada Det vai ser um único produto listado na nota. para evitar qualquer tipo de erro na hora da emissão
								Det = new List<Det>
								{
									new Det
									{
										NItem = 1,
										Prod = new Prod
										{
											CProd = "0000000", // Código de Identificação Interno do prodturo na empresa
											CEAN = "SEM GTIN", // Código GTIN do Produto
											XProd = "NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL", // Descrição do Produto
											NCM = "85272900", // Nomenclatura comum do Mercosul (Geralmente retorna pela consulta GTIN) 
											CFOP = "5405", // Código CFOP do produto;
											UCom = Produto.UnidadeDeMedida.Unidade, // Unidade Comercial.
											QCom = 1, // Quantidade Comercial (aparentemente é a quantidade vendida do produto)
											VUnCom = (decimal)Produto.Preco, // Valor da unidade comercial (valor da unidade vendida)
											VProd = Produto.Preco, // Valor do Produto (valor final do produto)
											CEANTrib = "SEM GTIN", // Código GTIN da Unidade Tributável, Caso Contrário Informar SEM GTIN
											UTrib = "UN", // Unidade Tributária
											QTrib = 1, // Quantidade Tributária (pelo visto é a quantidade tributável do produto?)
											VUnTrib = (decimal)Produto.Preco, // Valor da Unidade Tributável
											IndTot = SimNao.Sim, // Indica se o valor do item contribui para o valor final da nota
											XPed = "0000000000000", // Identificador do pedido dentro da loja (Identificador interno da Venda)
											NItemPed = "1", // Número do Item na lista de items comprados
											CBenef = "RJ802008",
										},
										Imposto = new Imposto
										{
											//O Código icms é diretamente relacionado ao código CST do produto, e a aliquota de cada produto está presente na nota fiscal de aquisição
											ICMS = Utilitarios.GetICMS(Produto.CST,
											out double VBCOUT,
											out double VFCPOUT,
											out double VBCSTOUT,
											out double VICMSOUT,
											out double VICMSSTOUT,
											2, // Esse campo precisa estar dentro do objeto do produto (VICMSDeson)
											4, // Esse campo precisa estar dentro do objeto do produto (PICMS)
											Produto.Preco,
											ModalidadeBaseCalculoICMS.ValorOperacao, // Esse campo precisa estar dentro do objeto do produto como INT
											ModalidadeBaseCalculoICMSST.ValorOperacao, // Esse campo precisa estar dentro do objeto do produto como INT
											MotivoDesoneracaoICMS.Outro, // Esse campo precisa estar dentro do objeto do produto como INT
											0,0,0,0,0,0), 
											//
											PIS = new PIS // Somente é aplicado Quando a empresa não se enquadra no Simples Nacional ou MEI.
											{
												PISOutr = new PISOutr
												{
													CST = "99", // esse código é atribuido a o tipo de atividade da empresa
													VBC = 0,
													PPIS = 0,
													VPIS = 0,
												}
											},
											COFINS = new COFINS // ?
											{
												COFINSOutr = new COFINSOutr
												{
													CST = "99", // Mesma Situação do Cofins, Geralmente Atribuido ao tipo de atividade da empresa
													VBC = 0.00,
													PCOFINS = 0.00,
													VCOFINS = 0.00,
												}
											}
										}
									}
								},
								// Identificador de Transporte do Item (geralmente sem ocorrência).
								// Mas Pode ser Identificado o valor de frete do produto também. (caso tenha)
								Transp = new Transp
								{
									ModFrete = ModalidadeFrete.SemOcorrenciaTransporte, // Modalidade de Frete
								},
								//
								Total = new Total // Total de todos os itens listados na nota (Inclui o valor de todos os itens)
								{
									ICMSTot = new ICMSTot
									{
										VBC = VBCOUT,			// Base de cálculo do ICMS (soma do valor dos produtos)
										VICMS = VICMSOUT,			// Valor total do ICMS (valor total dos ICMS calculados Juntos)
										VICMSDeson = 0,     // Valor do ICMS desonerado
										VFCP = VFCPOUT,           // Valor do Fundo de Combate à Pobreza (FCP)
										VBCST = VBCSTOUT,          // Base de cálculo do ICMS Substituição Tributária
										VST = VICMSSTOUT,            // Valor do ICMS Substituição Tributária
										VFCPST = 0,         // Valor do FCP sobre o ICMS ST
										VFCPSTRet = 0,      // Valor do FCP retido anteriormente por Substituição Tributária
										VProd = Produto.Preco,        // Valor total dos produtos/serviços
										VFrete = 0,         // Valor total do frete
										VSeg = 0,           // Valor total do seguro
										VDesc = 0,          // Valor total dos descontos
										VII = 0,            // Valor do Imposto de Importação
										VIPI = 0,           // Valor do IPI (Imposto sobre Produtos Industrializados)
										VIPIDevol = 0,      // Valor do IPI devolvido
										VPIS = 0,           // Valor total do PIS
										VCOFINS = 0,        // Valor total do COFINS
										VOutro = 0,         // Outras despesas acessórias
										VNF = Produto.Preco,          // Valor total da Nota Fiscal
									}
								},
								//
								Pag = new Pag // Seção Informando os pagamentos realizados
								{
									VTroco = 0, // Valor de troco sempre deve ser mencionado
									DetPag = new List<DetPag>()
									{
										//new DetPag
										//{
										//	TPag = MeioPagamento.Dinheiro,
										//	VPag = 25,
										//},
										new DetPag
										{
											TPag = MeioPagamento.PagamentoInstantaneo, //no PIX é obrigado a determinar o CNPJ de quem está recebendo o valor
											// e mencionar se o sistema é integrado ou não, o mesmo esquema do cartão
											VPag = Produto.Preco,
											DPag = DateTime.Now,
											Card = new Card
											{
												CNPJReceb = "54781393000147",
												TpIntegra= TipoIntegracaoPagamento.PagamentoNaoIntegrado,
											}
										},
										//new DetPag
										//{
										//	TPag = MeioPagamento.CartaoCredito,
										//	// CNPJPag -- CNPJ que representa a empresa TEF que está sendo a intermediadora.
										//	// UFPag -- Determina o estado em que a empresa TEF reside.
										//	IndPag = IndicadorPagamento.PagamentoVista,
										//	DPag = DateTime.Now, // Data em que o pagamento foi realizado
										//	VPag = 50,
										//	Card = new Card
										//	{
										//		// CAut -- Código de Autorização de Pagamento
										//		// CNPJ -- CNPJ da Credenciadora TEF
										//		// CNPJReceb -- CNPJ da empresa que está recebendo o pagamento. (Obrigatório)
										//		IdTermPag = "0", // Id do terminal em que foi realizado o pagamento (Maquininha)
										//		TBand = BandeiraOperadoraCartao.Elo,
										//		CNPJReceb = "54781393000147", //
										//		TpIntegra = TipoIntegracaoPagamento.PagamentoNaoIntegrado,
										//	}
										//}
									},
								}
							}
						}
					}
				}
			};
			//Config para emissão
			var configNFe = new Configuracao
			{
				TipoDFe = TipoDFe.NFe,
				TipoEmissao = TipoEmissao.Normal,
				CSC = "4E5DB7F3-EBEC-4455-A62C-11054D901E80",
				CSCIDToken = 1,
				//CertificadoDigital = Colocar aqui a seleção de certificado pela configuração do LabsMainApp
				CertificadoDigital = new X509Certificate2("C:\\Users\\Pc\\Desktop\\LabSolution.pfx", "solution2024")
			};
			//
			// Idealmente deveríamos trabalhar de forma assíncrona para a emissão!, (Faremos isso futuramente)
			try
			{
				var Auto = new Autorizacao(xmlNFE, configNFe);
				Auto.Executar();
				//
				Modais.MostrarInfo(Auto.Result.XMotivo);
				if (Auto.Result.ProtNFe != null)
				{
					switch (Auto.Result.ProtNFe.InfProt.CStat)
					{
						case 100: // NFE Autorizada
								  // Geramos o xml e salvamos no banco de dados para facilitar e assegurar o nosso cliente.
							var xAuto = Auto.Result.GerarXML().OuterXml;//
							var xNota = Auto.ConteudoXMLAssinado.OuterXml;
							var t = new NotaFiscalXml { XmlAuto = xAuto, XmlNota = xNota };
							await CloudDataBase.RegisterLocalAsync("Notas", t);
							break;
						case 204: // Nota Autorizada, Porém tem duplicata
							Modais.MostrarInfo($"CST/CSON : ({Produto.CST}) Autorizado, Porém Contém Duplicidade");
							break;
						case 110: // Uso Denegado
						case 150: // Autorizado o Uso, Autorização Fora de Prazo
						case 205: // NF-e Está denegada na base de dados da Sefaz
						case 301: // Uso Denegado: Irregularidade Fiscal do Emitente
						case 302: // Uso Denegado: Irregularidade Fiscal do Destinatário
						case 303: // Uso Denegado: Destinatário não habilitado a operar na UF
						default:
							Modais.MostrarErro($"{Auto.Result.ProtNFe.InfProt.CStat}: {Auto.Result.ProtNFe.InfProt.XMotivo}");
							break;
					}
				}
			}
			catch (Exception ex)
			{
				Modais.MostrarAviso("Não Foi possível processar o lote: " + ex);
			}

			//
		}
		// Formato NFCE: 
		// EnviNfe(obj's) => List(NFe)>new Nfe => infNFe(versão) => new IDE, new Emit,(não precisa de destinat.), new List<Det> -1 Det- (nItem Progressivo), new Prod,
		// new Imposto => new ICMS, PIS, COFINS.
		// Final da Det => new Total => new ICMSTot
		// Final do Total, new Transp
		// Final do Transp
		/// <summary>
		/// Função para construção e autorização de uso da Nota fiscal Eletrônica.
		/// </summary>
		/// <param name="NaturezaOperacao">Natureza de Operação da venda</param>
		/// <param name="IDVenda">Identificador Interno da Venda Realizada</param>
		/// <param name="Produtos">Lista de Produtos que vão ser adicionados na nota</param>
		/// <param name="Ambiente">Ambiente de Emissão da Nota (Por Padrão é Homologação)</param>
		public static async Task EmitirNotaFiscalDeConsumidorEletronicaAsync(string NaturezaOperacao,string IDVenda,double ValorTroco,bool PossuiParcelas,List<Produto> Produtos,List<PagamentoEfetuado> Pagamentos,TipoAmbiente Ambiente = TipoAmbiente.Homologacao) // PosteriorMente vai receber uma lista de produtos para a emissão de nota
		{
			//Escopo Geral de Campos para Contabilidade
			double vTotProdutos = 0; // Valor Total do Somatório de Todos os Produtos Listados
			double vTotTroco = ValorTroco; // Valor total do troco na compra
			double VBCTot = 0; // Valor Total do Somatório das bases de cálculos
			double VFCPTot = 0; // Valor total do Somatório FCP
			double VBCSTTot = 0; // Valor total do Somatório BC ST
			double VICMSTot = 0; // Valor total do ICMS
			double VICMSSTTot = 0; // Valor total do ICMS ST
			//
			var Detalhes = new List<Det>(); // Iniciamos a lista de Detalhes e iniciamos a contrução do corpo de detalhes
			var DetalhesPagamentos = new List<DetPag>();
			var ProdutosDetalhados = new List<Produto>();
			// Geramos uma lista detalhada dos produtos para evitar problemas na emissao
			foreach (Produto prod in Produtos)
			{
				if(prod.Quantidade > 1) { for (int i = 0; i < prod.Quantidade; i++) { ProdutosDetalhados.Add(prod); } }
				else { ProdutosDetalhados.Add(prod); }
			}
			//Tratamos os Pagamentos
			foreach (PagamentoEfetuado pag in Pagamentos)
			{
				DetalhesPagamentos.Add(Utilitarios.GetDetPag((MeioPagamento)pag.EnumID,pag.Valor,PossuiParcelas));
			}
			//Aqui Verificamos O Número da Nota Fiscal Baseado no Objeto Contido No Banco de Dados
			int NNF = 0;
			EnumeradorNotaFiscalHomologacao ENFH = Ambiente == TipoAmbiente.Homologacao? await CloudDataBase.GetLocalAsync<EnumeradorNotaFiscalHomologacao>(Collections.EnumeradoresNFe, _ => true) : null!;
			EnumeradorNotaFiscalProducao ENFP = Ambiente == TipoAmbiente.Producao? await CloudDataBase.GetLocalAsync<EnumeradorNotaFiscalProducao>(Collections.EnumeradoresNFe, _ => true) : null!;
			//Ambiente de Homologacao
			if(Ambiente == TipoAmbiente.Homologacao)
			{
				ENFH ??= new();
				ENFH.NFCe_Homo += 1;
				NNF = ENFH.NFCe_Homo;
				await CloudDataBase.RegisterLocalAsync(Collections.EnumeradoresNFe,ENFH,Builders<EnumeradorNotaFiscalHomologacao>.Filter.Eq("ID",ENFH.ID));
			}
			//Ambiente de Producao
			if(Ambiente == TipoAmbiente.Producao)
			{
				ENFP ??= new();
				ENFP.NFCe_Prod += 1;
				NNF = ENFP.NFCe_Prod;
				await CloudDataBase.RegisterLocalAsync(Collections.EnumeradoresNFe,ENFP,Builders<EnumeradorNotaFiscalProducao>.Filter.Eq("ID",ENFP.ID));
			}
			//NNF = 30;
			// Construção dos Detalhamentos baseado nas Informações do Produto.
			for (int i = 0; i < ProdutosDetalhados.Count; i++)
			{
				var Produto = ProdutosDetalhados[i];
				Detalhes.Add(new Det
				{
					NItem = i + 1,// Iteração começa do 1 (na nota obviamente).
					Prod = new Prod
					{
						CProd = Produto.CodInterno,
						CEAN = Produto.CodBarras,
						XProd = Produto.Descricao,
						NCM = Produto.NCM,
						CFOP = Produto.CFOP,
						UCom = Produto.UnidadeDeMedida.Unidade,
						QCom = 1,
						VUnCom = (decimal)Produto.Preco,
						VProd = Produto.Preco,
						CEANTrib = Produto.CodBarras,
						UTrib = Produto.UnidadeDeMedida.Unidade,
						QTrib = 1,
						VUnTrib = (decimal)Produto.Preco,
						IndTot = SimNao.Sim,
						XPed = IDVenda,
						NItemPed = $"{i + 1}",
						CBenef = Produto.CBENEF,
					},
					Imposto = new Imposto
					{
						ICMS = Utilitarios.GetICMS(Produto.CST,
						out double VBCOUT,
						out double VFCPOUT,
						out double VBCSTOUT,
						out double VICMSOUT,
						out double VICMSSTOUT,
						Produto.VICMSDESON, // Esse campo precisa estar dentro do objeto do produto
						Produto.PICMS, // Esse campo precisa estar dentro do objeto do produto
						Produto.Preco,
						(ModalidadeBaseCalculoICMS)Produto.BaseDeCalculoICMS, // Esse campo precisa estar dentro do objeto do produto como INT
						(ModalidadeBaseCalculoICMSST)Produto.BaseDeCalculoICMSST, // Esse campo precisa estar dentro do objeto do produto como INT
						(MotivoDesoneracaoICMS)Produto.MotivoDesoneracaoICMS, // Esse campo precisa estar dentro do objeto do produto como INT
						Produto.PFCP, Produto.PICMSST, Produto.PMVAST, Produto.PRedBCST, Produto.PRedBC, Produto.PICMSDIF, Produto.PCredSN),
						// PIS
						PIS = new PIS// ??
						{
							PISOutr = new PISOutr
							{
								CST = "99",
								VBC = 0,
								PPIS = 0,
								VPIS = 0,
							}
						},
						COFINS = new COFINS// ??
						{
							COFINSOutr = new COFINSOutr
							{
								CST = "99",
								VBC = 0,
								PCOFINS = 0.00,
								VCOFINS = 0.00
							}
						}
					}
				});
				//Aqui Adicionamos todos os valores para a conta final e arredondamos para evitar valores como (55.89585762747)
				//Valores de Impostos
				VBCTot += VBCOUT; VFCPTot += VFCPOUT; VBCSTTot += VBCSTOUT; VICMSTot += VICMSOUT; VICMSSTTot += VICMSSTOUT;
				//Arredondamentos
				VBCTot = Math.Round(VBCTot,2); VFCPTot = Math.Round(VFCPTot,2); VBCSTTot = Math.Round(VBCSTTot,2); VICMSTot = Math.Round(VICMSTot,2); VICMSSTTot = Math.Round(VICMSSTTot, 2);
				//Valor Total Final da Nota
				vTotProdutos += Produto.Preco;
				//Arredondamento
				vTotProdutos = Math.Round(vTotProdutos,2);
			}
			var xmlNFCE = new EnviNFe
			{
				Versao = "4.00",
				IdLote = "0000000001",
				IndSinc = SimNao.Sim,
				NFe = new List<NFe>
			{
				new NFe
				{
					InfNFe = new List<InfNFe>
					{
						new InfNFe
						{
							// Esse ponto é padrão, Contendo a natureza de operação e informações do Estabelecimento
							Versao = "4.00",
							Ide =  new Ide // Objeto de Identificação da Nota
							{
								CUF = UFBrasil.RJ, // Estado Emitente - Rio de Janeiro
								NatOp = NaturezaOperacao, // Natureza de Operação
								Mod = ModeloDFe.NFCe, // Modelo do documento- Nesse caso é NFC-e
								Serie = 1, // Numero de Série do documento
								NNF = NNF, // Numero da Nota Fiscal (Incremental)
								DhEmi = DateTime.Now, // Data e Hora Emitida
								DhSaiEnt = DateTime.Now, // Data e Hora (Saída/Entrada)
								TpNF = TipoOperacao.Saida, // Tipo de nota fiscal, se é saida ou entrada de produto (No Nosso caso é Saída)
								IdDest = DestinoOperacao.OperacaoInterna, // Destino de Operação, se é Interno(Estadual) ou Externo(InterEstadual)
								CMunFG = 3305505, // Código do Mun Emitente (Contido no site do IBGE (Depois vai ser Gerenciado Pelo sistema))
								TpImp = FormatoImpressaoDANFE.NFCe, // Tipo de Impressão de DANFE
								TpEmis = TipoEmissao.Normal, // Tipo de Emissão
								TpAmb = Ambiente, // Tipo de Ambiente de Emissão
								FinNFe = FinalidadeNFe.Normal, // Finalidade da NFE
								IndFinal = SimNao.Sim, // Indica se a nota é pra consumidor Final
								IndPres = IndicadorPresenca.OperacaoPresencial, // Indica se a operação de venda é presencial ou nao
								ProcEmi = ProcessoEmissao.AplicativoContribuinte, // Indica se a nota está sendo emitida de um aplicativo usado pelo Contribuinte (Nesse caso, SIM) 
								VerProc = "1.00", // Versão do Procedimento
							},
							//Emitente da Nota fiscal eletrônica (Identifica o estabelecimento Emissor)
							Emit = new Emit
							{
								CNPJ = "54781393000147",
								XNome = "IGOR DOS SANTOS MOURA",
								XFant = "Lab Soluções",
								EnderEmit = new EnderEmit
								{
									XLgr = "RUA CARLOS LACERDA",
									Nro = "N/A",
									XBairro = "BOQUEIRAO",
									CMun = 3305505,
									XMun = "SAQUAREMA",
									UF = UFBrasil.RJ,
									CEP = "28990524",
									Fone = "22988041803",
								},
								IE = "14800271",
								CRT = CRT.SimplesNacional, // Se for simples nacional usa o ICMSSN102(ICMS Simples Nacional 102)
							},							// Se não será usado o cálculo de imposto diretamente na nota usando a aliquota do estado contribuinte.
							//Não precisa de Destinatário mas colocaremos por questão fiscal
							//Nesse caso o destinatário é o consumidor final (o que pagou a nota)
							//Caso não seja Informado, Ficará como "Consumidor Não Informado" no site da SEFAZ
							Dest = new Dest
							{
								XNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
								CPF = "15758383708",
								Email = "igordossantos.00@hotmail.com",
								IndIEDest = IndicadorIEDestinatario.NaoContribuinte,
							},
							// Lista de produtos presentes na nota (Por algum motivo é uma lista de detalhes).
							// Aqui Constamos todos os itens e impostos residentes
							// Cada Det vai ser um único produto listado na nota. para evitar qualquer tipo de erro na hora da emissão

							Det = Detalhes, //Lista de Detalhes Montada Automaticamente

							// Identificador de Transporte do Item (geralmente sem ocorrência).
							// Mas Pode ser Identificado o valor de frete do produto também. (caso tenha)
							Transp = new Transp
							{
								ModFrete = ModalidadeFrete.SemOcorrenciaTransporte, // Modalidade de Frete
							},
							//
							Total = new Total // Total de todos os itens listados na nota (Inclui o valor de todos os itens)
							{
								ICMSTot = new ICMSTot
								{
									VBC = VBCTot,		// Base de cálculo do ICMS (soma do valor dos produtos)
									VICMS = VICMSTot,	// Valor total do ICMS (valor total dos ICMS calculados Juntos)
									VICMSDeson = 0,     // Valor do ICMS desonerado
									VFCP = VFCPTot,     // Valor do Fundo de Combate à Pobreza (FCP)
									VBCST = VBCSTTot,   // Base de cálculo do ICMS Substituição Tributária
									VST = VICMSSTTot,   // Valor do ICMS Substituição Tributária
									VFCPST = 0,         // Valor do FCP sobre o ICMS ST
									VFCPSTRet = 0,      // Valor do FCP retido anteriormente por Substituição Tributária
									VProd = vTotProdutos,   // Valor total dos produtos/serviços
									VFrete = 0,         // Valor total do frete
									VSeg = 0,           // Valor total do seguro
									VDesc = 0,          // Valor total dos descontos
									VII = 0,            // Valor do Imposto de Importação
									VIPI = 0,           // Valor do IPI (Imposto sobre Produtos Industrializados)
									VIPIDevol = 0,      // Valor do IPI devolvido
									VPIS = 0,           // Valor total do PIS
									VCOFINS = 0,        // Valor total do COFINS
									VOutro = 0,         // Outras despesas acessórias
									VNF = vTotProdutos, // Valor total da Nota Fiscal
								}
							},
							//
							Pag = new Pag // Seção Informando os pagamentos realizados
							{
								VTroco = vTotTroco, // Valor de troco sempre deve ser mencionado
								DetPag = DetalhesPagamentos
							}
						}
					}
				}
			}
			};
			//Config Para Emissão
			var configNFCe = new Configuracao
			{
				TipoDFe = TipoDFe.NFCe,
				TimeOutWebServiceConnect = 5000,
				TipoEmissao = TipoEmissao.Normal,
				CSC = "4E5DB7F3-EBEC-4455-A62C-11054D901E80", // CSC de Homologação, Resumindo o lojista vai poder utilizar uma opção de emissão de nota de teste pela sefaz.
				CSCIDToken = 1, // Id do token do CSC
				//CertificadoDigital = Colocar aqui a seleção de certificado pela configuração do LabsMainApp
				CertificadoDigital = new X509Certificate2("C:\\Users\\Pc\\Desktop\\LabSolution.pfx", "solution2024")
			};
			// Envio Assíncrono foi Descartado da SEFAZ
			try
			{
				//Executamos em uma tarefa assíncrona para que se caso não seja realizado o envio o sistema não trave por completo
				var Auto = new Unimake.Business.DFe.Servicos.NFCe.Autorizacao(xmlNFCE, configNFCe); // Aqui usamos o namespace para trabalhar com a autorização da NFc-e
				Auto.Executar();
				await Task.Yield();
				if (Auto.Result.ProtNFe != null)
				{
					switch (Auto.Result.ProtNFe.InfProt.CStat)
					{
						case 100: // NFE Autorizada
								  // Geramos o xml e salvamos no banco de dados para facilitar e assegurar o nosso cliente.
							var xAuto = Auto.Result.GerarXML().OuterXml;//
							var xNota = Auto.NfeProcResult.GerarXML().OuterXml;
							await CloudDataBase.RegisterLocalAsync(Collections.NotasFiscaisHomologacao, new NotaFiscalXml { XmlAuto = xAuto, XmlNota = xNota });
							Auto.GravarXmlDistribuicao(@"NFe\Autorizadas", $"NFCE-{IDVenda}.xml", xNota);
							//
							DANFE.ImprimirDANFE_NFCE(IDVenda, 1, false, true);
							//
							break;
						case 204: // Nota Autorizada, Porém tem duplicata
							Modais.MostrarInfo($"Autorizado, Porém Contém Duplicidade");
							break;
						case 110: // Uso Denegado
						case 150: // Autorizado o Uso, Autorização Fora de Prazo
						case 205: // NF-e Está denegada na base de dados da Sefaz
						case 301: // Uso Denegado: Irregularidade Fiscal do Emitente
						case 302: // Uso Denegado: Irregularidade Fiscal do Destinatário
						case 303: // Uso Denegado: Destinatário não habilitado a operar na UF
						default:
							Modais.MostrarErro($"Erro ({Auto.Result.ProtNFe.InfProt.CStat}): {Auto.Result.ProtNFe.InfProt.XMotivo}");
							break;
					}
				}
			}
			catch // Deu Qualquer merda, imprime offline mesmo
			{
				await Task.Yield();
				Modais.MostrarErro("Um Erro Ocorreu Durante a emissão de Nota\nClique em 'OK' para Iniciar o Protocolo de Emissão OFFLINE\n\nAssim que a conexão com a internet for reestabelecida, as notas pendentes serão enviadas para a SEFAZ e Armazenadas");
				// Caso não esteja conectado com a internet na hora do envio, puxamos essa saída aqui
				await Task.Yield();
				Modais.MostrarInfo("Emitindo Nota Fiscal em Contingência Offline \n(Não foi possível entrar em contato com os servidores da SEFAZ)");
				await Task.Yield();
				// Informamos Campos Importantes para a emissão em contingência
				xmlNFCE.NFe[0].InfNFe[0].Ide.TpEmis = TipoEmissao.ContingenciaOffLine;
				xmlNFCE.NFe[0].InfNFe[0].Ide.DhCont = DateTime.Now;
				xmlNFCE.NFe[0].InfNFe[0].Ide.XJust = "Emitido em Contingência devido a problemas técnicos";
				//Prosseguimos após a informação dos campos necessários
				StreamWriter writer = null!;
				var arqNFCe = Path.Combine(@$".\NFe\ContOffline\NFCE-{IDVenda}-OFFLINE.xml");
				//Now the Auto will run properly
				var Auto = new Unimake.Business.DFe.Servicos.NFCe.Autorizacao(xmlNFCE, configNFCe);
				try
				{
					writer = File.CreateText(arqNFCe);
					var xmlContent = Auto.ConteudoXMLAssinado.GetElementsByTagName("NFe")[0]!.OuterXml;
					writer.Write(xmlContent);
					// Por Questão de segurança ele gera a nota offline tbm
					var nota = new NotaFiscalXml { XmlAuto = "OFFLINE", XmlNota = xmlContent};
					await CloudDataBase.RegisterLocalAsync(Collections.NotasFiscaisHomologacao,nota);
				}
				catch
				{
					///
					await Task.Yield();
					Modais.MostrarErro("Um Erro Inesperado ocorreu ao tentar Salvar a NFC-e no Disco Local!\nA Pasta contendo o sistema está em um local de acesso restrito?\n\n" +
						"Por Segurança o conteúdo da Nota foi salvo no seu Banco de Dados Local!\n\nEste Erro é considerado grave, caso ocorra novamente Chame nossa equipe!");
				}
				finally
				{ 
					
					await Task.Yield();
					writer?.Close();
				}
				await Task.Yield();
				Modais.MostrarAviso("Por Exigências Fiscais, na Contingência Offline é OBRIGATÓRIO a Impressão da via do Cliente e do Lojista!");
				await Task.Yield();
				// Por Regulamento é necessário 2 cópias, 1 para o contribuinte e outra para o cliente
				DANFE.ImprimirDANFE_NFCE_Path(arqNFCe,2,true,false);
			}
		}
	
		public static async Task EmitirNotasFiscaisDeConsumidorGeradasOFFLINEAsync()
		{
			string[] Arquivos = Directory.GetFiles(@"NFe\ContOffline");
			var configNFCe = new Configuracao
			{
				TipoDFe = TipoDFe.NFCe,
				TimeOutWebServiceConnect = 5000,
				TipoEmissao = TipoEmissao.Normal,
				CSC = "4E5DB7F3-EBEC-4455-A62C-11054D901E80", // CSC de Homologação, Resumindo o lojista vai poder utilizar uma opção de emissão de nota de teste pela sefaz.
				CSCIDToken = 1, // Id do token do CSC
								//CertificadoDigital = Colocar aqui a seleção de certificado pela configuração do LabsMainApp
				CertificadoDigital = new X509Certificate2("C:\\Users\\Pc\\Desktop\\LabSolution.pfx", "solution2024")
			};
			//
			foreach (string arq in Arquivos)
			{
				string fName = arq.Replace(@"NFe\ContOffline\","");
				
				if (fName != "Init.txt") 
				{
					await Task.Yield();
					var doc = new XmlDocument();
					doc.Load(arq);
					//
					var xml = new EnviNFe
					{
						IdLote = "0000000001",
						Versao = "4.00",
						IndSinc = SimNao.Sim,
						NFe = new List<NFe>
						{
							XMLUtility.Deserializar<NFe>(doc)
						}
					};
					//
					//Config Para Emissão
					//
					var Auto = new Unimake.Business.DFe.Servicos.NFCe.Autorizacao(xml, configNFCe); // Aqui usamos o namespace para trabalhar com a autorização da NFc-e
					Auto.Executar();
					//Fazer os tratamentos dos status (Criar uma função para isso!).
				}
			}
		}
	
	}
}

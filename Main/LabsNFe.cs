using Labs.Main.ReceitaFederal.Utilitarios;
using System;
using System.Collections.Generic;
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
		public static async void EmitirNotaFiscalEletronica(Produto Produto)
		{
			var xmlNFE = new EnviNFe
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
		/// Função para construção e autorização de uso da Nota fiscal Eletrônica
		/// </summary>
		/// <param name="NaturezaOperacao"></param>
		/// <param name="Produto"></param>
		public static async void EmitirNotaFiscalDeConsumidorEletronica(string NaturezaOperacao, Produto Produto) // PosteriorMente vai receber uma lista de produtos para a emissão de nota
		{
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
									NatOp = "VENDA TESTE DO ESTABELECIMENTO", // Natureza de Operação
									Mod = ModeloDFe.NFCe, // Modelo do documento- Nesse caso é NFC-e
									Serie = 1, // Numero de Série do documento
									NNF = 25, // Numero da Nota Fiscal (Incremental)
									DhEmi = DateTime.Now, // Data e Hora Emitida
									DhSaiEnt = DateTime.Now, // Data e Hora (Saída/Entrada)
									TpNF = TipoOperacao.Saida, // Tipo de nota fiscal, se é saida ou entrada de produto
									IdDest = DestinoOperacao.OperacaoInterna, // Destino de Operação, se é Interno(Estadual) ou Externo(InterEstadual)
									CMunFG = 3305505, // Código do Mun Emitente
									TpImp = FormatoImpressaoDANFE.NFCe, // Tipo de Impressão de DANFE
									TpEmis = TipoEmissao.Normal, // Tipo de Emissão
									TpAmb = TipoAmbiente.Homologacao, // Tipo de Ambiente de Emissão
									FinNFe = FinalidadeNFe.Normal, // Finalidade da NFE
									IndFinal = SimNao.Sim, // Indica se a nota é pra consumidor Final
									IndPres = IndicadorPresenca.OperacaoPresencial, // Indica se a operação de venda é presencial ou nao
									ProcEmi = ProcessoEmissao.AplicativoContribuinte, // Indica se a nota está sendo emitida de um aplicativo usado pelo Contribuinte (Nesse caso, SIM) 
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
											CFOP = "5102", // Código CFOP do produto;
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
											CBenef = "RJ801007",
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
											2, // Esse campo precisa estar dentro do objeto do produto
											4, // Esse campo precisa estar dentro do objeto do produto
											Produto.Preco,
											ModalidadeBaseCalculoICMS.ValorOperacao, // Esse campo precisa estar dentro do objeto do produto como INT
											ModalidadeBaseCalculoICMSST.ValorOperacao, // Esse campo precisa estar dentro do objeto do produto como INT
											MotivoDesoneracaoICMS.Outro, // Esse campo precisa estar dentro do objeto do produto como INT
											0,0,0,0,0,0,0), 
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
			//Config Para Emissão
			var configNFCe = new Configuracao
			{
				TipoDFe = TipoDFe.NFCe,
				TipoEmissao = TipoEmissao.Normal,
				CSC = "4E5DB7F3-EBEC-4455-A62C-11054D901E80",
				CSCIDToken = 1,
				//CertificadoDigital = Colocar aqui a seleção de certificado pela configuração do LabsMainApp
				CertificadoDigital = new X509Certificate2("C:\\Users\\Pc\\Desktop\\LabSolution.pfx", "solution2024")
				//CertificadoDigital = new X509Certificate2("C:\\Users\\maria\\OneDrive\\Área de Trabalho\\LabSolution.pfx", "solution2024")
			};
			var s = xmlNFCE.GerarXML();
			// Idealmente deveríamos trabalhar de forma assíncrona para a emissão!, (Faremos isso futuramente)
			try
			{
				var Auto = new Unimake.Business.DFe.Servicos.NFCe.Autorizacao(xmlNFCE, configNFCe); // Aqui usamos o namespace para trabalhar com a autorização da NFc-e
				Auto.Executar();
				//
				Modais.MostrarInfo($"CSTAT({Auto.Result.CStat}) : {Auto.Result.XMotivo}");
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
							Modais.MostrarErro($"CSON/CST({Produto.CST}) Erro ({Auto.Result.ProtNFe.InfProt.CStat}): {Auto.Result.ProtNFe.InfProt.XMotivo}");
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
	}
}

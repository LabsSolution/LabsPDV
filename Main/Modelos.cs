using IdentityModel.Client;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimake.Business.DFe.Servicos;
using Unimake.Business.DFe.Xml.GNRE;

namespace Labs.Main
{
	/// <summary>
	/// ATENÇÃO!!!! TODOS OS MODELOS ABAIXO SÃO DE ALTO NÍVEL DE IMPORTÂNCIA!!!!, QUALQUER ALTERAÇÃO DESCUIDADA PODE OCASIONAR PROBLEMAS!!!
	/// ANTES DE ALTERAR QUALQUER VALOR, CERTIFIQUE-SE DE CONFIRMAR A NÃO UTILIZAÇÃO DO ITEM NO CÓDIGO!!!
	/// </summary>
	//
	public class UnidadesDeMedida
	{
		public static UnidadeDeMedida Unidade { get; } = new("UN", "Unidade");
		public static UnidadeDeMedida Peca { get; } = new("PC", "Peça");
		public static UnidadeDeMedida Pacote { get; } = new("PACOTE","Pacote");
		public static UnidadeDeMedida Kg { get; } = new("KG", "Quilograma");
		public static UnidadeDeMedida L { get; } = new("L", "Litro");
		public static UnidadeDeMedida M { get; } = new("M","Metro");
	}

	//
	public class UnidadeDeMedida(string Unidade, string Descricao)
	{
		public string Unidade { get; } = Unidade;
		public string Descricao { get; } = Descricao;
	}
	//
	public class NotaFiscalXml
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]

		public string ID { get; set; } = null!;
		/// <summary>
		/// XML da autorização de uso retornado do web service
		/// </summary>
		public string XmlAuto { get; set; } = null!;
		/// <summary>
		/// Xml original da nota Assinado Digitalmente
		/// </summary>
		public string XmlNota { get; set; } = null!;
	}
	//
	public class EnumeradorNotaFiscalHomologacao
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		//
		/// <summary>
		/// Número de Notas Fiscais Eletônicas Emitidas
		/// </summary>
		public int NFe_Homo { get; set; } = 0;
		/// <summary>
		/// Número de Notas Fiscais Eletônicas de Consumidor Emitidas
		/// </summary>
		public int NFCe_Homo { get; set; } = 0;
		/// <summary>
		/// Número de Notas Fiscais de Serviço Eletônicas Emitidas
		/// </summary>
		public int NFSe_Homo { get; set; } = 0;
	}
	public class EnumeradorNotaFiscalProducao
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		//
		/// <summary>
		/// Número de Notas Fiscais Eletônicas Emitidas
		/// </summary>
		public int NFe_Prod { get; set; } = 0;
		/// <summary>
		/// Número de Notas Fiscais Eletônicas de Consumidor Emitidas
		/// </summary>
		public int NFCe_Prod { get; set; } = 0;
		/// <summary>
		/// Número de Notas Fiscais de Serviço Eletônicas Emitidas
		/// </summary>
		public int NFSe_Prod { get; set; } = 0;
	}
	//
	public class EntradaDeProduto(string DataDaCompra, Produto Produto, Fornecedor Fornecedor, int Quantidade, double CustoUnitario, double PrecoDeVenda, double ValorTotalDaCompra)
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		public string DataDaCompra { get; set; } = DataDaCompra;
		public Produto Produto { get; set; } = Produto;
		public Fornecedor Fornecedor { get; set; } = Fornecedor;
		public int Quantidade { get; set; } = Quantidade;
		public double CustoUnitario { get; set; } = CustoUnitario;
		public string CustoUnitarioFormatado { get { return $"R$ {Utils.FormatarValor(CustoUnitario)}"; } }
		public double PrecoDeVenda { get; set; } = PrecoDeVenda;
		public string PrecoDeVendaFormatado { get { return $"R$ {Utils.FormatarValor(PrecoDeVenda)}"; } }
		public double ValorTotalDaCompra { get; set; } = ValorTotalDaCompra;
		public string ValorTotalDaCompraFormatado { get { return $"R$ {Utils.FormatarValor(ValorTotalDaCompra)}"; } }
	}

	public class Collections
	{
		public static string Produtos { get; } = "Produtos";
		public static string Fornecedores { get; } = "Fornecedores";
		public static string Entradas { get; } = "Entradas";
		public static string Saidas { get; } = "Saidas";
		public static string ProdutosComDefeito { get; } = "ProdutosComDefeito";
		public static string Devolucoes { get; } = "Devolucoes";
		public static string EstadoCaixa { get; } = "EstadoCaixa";
		public static string Fechamentos { get; } = "Fechamentos";
		public static string EnumeradoresNFe { get; } = "EnumeradoresNFe";
		public static string NotasFiscaisProducao { get; } = "NotasProducao";
		public static string NotasFiscaisHomologacao { get; } = "NotasHomologacao";
		public static string Vendas { get; } = "Vendas";
		public static string Clientes { get; } = "Clientes";
		public static string MeiosDePagamento { get; } = "MeiosDePagamento";
		public static string LabAdmins { get; } = "LabAdmins";
	}
	public class Devolucao(string Descricao, string Meio, string Motivo, Produto produto, string Data, string Hora)
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		public string Descricao { get; set; } = Descricao;
		/// <summary>
		/// Meio utilizado para estornar o valor do produto
		/// </summary>
		public string MeioDeEstorno { get; set; } = Meio;
		/// <summary>
		/// Motivo da Devolução
		/// </summary>
		public string Motivo { get; set; } = Motivo;
		/// <summary>
		/// Produto Devolvido, nele há o valor de devolução.
		/// </summary>
		public Produto Produto { get; set; } = produto;
		/// <summary>
		/// Data em que a devolução foi realizada
		/// </summary>
		public string Data { get; set; } = Data;
		/// <summary>
		/// Hora em que a devolução foi realizada
		/// </summary>
		public string Hora { get; set; } = Hora;
	}
	//
	public class ValorFechamento(string Nome, double ValorSistema, double ValorAferido)
	{
		/// <summary>
		/// Nome do Meio para Aferimento
		/// </summary>
		public string Nome { get; private set; } = Nome;
		/// <summary>
		/// Valor Aferido pelo sistema
		/// </summary>
		public double ValorSistema { get; private set; } = ValorSistema;
		/// <summary>
		/// Valor Aferido pelo lojista
		/// </summary>
		public double ValorAferido { get; set; } = ValorAferido;
	}
	//
	//
	public class ValorFechado(string Nome, double ValorSistema, double ValorAferido)
	{
		/// <summary>
		/// Nome do Meio para Aferimento
		/// </summary>
		public string Nome { get; private set; } = Nome;
		/// <summary>
		/// Valor Aferido pelo sistema
		/// </summary>
		public double ValorSistema { get; private set; } = ValorSistema;
		/// <summary>
		/// Valor Aferido pelo lojista
		/// </summary>
		public double ValorAferido { get; set; } = ValorAferido;
	}
	//
	public class FechamentoDeCaixa
	{
		/// <summary>
		/// ID Para Representação no Banco de Dados
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		//
		public string FechamentoID { get; set; } = null!;
		public double ValorDeAbertura { get; set; }
		public double FundoDeCaixa { get; set; }
		public double GanhosTotais { get; set; }
		public int ItensVendidos { get; set; }
		public int ItensDevolvidos { get; set; }
		//
		public List<ValorFechado> ValoresFechadosMeio { get; set; } = [];
		public List<ValorFechado> ValoresFechadosGeral { get; set; } = [];
		/// <summary>
		/// Array com todas as Sangrias feitas (Motivo => valor)
		/// </summary>
		public Tuple<string, double>[] Sangrias { get; set; } = [];
		/// <summary>
		/// Array com todos os Suprimentos Feitos (Motivo => valor)
		/// </summary>
		public Tuple<string, double>[] Suprimentos { get; set; } = [];
		/// <summary>
		/// Array com o valor recebido em cada meio de pagamento configurado
		/// </summary>
		public ValorFechado[] Recebimentos { get; set; } = [];

	}
	/// <summary>
	/// Segura o estado do caixa, para que caso venha faltar luz ou algo acontecer, a venda não seja perdida
	/// </summary>
	public class EstadoCaixa // Essa classe só existe no banco de dados local e é temporária (assim que o caixa é fechado deletamos)
	{
		/// <summary>
		/// ID Para Representação no Banco de Dados
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		/// <summary>
		/// Lista de produtos atuais da venda
		/// </summary>
		public List<Produto> Produtos { get; set; } = [];
		//
		public OperadorCaixa OperadorCaixa { get; set; } = null!;
		public bool CaixaAberto { get; set; } = false;
		public CaixaLabs CaixaLabs { get; set; } = null!;
		public bool RealizandoVenda { get; set; } = false;
	}

	/// <summary>
	/// Objeto de Venda (Usado para controle interno das vendas realizadas e futuramente ciencia de dados.)
	/// </summary>
	public class VendaRealizada
	{
		/// <summary>
		/// ID Para Representação no Banco de Dados
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		/// <summary>
		/// ID da venda realizada (usado para a pesquisa dentro do banco de dados)
		/// </summary>
		public string IDVenda { get; set; } = null!;
		/// <summary>
		/// Produtos contidos na venda (nome,quantidade,código, etc)
		/// </summary>
		public Produto[] Produtos { get; set; } = null!;
		//
		public int QuantidadeProdutosVendidos { get { return Produtos != null!? Produtos.Length : 0; } }
		//
		public double Total { get; set; }
		public string TotalFormat { get { return $"R$ {Utils.FormatarValor(Total)}"; } }
		//
		public double Desconto { get; set; }
		public string DescontoFormat { get { return $"{Utils.FormatarValor(Desconto)}%"; } }
		//
		public double TotalComDesconto { get; set; }
		public string TotalComDescontoFormat { get { return $"R$ {Utils.FormatarValor(TotalComDesconto)}"; } }
		//
		public double ValorPago { get; set; }
		public string ValorPagoFormat { get { return $"R$ {Utils.FormatarValor(ValorPago)}"; } }
		//
		public double Troco { get; set; }
		public string TrocoFormat { get { return $"R$ {Utils.FormatarValor(Troco)}"; } }
		//
		public string DataVenda { get; set; } = null!;
		//
		public string HoraVenda { get; set; } = null!;
		/// <summary>
		/// Pagamentos efetuados durante a venda (Contendo nome e valor pago)
		/// </summary>
		public PagamentoEfetuado[] PagamentosEfetuados { get; set; } = null!;
		//
		public ClienteLoja ClienteLoja { get; set; } = null!;
	}
	public class MeiosPagamentoNotaFiscal()
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		//
		public List<MeioPagamentoNotaFiscal> Meios = [ new MeioPagamentoNotaFiscal(MeioPagamento.Dinheiro, false, null!, true) ];
	}
	public class MeioPagamentoNotaFiscal(MeioPagamento Meio, bool AceitaParcelas = false, string NomeDoMeio = null!, bool SLDV = false)
	{
		/// <summary>
		/// Identificador do Meio de Pagamento Escolhido
		/// </summary>
		public MeioPagamento MeioPagamento { get; set; } = Meio;
		/// <summary>
		/// Retorna o MeioPagamento em formato String (Nome)
		/// </summary>
		public string MeioPagamentoFormat { get { return NomeDoMeio.IsNullOrEmpty()? Enum.GetName(MeioPagamento)! : NomeDoMeio; } }
		/// <summary>
		/// Identificador se esse meio de pagamento aceita parcelas ou não
		/// </summary>
		public bool AceitaParcelas { get; set; } = AceitaParcelas;
		/// <summary>
		/// Descrição do Meio de pagamento se ele foi adicionado artificialmente (99-Outros)
		/// </summary>
		public string NomeDoMeio { get; set; } = NomeDoMeio;
		/// <summary>
		/// Sem Limite de Valor. (Indica se esse meio de pagamento pode ultrapassar o valor final da compra [Geralmente Dinheiro, já que é muito dificil não precisar fornecer troco])
		/// </summary>
		public bool SLDV { get; set; } = SLDV;
	}
	//----------------------------------------------------------------------------------------//
	/// <summary>
	/// Operador de Caixa, Salvo Internamente No Banco de Dados (Registrado pelo Próprio sistema,
	/// Sem Uso do Auth0).
	/// </summary>
	public class OperadorCaixa(string NomeDoOperador, string UserName, string Password)
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string DataBaseID { get; set; } = null!;
		/// <summary>
		/// Nome do Operador de Caixa
		/// </summary>
		public string NomeDoOperador { get; set; } = NomeDoOperador;
		/// <summary>
		/// Nome de Usuário para Login
		/// </summary>
		public string UsernameOperador { get; set; } = UserName;
		/// <summary>
		/// Senha do Operador
		/// </summary>
		public string PasswordOperador { get; set; } = Password;
		//
	}
	/// <summary>
	/// Supervisor de Caixa, Salvo Internamente no Banco de Dados (Registrado pelo próprio Sistema,
	/// Sem Uso do Auth0).
	/// </summary>
	public class SupervisorCaixa(string NomeSupervisor, string SenhaSupervisor)
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string DataBaseID { get; set; } = null!;
		public string NomeSupervisor { get; } = NomeSupervisor;
		public string SenhaSupervisor { get; } = SenhaSupervisor;
	}
	//Admin
	public class AdminLabs(string Auth0ID, bool AdminAtivo = false, int PermLevel = 0)
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string DataBaseID { get; set; } = null!;
		public string Auth0ID { get; set; } = Auth0ID;
		public bool AdminAtivo { get; set; } = AdminAtivo;
		public int PermLevel { get; set; } = PermLevel;
	}
	//Clientes
	public class ClienteLabs(string Auth0ID, bool PrimeiroLogin = false, bool AssinaturaAtiva = false, bool PossuiPlanoCloud = false)
	{
		/// <summary>
		/// ID deste Objeto na Database
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string DataBaseID { get; set; } = null!;
		/// <summary>
		/// Determina se o Cliente Possui o Plano Cloud No seu Contrato.
		/// </summary>
		public bool PossuiPlanoCloud { get; set; } = PossuiPlanoCloud;
		/// <summary>
		/// ID De Autenticação do Usuário Dentro do Auth0.
		/// </summary>
		public string Auth0ID { get; private set; } = Auth0ID;
		/// <summary>
		/// Identifica se o Cliente é ativo ou não
		/// Padrão é false pois vai ser ativado somente depois do pagamento da assinatura
		/// E será desativado caso o cliente atrase a assinatura mais de 15 dias.
		/// </summary>
		public bool AssinaturaAtiva { get; private set; } = AssinaturaAtiva;
		/// <summary>
		/// Define se é o primeiro Login do Cliente no Sistema (False se Acabou de Registrar)
		/// </summary>
		public bool PrimeiroLogin { get; private set; } = PrimeiroLogin;
	}

	public class CompraCliente(string IDVenda = null!,string DataDaCompra = null!, string HoraDaCompra = null!,double TotalDaCompra = 0,double TotalPago = 0,double Troco = 0,int Parcelas = 1,List<Produto> ProdutosComprados = null!)
	{
		/// <summary>
		/// ID Deste Objeto na Database
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		/// <summary>
		/// ID Da Venda referente a esta compra.
		/// </summary>
		public string IDVenda { get; set; } = IDVenda;
		/// <summary>
		/// Data da Compra, Formato Obrigatório (dd/MM/yyyy)
		/// </summary>
		public string DataDaCompra { get; set; } = DataDaCompra;
		/// <summary>
		/// Hora da Compra, Formato Obrigatório (HH:mm:ss) 
		/// </summary>
		public string HoraDaCompra { get; set; } = HoraDaCompra;
		/// <summary>
		/// Total cobrado pela compra
		/// </summary>
		public double TotalDaCompra { get; set; } = TotalDaCompra;
		/// <summary>
		/// Total Pago pelo cliente nessa compra
		/// </summary>
		public double TotalPago { get; set; } = TotalPago;
		/// <summary>
		/// Total de troco nessa compra
		/// </summary>
		public double Troco { get; set; } = Troco;
		public PagamentoEfetuado[] PagamentosEfetuados { get; set; } = null!;
		/// <summary>
		/// Lista de Produtos Comprados Pelo Cliente
		/// </summary>
		public List<Produto> ProdutosComprados { get; set; } = ProdutosComprados;
	}

	public class ClienteLoja(string Nome = null!, string CPF = null!, string CNPJ = null!, string Fone = null!,string Email = null!,List<CompraCliente> compras = null!, string DataUltimaCompra = null!, string HoraUltimaCompra = null!)
	{
		/// <summary>
		/// ID Deste Objeto na Database
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		/// <summary>
		/// Nome do Cliente
		/// </summary>
		public string Nome { get; set; } = Nome;
		/// <summary>
		/// CPF do Cliente
		/// </summary>
		public string CPF { get; set; } = CPF;
		/// <summary>
		/// CNPJ do cliente
		/// </summary>
		public string CNPJ { get; set; } = CNPJ;
		/// <summary>
		/// Telefone do Cliente
		/// </summary>
		public string Fone { get; set; } = Fone;
		/// <summary>
		/// Email do Cliente
		/// </summary>
		public string Email { get; set; } = Email;
		/// <summary>
		/// Objeto que lista todas as compras realizadas pelo cliente (com data, produtos comprados, etc)
		/// </summary>
		public List<CompraCliente> Compras { get; set; } = compras;
		/// <summary>
		/// Data da Ultima Compra, Formato Obrigatório (dd/MM/yyyy)
		/// </summary>
		public string DataUltimaCompra { get; set; } = DataUltimaCompra;
		/// <summary>
		/// Hora da Ultima Compra, Formato Obrigatório (HH:mm:ss) 
		/// </summary>
		public string HoraUltimaCompra { get; set; } = HoraUltimaCompra;
		/// <summary>
		/// Status do cliente da loja (Ativo, não ativo.. etc)
		/// </summary>
		public string Status { get; set; } = null!;
	}

	//
	//MODELOS DE Objetos (Classes)
	public class PagamentoEfetuado(int ID, int EnumID, string DescPagamento, double Valor, double ValorTroco)
	{
		/// <summary>
		/// Identificador do RegistroInterno
		/// </summary>
		public int ID { get; private set; } = ID;
		/// <summary>
		/// Identificador do Enumerador do Pagamento Efetuado
		/// </summary>
		public int EnumID { get; private set; } = EnumID;
		/// <summary>
		/// Descrição do pagamento efetuado
		/// </summary>
		public string DescPagamento { get; private set; } = DescPagamento;
		/// <summary>
		/// Valor do pagamento efetuado
		/// </summary>
		public double Valor { get; private set; } = Valor;
		/// <summary>
		/// Valor de Troco do pagamento efetuado
		/// </summary>
		public double ValorTroco { get; private set; } = ValorTroco;
		/// <summary>
		/// Define em quantas vezes foi parcelado esse pagamento
		/// </summary>
		public int Parcelas { get; set; } = 1;
		/// <summary>
		/// Define o valor de cada parcela (Aprox)
		/// </summary>
		public double ValorParcela { get; set; } = 0;
		/// <summary>
		/// Retorna o Numero de parcelas em ( Nx )
		/// </summary>
		public string ParcelasFormat { get { return $"{Parcelas}x"; } }
	}
	//
	// Os Objetos que estavam nesta posição foram removidos por falta de uso.
	//
	public class Fornecedor(string CNPJ, string NomeEmpresa, string Contato, string Email, Endereco Endereco)
	{
		//Empresa,contato,email,endereço,totalcomprado
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		/// <summary>
		/// CNPJ do fornecedor
		/// </summary>
		public string CNPJ { get; set; } = CNPJ;
		/// <summary>
		/// Nome do Fornecedor
		/// </summary>
		public string NomeEmpresa { get; set; } = NomeEmpresa;
		/// <summary>
		/// Contato
		/// </summary>
		public string Contato { get; set; } = Contato;
		/// <summary>
		/// Email do Fornecedor
		/// </summary>
		public string Email { get; set; } = Email;
		/// <summary>
		/// Endereço
		/// </summary>
		public Endereco Endereco { get; set; } = Endereco;
		//
		public double TotalComprado { get; set; } = 0;
		/// <summary>
		/// Retorna o Total Comprado Formatado.
		/// </summary>
		public string TotalCompradoFormatado { get { return $"R$ {Utils.FormatarValor(TotalComprado)}"; } }
		/// <summary>
		/// Retorna o endereço Formatado (Rua,Bairro,Cidade-Estado)
		/// </summary>
		public string EnderecoFormatado { get { return $"{Endereco.Logradouro}, {Endereco.Bairro}, {Endereco.Localidade}-{Endereco.Uf}"; } }
	}
	//
	public class Produto(string Descricao = null!, 
	int Quantidade = 0, 
	int QuantidadeMin = 0, 
	UnidadeDeMedida UnidadeDeMedida = null!, 
	Fornecedor Fornecedor = null!, 
	double Custo = 0, 
	double Preco = 0,
	string CodBarras = null!,
	bool ComDefeito = false, 
	string Status = null!,
	//Campos Necessários para a nota fiscal (Alguns são Opcionais)
	string NCM = null!, 
	string CST = null!,
	string CodInterno = null!,
	string CFOP = null!,
	string CBENEF = null!,
	double VICMSDESON = 0,
	double PICMS = 0,
	double PICMSST = 0,
	double PMVAST = 0,
	double PFCP = 0,
	double PredBCST = 0,
	double PredBC = 0,
	double PICMSDIF = 0,
	double PCredSN = 0,
	int BaseDeCalculoICMS = 0,
	int BaseDeCalculoICMSST = 0,
	int MotivoDesoneracaoICMS = 0,
	bool PossuiInfosFiscais = false)
	{
		//Identificador na database
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		/// <summary>
		/// Nome do produto (Descrição do mesmo)
		/// </summary>
		public string Descricao { get; set; } = Descricao;
		/// <summary>
		/// Quantidade de produtos em estoque
		/// </summary>
		public int Quantidade { get; set; } = Quantidade;
		/// <summary>
		/// Quantidade Minima do produto no estoque
		/// </summary>
		public int QuantidadeMin { get; set; } = QuantidadeMin;
		/// <summary>
		/// Unidade de Medida do produto (Isso irá constar na nota fiscal)
		/// </summary>
		public UnidadeDeMedida UnidadeDeMedida { get; set; } = UnidadeDeMedida;
		/// <summary>
		/// Fornecedor do produto
		/// </summary>
		public Fornecedor Fornecedor { get; set; } = Fornecedor;
		/// <summary>
		/// Custo Unitário do produto (Custo do fornecedor)
		/// </summary>
		public double Custo { get; set; } = Custo;
		/// <summary>
		/// Devolve o custo formatado em R$
		/// </summary>
		public string CustoFormatado { get { return $"R$ {Utils.FormatarValor(Custo)}"; } }
		/// <summary>
		/// Preço de venda do produto
		/// </summary>
		public double Preco { get; set; } = Preco;
		/// <summary>
		/// Devolve o Preco formatado em R$
		/// </summary>
		public string PrecoFormatado { get { return $"R$ {Utils.FormatarValor(Preco)}"; } }
		/// <summary>
		/// Código GTIN do produto
		/// </summary>
		public string CodBarras { get; set; } = CodBarras;
		/// <summary>
		/// Nomenclatura Comum do Mercosul - NCM do produto
		/// </summary>
		public string NCM { get; set; } = NCM;
		/// <summary>
		/// Código de Situação Tributária
		/// </summary>
		public string CST { get; set; } = CST;
		/// <summary>
		/// Código CFOP da Natureza de Operação de Venda Referente a Este produto.
		/// </summary>
		public string CFOP { get; set; } = CFOP;
		/// <summary>
		/// Código de Benefício Tributário do Produto.
		/// </summary>
		public string CBENEF { get; set; } = CBENEF;
		/// <summary>
		/// Valor do ICMS Desonerado
		/// </summary>
		public double VICMSDESON { get; set; } = VICMSDESON;
		/// <summary>
		/// Porcentagem da Aliquota do ICMS
		/// </summary>
		public double PICMS { get; set;} = PICMS;
		/// <summary>
		/// Porcentagem da Aliquota do ICMS ST.
		/// </summary>
		public double PICMSST { get; set; } = PICMSST;
		/// <summary>
		/// Porcentagem da Aliquota da Margem de Lucro de Valor Agregado ST.
		/// </summary>
		public double PMVAST { get; set; } = PMVAST;
		/// <summary>
		/// Porcentagem da Aliquota do Fundo Contra Pobreza.
		/// </summary>
		public double PFCP { get; set; } = PFCP;
		/// <summary>
		/// Porcentagem da Redução da Base de Cálculo ST.
		/// </summary>
		public double PRedBCST { get; set; } = PredBCST;
		/// <summary>
		/// Porcentagem da Redução da Base de Cálculo.
		/// </summary>
		public double PRedBC { get; set; } = PredBC;
		/// <summary>
		/// Porcentagem de Diferimento do ICMS.
		/// </summary>
		public double PICMSDIF { get; set; } = PICMSDIF;
		/// <summary>
		/// Porcentagem de Crédito do Simples Nacional (Exclusivo SN).
		/// </summary>
		public double PCredSN { get; set; } = PCredSN;
		/// <summary>
		/// Identificador da Base de Cálculo do ICMS.
		/// </summary>
		public int BaseDeCalculoICMS { get; set; } = BaseDeCalculoICMS;
		/// <summary>
		/// Identificador da Base de Cálculo do ICMS ST
		/// </summary>
		public int BaseDeCalculoICMSST { get; set; } = BaseDeCalculoICMSST;
		/// <summary>
		/// Identificador do Motivo de Desoneração do ICMS
		/// </summary>
		public int MotivoDesoneracaoICMS { get; set; } = MotivoDesoneracaoICMS;
		//
		/// <summary>
		/// Código de Identificação Interna do produto (Não é Código GTIN)
		/// </summary>
		public string CodInterno { get; set; } = CodInterno;
		/// <summary>
		/// Status do produto no estoque (Gerenciado pelo sistema)
		/// </summary>
		public string Status { get; set; } = Status;
		/// <summary>
		/// Indicador se o produto foi devolvido com defeito ou não (usado somente no objeto de devolução)
		/// </summary>
		public bool ComDefeito { get; set; } = ComDefeito;
		///
		public bool PossuiInfosFiscais { get; set; } = PossuiInfosFiscais;
	}
	/// <summary>
	/// Objeto de Endereço Interno do Sistema
	/// </summary>
	public class Endereco
	{
		public string Cep { get; set; } = null!;
		public string Logradouro { get; set; } = null!;
		public string Complemento { get; set; } = null!;
		public string Bairro { get; set; } = null!;
		public string Localidade { get; set; } = null!;
		public string Uf { get; set; } = null!;
		public string Ibge { get; set; } = null!;
		public string Gia { get; set; } = null!;
		public string Ddd { get; set; } = null!;
		public string Siafi { get; set; } = null!;
	}
	//
}

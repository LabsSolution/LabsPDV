using IdentityModel.Client;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimake.Business.DFe.Xml.GNRE;

namespace Labs.Main
{
	//
	public class UnidadesDeMedida
	{
		public static UnidadeDeMedida Unidade { get; } = new("UN", "Unidade");
		public static UnidadeDeMedida Pacote { get; } = new("PC", "Pacote");
		public static UnidadeDeMedida Kg { get; } = new("Kg", "Quilograma");
		public static UnidadeDeMedida G { get; } = new("g", "Grama");
		public static UnidadeDeMedida Mg { get; } = new("mg", "Miligrama");
		public static UnidadeDeMedida L { get; } = new("L", "Litro");
		public static UnidadeDeMedida Ml { get; } = new("ml", "Mililitro");
	}

	//
	public class UnidadeDeMedida(string Unidade, string Descricao)
	{
		public string Unidade { get; } = Unidade;
		public string Descricao { get; } = Descricao;
	}

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

	/// <summary>
	/// ATENÇÃO!!!! TODOS OS MODELOS ABAIXO SÃO DE ALTO NÍVEL DE IMPORTÂNCIA!!!!, QUALQUER ALTERAÇÃO DESCUIDADA PODE OCASIONAR PROBLEMAS!!!
	/// ANTES DE ALTERAR QUALQUER VALOR, CERTIFIQUE-SE DE CONFIRMAR A NÃO UTILIZAÇÃO DO ITEM NO CÓDIGO!!!
	/// </summary>
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
	/// <summary>
	/// Essa classe precisa ser instanciada como objeto para poder funcionar corretamente!
	/// </summary>
	public class MeiosPagamento
	{
		/// <summary>
		/// ID Para Representação no banco de dados
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		//
		/// <summary>
		/// Lista contendo todos os Nomes de Meios de pagamento
		/// </summary>
		public List<Meio> Meios { get; private set; } = null!;
		/// <summary>
		/// Construtor Padrão
		/// </summary>
		public MeiosPagamento() { Meios = [new("DINHEIRO", true)]; }
	}
	//
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

	//
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
		public double Total { get; set; }
		public double Desconto { get; set; }
		public double TotalComDesconto { get; set; }
		public double ValorPago { get; set; }
		public double Troco { get; set; }
		/// <summary>
		/// Pagamentos efetuados durante a venda (Contendo nome e valor pago)
		/// </summary>
		public PagamentoEfetuado[] PagamentosEfetuados { get; set; } = null!;
	}
	//
	public class Meio(string NomeDoMeio, bool SLDV = false)
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		//
		/// <summary>
		/// Nome do Meio de Pagamento
		/// </summary>
		public string Item1 { get; set; } = NomeDoMeio;
		/// <summary>
		/// Indicador SLDV
		/// </summary>
		public bool Item2 { get; set; } = SLDV;
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
	public class Cliente(string Auth0ID, bool ClienteLabs = false, bool AssinaturaAtiva = false, bool PossuiPlanoCloud = false)
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
		public bool ClienteLabs { get; private set; } = ClienteLabs;
	}


	//MODELOS DE Objetos (Structs)
	public class PagamentoEfetuado(int ID, string DescPagamento, double Valor, double ValorTroco)
	{
		/// <summary>
		/// Identificador do RegistroInterno
		/// </summary>
		public int ID { get; private set; } = ID;
		/// <summary>
		/// Descrição do pagamento efetuado
		/// </summary>
		public string DescPagamento { get; private set; } = DescPagamento;
		/// <summary>
		/// Valor do pagamento efetuado
		/// </summary>
		public double Valor { get; private set; } = Valor;
		public double ValorTroco { get; private set; } = ValorTroco;
	}
	//
	public class MeioDePagamento(string Meio, bool PodeUltrapassarOValorTotal = false, bool PossuiModos = false, List<ModoDePagamento> Modos = default!)
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		/// <summary>
		/// Nome do Meio de Pagamento (Grupo Utilizado como biblioteca para o Modo)
		/// </summary>
		public string Meio { get; set; } = Meio;
		/// <summary>
		/// Determinador se esse meio de pagamento possui mais de um modo.
		/// </summary>
		public bool PossuiModos { get; set; } = PossuiModos;
		public bool PodeUltrapassarOValorTotal { get; set; } = PodeUltrapassarOValorTotal;
		public List<ModoDePagamento> Modos { get; set; } = Modos;
	}
	//
	public class ModoDePagamento(string Modo, bool PossuiBandeira = false, List<string> Bandeiras = default!, bool PossuiParcelas = false, int Parcelas = 0, double Taxa = 0.0)
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		/// <summary>
		/// Nome do Modo de Pagamento (Usado Para a Biblioteca de Meios de Pagamento)
		/// </summary>
		public string Modo { get; set; } = Modo;
		/// <summary>
		/// Diz se esse modo de pagamento Possui Bandeira
		/// </summary>
		public bool PossuiBandeira { get; set; } = PossuiBandeira;
		/// <summary>
		/// Bandeira Representante do Modo de Pagamento (Usado em Cartões)
		/// </summary>
		public List<string> Bandeiras { get; set; } = Bandeiras;
		/// <summary>
		/// Indicador se Possui Parcelas
		/// </summary>
		public bool PossuiParcelas { get; set; } = PossuiParcelas;
		/// <summary>
		/// Quantidade de Parcelas do Modo de pagamento
		/// </summary>
		public int Parcelas { get; set; } = Parcelas;
		/// <summary>
		/// Taxa Adicional do Modo de Pagamento (Geralmente Cartão de Crédito faz isso).
		/// </summary>
		public double Taxa { get; set; } = Taxa;
	}
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
	public class Produto(string Descricao = null!, int Quantidade = 0, int QuantidadeMin = 0, UnidadeDeMedida UnidadeDeMedida = null!, Fornecedor Fornecedor = null!, double Custo = 0, double Preco = 0, string CodBarras = null!, bool ComDefeito = false, string Status = null!)
	{
		//Identificador na database
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		//Parâmetros
		public string Descricao { get; set; } = Descricao;
		//
		public int Quantidade { get; set; } = Quantidade;
		//
		public int QuantidadeMin { get; set; } = QuantidadeMin;
		//
		public UnidadeDeMedida UnidadeDeMedida { get; set; } = UnidadeDeMedida;
		//
		public Fornecedor Fornecedor { get; set; } = Fornecedor;
		//
		public double Custo { get; set; } = Custo;
		/// <summary>
		/// Devolve o custo formatado em R$
		/// </summary>
		public string CustoFormatado { get { return $"R$ {Utils.FormatarValor(Custo)}"; } }
		//
		public double Preco { get; set; } = Preco;
		/// <summary>
		/// Devolve o Preco formatado em R$
		/// </summary>
		public string PrecoFormatado { get { return $"R$ {Utils.FormatarValor(Preco)}"; } }
		//
		public string CodBarras { get; set; } = CodBarras;
		//
		public string Status { get; set; } = Status;
		//
		public bool ComDefeito { get; set; } = ComDefeito;
	}
	//
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
}

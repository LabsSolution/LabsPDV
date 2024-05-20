using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.LABS_PDV
{ 
	/// <summary>
	/// ATENÇÃO!!!! TODOS OS MODELOS ABAIXO SÃO DE ALTO NÍVEL DE IMPORTÂNCIA!!!!, QUALQUER ALTERAÇÃO DESCUIDADA PODE OCASIONAR PROBLEMAS!!!
	/// ANTES DE ALTERAR QUALQUER VALOR, CERTIFIQUE-SE DE CONFIRMAR A NÃO UTILIZAÇÃO DO ITEM NO CÓDIGO!!!
	/// </summary>
	public class Collections
	{
		public static string Produtos { get; } = "Produtos";
		public static string Clientes { get; } = "Clientes";
		public static string MeiosDePagamento { get; } = "MeiosDePagamento";
		public static string LabAdmins { get; } = "LabAdmins";
	}
	public class Modelos
	{
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
			public MeiosPagamento() { Meios = [new("DINHEIRO",false)]; }
		}
		//
		public class Meio(string NomeDoMeio,bool SLDV = false)
		{
			[BsonId]
			[BsonRepresentation(BsonType.ObjectId)]
			public string ID { get; set; } = null!;
			//
			public string Item1 { get; set; } = NomeDoMeio;
			public bool Item2 { get; set; } = SLDV;
		}
		//----------------------------------------------------------------------------------------//
		/// <summary>
		/// Operador de Caixa, Salvo Internamente No Banco de Dados (Registrado pelo Próprio sistema,
		/// Sem Uso do Auth0).
		/// </summary>
		public class OperadorCaixa(string NomeDoOperador,string UserName, string Password)
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
			public int PermLevel { get; set;} = PermLevel;
		}
		//Clientes
		public class Cliente(string Auth0ID,bool ClienteLabs = false, bool AssinaturaAtiva = false, bool PossuiPlanoCloud = false)
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
		public struct PagamentoEfetuado(int ID, string DescPagamento, double valor)
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
			public double Valor { get; private set; } = valor;
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
		public class ModoDePagamento(string Modo,bool PossuiBandeira = false,List<string> Bandeiras = default!,bool PossuiParcelas = false, int Parcelas = 0, double Taxa = 0.0)
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
			public List<string> Bandeiras {  get; set; } = Bandeiras;
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
		//
		//
		public class Produto(string Descricao = null!, int Quantidade = 0, double Preco = 0, string CodBarras = null!)
		{
			//Identificador na database
			[BsonId]
			[BsonRepresentation(BsonType.ObjectId)]
			public string ID { get; set; } = null!;
			//Parâmetros
			public string Descricao { get; set; } = Descricao;
			public int Quantidade { get; set; } = Quantidade;
			public double Preco { get; set; } = Preco;
			public string CodBarras { get; set; } = CodBarras;
		}
		//
	}
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labs.LABS_PDV.Modelos;

namespace Labs.LABS_PDV
{
	//Exemplo de como deve ser os objetos salvos no banco de dados
	public class PersonModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; } = null!;
		public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }


	public class CloudDataBase
	{
		//
		const string DataBase = "DataBaseCentral";
		// Lista de Coleções de DataBase (Constantes)
		const string ClientesCollection = "Clientes";
		const string ProdutosCollection = "Produtos";
		const string MeiosDePagamentoCollection = "MeiosDePagamento";
		
		//Implementação de Interface Para Busca de Coleções dentro da CloudDataBase
		private static IMongoCollection<T> ConnectToMongoLocal<T>(in string collection)
		{
			var client = new MongoClient(LABS_PDV_MAIN.LocalDataBase);
			var db = client.GetDatabase(DataBase);
			return db.GetCollection<T>(collection);
		}
		//
		private static IMongoCollection<T> ConnectToMongoCloud<T>(in string collection)
		{
			var client = new MongoClient(LABS_PDV_MAIN.CloudDataBase);
			var db = client.GetDatabase(DataBase);
			return db.GetCollection<T>(collection);
		}
        //
        private static IMongoCollection<T> ConnectToLabsMongoCloud<T>(in string collection)
        {
            var client = new MongoClient("mongodb+srv://solutionlab:solution%402024@labsolutions.p94r7be.mongodb.net/");
            var db = client.GetDatabase(DataBase);
            return db.GetCollection<T>(collection);
        }
		//
		public static async Task<AdminLabs> GetAdminLabsAsync(string Auth0ID)
		{
            var Admins = ConnectToLabsMongoCloud<AdminLabs>("ADMINS");
            var results = await Admins.FindAsync(x => x.Auth0ID == Auth0ID);
            return results.ToList().FirstOrDefault()!;
        }
		//
		public static async void RegisterAdminLabs(AdminLabs admin)
		{
			var Admins = ConnectToLabsMongoCloud<AdminLabs>("ADMINS");
			await Admins.InsertOneAsync(admin);
		}
        //
        //-------------------------------//
        // -- Gerenciador de Clientes -- //
        //-------------------------------//
        /// <summary>
        /// Pesquisa um Cliente Na DataBase a partir do seu Auth0ID
        /// </summary>
        /// <param name="Auth0ID">Auth0ID Requerido</param>
        /// <returns>Retorna Cliente, caso não exista retorna nulo</returns>
        public static async Task<Cliente> GetClienteAsync(string Auth0ID)
		{
			var Clientes = ConnectToMongoLocal<Cliente>(ClientesCollection);
			var results = await Clientes.FindAsync(x => x.Auth0ID == Auth0ID);
			return results.ToList().FirstOrDefault()!;

		}
		//
		public static async void RegisterClienteAsync(Cliente cliente)
		{
			var Clientes = ConnectToMongoLocal<Cliente>(ClientesCollection);
			await Clientes.InsertOneAsync(cliente);
		}
		//
		public static async void UpdateClienteAsync(Cliente cliente)
		{
			var Clientes = ConnectToMongoLocal<Cliente>(ClientesCollection);
			var filter = Builders<Cliente>.Filter.Eq("DataBaseID",cliente.DataBaseID);
			await Clientes.ReplaceOneAsync(filter, cliente, new ReplaceOptions { IsUpsert = true });
		}
		//
		public static async void RemoverClienteAsync(Cliente cliente)
		{
            var Clientes = ConnectToMongoLocal<Cliente>(ClientesCollection);
            await Clientes.DeleteOneAsync(c => c.DataBaseID == cliente.DataBaseID);
        }
        //
        // Área de Produtos
        //
        public static async Task<List<Produto>> GetProdutosAsync()
		{
			var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
			var results = await produtos.FindAsync(_ => true); // pegamos todos os valores
			return results.ToList();
		}
		//
		public static async void RegisterProdutoAsync(Produto produto)
		{
			var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
			await produtos.InsertOneAsync(produto);
		}
		//
		public static async void UpdateProdutoAsync(Produto produto)
		{
			var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
			var filter = Builders<Produto>.Filter.Eq("ID",produto.ID);
			await produtos.ReplaceOneAsync(filter, produto, new ReplaceOptions { IsUpsert = true }); // IsUpsert Garante que o produto será atualizado;
		}
		//
		public static async Task AbaterProdutosEmEstoqueAsync(List<Produto> produtos)
		{
			//Atualiza a lista com os valores de estoque alterados
			var pColl = ConnectToMongoLocal<Produto>(ProdutosCollection);
			//
			foreach (var p in produtos)
			{
				//
				var pr = await GetProdutoByCodBarrasAsync(p.CodBarras);
				//
				pr.Quantidade -= p.Quantidade;
				//
				var filter = Builders<Produto>.Filter.Eq("ID", p.ID);
				await pColl.ReplaceOneAsync(filter, pr, new ReplaceOptions { IsUpsert = true });
			}
		}
		//
		public static async void RemoveProdutoAsync(Produto produto)
		{
			var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
			await produtos.DeleteOneAsync(c => c.ID == produto.ID);
		}
		//
		public static async Task<Produto> GetProdutoByCodBarrasAsync(string CodBarras)
		{
			var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
			var prodList = await produtos.FindAsync(c => c.CodBarras == CodBarras);
			return prodList.ToList().FirstOrDefault()!;
		}
		//
		public static async Task<long> GetProdutosCountAsync()
		{
			var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
			var filter = Builders<Produto>.Filter.Empty;
			return await produtos.CountDocumentsAsync(filter);
		}
		//
		// MEIO DE PAGAMENTO
		//
		public static async void RegisterMeioDePagamentoAsync(MeioDePagamento MDP)
		{
			var MDPS = ConnectToMongoLocal<MeioDePagamento>(MeiosDePagamentoCollection);
			await MDPS.InsertOneAsync(MDP);
		}
		//
		public static async void UpdateMeioDePagamentoAsync(MeioDePagamento MDP)
		{
			var MDPS = ConnectToMongoLocal<MeioDePagamento>(MeiosDePagamentoCollection);
			var filter = Builders<MeioDePagamento>.Filter.Eq("Meio",MDP.Meio);
			await MDPS.ReplaceOneAsync(filter,MDP,new ReplaceOptions { IsUpsert = true });
		}
		//
		public static async void RemoveMeioDePagamentoAsync(MeioDePagamento MDP)
		{
			var MDPS = ConnectToMongoLocal<MeioDePagamento>(MeiosDePagamentoCollection);
			await MDPS.DeleteOneAsync(c => c.ID == MDP.ID);
		}
		//
		public static async Task<List<MeioDePagamento>> GetMeiosDePagamentoAsync()
		{
			var MDPS = ConnectToMongoLocal<MeioDePagamento>(MeiosDePagamentoCollection);
			var MDPSList = await MDPS.FindAsync(_ => true);
			return MDPSList.ToList();
		}
		//
		public static async Task<string> GetMeioDePagamentoIDByNameAsync(string Name)
		{
			var MDPS = ConnectToMongoLocal<MeioDePagamento>(MeiosDePagamentoCollection);
			var MDPSList = await MDPS.FindAsync(x => x.Meio == Name);
			return MDPSList.FirstOrDefault().ID;
		}
	}
}

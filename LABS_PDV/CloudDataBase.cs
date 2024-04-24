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
		const string CloudConnectionURI = "mongodb+srv://solutionlab:solution%402024@labsolutions.p94r7be.mongodb.net/"; // link para a database na nuvem
		const string LocalConnectionString = "mongodb://localhost:27017/"; // o servidor local servirá para configurações
		//
		const string DataBase = "DataBaseCentral";
		//Lista de Coleções de DataBase (Constantes)
		const string ProdutosCollection = "Produtos";
		const string MeiosDePagamentoCollection = "MeiosDePagamento";
		
		//Implementação de Interface Para Busca de Coleções dentro da CloudDataBase
		private static IMongoCollection<T> ConnectToMongo<T>(in string collection)
		{
			var client = new MongoClient(CloudConnectionURI);
			var db = client.GetDatabase(DataBase);
			return db.GetCollection<T>(collection);
		}
		//
		public static async Task<List<Produto>> GetProdutosAsync()
		{
			var produtos = ConnectToMongo<Produto>(ProdutosCollection);
			var results = await produtos.FindAsync(_ => true); // pegamos todos os valores
			return results.ToList();
		}
		//
		public static async void RegisterProdutoAsync(Produto produto)
		{
			var produtos = ConnectToMongo<Produto>(ProdutosCollection);
			await produtos.InsertOneAsync(produto);
		}
		//
		public static async void UpdateProdutoAsync(Produto produto)
		{
			var produtos = ConnectToMongo<Produto>(ProdutosCollection);
			var filter = Builders<Produto>.Filter.Eq("ID",produto.ID);
			await produtos.ReplaceOneAsync(filter, produto, new ReplaceOptions { IsUpsert = true }); // IsUpsert Garante que o produto será atualizado;
		}
		//
		public static async void RemoveProdutoAsync(Produto produto)
		{
			var produtos = ConnectToMongo<Produto>(ProdutosCollection);
			await produtos.DeleteOneAsync(c => c.ID == produto.ID);
		}
		//
		public static async Task<Produto> GetProdutoByCodBarrasAsync(string CodBarras)
		{
			var produtos = ConnectToMongo<Produto>(ProdutosCollection);
			var prodList = await produtos.FindAsync(c => c.CodBarras == CodBarras);
			return prodList.ToList().FirstOrDefault()!;
		}
		//
		public static async void RegisterMeioDePagamentoAsync(MeioDePagamento MDP)
		{
			var MDPS = ConnectToMongo<MeioDePagamento>(MeiosDePagamentoCollection);
			await MDPS.InsertOneAsync(MDP);
		}
		//
		public static async void UpdateMeioDePagamentoAsync(MeioDePagamento MDP)
		{
			var MDPS = ConnectToMongo<MeioDePagamento>(MeiosDePagamentoCollection);
			var filter = Builders<MeioDePagamento>.Filter.Eq("Meio",MDP.Meio);
			await MDPS.ReplaceOneAsync(filter,MDP,new ReplaceOptions { IsUpsert = true });
		}
		//
		public static async void RemoveMeioDePagamentoAsync(MeioDePagamento MDP)
		{
			var MDPS = ConnectToMongo<MeioDePagamento>(MeiosDePagamentoCollection);
			await MDPS.DeleteOneAsync(c => c.ID == MDP.ID);
		}
		//
		public static async Task<List<MeioDePagamento>> GetMeiosDePagamentoAsync()
		{
			var MDPS = ConnectToMongo<MeioDePagamento>(MeiosDePagamentoCollection);
			var MDPSList = await MDPS.FindAsync(_ => true);
			return MDPSList.ToList();
		}
	}
}

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
		//Check de Retorno das Conexões
		/// <summary>
		/// Retorna O estado de conexão das databases
		/// </summary>
		/// <returns>True se todas as conexões estiverem ok</returns>
		public static bool CheckDataBaseConnection()
		{
			bool status = true;
			var local = ConnectToMongoLocal<Produto>(ProdutosCollection);
			var cloud = ConnectToMongoCloud<Produto>(ProdutosCollection);
			var labsCloud = ConnectToLabsMongoCloud<Cliente>(ClientesCollection);
			//
			if(local == null || cloud == null || labsCloud == null) { status = false; }
			//
			return status;
		}

		//Implementação de Interface Para Busca de Coleções dentro da CloudDataBase
		private static IMongoCollection<T> ConnectToMongoLocal<T>(in string collection)
		{
			try
			{
                var client = new MongoClient(LABS_PDV_MAIN.LocalDataBase);
                var db = client.GetDatabase(DataBase);
                return db.GetCollection<T>(collection);
            }
			catch (Exception ex)
			{
				Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
				return null!;
			}
		}
		//
		private static IMongoCollection<T> ConnectToMongoCloud<T>(in string collection)
		{
			try
			{
                var client = new MongoClient(LABS_PDV_MAIN.CloudDataBase);
                var db = client.GetDatabase(DataBase);
                return db.GetCollection<T>(collection);
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
			}
		}
        //
        private static IMongoCollection<T> ConnectToLabsMongoCloud<T>(in string collection)
        {
			try
			{
                var client = new MongoClient("mongodb+srv://solutionlab:solution%402024@labsolutions.p94r7be.mongodb.net/");
                var db = client.GetDatabase(DataBase);
                return db.GetCollection<T>(collection);
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
			}
        }
		//
		public static async Task<AdminLabs> GetAdminLabsAsync(string Auth0ID)
		{
			try
			{
                var Admins = ConnectToLabsMongoCloud<AdminLabs>("ADMINS");
                var results = await Admins.FindAsync(x => x.Auth0ID == Auth0ID);
                return results.ToList().FirstOrDefault()!;
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
			}
        }
		//
		public static async void RegisterAdminLabs(AdminLabs admin)
		{
			try
			{
                var Admins = ConnectToLabsMongoCloud<AdminLabs>("ADMINS");
                await Admins.InsertOneAsync(admin);
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
			}
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
			try
			{
                var Clientes = ConnectToMongoLocal<Cliente>(ClientesCollection);
                var results = await Clientes.FindAsync(x => x.Auth0ID == Auth0ID);
                return results.ToList().FirstOrDefault()!;
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
			}
		}
		//
		public static async void RegisterClienteAsync(Cliente cliente)
		{
			try
			{
                var Clientes = ConnectToMongoLocal<Cliente>(ClientesCollection);
                await Clientes.InsertOneAsync(cliente);
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
			}
		}
		//
		public static async void UpdateClienteAsync(Cliente cliente)
		{
			try
			{
                var Clientes = ConnectToMongoLocal<Cliente>(ClientesCollection);
                var filter = Builders<Cliente>.Filter.Eq("DataBaseID", cliente.DataBaseID);
                await Clientes.ReplaceOneAsync(filter, cliente, new ReplaceOptions { IsUpsert = true });
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
			}
			
		}
		//
		public static async void RemoverClienteAsync(Cliente cliente)
		{
			try
			{
                var Clientes = ConnectToMongoLocal<Cliente>(ClientesCollection);
                await Clientes.DeleteOneAsync(c => c.DataBaseID == cliente.DataBaseID);
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
			}
            
        }
        //
        // Área de Produtos
        //
        public static async Task<List<Produto>> GetProdutosAsync()
		{
			try
			{
				var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
				var results = await produtos.FindAsync(_ => true); // pegamos todos os valores
				return results.ToList();
			}
			catch (Exception ex)
			{
				Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
				return null!;
			}
		}
		//
		public static async void RegisterProdutoAsync(Produto produto)
		{
			try
			{
                var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
                await produtos.InsertOneAsync(produto);
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
			}
			
		}
		//
		public static async void UpdateProdutoAsync(Produto produto)
		{
			try
			{
				var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
				var filter = Builders<Produto>.Filter.Eq("ID",produto.ID);
				await produtos.ReplaceOneAsync(filter, produto, new ReplaceOptions { IsUpsert = true }); // IsUpsert Garante que o produto será atualizado;
			}
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
			}
			
		}
		//
		public static async Task AbaterProdutosEmEstoqueAsync(List<Produto> produtos)
		{
            try
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
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
            }
		}
		//
		public static async void RemoveProdutoAsync(Produto produto)
		{
            try
            {
				var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
				await produtos.DeleteOneAsync(c => c.ID == produto.ID);
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
            }
            
		}
		//
		public static async Task<Produto> GetProdutoByCodBarrasAsync(string CodBarras)
		{
            try
            {
				var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
				var prodList = await produtos.FindAsync(c => c.CodBarras == CodBarras);
				return prodList.ToList().FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
            }
            
		}
		//
		public static async Task<long> GetProdutosCountAsync()
		{
			try
			{
				var produtos = ConnectToMongoLocal<Produto>(ProdutosCollection);
				if(produtos != null)
				{
					var filter = Builders<Produto>.Filter.Empty;
                    return await produtos.CountDocumentsAsync(filter);
                }
				return -1;
			}
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return -1;
			}
		}
		//
		// MEIO DE PAGAMENTO
		//
		public static async void RegisterMeioDePagamentoAsync(MeioDePagamento MDP)
		{
			try
			{
                var MDPS = ConnectToMongoLocal<MeioDePagamento>(MeiosDePagamentoCollection);
                await MDPS.InsertOneAsync(MDP);
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
			}
		}
		//
		public static async void UpdateMeioDePagamentoAsync(MeioDePagamento MDP)
		{
            try
            {
                var MDPS = ConnectToMongoLocal<MeioDePagamento>(MeiosDePagamentoCollection);
                var filter = Builders<MeioDePagamento>.Filter.Eq("Meio", MDP.Meio);
                await MDPS.ReplaceOneAsync(filter, MDP, new ReplaceOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
            }
		}
		//
		public static async void RemoveMeioDePagamentoAsync(MeioDePagamento MDP)
		{
            try
            {
                var MDPS = ConnectToMongoLocal<MeioDePagamento>(MeiosDePagamentoCollection);
                await MDPS.DeleteOneAsync(c => c.ID == MDP.ID);
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw null!;
            }
		}
		//
		public static async Task<List<MeioDePagamento>> GetMeiosDePagamentoAsync()
		{
            try
            {
                var MDPS = ConnectToMongoLocal<MeioDePagamento>(MeiosDePagamentoCollection);
                var MDPSList = await MDPS.FindAsync(_ => true);
                return MDPSList.ToList();
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
            }
		}
		//
		public static async Task<string> GetMeioDePagamentoIDByNameAsync(string Name)
		{
            try
            {
                var MDPS = ConnectToMongoLocal<MeioDePagamento>(MeiosDePagamentoCollection);
                var MDPSList = await MDPS.FindAsync(x => x.Meio == Name);
                return MDPSList.FirstOrDefault().ID;
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
            }
		}
	}
}

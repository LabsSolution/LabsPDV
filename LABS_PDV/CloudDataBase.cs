using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unimake.Business.DFe.Xml.NFe;
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
    //

	public class CloudDataBase
	{   //Quando estiver próximo de produção essa classe será refatorada, para ter somente membros genéricos
		//
		const string DataBase = "DataBaseCentral"; // Nome da database da empresa
		// Lista de Coleções de DataBase (Constantes) (Usados para gerenciamento de membros)
		const string ClientesCollection = "Clientes";
		const string ProdutosCollection = "Produtos";
		const string MeiosDePagamentoCollection = "MeiosDePagamento";
        //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LocalOK"></param>
        /// <param name="CloudOK"></param>
        /// <param name="LabsCloudOK"></param>
        /// <returns></returns>
		public static bool CheckDataBaseConnection(out bool LocalOK, out bool CloudOK, out bool LabsCloudOK)
        { 
			var local = ConnectToMongoLocal<Produto>(ProdutosCollection);
			var cloud = ConnectToMongoCloud<Produto>(ProdutosCollection);
			var labsCloud = ConnectToLabsMongoCloud<Cliente>(ClientesCollection);
			//
            LocalOK = local != null; 
            //
            CloudOK = cloud != null; 
            //
            LabsCloudOK = labsCloud != null;
			//
			return LocalOK && CloudOK && LabsCloudOK;
		}

		//Implementação de Interface Para Busca de Coleções dentro da CloudDataBase
		private static IMongoCollection<T> ConnectToMongoLocal<T>(in string collection)
		{
			try
			{
                var client = new MongoClient(LABS_PDV_MAIN.LocalDataBaseConnectionURI);
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
                var client = new MongoClient(LABS_PDV_MAIN.CloudDataBaseConnectionURI);
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
		//---------------------------------------------------//
		//------------METODOLOGIA GENÉRICA-------------------//
		//-----TROCAR TODOS OS USOS PELOS MÉTODOS ABAIXO-----//
		//---------------------------------------------------//

		//--------------------MÉTODOS DE ACESSO AO BANCO DE DADOS LOCAL-------------------//

		/// <summary>
		/// Método para Retornar um objeto da DataBase Local Dentro de uma coleção Esperada
		/// </summary>
		/// <typeparam name="T">Tipo de Objeto a ser Retornado (Precisa derivar Diretamente de BSONID)</typeparam>
		/// <param name="collectionName">Nome da Coleção para a busca</param>
		/// <param name="predicate">Expressão Lambda para Requisição, default = "(_ => true)"</param>
		/// <returns>Objeto Requisitado ou Nulo</returns>
		public static async Task<T> GetLocalAsync<T>(string collectionName,Expression<Func<T,bool>> predicate)
		{
			try
			{
				var collection = ConnectToMongoLocal<T>(collectionName);
				var results = await collection.FindAsync(predicate);
				return results.ToList().FirstOrDefault()!;
			}
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return default!;
            }
		}
		//
		/// <summary>
		/// Registra um Objeto na database local em uma coleção determinada
		/// </summary>
		/// <typeparam name="T">Tipo de objeto para registro</typeparam>
		/// <param name="collectionName">Nome da coleção</param>
		/// <param name="ToRegisterObject">Objeto para registro (Respeitando o tipo)</param>
		/// <param name="filter">Filtro para registro</param>
		public static async void RegisterLocalAsync<T>(string collectionName,T ToRegisterObject,FilterDefinition<T> filter)
		{
			try
			{
                var collection = ConnectToMongoLocal<T>(collectionName);
                await collection.ReplaceOneAsync(filter, ToRegisterObject, new ReplaceOptions() { IsUpsert = true });
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw;
            }
		}
		/// <summary>
		/// Atualiza um objeto na database local utilizando um filtro
		/// </summary>
		/// <typeparam name="T">Tipo de objeto para ser atualizado</typeparam>
		/// <param name="collectionName">Nome da coleção</param>
		/// <param name="toUpdateObject">Objeto para atualização</param>
		/// <param name="filter">Filtro para ser aplicado</param>
		public static async void UpdateLocalAsync<T>(string collectionName, T toUpdateObject,FilterDefinition<T> filter)
		{
			try
			{
                var collection = ConnectToMongoLocal<T>(collectionName);
                await collection.ReplaceOneAsync(filter, toUpdateObject, new ReplaceOptions { IsUpsert = true });
            }
			catch (Exception ex)
			{
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw;
            }
		}
		/// <summary>
		/// Remove um objeto na database local utilizando um predicado
		/// </summary>
		/// <typeparam name="T">Tipo de objeto para remoção</typeparam>
		/// <param name="collectionName">Nome da coleção alvo</param>
		/// <param name="predicate">Comparativo para remoção ex: "(x => x.ID == obj.ID)"</param>
		public static async void RemoveLocalAsync<T>(string collectionName,Expression<Func<T,bool>> predicate)
		{
            try
            {
                var collection = ConnectToMongoLocal<T>(collectionName);
                await collection.DeleteOneAsync(predicate);
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw;
            }
        }
		/// <summary>
		/// Pega a quantidade de objetos presentes em uma coleção (Não itera em subgrupos)
		/// </summary>
		/// <typeparam name="T">Tipo de objeto</typeparam>
		/// <param name="collectionName">Nome da coleção</param>
		/// <returns>Retorna a quantidade de objetos do Tipo especificado dentro da coleção</returns>
		public static async Task<long> GetLocalCountAsync<T>(string collectionName)
		{
            try
            {
                var collection = ConnectToMongoLocal<T>(collectionName);
                if (collection != null)
                {
                    var filter = Builders<T>.Filter.Empty;
                    return await collection.CountDocumentsAsync(filter);
                }
                return -1;
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return -1;
            }
        }
        //--------------MÉTODOS PARA BANCO DE DADOS EM NUVEM-------------------------------//
        /// <summary>
        /// Método para Retornar um objeto da DataBase Cloud Dentro de uma coleção Esperada
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto a ser Retornado (Precisa derivar Diretamente de BSONID)</typeparam>
        /// <param name="collectionName">Nome da Coleção para a busca</param>
        /// <param name="predicate">Expressão Lambda para Requisição, default = "(_ => true)"</param>
        /// <returns>Objeto Requisitado ou Nulo</returns>
        public static async Task<T> GetCloudAsync<T>(string collectionName, Expression<Func<T, bool>> predicate)
        {
            try
            {
                var collection = ConnectToMongoCloud<T>(collectionName);
                var results = await collection.FindAsync(predicate);
                return results.ToList().FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return default!;
            }
        }
        //
        /// <summary>
        /// Registra um Objeto na database Cloud em uma coleção determinada
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para registro</typeparam>
        /// <param name="collectionName">Nome da coleção</param>
        /// <param name="ToRegisterObject">Objeto para registro (Respeitando o tipo)</param>
        /// <param name="filter">filtro Para Registro</param>
        public static async void RegisterCloudAsync<T>(string collectionName, T ToRegisterObject, FilterDefinition<T> filter)
        {
            try
            {
                var collection = ConnectToMongoCloud<T>(collectionName);
                await collection.ReplaceOneAsync(filter, ToRegisterObject, new ReplaceOptions() { IsUpsert = true });
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Atualiza um objeto na database Cloud utilizando um filtro
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para ser atualizado</typeparam>
        /// <param name="collectionName">Nome da coleção</param>
        /// <param name="toUpdateObject">Objeto para atualização</param>
        /// <param name="filter">Filtro para ser aplicado</param>
        public static async void UpdateCloudAsync<T>(string collectionName, T toUpdateObject, FilterDefinition<T> filter)
        {
            try
            {
                var collection = ConnectToMongoCloud<T>(collectionName);
                await collection.ReplaceOneAsync(filter, toUpdateObject, new ReplaceOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Remove um objeto na database Cloud utilizando um predicado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para remoção</typeparam>
        /// <param name="collectionName">Nome da coleção alvo</param>
        /// <param name="predicate">Comparativo para remoção ex: "(x => x.ID == obj.ID)"</param>
        public static async void RemoveCloudAsync<T>(string collectionName, Expression<Func<T, bool>> predicate)
        {
            try
            {
                var collection = ConnectToMongoLocal<T>(collectionName);
                await collection.DeleteOneAsync(predicate);
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Pega a quantidade de objetos presentes em uma coleção (Não itera em subgrupos)
        /// </summary>
        /// <typeparam name="T">Tipo de objeto</typeparam>
        /// <param name="collectionName">Nome da coleção</param>
        /// <returns>Retorna a quantidade de objetos do Tipo especificado dentro da coleção</returns>
        public static async Task<long> GetCloudCountAsync<T>(string collectionName)
        {
            try
            {
                var collection = ConnectToMongoCloud<T>(collectionName);
                if (collection != null)
                {
                    var filter = Builders<T>.Filter.Empty;
                    return await collection.CountDocumentsAsync(filter);
                }
                return -1;
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return -1;
            }
        }
        //---------------------MÉTODOS DE ACESSO AO BANCO DE DADOS LABS------------------------------//
        public static async Task<T> GetLabsCloudAsync<T>(string collectionName, Expression<Func<T, bool>> predicate)
        {
            try
            {
                var collection = ConnectToLabsMongoCloud<T>(collectionName);
                var results = await collection.FindAsync(predicate);
                return results.ToList().FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return default!;
            }
        }
		//----------------MÉTODOS DE ACESSO ESPECÍFICOS (NÃO PODEM SER GENERALIZADOS!)--------------------------------//


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
                throw;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
			}
			
		}
		//REMOVER ESSA FUNÇÃO ABAIXO E MOVER PARA OUTRO PONTO
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
                throw;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
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

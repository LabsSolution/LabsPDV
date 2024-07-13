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

/// TODOS OS MEMBROS DEFINIDOS AQUI DEVEM SER DO TIPO TASK!

namespace Labs.Main
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
        const string LabsDataBase = "DataBaseCentral";  // Nome da dataBase da Empresa
        //
        /// <summary>
        /// Verifica a Conexão com as Databases e Retorna o Status de Cada Uma.
        /// Retorna os Status em Array Na Seguinte Ordem [0]=Local,[1]=Cloud,[2]=LabsCloud
        /// </summary>
        /// <returns>Retorna True se todas as conexões estiverem funcionando corretamente</returns>
		public static async Task<bool[]> CheckDataBaseConnection()
        {
            var LocalDatabase = GetLocalDataBase();
            var CloudDatabase = LabsMain.Cliente.PossuiPlanoCloud? GetCloudDataBase() : null!; // Se o cliente não possui plano cloud nem tentamos fazer a atribuição
            var LabsCloudDataBase = GetLabsCloudDataBase();
			//Fazemos as Verificações e atribuimos os valores
			bool LocalOK;
			bool CloudOK;
            bool LabsCloudOK;
            //
            var command = (Command<BsonDocument>)"{ping:1}";
			//
            if(LocalDatabase == null) { LocalOK = false; }
            else { try { await LocalDatabase.RunCommandAsync(command); LocalOK = true; } catch { LocalOK = false; } }
			//
            
            if (CloudDatabase == null) { CloudOK = false; }
            else { try { await CloudDatabase.RunCommandAsync(command); CloudOK = true; } catch { CloudOK = false; } }
            //

            if(LabsCloudDataBase == null) { LabsCloudOK = false; }
            else { try { await LabsCloudDataBase.RunCommandAsync(command); LabsCloudOK = true; } catch { LabsCloudOK = false; } }
            //
            return [ LocalOK, CloudOK, LabsCloudOK ];
        }
        /// <summary>
        /// Retorna a Database Pura do Servidor Local
        /// </summary>
        /// <returns>Database</returns>
        public static IMongoDatabase GetLocalDataBase()
        {
            try
            {
                var client = new MongoClient(LabsMain.LocalDataBaseConnectionURI);
                return client.GetDatabase(LabsMain.ClientDataBase);
            }
            catch
            {
				return null!;
			}
        }
        /// <summary>
        /// Retorna a Database Pura do Servidor Cloud
        /// </summary>
        /// <returns>Database</returns>
        public static IMongoDatabase GetCloudDataBase()
        {
            try
            {
                var client = new MongoClient(LabsMain.CloudDataBaseConnectionURI);
                return client.GetDatabase(LabsMain.ClientDataBase);
            }
            catch
            {
				return null!;
			}
        }
        /// <summary>
        /// Retorna a Database Pura do Servidor Cloud Labs
        /// </summary>
        /// <returns>Database</returns>
        public static IMongoDatabase GetLabsCloudDataBase()
        {
            try
            {
                var client = new MongoClient(LabsMain.LabsCloudDataBaseConnectionURI);
                return client.GetDatabase(LabsDataBase);
            }
            catch
            {
				return null!;
			}
        }
        /// <summary>
        /// Conecta na Database do cliente Procurando uma coleção específica
        /// </summary>
        /// <typeparam name="T">Tipo da Coleção</typeparam>
        /// <param name="collection">Nome da Coleção</param>
        /// <returns>Retorna a Conexão</returns>
        private static IMongoCollection<T> ConnectToMongoLocal<T>(in string collection)
        {
            try
            {
                var client = new MongoClient(LabsMain.LocalDataBaseConnectionURI);
                var db = client.GetDatabase(LabsMain.ClientDataBase);
                return db.GetCollection<T>(collection);
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
            }
        }
        /// <summary>
        /// Conecta na Database Remota (Caso esteja habilitado para o cliente)
        /// </summary>
        /// <typeparam name="T">Tipo da Coleção</typeparam>
        /// <param name="collection">Nome da Coleção</param>
        /// <returns>Retorna a Conexão</returns>
        // Esse método é desabilitado diretamente caso o cliente não possua o plano cloud
        private static IMongoCollection<T> ConnectToMongoCloud<T>(in string collection)
        {
            //
            if (!LabsMain.Cliente.PossuiPlanoCloud) { return null!; }
            //Desabilita a comunicação caso o cliente não tenha o plano cloud!
            try
            {
                var client = new MongoClient(LabsMain.CloudDataBaseConnectionURI);
                var db = client.GetDatabase(LabsMain.ClientDataBase);
                return db.GetCollection<T>(collection);
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
            }
        }
        /// <summary>
        /// Conecta na Database Remota da LABS (Uso Restrito do sistema somente)
        /// </summary>
        /// <typeparam name="T">Tipo da Coleção</typeparam>
        /// <param name="collection">Nome da Coleção</param>
        /// <returns>Retorna a Conexão</returns>
        private static IMongoCollection<T> ConnectToLabsMongoCloud<T>(in string collection)
        {
            try
            {
                var client = new MongoClient(LabsMain.LabsCloudDataBaseConnectionURI);
                var db = client.GetDatabase(LabsDataBase);
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
        public static async Task<T> GetLocalAsync<T>(string collectionName, Expression<Func<T, bool>> predicate)
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
        /// <summary>
		/// Método para Retornar uma lista de objetos da DataBase Local Dentro de uma coleção Esperada
		/// </summary>
		/// <typeparam name="T">Tipo de Objeto a ser Retornado (Precisa derivar Diretamente de BSONID)</typeparam>
		/// <param name="collectionName">Nome da Coleção para a busca</param>
		/// <param name="predicate">Expressão Lambda para Requisição, default = "(_ => true)"</param>
		/// <returns>Lista contendo todos os objetos compatíveis com o filtro</returns>
        public static async Task<List<T>> GetManyLocalAsync<T>(string collectionName, Expression<Func<T, bool>> predicate)
        {
            try
            {
                var collection = ConnectToMongoLocal<T>(collectionName);
                var results = await collection.FindAsync<T>(predicate);
                return results.ToList();
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
        public static async Task RegisterLocalAsync<T>(string collectionName, T ToRegisterObject)
        {
            try
            {
                var collection = ConnectToMongoLocal<T>(collectionName);
                await collection.InsertOneAsync(ToRegisterObject);
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                throw;
            }
        }
        //
        /// <summary>
        /// Registra um Objeto na database local em uma coleção determinada
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para registro</typeparam>
        /// <param name="collectionName">Nome da coleção</param>
        /// <param name="ToRegisterObject">Objeto para registro (Respeitando o tipo)</param>
        /// <param name="filter">Filtro Para Upsert (Respeitando o tipo)</param>
        public static async Task RegisterLocalAsync<T>(string collectionName, T ToRegisterObject,FilterDefinition<T> filter)
        {
            try
            {
                var collection = ConnectToMongoLocal<T>(collectionName);
                var options = new ReplaceOptions { IsUpsert = true };
                await collection.ReplaceOneAsync(filter, ToRegisterObject, options);
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
        public static async Task UpdateOneLocalAsync<T>(string collectionName, T toUpdateObject, FilterDefinition<T> filter)
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
        /// Atualiza uma lista de objetos na database Utilizando um filtro
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para a atualização</typeparam>
        /// <param name="collectionName">Nome da coleção</param>
        /// <param name="updatedObjectList">Lista de objetos com as alterações aplicadas</param>
        /// <param name="filter">filtro para atualização</param>
        public static async Task UpdateManyLocalAsync<T>(string collectionName, List<T> updatedObjectList, FilterDefinition<T> filter)
        {
            //Somente Atualiza os Itens já que a Alteração já foi feita via script antes da chamada
            // na teoria vai funcionar
            try
            {
                var collection = ConnectToMongoLocal<T>(collectionName);
                //
                foreach (var item in updatedObjectList)
                {
                    await collection.ReplaceOneAsync(filter, item, new ReplaceOptions() { IsUpsert = true });
                }
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
        public static async Task RemoveLocalAsync<T>(string collectionName, Expression<Func<T, bool>> predicate)
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
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var collection = ConnectToMongoCloud<T>(collectionName);
					var results = await collection.FindAsync(predicate);
					return results.ToList().FirstOrDefault()!;
				}
                return default!;
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return default!;
            }
        }
        /// <summary>
        /// Método para Retornar uma lista de objetos da DataBase Cloud Dentro de uma coleção Esperada
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto a ser Retornado (Precisa derivar Diretamente de BSONID)</typeparam>
        /// <param name="collectionName">Nome da Coleção para a busca</param>
        /// <param name="predicate">Expressão Lambda para Requisição, default = "(_ => true)"</param>
        /// <returns>Lista contendo todos os Objetos compatíveis com o filtro</returns>
        public static async Task<List<T>> GetManyCloudAsync<T>(string collectionName, Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var collection = ConnectToMongoCloud<T>(collectionName);
					var results = await collection.FindAsync<T>(predicate);
					return results.ToList();
				}
                return default!;
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
        public static async Task RegisterCloudAsync<T>(string collectionName, T ToRegisterObject)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var collection = ConnectToMongoCloud<T>(collectionName);
					await collection.InsertOneAsync(ToRegisterObject);
				}
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
            }
        }
        //
        /// <summary>
        /// Registra um Objeto caso ele não exista na database Cloud em uma coleção determinada
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para registro</typeparam>
        /// <param name="collectionName">Nome da coleção</param>
        /// <param name="ToRegisterObject">Objeto para registro (Respeitando o tipo)</param>
        /// <param name="filter">Filtro Para Upsert (Respeitando o tipo)</param>
        public static async Task RegisterCloudAsync<T>(string collectionName, T ToRegisterObject,FilterDefinition<T> filter)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var collection = ConnectToMongoCloud<T>(collectionName);
					var options = new ReplaceOptions { IsUpsert = true };
					await collection.ReplaceOneAsync(filter, ToRegisterObject, options);
				}
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
            }
        }
        /// <summary>
        /// Atualiza um objeto na database Cloud utilizando um filtro
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para ser atualizado</typeparam>
        /// <param name="collectionName">Nome da coleção</param>
        /// <param name="toUpdateObject">Objeto para atualização</param>
        /// <param name="filter">Filtro para ser aplicado</param>
        public static async Task UpdateCloudAsync<T>(string collectionName, T toUpdateObject, FilterDefinition<T> filter)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var collection = ConnectToMongoCloud<T>(collectionName);
					await collection.ReplaceOneAsync(filter, toUpdateObject, new ReplaceOptions { IsUpsert = true });
				}
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
            }
        }
        /// <summary>
        /// Remove um objeto na database Cloud utilizando um predicado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para remoção</typeparam>
        /// <param name="collectionName">Nome da coleção alvo</param>
        /// <param name="predicate">Comparativo para remoção ex: "(x => x.ID == obj.ID)"</param>
        public static async Task RemoveCloudAsync<T>(string collectionName, Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var collection = ConnectToMongoCloud<T>(collectionName);
					await collection.DeleteOneAsync(predicate);
				}
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
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
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var collection = ConnectToMongoCloud<T>(collectionName);
					if (collection != null)
					{
						var filter = Builders<T>.Filter.Empty;
						return await collection.CountDocumentsAsync(filter);
					}
					return -1;
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
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var collection = ConnectToLabsMongoCloud<T>(collectionName);
					var results = await collection.FindAsync(predicate);
					return results.ToList().FirstOrDefault()!;
				}
                return default!;
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return default!;
            }
        }
        //----------------MÉTODOS DE ACESSO ESPECÍFICOS (NÃO PODEM SER GENERALIZADOS!)--------------------------------//

        //-------------------------------//
        // -- RESTRITO PARA ACESSO DIRETO AO BANCO LABS
        // -- OS MÉTODOS ABAIXO NÃO SÃO GENÉRICOS!
        //-------------------------------//
        /// <summary>
        /// Pesquisa Um Admin Labs a partir do código Auth0ID Fornecido
        /// </summary>
        /// <param name="Auth0ID">Auth0ID Para Pesquisa</param>
        /// <returns>Retorna Admin Caso Esteja Registrado, senão retorna Nulo</returns>
        public static async Task<AdminLabs> GetAdminLabsAsync(string Auth0ID)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var Admins = ConnectToLabsMongoCloud<AdminLabs>(Collections.LabAdmins);
					//
					var results = await Admins.FindAsync(x => x.Auth0ID == Auth0ID);
					return results.ToList().FirstOrDefault()!;
				}
                return null!;
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
            }
        }
        /// <summary>
        /// Registra um Admin No Banco Labs
        /// </summary>
        /// <param name="admin">Objeto de Admin Para ser Registrado</param>
        public static async Task RegisterAdminLabs(AdminLabs admin)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var Admins = ConnectToLabsMongoCloud<AdminLabs>(Collections.LabAdmins);
					await Admins.InsertOneAsync(admin);
				}
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
            }
        }
        //----------------------------------------------------------------//
        // Gestor de Acesso a Clientes
        //----------------------------------------------------------------//
        /// <summary>
        /// Pesquisa um Cliente Na DataBase da LABS a partir do Auth0ID do Cliente
        /// </summary>
        /// <param name="Auth0ID">Auth0ID Requerido</param>
        /// <returns>Retorna Cliente, caso não exista retorna nulo</returns>
        public static async Task<ClienteLabs> GetClienteAsync(string Auth0ID)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var Clientes = ConnectToLabsMongoCloud<ClienteLabs>(Collections.Clientes);
					//
					var results = await Clientes.FindAsync(x => x.Auth0ID == Auth0ID);
					//
					return results.ToList().FirstOrDefault()!;
				}
                return null!;
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
                return null!;
            }
        }
        /// <summary>
        /// Registra um Cliente No Banco de Dados da LABS
        /// </summary>
        /// <param name="cliente">Objeto de Cliente para Registro</param>
        public static async Task RegisterClienteAsync(ClienteLabs cliente)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var Clientes = ConnectToLabsMongoCloud<ClienteLabs>(Collections.Clientes);
					await Clientes.InsertOneAsync(cliente);
				}
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
            }
        }
        /// <summary>
        /// Atualiza um Cliente no Banco de Dados da LABS
        /// </summary>
        /// <param name="cliente">Objeto de cliente para ser atualizado</param>
        public static async Task UpdateClienteAsync(ClienteLabs cliente)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var Clientes = ConnectToLabsMongoCloud<ClienteLabs>(Collections.Clientes);
					//
					var filter = Builders<ClienteLabs>.Filter.Eq("DataBaseID", cliente.DataBaseID);
					await Clientes.ReplaceOneAsync(filter, cliente, new ReplaceOptions { IsUpsert = true });
				}
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
            }

        }
        //
        public static async Task RemoverClienteAsync(ClienteLabs cliente)
        {
            try
            {
                if (LabsMainAppWPF.IsConnectedToInternet)
                {
					var Clientes = ConnectToMongoLocal<ClienteLabs>(Collections.Clientes);
					await Clientes.DeleteOneAsync(c => c.DataBaseID == cliente.DataBaseID);
				}
            }
            catch (Exception ex)
            {
                Modais.MostrarErro($"ERRO CRÍTICO\n{ex.Message}");
            }
        }
    }
}

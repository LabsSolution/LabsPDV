using Labs.Janelas;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.LABS_PDV
{
	public class CloudDataBaseSync
	{
		/// <summary>
		/// Sincroniza as Databases
		/// </summary>
		/// <param name="JDC">Janela de Carregamento</param>
		/// <param name="SyncFromCloud">Define se Deve puxar do cloud para o cliente, ou só replicar do cliente para cloud</param>
		/// <returns></returns>
		public static async Task SyncDatabase(JanelaCarregamentoWPF JDC,bool SyncFromCloud)
		{

			var localDatabase = CloudDataBase.GetLocalDataBase();
			var cloudDatabase = CloudDataBase.GetCloudDataBase();
			var collectionNames = await localDatabase.ListCollectionNames().ToListAsync();
			JDC.ConfigBarraDeCarregamento(0, collectionNames.Count);
			JDC.SetarValorBarra(0);
			foreach (var collectionName in collectionNames)
			{
				JDC.SetTextoFrontEnd($"Sincronizando {collectionName}");
				var localCollection = localDatabase.GetCollection<BsonDocument>(collectionName);
				var cloudCollection = cloudDatabase.GetCollection<BsonDocument>(collectionName);

				var localDocuments = await localCollection.Find(_ => true).ToListAsync();
				var cloudDocuments = await cloudCollection.Find(_ => true).ToListAsync();

				// Sincronizar documentos locais para a nuvem
				foreach (var document in localDocuments)
				{
					var filter = Builders<BsonDocument>.Filter.Eq("_id", document["_id"]);
					var existingDocument = await cloudCollection.Find(filter).FirstOrDefaultAsync();
					if (existingDocument == null)
					{
						await cloudCollection.InsertOneAsync(document);
					}
					else
					{
						await cloudCollection.ReplaceOneAsync(filter, document);
					}
				}
				//
				// Sincronizar documentos da nuvem para o local
				if(SyncFromCloud == false) { return; }
				//
				foreach (var document in cloudDocuments)
				{
					var filter = Builders<BsonDocument>.Filter.Eq("_id", document["_id"]);
					var existingDocument = await localCollection.Find(filter).FirstOrDefaultAsync();
					if (existingDocument == null)
					{
						await localCollection.InsertOneAsync(document);
					}
					else
					{
						await localCollection.ReplaceOneAsync(filter, document);
					}
				}
				JDC.AumentarBarraDeCarregamento(1);
			}
		}
	}
}

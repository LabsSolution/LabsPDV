using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Search.Similarities;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Main
{
	public class MotorDeBusca
	{
		const LuceneVersion version = LuceneVersion.LUCENE_48;
		private readonly StandardAnalyzer _analyzer;
		private readonly IndexWriter _writer;
		private readonly string Dir = "SearchEngineIndexer";

		public MotorDeBusca()
		{
			_analyzer = new StandardAnalyzer(version);
			var config = new IndexWriterConfig(version,_analyzer);
			var _directory = FSDirectory.Open(Dir);
			_writer = new IndexWriter(_directory,config);
		}
		//

		/// <summary>
		/// Esse processo é importantíssimo ser realizado a cada inicialização
		/// </summary>
		public async void RealizarIndexacaoDosProdutos()
		{
			//Pega todos os produtos
			var produtos = await CloudDataBase.GetManyLocalAsync<Produto>(Collections.Produtos,_ => true);
			//
			foreach (var prod in produtos)
			{
				var term = new Term("ID",prod.ID);
				_writer.DeleteDocuments(term);
				//Aqui temos certeza que os documentos duplicados estão sendo removidos.

				var doc = new Document
				{
					new StringField("ID",prod.ID,Field.Store.YES),
					new TextField("Descricao",prod.Descricao,Field.Store.YES)
				};
				_writer.AddDocument(doc);
			}
			_writer.Flush(triggerMerge: false, applyAllDeletes: false);
			_writer.Commit();
			var dir = FSDirectory.Open(Dir);
			var reader = DirectoryReader.Open(dir);
			//
			//
			Modais.MostrarInfo("Indexação Finalizada!");
		}
		/// <summary>
		/// Mecanismo de Busca Melhorado para pesquisa de produtos
		/// </summary>
		/// <param name="Descricao"> Descrição do produto a ser buscado </param>
		/// <returns> Retorna uma lista de ID's que coincidem com a busca do produto </returns>
		public async Task<List<string>> ProcurarProduto(string Descricao)
		{
			//Método Ássíncrono pois não queremos que uma função de busca trave nosso sistema :D
			var dir = FSDirectory.Open(Dir);
			var reader = DirectoryReader.Open(dir);
			var searcher = new IndexSearcher(reader);
			//
			var query = new FuzzyQuery(new Term("Descricao", Descricao));
			var hits = searcher.Search(query, 10).ScoreDocs;
			List<string> s = [];
			foreach (var hit in hits)
			{
				var foundDoc = searcher.Doc(hit.Doc);
				s.Add(foundDoc.Get("ID"));
				await Task.Delay(0);
			}
			reader.Dispose();
			dir.Dispose();
			//
			return s!; // Devolvemos com o indicador de nullidade por questões óbvias
		}
	}
}

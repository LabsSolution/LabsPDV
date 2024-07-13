using Amazon.SecurityToken.Model;
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
using Unimake.Business.DFe.Xml.ESocial;

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
				string nomeFornecedor = null!;
				if(prod.Fornecedor != null) { nomeFornecedor = prod.Fornecedor.NomeEmpresa; }
				var doc = new Document
				{
					new StringField("ID",prod.ID,Field.Store.YES),
					new TextField("Descricao",prod.Descricao,Field.Store.YES),
					new TextField("Fornecedor",nomeFornecedor,Field.Store.YES)
				};
				//
				// Aqui criamos o objeto com o campo fornecedor podendo ser nulo, para facilitar nossa vida na indexação :D
				//
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
		/// <param name="NameSpace"> Namespace de Busca, No caso Seria "Descricao ou Fornecedor" </param>
		/// <returns> Retorna uma lista de ID's que coincidem com a busca do produto </returns>
		public async Task<List<string>> ProcurarProduto(string NameSpace,string Descricao)
		{
			// Método assíncrono para não travar o sistema
			var dir = FSDirectory.Open(Dir);
			var reader = DirectoryReader.Open(dir);
			var searcher = new IndexSearcher(reader);
			//Parser
			var parser = new QueryParser(version, NameSpace, _analyzer);
			// Dividir a descrição em termos
			var terms = Descricao.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			//
			// Construir a query baseando-se no NameSpace atribuido.
			var booleanQuery = new BooleanQuery();
			//
			foreach (var term in terms)
			{
				var fuzzyQuery = new FuzzyQuery(new Term(NameSpace, term));
				booleanQuery.Add(fuzzyQuery, Occur.SHOULD);

				var phraseQuery = new PhraseQuery
				{
					new Term(NameSpace, term)
				};
				booleanQuery.Add(phraseQuery, Occur.SHOULD);
			}
			//
			// Executar a busca
			var hits = searcher.Search(booleanQuery, 10).ScoreDocs;
			var resultado = new List<string>();
			foreach (var hit in hits)
			{
				var foundDoc = searcher.Doc(hit.Doc);
				resultado.Add(foundDoc.Get("ID"));
				await Task.Delay(0); // Simulação de espera assíncrona
			}

			reader.Dispose();
			dir.Dispose();

			// Retornar os resultados
			return resultado!;
		}
	}
}

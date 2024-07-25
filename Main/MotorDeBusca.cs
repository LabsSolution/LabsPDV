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
		private readonly IndexWriterConfig Config;

		public MotorDeBusca()
		{
			_analyzer = new StandardAnalyzer(version);
			Config = new IndexWriterConfig(version,_analyzer);
		}
		//=========================================================================//
		// METODOLOGIA GENÉRICA
		//=========================================================================//
		/// <summary>
		/// Indexa um Novo Item no Banco de Dados do motor de busca.
		/// </summary>
		public void IndexarNovoDocumento(string CollectionName,Document document)
		{
			//
			var dir = FSDirectory.Open(CollectionName);
			var _writer = new IndexWriter(dir,Config);
			//
			_writer.AddDocument(document);
			_writer.Flush(triggerMerge: false, applyAllDeletes: false);
			_writer.Commit();
			_writer.Dispose();
			//
		}
		//
		/// <summary>
		/// Remove um item da pasta indexada de busca.
		/// Exemplo de item Construido (new Term(FieldName,FieldValue)) =>
		/// Se referem a o campo do documento que será verificado e deletado.
		/// </summary>
		/// <param name="CollectionName">Nome da Coleção em que o item se encontra</param>
		/// <param name="FieldName">Nome do Campo a ser pesquisado</param>
		/// <param name="FieldValue">Valor do campo Pesquisado</param>
		public void RemoverItemIndexado(string CollectionName,string FieldName,string FieldValue)
		{
			//
			var dir = FSDirectory.Open(CollectionName);
			var _writer = new IndexWriter(dir,Config);
			var term = new Term(FieldName,FieldValue);
			_writer.DeleteDocuments(term);
			_writer.Flush(triggerMerge: false, applyAllDeletes: false);
			_writer.Commit();
			_writer.Dispose();
		}
		//
		/// <summary>
		/// Realiza o processo de indexação em Lote, utilizando uma lista
		/// </summary>
		/// <param name="documents">Documentos para serem indexados</param>
		/// <param name="CollectionName">Nome da pasta em que os documentos serão indexados</param>
		/// <param name="FieldName">Nome do campo para verificar duplicidade (geralmente o campo "ID")</param>
		public void RealizarIndexacaoEmLote(List<Document> documents,string CollectionName, string FieldName)
		{
			var dir = FSDirectory.Open(CollectionName);
			var conf = new IndexWriterConfig(version,_analyzer); // criamos um arquivo de conf novo, porque por algum motivo o indexWriter não aceita um estático.
			using (var _writer = new IndexWriter(dir, conf)) 
			{
				foreach (Document doc in documents)
				{
					//Aqui temos certeza que os documentos duplicados estão sendo removidos.
					var term = new Term(FieldName, doc.Get(FieldName));
					_writer.DeleteDocuments(term);
					//
					_writer.AddDocument(doc);
				}
				_writer.Flush(triggerMerge: false, applyAllDeletes: false);
				_writer.Commit();
				_writer.Dispose();
			}
		}
		/// <summary>
		/// Procura os Itens indexados baseados na descrição fornecida comparando com o valor do NameSpace fornecido.
		/// </summary>
		/// <param name="CollectionName"> Nome da coleção em que o item está contido </param>
		/// <param name="FieldName"> Nome do Campo Cujo valor irá ser retornado pelo método em formato de lista </param>
		/// <param name="NameSpace"> Namespace de Busca, No caso Seria o Campo cujo valor de busca estaria contido para retorno </param>
		/// <param name="Descricao"> Descrição do Item a ser buscado Baseado no valor contido no NameSpace </param>
		/// <returns> Retorna uma lista de ID's que coincidem com a busca do produto </returns>
		public async Task<List<string>> ProcurarItem(string CollectionName,string FieldName,string NameSpace,string Descricao)
		{
			// Método assíncrono para não travar o sistema
			var dir = FSDirectory.Open(CollectionName);
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
				resultado.Add(foundDoc.Get(FieldName));
				await Task.Delay(0); // Simulação de espera assíncrona
			}

			reader.Dispose();
			dir.Dispose();
			// Retornar os resultados
			return resultado!;
		}
	}
}

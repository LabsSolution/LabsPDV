using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static LabsPDV.LABS_PDV.Modelos;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace LabsPDV.LABS_PDV
{
	internal class DataBase
	{
		public static string DatabasePath { get; private set; } = @"Data Source=.\LABS_PDV\DB\DataBase.db";

		/// <summary>
		/// Retorna um Produto (Struct) Pelo Cód.Barras Fornecido
		/// </summary>
		/// <param name="CodBarras">Código De Barras Para a Busca</param>
		/// <returns>Retorna o produto como objeto (Struct)</returns>
		public static Produto GetProdutoByCodBarras(string CodBarras)
		{
			using (IDbConnection cnn = new SQLiteConnection(DatabasePath))
			{
				var output = cnn.Query<Produto>($"Select * FROM Produtos WHERE CodBarras='{CodBarras}'", new DynamicParameters());
				return output.FirstOrDefault();
			};
		}
		/// <summary>
		/// Registra um produto na database Local
		/// </summary>
		/// <param name="produto">Produto para registro</param>
		public static async void RegisterProduto(Produto produto)
		{
			using (IDbConnection cnn = new SQLiteConnection(DatabasePath))
			{
				await cnn.ExecuteAsync("INSERT INTO Produtos (Nome, Quantidade, Preco, CodBarras) VALUES (@Nome, @Quantidade, @Preco, @CodBarras)",produto);
			}
		}
		/// <summary>
		/// Retorna uma lista contendo todos os produtos registrados
		/// </summary>
		/// <returns>Retorna um List<Produtos></returns>
		public static List<Produto> GetProdutos()
		{
			using (IDbConnection cnn = new SQLiteConnection(DatabasePath))
			{
				var output = cnn.Query<Produto>($"SELECT * FROM Produtos",new DynamicParameters());
				return output.ToList();
			}
		}
	}
}

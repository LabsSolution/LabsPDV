using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Labs.LABS_PDV.Modelos;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace Labs.LABS_PDV
{
	internal class DataBase
	{
		public static string DatabasePath { get; private set; } = @"Data Source=.\LABS_PDV\DB\DataBase.db";

		/// <summary>
		/// Retorna um Produto (Struct) Pelo Cód.Barras Fornecido
		/// </summary>
		/// <param name="CodBarras">Código De Barras Para a Busca</param>
		/// <returns>Retorna o produto como objeto (Struct)</returns>
		public static bool GetProdutoByCodBarras(string CodBarras, out Produto produto)
		{
			using (IDbConnection cnn = new SQLiteConnection(DatabasePath))
			{
				var output = cnn.Query<Produto>($"Select * FROM Produtos WHERE CodBarras='{CodBarras}'", new DynamicParameters()).ToList();
				if(output.Count > 0) 
				{
					produto = output[0];
					return true;
				}
				produto = default;
				return false;
			};
		}
		/// <summary>
		/// Remove um produto na database usando o próprio objeto
		/// </summary>
		/// <param name="produto">Produto a ser Removido</param>
		public static async void RemoveProduto(Produto produto)
		{
			using (IDbConnection cnn = new SQLiteConnection(DatabasePath))
			{
				await cnn.ExecuteAsync("DELETE FROM Produtos WHERE ID= @ID",produto);
			}
		}
		/// <summary>
		/// Atualiza um produto na database usando o próprio objeto
		/// </summary>
		/// <param name="produto">O produto atualizado</param>
		public static async void UpdateProduto(Produto produto)
		{
			using (IDbConnection cnn = new SQLiteConnection(DatabasePath))
			{
				await cnn.ExecuteAsync("UPDATE Produtos SET Descricao = @Descricao, Quantidade = @Quantidade, Preco = @Preco, CodBarras = @CodBarras WHERE ID= @ID ",produto);
			}
		}
		/// <summary>
		/// Registra um produto na database Local
		/// </summary>
		/// <param name="produto">Produto para registro</param>
		public static async void RegisterProduto(Produto produto)
		{
			using (IDbConnection cnn = new SQLiteConnection(DatabasePath))
			{
				await cnn.ExecuteAsync("INSERT INTO Produtos (Descricao, Quantidade, Preco, CodBarras) VALUES (@Descricao, @Quantidade, @Preco, @CodBarras)",produto);
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
		//------------------------------//
		//  METODOS GERAIS DE DATABASE -- Usado para Outros acessos a database
		//------------------------------//
	}
}

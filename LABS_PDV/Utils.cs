﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Labs.LABS_PDV.Modelos;

namespace Labs.LABS_PDV
{
	internal class Utils
	{
		//propriedades Privadas
		private static Dictionary<string, string> nKeys = new() { { "D0", "0" },{ "D1", "1" },{ "D2", "2" },{ "D3", "3" },{ "D4", "4" },{ "D5", "5" },{ "D6", "6" },{ "D7", "7" },{ "D8", "8" },{ "D9", "9" },{ "NumPad0", "0" },{ "NumPad1", "1" },{ "NumPad2", "2" },{ "NumPad3", "3" },{ "NumPad4", "4" },{ "NumPad5", "5" },{ "NumPad6", "6" },{ "NumPad7", "7" },{ "NumPad8", "8" },{ "NumPad9", "9" },};

		//Propriedades Públicas
		public static string[] ValidNumberKeys { get; } = ["D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9"];

		//------------------------------------//
		//Verificadores de acesso público
		//------------------------------------//
		/// <summary>
		/// Retorna um produto usando seu código de registro
		/// </summary>
		/// <param name="Cod">Código de registro do produto</param>
		/// <param name="produto">Retorno (out) do produto cadastrado</param>
		/// <returns>Retorna um booleano representado se a busca foi bem sucedida ou não</returns>
		public static bool GetProdutoByCode(string Cod, out Produto produto)
		{
			if (DataBase.GetProdutoByCodBarras(Cod,out produto))
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// Verifica se a tecla fornecida é uma tecla numérica válida
		/// </summary>
		/// <param name="keyCode">Código da tecla</param>
		/// <param name="key">Numero correspondente a essa tecla</param>
		/// <returns>Retorna verdadeiro se a tecla for válida</returns>
		public static bool IsValidNumberKey(Keys keyCode, out string key)
		{
			key = "NULL";
			if (ValidNumberKeys.Contains(keyCode.ToString())) { key = nKeys[keyCode.ToString()]; return true; }
			return false;
		}
		//------------------------------------//
		//Métodos de Utilidade Geral
		//------------------------------------//
		/// <summary>
		/// Recebe um valor em String e tenta passar para Int, caso não seja possível retornará falso
		/// </summary>
		/// <param name="toParse">Valor para conversão</param>
		/// <param name="value">Valor convertido</param>
		/// <returns>Booleano representando sucesso ou falha</returns>
		public static bool TryParseToInt(string toParse, out int value)
		{
			try
			{
				value = int.Parse(toParse);
				return true;
			}
			catch (Exception)
			{
				value = -1;
				return false;
			}
		}
		public static bool IsValidBarCode(string Cod) 
		{
			try
			{
				long.Parse(Cod); return true;
			}
			catch
			{
				return false;
			}
		}
		/// <summary>
		/// Recebe um valor em String e tenta passar para Double, caso não seja possível retornará falso
		/// </summary>
		/// <param name="toParse">Valor para conversão</param>
		/// <param name="value">Valor convertido</param>
		/// <returns>Booleano representando sucesso ou falha</returns>
		public static bool TryParseToDouble(string toParse, out double value)
		{
			try
			{
				value = double.Parse(toParse);
				return true;
			}
			catch (Exception)
			{
				value = -1;
				return false;
			}
		}
	}
}

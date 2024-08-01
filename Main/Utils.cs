using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Labs.Main
{
	partial class Utils
	{
		//Métodos Parciais

		[GeneratedRegex("[^0-9]+")]
		public static partial Regex OnlyNumber();
		[GeneratedRegex("[^0-9.,]+")]
		public static partial Regex OnlyMonetary();
		[GeneratedRegex("[^A-Za-z]+")]
		public static partial Regex OnlyAlphabet();

		//propriedades Privadas
		private static Dictionary<string, string> nKeys = new() { { "D0", "0" },{ "D1", "1" },{ "D2", "2" },{ "D3", "3" },{ "D4", "4" },{ "D5", "5" },{ "D6", "6" },{ "D7", "7" },{ "D8", "8" },{ "D9", "9" },{ "NumPad0", "0" },{ "NumPad1", "1" },{ "NumPad2", "2" },{ "NumPad3", "3" },{ "NumPad4", "4" },{ "NumPad5", "5" },{ "NumPad6", "6" },{ "NumPad7", "7" },{ "NumPad8", "8" },{ "NumPad9", "9" },};

		//Propriedades Públicas
		public static string HoraFormatada { get { return $"{DateTime.Now:HH:mm:ss}"; } }
		public static string DataFormatada { get { return $"{DateTime.Now:dd/MM/yyyy}"; } }
		public static string[] ValidNumberKeys { get; } = ["D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9"];

		//------------------------------------//
		//Verificadores de acesso público
		//------------------------------------//
		/// <summary>
		/// Define se a data fornecida é hoje
		/// </summary>
		/// <param name="Data">Data no formato dd/MM/yyyy</param>
		/// <returns>Retorna verdadeiro se a data fornecida é hoje</returns>
		public static bool IsToday(string Data) 
		{
			return Data == $"{DateTime.Now:dd/MM/yyyy}";
		}
		/// <summary>
		/// Verifica se um código GTIN13 é válido
		/// </summary>
		/// <param name="GTIN">Código</param>
		/// <returns>Retorna Verdadeiro se o código é válido</returns>
		public static bool IsValidGtin13(string GTIN)
		{
			List<int> ns = [];
			//
			if (GTIN.IsNullOrEmpty()) { return false; }
			if(GTIN.Length > 13) { return false; } // retornamos se o gtin for maior que 13 digitos
			// Fazemos a iteração dos digitos e retornamos caso tenha algum digito errado (Uma letra por exemplo)
			foreach(char c in GTIN) { if (!int.TryParse($"{c}", out int r)) { return false; } ns.Add(r); }
			//
			int resPar = (ns[1] + ns[3] + ns[5] + ns[7] + ns[9] + ns[11]) * 3;
			int resImpar = ns[0] + ns[2] + ns[4] + ns[6] + ns[8] + ns[10] + resPar;
			// Agora Achamos o Digito Verificador (é literalmente o que falta adicionar para que o resultado do módulo por 10 seja 0)
			int df = 0;
			while ((resImpar + df) % 10 != 0) {df++;} // Aqui o loop while simplesmente quebra e retorna o digito verificador
			// Agora verificamos se o último digito é valido ou não
			return df == ns.Last();
			//
		}
		/// <summary>
		/// Retorna um produto usando seu código de registro
		/// </summary>
		/// <param name="Cod">Código de registro do produto</param>
		/// <returns>Retorna um booleano representado se a busca foi bem sucedida ou não</returns>
		public static async Task<Produto> GetProdutoByCode(string Cod)
		{
			return await CloudDataBase.GetLocalAsync<Produto>(Collections.Produtos,x => x.CodBarras == Cod);
		}
		/// <summary>
		/// Verifica se a tecla fornecida é uma tecla numérica válida
		/// </summary>
		/// <param name="keyCode">Código da tecla</param>
		/// <param name="key">Numero correspondente a essa tecla</param>
		/// <returns>Retorna verdadeiro se a tecla for válida</returns>
		public static bool IsValidNumberKey(Key keyCode, out string key)
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
		/// <summary>
		/// Verifica se o Código de Barras fornecido é Válido Checando se são somente números
		/// </summary>
		/// <param name="Cod">Código Para Checar</param>
		/// <returns>Retorna True caso o código seja válido</returns>
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
		//
		public static double GetInverseRelativeValue(double value, double min, double max)
		{
			return (value-min)/(max-min)-1;
		}
		/// <summary>
		/// Formata um valor para string preservando os campos
		/// </summary>
		/// <param name="valor">Valor para formatar</param>
		/// <returns>Valor Formatado</returns>
		public static string FormatarValor(double valor)
		{
            return valor.ToString("0.00", CultureInfo.GetCultureInfo("pt-BR"));
        }
        /// <summary>
        /// Formata um valor para string preservando os campos
        /// </summary>
        /// <param name="valor">Valor para formatar</param>
        /// <returns>Valor Formatado</returns>
        public static string FormatarValor(float valor)
		{
            return valor.ToString("0.00", CultureInfo.GetCultureInfo("pt-BR"));
        }
		/// <summary>
		/// Formata um texto baseado em uma largura limite
		/// </summary>
		/// <param name="texto">Texto para ser formatado</param>
		/// <param name="font">Fonte usada no texto</param>
		/// <param name="graphics">Interface grafica que está sendo usada</param>
		/// <param name="larguraPagina">Largura da página (Largura limite)</param>
		/// <param name="Altura">Altura total do texto</param>
		/// <returns></returns>
		public static string FormatarTexto(string texto, Font font, Graphics graphics, float larguraPagina, out float Altura)
		{
			if (texto == null) { Altura = 0; return texto!; }
			string[] palavras = texto.Split(' ');
            StringBuilder linhaAtual = new StringBuilder();
            StringBuilder textoFormatado = new StringBuilder();

            foreach (string palavra in palavras)
            {
                linhaAtual.Append(palavra + " ");
                float largura = graphics.MeasureString(linhaAtual.ToString(), font).Width;

                if (largura > larguraPagina)
                {
                    linhaAtual.Remove(linhaAtual.Length - palavra.Length - 1, palavra.Length + 1);
                    textoFormatado.AppendLine(linhaAtual.ToString());
                    linhaAtual.Clear();
                    linhaAtual.Append(palavra + " ");
                }
            }

            if (linhaAtual.Length > 0)
            {
                textoFormatado.AppendLine(linhaAtual.ToString());
            }
			Altura = graphics.MeasureString(textoFormatado.ToString(), font).Height;
            return textoFormatado.ToString();
        }
    }
}

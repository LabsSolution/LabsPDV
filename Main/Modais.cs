using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Labs.Main
{
	internal class Modais
	{
		public static string Titulo = "Lab Soluções";
		/// <summary>
		/// Mostra uma mensagem de erro para o usuário
		/// </summary>
		/// <param name="Descricao">Descrição do erro</param>
		public static MessageBoxResult MostrarErro(string Descricao)
		{
			return MessageBox.Show(Descricao,Titulo,MessageBoxButton.OK,MessageBoxImage.Error);
		}
		/// <summary>
		/// Mostra uma mensagem de aviso para o usuário
		/// </summary>
		/// <param name="Descricao">Descrição do aviso</param>
		public static void MostrarAviso(string Descricao)
		{
			MessageBox.Show(Descricao,Titulo, MessageBoxButton.OK, MessageBoxImage.Warning);
		}
		/// <summary>
		/// Mostra uma informação para o usuário
		/// </summary>
		/// <param name="Info">Informação a ser mostrada</param>
		public static void MostrarInfo(string Info)
		{
			MessageBox.Show(Info,Titulo,MessageBoxButton.OK,MessageBoxImage.Information);
		}
		/// <summary>
		/// Mostra uma pergunta para o usuário
		/// </summary>
		/// <param name="Descricao">Pergunta a ser feita</param>
		public static MessageBoxResult MostrarPergunta(string Descricao)
		{
			return MessageBox.Show(Descricao,Titulo,MessageBoxButton.YesNo, MessageBoxImage.Question);
		}
	}
}

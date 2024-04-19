using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.LABS_PDV
{
	internal class Modais
	{
		public static string Titulo = "Lab Soluções";
		/// <summary>
		/// Mostra uma mensagem de erro para o usuário
		/// </summary>
		/// <param name="Descricao">Descrição do erro</param>
		public static DialogResult MostrarErro(string Descricao)
		{
			return MessageBox.Show(Descricao,Titulo,MessageBoxButtons.AbortRetryIgnore,MessageBoxIcon.Error);
		}
		/// <summary>
		/// Mostra uma mensagem de aviso para o usuário
		/// </summary>
		/// <param name="Descricao">Descrição do aviso</param>
		public static void MostrarAviso(string Descricao)
		{
			MessageBox.Show(Descricao,Titulo,MessageBoxButtons.OK,MessageBoxIcon.Warning);
		}
		/// <summary>
		/// Mostra uma informação para o usuário
		/// </summary>
		/// <param name="Info">Informação a ser mostrada</param>
		public static void MostrarInfo(string Info)
		{
			MessageBox.Show(Info,Titulo,MessageBoxButtons.OK,MessageBoxIcon.Information);
		}
		/// <summary>
		/// Mostra uma pergunta para o usuário
		/// </summary>
		/// <param name="Descricao">Pergunta a ser feita</param>
		public static DialogResult MostrarPergunta(string Descricao)
		{
			return MessageBox.Show(Descricao,Titulo,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
		}
	}
}

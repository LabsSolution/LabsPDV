//Importação das Janelas
using Labs.Janelas.LabsEstoque;
//
namespace Labs
{

	internal static class LABS_PDV_MAIN
	{ 
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[MTAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.Run(new LabsMainApp());
			//Inicializamos a lista de Aplicações
		}

		/// <summary>
		/// Inicia uma dependencia em cima da janela que a requisitou (Não Esconde a janela anterior)
		/// Caso queira iniciar uma janela Diretamente como foco use o método IniciarApp para melhor performance
		/// </summary>
		/// <typeparam name="T">Dependencia a ser Iniciada</typeparam>
		public static void IniciarDependencia<T>() where T : Form, new()
		{
			T App = new();
			App.Show();
		}

		/// <summary>
		/// Inicia uma aplicação Onde o Tipo deve derivar de Form
		/// </summary>
		/// <typeparam name="T">Tipo de Janela que será iniciado</typeparam>
		public static void IniciarApp<T>() where T : Form, new()
		{
			T App = new();
			App.Show();
			// Quando uma nova Instância for Iniciada Escondemos a principal
			LabsMainApp.App.Hide();
			//Deixamos um evento atribuido para que quando a janela for fechada, a principal retorne.
			App.FormClosed += AppClosed;
		}
		//Método De Retorno Caso a janela seja fechada (Chamada somente nas janelas instanciadas pelo IniciarApp)
		private static void AppClosed(object? sender, FormClosedEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (sender is Form App)
				{
					App.FormClosed -= AppClosed;
					LabsMainApp.App.Show();
				}
			}
		}
	}
}

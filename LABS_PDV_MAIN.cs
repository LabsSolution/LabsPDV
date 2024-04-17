//Importa��o das Janelas
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
			//Inicializamos a lista de Aplica��es
		}

		/// <summary>
		/// Inicia uma dependencia em cima da janela que a requisitou (N�o Esconde a janela anterior)
		/// Caso queira iniciar uma janela Diretamente como foco use o m�todo IniciarApp para melhor performance
		/// </summary>
		/// <typeparam name="T">Dependencia a ser Iniciada</typeparam>
		public static void IniciarDependencia<T>() where T : Form, new()
		{
			T App = new();
			App.Show();
		}

		/// <summary>
		/// Inicia uma aplica��o Onde o Tipo deve derivar de Form
		/// </summary>
		/// <typeparam name="T">Tipo de Janela que ser� iniciado</typeparam>
		public static void IniciarApp<T>() where T : Form, new()
		{
			T App = new();
			App.Show();
			// Quando uma nova Inst�ncia for Iniciada Escondemos a principal
			LabsMainApp.App.Hide();
			//Deixamos um evento atribuido para que quando a janela for fechada, a principal retorne.
			App.FormClosed += AppClosed;
		}
		//M�todo De Retorno Caso a janela seja fechada (Chamada somente nas janelas instanciadas pelo IniciarApp)
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

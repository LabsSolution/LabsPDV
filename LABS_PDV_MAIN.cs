//Importa��o das Janelas
using Labs.Janelas.LabsEstoque;
//
namespace Labs
{

	internal static class LABS_PDV_MAIN
	{
		//Controle de Inst�ncias (Endere�amento de mem�ria)
		private static Dictionary<string, Form> RunningApps = new();

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
		public static T IniciarDependencia<T>() where T : Form, new()
		{
			T App;
			// Verifica se a aplica��o j� est� rodando
			if (RunningApps.TryGetValue(typeof(T).Name, out Form? existingApp))
			{
				App = existingApp as T;
			}
			else
			{
				// Cria uma nova inst�ncia da aplica��o se ela ainda n�o estiver rodando
				App = new T();
				RunningApps.Add(typeof(T).Name, App);
			}

			// Verifica se a aplica��o est� descartada
			if (App.IsDisposed)
			{
				// Cria uma nova inst�ncia da aplica��o se a antiga estiver descartada
				App = new T();
				RunningApps[typeof(T).Name] = App;
			}

			// Mostra a aplica��o
			App.Show();
			return App;
		}

		/// <summary>
		/// Inicia uma aplica��o Onde o Tipo deve derivar de Form
		/// </summary>
		/// <typeparam name="T">Tipo de Janela que ser� iniciado</typeparam>
		public static T IniciarApp<T>() where T : Form, new()
		{
			T App;
			// Verifica se a aplica��o j� est� rodando
			if (RunningApps.TryGetValue(typeof(T).Name, out Form? existingApp))
			{
				App = existingApp as T;
			}
			else
			{
				// Cria uma nova inst�ncia da aplica��o se ela ainda n�o estiver rodando
				App = new T();
				RunningApps.Add(typeof(T).Name, App);
			}

			// Verifica se a aplica��o est� descartada
			if (App.IsDisposed)
			{
				// Cria uma nova inst�ncia da aplica��o se a antiga estiver descartada
				App = new T();
				RunningApps[typeof(T).Name] = App;
			}

			// Mostra a aplica��o
			App.Show();

			// Quando uma nova Inst�ncia for Iniciada Escondemos a principal
			LabsMainApp.App.Hide();
			App.FormClosed += AppClosed;
			return App;
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

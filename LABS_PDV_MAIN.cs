//Importação das Janelas
using Labs.Janelas.LabsEstoque;
//
namespace Labs
{

	internal static class LABS_PDV_MAIN
	{
		//Controle de Instâncias (Endereçamento de memória)
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
			//Inicializamos a lista de Aplicações
		}

		/// <summary>
		/// Inicia uma dependencia em cima da janela que a requisitou (Não Esconde a janela anterior)
		/// Caso queira iniciar uma janela Diretamente como foco use o método IniciarApp para melhor performance
		/// </summary>
		/// <typeparam name="T">Dependencia a ser Iniciada</typeparam>
		public static T IniciarDependencia<T>() where T : Form, new()
		{
			T App;
			// Verifica se a aplicação já está rodando
			if (RunningApps.TryGetValue(typeof(T).Name, out Form? existingApp))
			{
				App = existingApp as T;
			}
			else
			{
				// Cria uma nova instância da aplicação se ela ainda não estiver rodando
				App = new T();
				RunningApps.Add(typeof(T).Name, App);
			}

			// Verifica se a aplicação está descartada
			if (App.IsDisposed)
			{
				// Cria uma nova instância da aplicação se a antiga estiver descartada
				App = new T();
				RunningApps[typeof(T).Name] = App;
			}

			// Mostra a aplicação
			App.Show();
			return App;
		}

		/// <summary>
		/// Inicia uma aplicação Onde o Tipo deve derivar de Form
		/// </summary>
		/// <typeparam name="T">Tipo de Janela que será iniciado</typeparam>
		public static T IniciarApp<T>() where T : Form, new()
		{
			T App;
			// Verifica se a aplicação já está rodando
			if (RunningApps.TryGetValue(typeof(T).Name, out Form? existingApp))
			{
				App = existingApp as T;
			}
			else
			{
				// Cria uma nova instância da aplicação se ela ainda não estiver rodando
				App = new T();
				RunningApps.Add(typeof(T).Name, App);
			}

			// Verifica se a aplicação está descartada
			if (App.IsDisposed)
			{
				// Cria uma nova instância da aplicação se a antiga estiver descartada
				App = new T();
				RunningApps[typeof(T).Name] = App;
			}

			// Mostra a aplicação
			App.Show();

			// Quando uma nova Instância for Iniciada Escondemos a principal
			LabsMainApp.App.Hide();
			App.FormClosed += AppClosed;
			return App;
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

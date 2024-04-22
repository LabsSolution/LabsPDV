//Importação das Janelas
using Labs.Janelas.LabsEstoque;
using Labs.LABS_PDV;
using static Dapper.SqlMapper;
using System.Security.AccessControl;
using System.Security.Cryptography;
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
			LabsMainApp labsMainApp = new();
			labsMainApp.Resize += OnAppSizeChange;
			labsMainApp.FormClosed += AppClosed;
			INIT(labsMainApp);
		}
		static void INIT(LabsMainApp labsMainApp)
		{
			//Inicializamos as dependências obrigatórias
			//Verifica a pasta config (se não tiver, vai criar uma)
			bool init = JsonManager.InitializeJsonManager();
			if (!init) 
			{ 
				//O Init só é falso caso dê algo de errado na pasta config e não seja possível inicializar
				var r = Modais.MostrarErro("Não Foi Possivel Iniciar o Sistema Por Conter Erros Críticos!");
				if(r == DialogResult.Ignore) { INIT(labsMainApp); return; }
				if(r == DialogResult.Retry) { INIT(labsMainApp); return; }
				return; 
			}
			// Somente após o sistema verificar tudo é que inicializamos.
			Application.Run(labsMainApp);
			//
		}


		//EVENTOS//
		//Previne o Cliente de Minimizar o sistema
		private static void OnAppSizeChange(object? sender, EventArgs e)
		{
			if (sender is Form App)
			{
				App.WindowState = FormWindowState.Maximized;
			}
		}
		//Método De Retorno Caso a janela seja fechada (Chamada somente nas janelas instanciadas pelo IniciarApp)
		private static void AppClosed(object? sender, FormClosedEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (sender is Form App)
				{
					App.FormClosed -= AppClosed;
					App.Resize -= OnAppSizeChange;
					LabsMainApp.App.Show();
				}
			}
		}
		//Método De Retorno caso a janela seja escondida (Também chamada somente nas janelas Instanciadas pelo InicarApp)
		//Mas nesse caso, somente quando o parametro "Persistente for ativo"
		private static void AppHidden(object? sender, EventArgs e)
		{
			if (sender is Form App)
			{
				if (!App.Visible)
				{
					App.VisibleChanged -= AppHidden;
					App.Resize -= OnAppSizeChange;
					LabsMainApp.App.Show();
				}
			}
		}
		/// <summary>
		/// Inicia uma dependencia em cima da janela que a requisitou (Não Esconde a janela anterior)
		/// Caso queira iniciar uma janela Diretamente como foco use o método IniciarApp para melhor performance
		/// </summary>
		/// <typeparam name="T">Dependencia a ser Iniciada</typeparam>
		/// <param name="SempreNoTopo">Mostrar Janela sempre no topo ou não</param>
		public static T IniciarDependencia<T>(Action<T> config = null!, bool SempreNoTopo = true) where T : Form, new()
		{
			T? App;
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
			config?.Invoke(App);
			// Mostra a aplicação
			if (SempreNoTopo) { App.ShowDialog(); return App; }
			App.Show();
			return App;
		}

		/// <summary>
		/// Inicia uma aplicação Onde o Tipo deve derivar de Form
		/// </summary>
		/// <typeparam name="T">Tipo de Janela que será iniciado</typeparam>
		/// <param name="Persistente">Define se a janela permanece carregada até ser forçado seu fechamento ou não</param>
		public static T IniciarApp<T>(bool Persistente = false) where T : Form, new()
		{
			T? App;
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
			//Aqui lidamos com qual evento queremos chamar (Dependendo do parametro de persistência)
			//
			// Aqui atrelamos os dois eventos porque em algum momento a janela será realmente fechada;
			//
			if (Persistente) { App.VisibleChanged += AppHidden; App.FormClosed += AppClosed; }
			if (!Persistente) { App.FormClosed += AppClosed; }
			App.Resize += OnAppSizeChange; // O evento de SizeChange é global para qualquer janela;
			return App; // Após isso tudo, retornamos a janela
		}
	}
}

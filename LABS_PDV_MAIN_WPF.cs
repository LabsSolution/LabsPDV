//Importação das Janelas
using Labs.Janelas.LabsEstoque;
using Labs.LABS_PDV;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Configuration;
using Labs.Janelas.Configuracoes.Dependencias;
using Labs.Janelas.LabsPDV.Dependencias;
using Labs.Janelas.LabsPDV;
using static Labs.LABS_PDV.Modelos;
using System.Windows;
using System.Net.NetworkInformation;
using Application = System.Windows.Application;
//
namespace Labs
{

	internal static class LABS_PDV_MAIN_WPF
	{
		//Controle de Instâncias (Endereçamento de memória)
		private static Dictionary<string, Window> RunningApps = new();
		//
		public static string TradeMark = "© Lab Soluções © ";
		//
		public static string LabsCloudDataBaseConnectionURI = "mongodb+srv://labscentral:solution2024@labs-central.vqvqvje.mongodb.net/";
		//
		// Acessores Públicos para a database do cliente
		public static string ClientDataBase = null!; // Nome da database do cliente
		public static string CloudDataBaseConnectionURI = null!;
		public static string LocalDataBaseConnectionURI = null!;
		//
		//
		/// <summary>
		/// Objeto de Controle Cliente Labs
		/// </summary>
		//Estamos iniciando aqui por conta de Desenvolvimento
		public static Cliente Cliente { get; private set; } = new("0029310",true,true,true); // aqui testamos as configs
        //
        public static Application AppInitializer = new();
        /// <summary>
		/// Propriedade para verificar a conexão com a internet
		/// </summary>
		public static Task<PingReply> CheckInternet { get { return new Ping().SendPingAsync("www.google.com"); } }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main() // Desabilitado Por Enquanto
		{	
			//A Criptografia é algo essencial para a segurança dos nossos clientes!
			// Politica LGPD
			//Descriptografa a database local
			if(LabsCripto.Decript("L_Data",out string LDecripted)) { LocalDataBaseConnectionURI = LDecripted; }
			//Descriptografa a Database Remota
			if(LabsCripto.Decript("C_Data",out string CDecripted)) { CloudDataBaseConnectionURI = CDecripted; }
			//Descriptografa o nome da Database da empresa
			if(LabsCripto.Decript("N_Data",out string NDecripted)) { ClientDataBase = NDecripted; }
			//
			LabsMainAppWPF App = new(); // Altere esse campo para modificar a primeira janela a ser aberta (Utilizar somente para debug)             //
            //App.SizeChanged += OnAppSizeChange;
            INIT(App);
        }
        static void INIT<T>(T App) where T : Window
		{
			//Inicializamos as dependências obrigatórias
			//Verifica a pasta config (se não tiver, vai criar uma)
			bool init = JsonManager.InitializeJsonManager();
			if (!init) 
			{ 
				//O Init só é falso caso dê algo de errado na pasta config e não seja possível inicializar
				var r = Modais.MostrarErro("Não Foi Possivel Iniciar o Sistema Por Conter Erros Críticos!");
				if(r == MessageBoxResult.OK) { INIT(App); return; }
				return; 
			}
			// Somente após o sistema verificar tudo é que inicializamos
			AppInitializer.Run(App);
            //
        }
        //
        //EVENTOS//
        //Previne o Cliente de Minimizar o sistema
        private static void OnAppSizeChange(object? sender, SizeChangedEventArgs e)
		{
			if (sender is Window App)
			{
				App.WindowState = WindowState.Maximized;
			}
		}
		//Método De Retorno Caso a janela seja fechada (Chamada somente nas janelas instanciadas pelo IniciarApp)
		private static void AppClosed(object? sender, EventArgs e)
		{
			if (sender is Window App)
			{
				App.Closed -= AppClosed;
				App.SizeChanged -= OnAppSizeChange;
                //
                LabsMainAppWPF.App?.Show();
            }
		}
		//Método De Retorno caso a janela seja escondida (Também chamada somente nas janelas Instanciadas pelo InicarApp)
		//Mas nesse caso, somente quando o parametro "Persistente for ativo"
		private static void AppHidden(object? sender, DependencyPropertyChangedEventArgs e)
		{
			if (sender is Window App)
			{
				if (!App.IsVisible)
				{
					App.IsVisibleChanged -= AppHidden;
					App.SizeChanged -= OnAppSizeChange;
                    //
                    LabsMainAppWPF.App?.Show();
                }
			}
		}
		//
		private static void DepAppClosed(object? sender, EventArgs e)
		{
			if (sender is Window App)
			{
			    App.IsVisibleChanged -= AppHidden;
			    App.SizeChanged -= OnAppSizeChange;
			    //
			}
        }
		//
		private static void DepAppHidden(object? sender, DependencyPropertyChangedEventArgs e)
		{
            if (sender is Window App)
            {
                if (!App.IsVisible)
                {
                    App.IsVisibleChanged -= AppHidden;
                    App.SizeChanged -= OnAppSizeChange;
                }
            }
        }
		/// <summary>
		/// Inicia uma dependencia em cima da janela que a requisitou (Não Esconde a janela anterior)
		/// Caso queira iniciar uma janela Diretamente como foco use o método IniciarApp para melhor performance
		/// </summary>
		/// <typeparam name="T">Dependencia a ser Iniciada</typeparam>
		/// <param name="SempreNoTopo">Mostrar Janela sempre no topo ou não</param>
		public static T IniciarDependencia<T>(Action<T> config = null!, bool SempreNoTopo = true, bool BackgroundImage = false) where T : Window, new()
		{
			T? App;
			// Verifica se a aplicação já está rodando
			if (RunningApps.TryGetValue(typeof(T).Name, out Window? existingApp))
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
			if (App?.IsActive == false)
			{
				// Cria uma nova instância da aplicação se a antiga estiver descartada
				App = new T();
				RunningApps[typeof(T).Name] = App;
			}
			//
			if(App != null) { config?.Invoke(App); }
			// Mostra a aplicação
			//
            if (SempreNoTopo) 
			{ 
				//
				if(App != null)
				{
                    App.Topmost = true;
                    App.Closed += DepAppClosed;
                    App.IsVisibleChanged += DepAppHidden;
                    App.ShowDialog();
                    //
                    return App;
                }
			}
            //Retornamos o App caso não seja nulo
			if(App != null) { App.Closed += DepAppClosed; App.Show(); return App; }
			//
			return null!;
		}

		/// <summary>
		/// Inicia uma aplicação Onde o Tipo deve derivar de Form
		/// </summary>
		/// <typeparam name="T">Tipo de Janela que será iniciado</typeparam>
		/// <param name="Persistente">Define se a janela permanece carregada até ser forçado seu fechamento ou não</param>
		/// <param name="BackgroundImage">Define se a janela Carrega a imagem de Background ou não, padrão = true</param>
		public static T IniciarApp<T>(bool Persistente = false, bool BackgroundImage = false, bool AutoMaximize = true) where T : Window, new()
		{
			T? App;
			// Verifica se a aplicação já está rodando
			if (RunningApps.TryGetValue(typeof(T).Name, out Window? existingApp))
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
			if (App?.IsActive == false)
			{
				// Cria uma nova instância da aplicação se a antiga estiver descartada
				App = new T();
				RunningApps[typeof(T).Name] = App;
			}
			//

			// Quando uma nova Instância for Iniciada Escondemos a principal
			LabsMainAppWPF.App?.Hide();
			//Aqui lidamos com qual evento queremos chamar (Dependendo do parametro de persistência)
			//
			// Aqui atrelamos os dois eventos porque em algum momento a janela será realmente fechada;
			//
			if(App != null) // se não for nulo checamos os requisitos e devolvemos a janela
			{
				if (Persistente) { App.IsVisibleChanged += AppHidden;  }
				if (AutoMaximize) { App.SizeChanged += OnAppSizeChange; }
				App.Closed += AppClosed; // Global
				// Mostra a aplicação
				App.Show();
				return App;
			}
            return null!; // Após isso tudo, retornamos a janela nula caso seja
		}
	}
}

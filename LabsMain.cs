//Importa��o das Janelas
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

	class LabWindow (Window window,bool IsClosed)
	{ 
		public Window Window { get; set; } = window;
		public bool IsClosed { get; set; } = IsClosed;
	}
	//
	internal static class LabsMain
	{
		//Controle de Inst�ncias (Endere�amento de mem�ria)
		private static Dictionary<string, LabWindow> RunningApps = new();
		//
		public static string TradeMark = "� Lab Solu��es � ";
		//
		public static string LabsCloudDataBaseConnectionURI = "mongodb+srv://labscentral:solution2024@labs-central.vqvqvje.mongodb.net/";
		//
		// Acessores P�blicos para a database do cliente
		public static string ClientDataBase = null!; // Nome da database do cliente
		public static string CloudDataBaseConnectionURI = null!;
		public static string LocalDataBaseConnectionURI = null!;
		//
		public static Application Application = new();
		//
		/// <summary>
		/// Objeto de Controle Cliente Labs
		/// </summary>
		//Estamos iniciando aqui por conta de Desenvolvimento
		public static Cliente Cliente { get; private set; } = new("0029310",true,true,true); // aqui testamos as configs
        /// <summary>
		/// Propriedade para verificar a conex�o com a internet
		/// </summary>
		public static Task<PingReply> CheckInternet { get { return new Ping().SendPingAsync("www.google.com"); } }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main() // Desabilitado Por Enquanto
		{	
			//A Criptografia � algo essencial para a seguran�a dos nossos clientes!
			// Politica LGPD
			//Descriptografa a database local
			if(LabsCripto.Decript("L_Data",out string LDecripted)) { LocalDataBaseConnectionURI = LDecripted; }
			//Descriptografa a Database Remota
			if(LabsCripto.Decript("C_Data",out string CDecripted)) { CloudDataBaseConnectionURI = CDecripted; }
			//Descriptografa o nome da Database da empresa
			if(LabsCripto.Decript("N_Data",out string NDecripted)) { ClientDataBase = NDecripted; }
			//
			LabsMainAppWPF App = new(); // Altere esse campo para modificar a primeira janela a ser aberta (Utilizar somente para debug)             //
            INIT(App);
        }
        static void INIT<T>(T App) where T : Window
		{
			//Inicializamos as depend�ncias obrigat�rias
			//Verifica a pasta config (se n�o tiver, vai criar uma)
			bool init = JsonManager.InitializeJsonManager();
			if (!init) 
			{ 
				//O Init s� � falso caso d� algo de errado na pasta config e n�o seja poss�vel inicializar
				var r = Modais.MostrarErro("N�o Foi Possivel Iniciar o Sistema Por Conter Erros Cr�ticos!");
				if(r == MessageBoxResult.OK) { INIT(App); return; }
				return; 
			}
			// Somente ap�s o sistema verificar tudo � que inicializamos
			Application.Run(App);
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
		//M�todo De Retorno Caso a janela seja fechada (Chamada somente nas janelas instanciadas pelo IniciarApp)
		private static void AppClosed(object? sender, EventArgs e)
		{
			if (sender is Window App)
			{
				App.Closed -= AppClosed;
				App.SizeChanged -= OnAppSizeChange;
                //se a janela existe no mapeamento, dizemos que ela foi fechada
                //
                if (RunningApps.TryGetValue(App.Name, out _)) { RunningApps[App.Name].IsClosed = true; }
				//
                LabsMainAppWPF.App?.Show();
            }
		}
		//M�todo De Retorno caso a janela seja escondida (Tamb�m chamada somente nas janelas Instanciadas pelo InicarApp)
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
				//se a janela existe no mapeamento, dizemos que ela foi fechada
				//
                if (RunningApps.TryGetValue(App.Name, out _)) { RunningApps[App.Name].IsClosed = true; }
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
		/// Inicia uma dependencia em cima da janela que a requisitou (N�o Esconde a janela anterior)
		/// Caso queira iniciar uma janela Diretamente como foco use o m�todo IniciarApp para melhor performance
		/// Esse M�todo Tamb�m n�o Deixa o Estado da Janela Salvo na mem�ria, uma vez que a janela for fechada � descartada.
		/// </summary>
		/// <typeparam name="T">Dependencia a ser Iniciada</typeparam>
		/// <param name="SempreNoTopo">Mostrar Janela sempre no topo ou n�o</param>
		public static T IniciarDependencia<T>(Action<T> config = null!, bool SempreNoTopo = true, bool BackgroundImage = false) where T : Window, new()
		{
			T? App = new();
			//
            // Verifica se a aplica��o j� est� rodando
            if (RunningApps.TryGetValue(App.Name, out LabWindow? existingLabWindow))
            {
				RunningApps[App.Name].Window = new T();
            }
            else
            {
                existingLabWindow = new(App, true);
                RunningApps.Add(App.Name, existingLabWindow);
			}
			//	
			if (existingLabWindow != null && App != null)
			{
                //
                // Invocamos a config antes de chamar a janela;
                config?.Invoke((existingLabWindow.Window as T)!);
                //Atrelamos eventos
                existingLabWindow.Window.Topmost = SempreNoTopo;
				existingLabWindow.Window.Closed += DepAppClosed;
				existingLabWindow.Window.IsVisibleChanged += DepAppHidden;
				//Chamamos a Janela
				existingLabWindow.Window.Show();
				// Registramos que foi Aberta(Padroniza��o)
				if (RunningApps.TryGetValue(App.Name, out _)) { RunningApps[App.Name].IsClosed = false; }
				//Retornamos a Janela
				return App;
			}
			// se nada for atendido retornamos nulo
			return null!;
		}

		/// <summary>
		/// Inicia uma aplica��o Onde o Tipo deve derivar de Form
		/// </summary>
		/// <typeparam name="T">Tipo de Janela que ser� iniciado</typeparam>
		/// <param name="Persistente">Define se a janela permanece carregada at� ser for�ado seu fechamento ou n�o</param>
		/// <param name="BackgroundImage">Define se a janela Carrega a imagem de Background ou n�o, padr�o = true</param>
		public static T IniciarApp<T>(bool Persistente = false, bool BackgroundImage = false, bool AutoMaximize = true) where T : Window, new()
		{
			T? App = new();
            // Verifica se a aplica��o j� est� rodando
            if (RunningApps.TryGetValue(App.Name, out LabWindow? existingLabWindow))
            {
                App = existingLabWindow.Window as T;
            }
            else
            {
                // Cria uma nova inst�ncia da aplica��o se ela ainda n�o estiver rodando
				existingLabWindow = new(App, true);
                RunningApps.Add(App.Name, existingLabWindow);
            }
            // Verifica se a aplica��o s� est� escondida e a janela est� ativa
			// Se o container existe
			if(existingLabWindow != null && App != null)
			{
				if (existingLabWindow.IsClosed == false)
				{
					App.Visibility = Visibility.Visible;
					return App;
				}
				else
				{
					existingLabWindow.Window = new T();
				}
                //
                // Quando uma nova Inst�ncia for Iniciada Escondemos a principal
                LabsMainAppWPF.App?.Hide();
                //Aqui lidamos com qual evento queremos chamar (Dependendo do parametro de persist�ncia)
                //
                // Aqui atrelamos os dois eventos porque em algum momento a janela ser� realmente fechada;
                //
                if (Persistente) { existingLabWindow.Window.IsVisibleChanged += AppHidden; }
				//
                if (AutoMaximize) { existingLabWindow.Window.SizeChanged += OnAppSizeChange; }
				//
                existingLabWindow.Window.Closed += AppClosed; // Global
				// Mostra a aplica��o
                existingLabWindow.Window.Show();
				// registramos que o aplicativo foi aberto
                existingLabWindow.IsClosed = false;
				//
                return App;
            }
            return null!; // Ap�s isso tudo, retornamos a janela nula caso seja
		}
	}
}
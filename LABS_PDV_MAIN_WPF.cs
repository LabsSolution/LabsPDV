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
using Application = System.Windows.Application;
//
namespace Labs
{

	internal static class LABS_PDV_MAIN_WPF
	{
		//Controle de Inst�ncias (Endere�amento de mem�ria)
		private static Dictionary<string, Window> RunningApps = new();
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
		public static Cliente Cliente { get; private set; } = null!;
        //
        static Application AppInitializer = new();
        //
        static readonly SVGParser SVGParser = new(); // Desabilitado por enquanto por motivos de performance;
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
			LabsMainAppWPF App = new(); // Altere esse campo para modificar a primeira janela a ser aberta (Utilizar somente para debug)
			//
			//App.SizeChanged += OnAppSizeChange;
            App.Loaded += OnAppLoad;
            INIT(App);
        }
        //
        static void INIT<T>(T App) where T : Window
		{
			//Inicializamos as depend�ncias obrigat�rias
			//Verifica a pasta config (se n�o tiver, vai criar uma)
			bool init = JsonManager.InitializeJsonManager();
			if (!init) 
			{ 
				//O Init s� � falso caso d� algo de errado na pasta config e n�o seja poss�vel inicializar
				var r = Modais.MostrarErro("N�o Foi Possivel Iniciar o Sistema Por Conter Erros Cr�ticos!");
				if(r == DialogResult.Ignore) { INIT(App); return; }
				if(r == DialogResult.Retry) { INIT(App); return; }
				return; 
			}
			// Somente ap�s o sistema verificar tudo � que inicializamos
			AppInitializer.Run(App);
            //
        }

        private static void OnAppLoad(object? sender, EventArgs e)
        {
            if(sender is Form App)
			{
				App.BackgroundImageLayout = ImageLayout.Stretch;
				App.BackgroundImage = SVGParser.GetImageFromSVG(); ;
            }
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
                App.Loaded -= OnAppLoad;
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
                    App.Loaded -= OnAppLoad;
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
			    App.Loaded -= OnAppLoad;
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
					App.Loaded -= OnAppLoad;
                }
            }
        }
		/// <summary>
		/// Inicia uma dependencia em cima da janela que a requisitou (N�o Esconde a janela anterior)
		/// Caso queira iniciar uma janela Diretamente como foco use o m�todo IniciarApp para melhor performance
		/// </summary>
		/// <typeparam name="T">Dependencia a ser Iniciada</typeparam>
		/// <param name="SempreNoTopo">Mostrar Janela sempre no topo ou n�o</param>
		public static T IniciarDependencia<T>(Action<T> config = null!, bool SempreNoTopo = true, bool BackgroundImage = false) where T : Window, new()
		{
			T? App;
			// Verifica se a aplica��o j� est� rodando
			if (RunningApps.TryGetValue(typeof(T).Name, out Window? existingApp))
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
			if (App?.IsActive == false)
			{
				// Cria uma nova inst�ncia da aplica��o se a antiga estiver descartada
				App = new T();
				RunningApps[typeof(T).Name] = App;
			}
			config?.Invoke(App);
			// Mostra a aplica��o
			//
            if (SempreNoTopo) 
			{ 
				if (BackgroundImage == true)  { App.Loaded += OnAppLoad; } 
				//
				App.Topmost = true;
				App.Closed += DepAppClosed; 
				App.IsVisibleChanged += DepAppHidden; 
				App.ShowDialog();
				//
				return App; 
			}
			//Retornamos o App
			if (BackgroundImage == true) { App.Loaded += OnAppLoad; }
			App.Closed += DepAppClosed;
			App.Show();
			return App;
		}

		/// <summary>
		/// Inicia uma aplica��o Onde o Tipo deve derivar de Form
		/// </summary>
		/// <typeparam name="T">Tipo de Janela que ser� iniciado</typeparam>
		/// <param name="Persistente">Define se a janela permanece carregada at� ser for�ado seu fechamento ou n�o</param>
		/// <param name="BackgroundImage">Define se a janela Carrega a imagem de Background ou n�o, padr�o = true</param>
		public static T IniciarApp<T>(bool Persistente = false, bool BackgroundImage = false, bool AutoMaximize = true) where T : Window, new()
		{
			T? App;
			// Verifica se a aplica��o j� est� rodando
			if (RunningApps.TryGetValue(typeof(T).Name, out Window? existingApp))
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
			if (App?.IsActive == false)
			{
				// Cria uma nova inst�ncia da aplica��o se a antiga estiver descartada
				App = new T();
				RunningApps[typeof(T).Name] = App;
			}
			//

			// Quando uma nova Inst�ncia for Iniciada Escondemos a principal
			LabsMainAppWPF.App?.Hide();
			//Aqui lidamos com qual evento queremos chamar (Dependendo do parametro de persist�ncia)
			//
			// Aqui atrelamos os dois eventos porque em algum momento a janela ser� realmente fechada;
			//
			if (Persistente) { App.IsVisibleChanged += AppHidden;  }
			if (BackgroundImage == true) { App.Loaded += OnAppLoad; }
			if (AutoMaximize) { App.SizeChanged += OnAppSizeChange; }
            App.Closed += AppClosed; // Global
            // Mostra a aplica��o
            App.Show();
            return App; // Ap�s isso tudo, retornamos a janela
		}
	}
}

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
//
namespace Labs
{

	internal static class LABS_PDV_MAIN
	{
		//Controle de Inst�ncias (Endere�amento de mem�ria)
		private static Dictionary<string, Form> RunningApps = new();
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
		//
		static readonly SVGParser SVGParser = new(); // Desabilitado por enquanto por motivos de performance;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main()
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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
			//
			LabsMainApp App = new(); // Altere esse campo para modificar a primeira janela a ser aberta (Utilizar somente para debug)
			//
			App.Resize += OnAppSizeChange;
            App.Load += OnAppLoad;
            INIT(App);
        }
        //
        static void INIT<T>(T App) where T : Form
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
            // Somente ap�s o sistema verificar tudo � que inicializamos.
            Application.Run(App);
            //
        }

        private static void OnAppLoad(object? sender, EventArgs e)
        {
            if(sender is Form App)
			{
				//App.BackgroundImageLayout = ImageLayout.Stretch;
				//App.BackgroundImage = SVGParser.GetImageFromSVG(); ;
            }
        }

        //

        //EVENTOS//
        //Previne o Cliente de Minimizar o sistema
        private static void OnAppSizeChange(object? sender, EventArgs e)
		{
			if (sender is Form App)
			{
				App.WindowState = FormWindowState.Maximized;
			}
		}
		//M�todo De Retorno Caso a janela seja fechada (Chamada somente nas janelas instanciadas pelo IniciarApp)
		private static void AppClosed(object? sender, FormClosedEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (sender is Form App)
				{
					App.FormClosed -= AppClosed;
					App.Resize -= OnAppSizeChange;
                    App.Load -= OnAppLoad;
                    App.BackgroundImage?.Dispose();
                    //
                    LabsMainApp.App.Show();
				}
			}
		}
		//M�todo De Retorno caso a janela seja escondida (Tamb�m chamada somente nas janelas Instanciadas pelo InicarApp)
		//Mas nesse caso, somente quando o parametro "Persistente for ativo"
		private static void AppHidden(object? sender, EventArgs e)
		{
			if (sender is Form App)
			{
				if (!App.Visible)
				{
					App.VisibleChanged -= AppHidden;
					App.Resize -= OnAppSizeChange;
                    App.Load -= OnAppLoad;
                    //
                    LabsMainApp.App.Show();
				}
			}
		}
		//
		private static void DepAppClosed(object? sender, FormClosedEventArgs e)
		{
			if(e.CloseReason == CloseReason.UserClosing)
			{
                if (sender is Form App)
                {
                    App.VisibleChanged -= AppHidden;
                    App.Resize -= OnAppSizeChange;
                    App.Load -= OnAppLoad;
                    App.BackgroundImage?.Dispose();
                    //
                }
            }
        }
		//
		private static void DepAppHidden(object? sender, EventArgs e)
		{
            if (sender is Form App)
            {
                if (!App.Visible)
                {
                    App.VisibleChanged -= AppHidden;
                    App.Resize -= OnAppSizeChange;
					App.Load -= OnAppLoad;
					App.Dispose();
                }
            }
        }
		/// <summary>
		/// Inicia uma dependencia em cima da janela que a requisitou (N�o Esconde a janela anterior)
		/// Caso queira iniciar uma janela Diretamente como foco use o m�todo IniciarApp para melhor performance
		/// </summary>
		/// <typeparam name="T">Dependencia a ser Iniciada</typeparam>
		/// <param name="SempreNoTopo">Mostrar Janela sempre no topo ou n�o</param>
		public static T IniciarDependencia<T>(Action<T> config = null!, bool SempreNoTopo = true, bool BackgroundImage = false) where T : Form, new()
		{
			T? App;
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
			config?.Invoke(App);
			// Mostra a aplica��o
			//
            if (SempreNoTopo) 
			{ 
				if (BackgroundImage == true)  { App.Load += OnAppLoad; } 
				//
				App.FormClosed += DepAppClosed; 
				App.VisibleChanged += DepAppHidden; 
				App.ShowDialog(); 
				//
				return App; 
			}
			//Retornamos o App
			if (BackgroundImage == true) { App.Load += OnAppLoad; }
			App.FormClosed += DepAppClosed;
			App.Show();
			return App;
		}

		/// <summary>
		/// Inicia uma aplica��o Onde o Tipo deve derivar de Form
		/// </summary>
		/// <typeparam name="T">Tipo de Janela que ser� iniciado</typeparam>
		/// <param name="Persistente">Define se a janela permanece carregada at� ser for�ado seu fechamento ou n�o</param>
		/// <param name="BackgroundImage">Define se a janela Carrega a imagem de Background ou n�o, padr�o = true</param>
		public static T IniciarApp<T>(bool Persistente = false, bool BackgroundImage = false) where T : Form, new()
		{
			T? App;
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
			//

			// Quando uma nova Inst�ncia for Iniciada Escondemos a principal
			LabsMainApp.App.Hide();
			//Aqui lidamos com qual evento queremos chamar (Dependendo do parametro de persist�ncia)
			//
			// Aqui atrelamos os dois eventos porque em algum momento a janela ser� realmente fechada;
			//
			if (Persistente) { App.VisibleChanged += AppHidden;  }
			if (BackgroundImage == true) { App.Load += OnAppLoad; }
			App.Resize += OnAppSizeChange; // O evento de SizeChange � global para qualquer janela;
            App.FormClosed += AppClosed; // Global
            // Mostra a aplica��o
            App.Show();
            return App; // Ap�s isso tudo, retornamos a janela
		}
	}
}

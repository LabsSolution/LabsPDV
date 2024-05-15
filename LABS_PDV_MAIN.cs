//Importação das Janelas
using Labs.Janelas.LabsEstoque;
using Labs.LABS_PDV;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Configuration;
using Labs.Janelas.Configuracoes.Dependencias;
using Labs.Janelas.LabsPDV.Dependencias;
using Labs.Janelas.LabsPDV;
//
namespace Labs
{

	internal static class LABS_PDV_MAIN
	{
		//Controle de Instâncias (Endereçamento de memória)
		private static Dictionary<string, Form> RunningApps = new();
		// Acessores Públicos para a database
		public static string CloudDataBase = null!;
		public static string LocalDataBase = null!;
		//
		static SVGParser Parser = new();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main()
		{	
			//A Criptografia é algo essencial para a segurança dos nossos clientes!
			// Politica LGPD
			//Descriptografa a database local
			if(LabsCripto.Decript("L_Data",out string LDecripted)) { LocalDataBase = LDecripted; }
			//Descriptograda a Datavbase Remota
			if(LabsCripto.Decript("C_Data",out string CDecripted)) { CloudDataBase = CDecripted; }
			//
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
			//
			LabsMainApp App = new(); // Altere esse campo para modificar a primeira janela a ser aberta (Utilizar somente para debug)
			//
			//svgtest App = new();
			App.Resize += OnAppSizeChange;
            App.Load += OnAppLoad;
            INIT(App);
        }
        //
        static void INIT<T>(T App) where T : Form
		{
			//Inicializamos as dependências obrigatórias
			//Verifica a pasta config (se não tiver, vai criar uma)
			bool init = JsonManager.InitializeJsonManager();
			if (!init) 
			{ 
				//O Init só é falso caso dê algo de errado na pasta config e não seja possível inicializar
				var r = Modais.MostrarErro("Não Foi Possivel Iniciar o Sistema Por Conter Erros Críticos!");
				if(r == DialogResult.Ignore) { INIT(App); return; }
				if(r == DialogResult.Retry) { INIT(App); return; }
				return; 
			}
            // Somente após o sistema verificar tudo é que inicializamos.
            Application.Run(App);
            //
        }

        private static void OnAppLoad(object? sender, EventArgs e)
        {
            if(sender is Form App)
			{
				App.BackgroundImageLayout = ImageLayout.Stretch;
				App.BackgroundImage = Parser.GetImageFromSVG(); ;
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
		//Método De Retorno Caso a janela seja fechada (Chamada somente nas janelas instanciadas pelo IniciarApp)
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
		/// Inicia uma dependencia em cima da janela que a requisitou (Não Esconde a janela anterior)
		/// Caso queira iniciar uma janela Diretamente como foco use o método IniciarApp para melhor performance
		/// </summary>
		/// <typeparam name="T">Dependencia a ser Iniciada</typeparam>
		/// <param name="SempreNoTopo">Mostrar Janela sempre no topo ou não</param>
		public static T IniciarDependencia<T>(Action<T> config = null!, bool SempreNoTopo = true, bool BackgroundImage = true) where T : Form, new()
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
		/// Inicia uma aplicação Onde o Tipo deve derivar de Form
		/// </summary>
		/// <typeparam name="T">Tipo de Janela que será iniciado</typeparam>
		/// <param name="Persistente">Define se a janela permanece carregada até ser forçado seu fechamento ou não</param>
		/// <param name="BackgroundImage">Define se a janela Carrega a imagem de Background ou não, padrão = true</param>
		public static T IniciarApp<T>(bool Persistente = false, bool BackgroundImage = true) where T : Form, new()
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
			//

			// Quando uma nova Instância for Iniciada Escondemos a principal
			LabsMainApp.App.Hide();
			//Aqui lidamos com qual evento queremos chamar (Dependendo do parametro de persistência)
			//
			// Aqui atrelamos os dois eventos porque em algum momento a janela será realmente fechada;
			//
			if (Persistente) { App.VisibleChanged += AppHidden;  }
			if (BackgroundImage == true) { App.Load += OnAppLoad; }
			App.Resize += OnAppSizeChange; // O evento de SizeChange é global para qualquer janela;
            App.FormClosed += AppClosed; // Global
            // Mostra a aplicação
            App.Show();
            return App; // Após isso tudo, retornamos a janela
		}
	}
}

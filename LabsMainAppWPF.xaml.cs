using Labs.Janelas.Configuracoes;
using Labs.Janelas.LabsEstoque;
using Labs.Janelas.LabsEstoque.Dependencias;
using Labs.Janelas.LabsPDV;
using Labs.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Labs
{
    /// <summary>
    /// Lógica interna para Window1.xaml
    /// </summary>
    public partial class LabsMainAppWPF : Window
    {
        //Referencia de Instancia
        public static LabsMainAppWPF App { get; private set; } = null!;
        //
        public static MotorDeBusca MotorDeBusca = new ();
        // Esses Campos são referentes as configurações gerais
        public static string NomeEmpresa { get; private set; } = "N/A";
        public static string EnderecoEmpresa { get; private set; } = "N/A";
        // Esses campos serão setados ao carregar as impressoras configuradas
        public static string ImpressoraTermica { get; private set; } = null!;
        public static string ImpressoraA4 { get; private set; } = null!;
        /// <summary>
        /// Habilitado Somente Caso Algum Erro Crítico de Inicialização for Detectado
        /// </summary>
        public static bool ModoSegurança { get; private set; } = false;
        //
        public static bool IsDatabaseConnected { get; private set; }
        //
        public static bool IsConnectedToInternet { get; private set; } = true;
        //
        public LabsMainAppWPF()
        {
            InitializeComponent();
            //Antes mesmo de inicializar, Chamamos o método INIT para configurar tudo
            LabsMain.INIT();
            //
            if (App == null) { App = this; }
            //Caso tenha uma instância rodando, fechamos esta janela e jogamos um erro
            //Dando a informação para que se o erro persistir, entrar em contato com o suporte técnico.
            else { this.Close(); Modais.MostrarAviso("ERRO-800 \n UMA INSTÂNCIA DO APLICATIVO JÁ ESTÁ EM EXECUCÃO\n Caso o erro persista recomendamos entrar em contato com o suporte."); }
            //Carrega as Configs/
            LoadConfigs();
			//
			LabsMain.Timer.Tick += InternalTimer;
            //
        }
        //
		private void InternalTimer(object? sender, EventArgs e)
		{
            //Atualiza a hora e data
            HoraLabel.Text = DateTime.Now.ToString("HH:mm:ss");
            DataLabel.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //
            DataBaseAndInternetChecker();
		}
		//
        private async void DataBaseAndInternetChecker()
        {
            // Faz checagens a cada segundo
            bool Internet = Unimake.Net.Utility.HasInternetConnection();
            // Aqui verificamos se o Testador Retornou um valor diferente
            if(Internet != IsConnectedToInternet) 
            { 
                if(Internet == false) { Modais.MostrarAviso("A Sua Máquina Está sem Conexão com a Internet!\nVocê será notificado assim que a conexão for reestabelecida."); }
                if(Internet == true) { Modais.MostrarInfo("A Conexão com a Internet foi Reestabelecida!"); }
                IsConnectedToInternet = Internet;
            }
            //
			await VerifyDataBases();
		}
		//
		async Task<bool> VerifyDataBases()
        {
            var bools = await CloudDataBase.CheckDataBaseConnection();
            IsDatabaseConnected = bools[0];
            bool planoCloud = LabsMain.Cliente.PossuiPlanoCloud;
            //
            IndicadorPlanoCloud.Visibility = planoCloud? Visibility.Collapsed: Visibility.Visible;
            //
            IndicadorDatabaseLocal.Fill = bools[0] ? new SolidColorBrush(Color.FromArgb(255,80,255,90)) : new SolidColorBrush(Color.FromArgb(255,225,80,80));
            IndicadorDatabaseCloud.Fill = bools[1] ? new SolidColorBrush(Color.FromArgb(255,80,255,90)) : new SolidColorBrush(Color.FromArgb(255,225,80,80));
            IndicadorDatabaseLabs.Fill = bools[2] ? new SolidColorBrush(Color.FromArgb(255,80,255,90)) : new SolidColorBrush(Color.FromArgb(255,225,80,80));
            //
            LocalDatabasePanel.ToolTip = bools[0] ? "Conectado" : "Sem Conexão";
            CloudDatabasePanel.ToolTip = bools[1] ? "Conectado" : "Sem Conexão";
            LabsDatabasePanel.ToolTip = bools[2] ? "Conectado" : "Sem Conexão";
            //
            return IsDatabaseConnected;
        }
        //
        private void LoadConfigs()
        {
            //Aqui Puxamos as Impressoras Configuradas Para uso do sistema (Configuradas pelo painel de Configs);
            // o valor vai ser retornado como Null caso não tenha nenhuma impressora configurada!
            ImpressoraTermica = LabsConfigs.GetConfigValue("ImpressoraTermica");
            ImpressoraA4 = LabsConfigs.GetConfigValue("ImpressoraA4");
            //Configs Gerais
            NomeEmpresa = LabsConfigs.GetConfigValue("NomeEmpresa");
            EnderecoEmpresa = LabsConfigs.GetConfigValue("EnderecoEmpresa");
            //
        }
        //
        private void OnLabsMainAppLoad(object sender, RoutedEventArgs e)
        {
            SetModoSeguranca();
        }
        //
        private async void SetModoSeguranca()
        {
			if (!await VerifyDataBases()) { ModoSegurança = true; Modais.MostrarAviso("MODO DE SEGURANÇA HABILITADO!\nPara Sair Desse Modo, Os Conflitos Devem ser Resolvidos\ne Logo Após o Sistema Deve Ser Reiniciado!"); return; }
            DataBaseAndInternetChecker();
            // Desabilitado somente para debug
			VerificacoesPreventivas();
		}
		//
		static async void VerificacoesPreventivas()
        {
            var JDC = GerenciadorPDV.Initiate("Sincronizando Banco de Dados...");
            await GerenciadorPDV.VerificarEstoque(JDC);
            await Task.Delay(3000);
            //
            if (!IsConnectedToInternet)
            {
                Modais.MostrarAviso("Não foi possível iniciar o espelhamento de estoque!\n\nMotivo: Sem Conexão com a Internet.\n\nVocê será notificado assim que a conexão for reestabelecida.");
				await GerenciadorPDV.Terminate(JDC);
				return;
            }
            if (!IsDatabaseConnected) { Modais.MostrarAviso("Sua Máquina está sem acesso ao Banco de dados remoto.\n" +
                "Iniciando com acesso restrito ao banco de dados local\n" +
                "Caso a conexão com o banco de dados remoto seja recuperada você será notificado(a)");
				await Task.Delay(3000);
				await GerenciadorPDV.Terminate(JDC);
				return;
            }
            //Só queremos garantir que espelhamos os itens para a cloud
            await CloudDataBaseSync.SyncDatabase(JDC,true);
            await Task.Delay(3000);
            await GerenciadorPDV.Terminate(JDC);
        }
        //
        //
        private void OnLabsEstoqueClick(object sender, RoutedEventArgs e)
        {
            //
            //if (ModoSegurança) { Modais.MostrarAviso("Sem Conexão Primária com o Banco de Dados!\nSe o problema persistir, entre em contato com o nosso suporte."); return; }
            //
            MotorDeBusca.RealizarIndexacaoDosProdutos();
            
            LabsMain.IniciarApp<LabsEstoqueWPF>(true,false,true);
        }

        private void OnLabsPDVClick(object sender, RoutedEventArgs e)
        {
            if (ModoSegurança) { Modais.MostrarAviso("Sem Conexão Primária com o Banco de Dados!\nSe o problema persistir, entre em contato com o nosso suporte."); return; }
            LabsMain.IniciarApp<LabsPDVWPF>(true,false,true);
            //t();
        }

        private async void t()
        {
            await MotorDeBusca.ProcurarProduto("bishito");
        }

        private void OnLabsConfigClick(object sender, RoutedEventArgs e)
        {
            LabsMain.IniciarApp<LabsConfigWPF>(true,false,false);
        }

        private void OnLabsSairClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Labs.App.Current?.Shutdown();
        }
        //
    }
}

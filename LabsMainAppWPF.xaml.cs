using Labs.Janelas.Configuracoes;
using Labs.Janelas.LabsEstoque;
using Labs.Janelas.LabsPDV;
using Labs.LABS_PDV;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static int QMDP { get; private set; } = -1;
        //
        // Esses campos serão setados ao carregar as impressoras configuradas
        public static string ImpressoraTermica { get; private set; } = null!;
        public static string ImpressoraA4 { get; private set; } = null!;
        /// <summary>
        /// Habilitado Somente Caso Algum Erro Crítico de Inicialização for Detectado
        /// </summary>
        public static bool ModoSegurança { get; private set; } = false;
        //
        public LabsMainAppWPF()
        {
            InitializeComponent();
            if (App == null) { App = this; }
            //Caso tenha uma instância rodando, fechamos esta janela e jogamos um erro
            //Dando a informação para que se o erro persistir, entrar em contato com o suporte técnico.
            else { this.Close(); Modais.MostrarAviso("ERRO-800 \n UMA INSTÂNCIA DO APLICATIVO JÁ ESTÁ EM EXECUCÃO\n Caso o erro persista recomendamos entrar em contato com o suporte."); }
            //Carrega as Configs/
            LoadConfigs();
            //TEMPORÁRIO
            LABS_PDV_MAIN.LabsCloudDataBaseConnectionURI = LABS_PDV_MAIN_WPF.LabsCloudDataBaseConnectionURI;
            LABS_PDV_MAIN.CloudDataBaseConnectionURI = LABS_PDV_MAIN_WPF.CloudDataBaseConnectionURI;
            LABS_PDV_MAIN.LocalDataBaseConnectionURI = LABS_PDV_MAIN_WPF.LocalDataBaseConnectionURI;
            LABS_PDV_MAIN.ClientDataBase = LABS_PDV_MAIN_WPF.ClientDataBase;
        }
        //
        bool VerifyDataBases()
        {
            bool canProceed = true;
            if (!CloudDataBase.CheckDataBaseConnection(out bool LocalOK, out bool CloudOK, out bool LabsCloudOK))
            {
                Modais.MostrarErro("ERRO CRÍTICO\nNão Foi Possivel Estabelecer Conexão com os Bancos de Dados!");
                LabsPDVButton.IsEnabled = false;
                LabsEstoqueButton.IsEnabled = false;
                canProceed = false;
            }
            return canProceed;
        }
        //
        private void LoadConfigs()
        {
            //Aqui Lemos as Configs do App.Config;
            if (Utils.TryParseToInt(LabsConfigs.GetConfigValue("QMDP"), out int value))
            {
                QMDP = value;
            }
            //Aqui Puxamos as Impressoras Configuradas Para uso do sistema (Configuradas pelo painel de Configs);
            // o valor vai ser retornado como Null caso não tenha nenhuma impressora configurada!
            ImpressoraTermica = LabsConfigs.GetConfigValue("ImpressoraTermica");
            ImpressoraA4 = LabsConfigs.GetConfigValue("ImpressoraA4");
            //
        }
        //
        static async void VerificacoesPreventivas()
        {
            await GerenciadorPDV.VerificarEstoque(QMDP);
        }
        //
        private void OnLabsEstoqueClick(object sender, RoutedEventArgs e)
        {
            LABS_PDV_MAIN_WPF.IniciarApp<LabsEstoqueWPF>(true,false,true);
        }

        private void OnLabsPDVClick(object sender, RoutedEventArgs e)
        {
            LABS_PDV_MAIN_WPF.IniciarApp<LabsPDVWPF>(true,false,true);
        }

        private void OnLabsConfigClick(object sender, RoutedEventArgs e)
        {
            LABS_PDV_MAIN_WPF.IniciarApp<LabsConfigWPF>(true,false,false);
        }

        private void OnLabsSairClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnLabsMainAppLoad(object sender, RoutedEventArgs e)
        {
            //Quando forem repassadas para wpf reativar

            //if (!VerifyDataBases()) { ModoSegurança = true; Modais.MostrarAviso("MODO DE SEGURANÇA HABILITADO!\nPara Sair Desse Modo, Os Conflitos Devem ser Resolvidos\ne Logo Após o Sistema Deve Ser Reiniciado!"); return; }
            //VerificacoesPreventivas();
        }
        //

    }
}

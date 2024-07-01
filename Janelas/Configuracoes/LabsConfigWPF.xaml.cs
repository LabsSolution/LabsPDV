using Labs.Janelas.Configuracoes.Dependencias;
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

namespace Labs.Janelas.Configuracoes
{
    /// <summary>
    /// Lógica interna para LabsConfigWPF.xaml
    /// </summary>
    public partial class LabsConfigWPF : Window
    {
        public LabsConfigWPF()
        {
            InitializeComponent();
            if (LabsMainAppWPF.ModoSegurança) { MeioPagamentoButton.IsEnabled = false; }
        }
        private async void DataBaseConfig()
        {
            Login painel = new();
            bool isAdmin = await painel.RealizarLoginAdmin();
            if (isAdmin)
            {
                LabsMain.IniciarDependencia<DatabaseConfigWPF>(null!, true, false);
            }
        }
        //
        private void MeioPagamento_Click(object sender, RoutedEventArgs e)
        {
            LabsMain.IniciarDependencia<MeiosDePagamentoConfigWPF>(null!, true, false);
        }

        private void DatabaseConfig_Click(object sender, RoutedEventArgs e)
        {
            DataBaseConfig();
        }
		private void Impressoras_Click(object sender, RoutedEventArgs e)
		{
            LabsMain.IniciarDependencia<ImpressorasConfigWPF>(app => 
            {
                app.ListarImpressoras();
            });
		}
		private void ConfigGeral_Click(object sender, RoutedEventArgs e)
		{
            LabsMain.IniciarDependencia<ConfigGeraisWPF>(app =>
            {
                app.CarregarConfigs();
            });
		}

		private void Sair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		
	}
}

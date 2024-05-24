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
        }

        private void MeioPagamento_Click(object sender, RoutedEventArgs e)
        {
            LABS_PDV_MAIN_WPF.IniciarDependencia<MeiosDePagamentoConfigWPF>();
        }

        private void DatabaseConfig_Click(object sender, RoutedEventArgs e)
        {
            LABS_PDV_MAIN_WPF.IniciarDependencia<DatabaseConfigWPF>();
        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

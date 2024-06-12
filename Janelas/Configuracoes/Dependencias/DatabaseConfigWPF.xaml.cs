using Labs.Main;
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

namespace Labs.Janelas.Configuracoes.Dependencias
{
    /// <summary>
    /// Lógica interna para DatabaseConfigWPF.xaml
    /// </summary>
    public partial class DatabaseConfigWPF : Window
    { 
        private protected string DecriptedNomeDatabase { get; set; } = null!;
        private protected string DecriptedCloudData { get; set; } = null!;
        private protected string DecriptedLocalData { get; set; } = null!;
        //
        private protected char BulletMask = '•';
        //
        private protected string CFileName = "C_Data";
        private protected string LFileName = "L_Data";
        private protected string NFileName = "N_Data";

        public DatabaseConfigWPF()
        {
            InitializeComponent();
            //
            InitializeComponent();
            //DecryptData
            if (LabsCripto.Decript(CFileName, out string CDec)) { DecriptedCloudData = CDec; }
            if (LabsCripto.Decript(LFileName, out string LDec)) { DecriptedLocalData = LDec; }
            if (LabsCripto.Decript(NFileName, out string NDec)) { DecriptedNomeDatabase = NDec; }
            //
            LoadData();
        }
        void LoadData()
        {
            CloudDataBaseURIBox.Password = DecriptedCloudData;
            LocalDataBaseURIBox.Password = DecriptedLocalData;
            NomeDatabaseInputBox.Text = DecriptedNomeDatabase;
        }
        //GLOBAL
        private void VisualizarURICloud_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloudDataBaseURIBox.PasswordChar = char.Parse(char.ConvertFromUtf32(0x00000000)); //Constante
        }
        private void VisualizarURICloud_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CloudDataBaseURIBox.PasswordChar = BulletMask;
        }
        //LOCAL
        private void VisualizarURILocal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LocalDataBaseURIBox.PasswordChar = char.Parse(char.ConvertFromUtf32(0x00000000)); //Constante
        }

        private void VisualizarURILocal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LocalDataBaseURIBox.PasswordChar = BulletMask;
        }

        private void VisualizarURICloud_Click(object sender, RoutedEventArgs e)
        {

            Modais.MostrarInfo("AAA");
        }

        private void SalvarButton_Click(object sender, RoutedEventArgs e)
        {
            string cData = CloudDataBaseURIBox.Password;
            string lData = LocalDataBaseURIBox.Password;
            string nData = NomeDatabaseInputBox.Text;
            //
            LabsCripto.Encript(CFileName, cData);
            LabsCripto.Encript(LFileName, lData);
            LabsCripto.Encript(NFileName, nData);
            //
            Modais.MostrarInfo("Alterações Salvas com Sucesso!\nReinicie o Sistema Para que as Alterações Tenham Efeito!");
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

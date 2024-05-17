using Labs.LABS_PDV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Janelas.Configuracoes.Dependencias
{
    public partial class DatabaseConfig : Form
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
        //
        public DatabaseConfig()
        {
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
            CloudURIBox.Text = DecriptedCloudData;
            LocalURIBox.Text = DecriptedLocalData;
            NomeDatabaseBox.Text = DecriptedNomeDatabase;
        }
        //CLOUD
        private void VisualizarCloudURIButton_MouseDown(object sender, MouseEventArgs e)
        {
            CloudURIBox.PasswordChar = char.Parse(char.ConvertFromUtf32(0x00000000)); //Constante
        }
        private void VisualizarCloudURIButton_MouseUp(object sender, MouseEventArgs e)
        {
            CloudURIBox.PasswordChar = BulletMask;
        }
        //LOCAl
        private void VisualizarLocalURIButton_MouseDown(object sender, MouseEventArgs e)
        {
            LocalURIBox.PasswordChar = char.Parse(char.ConvertFromUtf32(0x00000000)); //Constante
        }
        private void VisualizarLocalURIButton_MouseUp(object sender, MouseEventArgs e)
        {
            LocalURIBox.PasswordChar = BulletMask;
        }
        //
        private void SalvarButton_Click(object sender, EventArgs e)
        {
            string cData = CloudURIBox.Text;
            string lData = LocalURIBox.Text;
            string nData = NomeDatabaseBox.Text;
            //
            LabsCripto.Encript(CFileName,cData);
            LabsCripto.Encript(LFileName,lData);
            LabsCripto.Encript(NFileName,nData);
            //
            Modais.MostrarInfo("Alterações Salvas com Sucesso!\nReinicie o Sistema Para que as Alterações Tenham Efeito!");
        }
        //
        private void SairButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}

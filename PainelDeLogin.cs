using Labs.LABS_PDV;
using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs
{
    public partial class PainelDeLogin : Form
    {


        public PainelDeLogin()
        {
            InitializeComponent();
        }

        private async void RealizarLoginButton_Click(object sender, EventArgs e)
        {
            Login login = new();
            var cliente = await login.RealizarLoginCliente();
            if (!cliente.AssinaturaAtiva) { Modais.MostrarAviso("Que Pena!\nA Sua Assinatura LABS Expirou!\nPara Retomar o Acesso Basta Renovar sua Assinatura!"); return; }
            LABS_PDV_MAIN.IniciarApp<LabsMainApp>(true);
        }

        private void SairButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

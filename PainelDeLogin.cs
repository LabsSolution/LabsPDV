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
            //Se for o primeiro login do cliente Informamos o que ele precisa fazer (Talvez abrir no site da Empresa?)
            if (!cliente.ClienteLabs) 
            { 
                Modais.MostrarInfo("Olá!, A Lab Soluções Agradece a sua Preferência!\n Para Ter Acesso ao Sistema Basta Contratar a Assinatura que" +
                $"Melhor Atende as suas Necessidades!\n{LABS_PDV_MAIN.TradeMark}");
                return;
            }
            //
            if (!cliente.AssinaturaAtiva) 
            { 
                Modais.MostrarAviso($"Que Pena!\nA Sua Assinatura LABS Expirou!\nPara Retomar o Acesso Basta Renovar sua Assinatura!\n{LABS_PDV_MAIN.TradeMark}"); 
                return; 
            }
            //
            LABS_PDV_MAIN.IniciarApp<LabsMainApp>(true);
        }

        private void SairButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

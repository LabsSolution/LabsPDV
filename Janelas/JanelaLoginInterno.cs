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

namespace Labs.Janelas
{
    public partial class JanelaLoginInterno : Form
    {
        public delegate void TentandoLogar(string Username, string Password);
        /// <summary>
        /// Evento chamado quando o Botão de login é pressionado!
        /// </summary>
        public event TentandoLogar TentativaDeLogin = null!;
        //
        public JanelaLoginInterno()
        {
            InitializeComponent();
            //
        }
        private void DispararTentativaLogin()
        {
            string Username = UsernameInputBox.Text;
            string Password = PasswordInputBox.Text;
            if (Username.IsNullOrEmpty() || Password.IsNullOrEmpty()) { Modais.MostrarAviso("Preencha todos os Campos!"); return; }
            //
            TentativaDeLogin?.Invoke(Username, Password);
        }
        //Eventos
        private void LoginButton_Click(object sender, EventArgs e)
        {
            DispararTentativaLogin();
        }
    }
}

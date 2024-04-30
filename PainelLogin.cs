using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Auth0.OidcClient;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using Labs.Janelas.Configuracoes.Dependencias;
using Labs.Janelas.LabsPDV;
using Labs.LABS_PDV;
using Unimake.Business.DFe.Xml.NFe;
using static Labs.LABS_PDV.Modelos;

namespace Labs
{
    public partial class PainelLogin : Form
    {
        readonly static Auth0ClientOptions ClientOptions = new() { Domain = "solucaeslab.us.auth0.com", LoadProfile = true, Scope = "read:role", ClientId = "00hKMs1GjChBpf0bMo9WVrPNXXe2ycGv"};
        readonly static Auth0Client Client = new Auth0Client(ClientOptions);
        //
        private Dictionary<string,string> extraParameters = new Dictionary<string,string> ();
        //
        public PainelLogin()
        {
            //
            extraParameters.Add("audience", "https://LabsAPI");
            //
            InitializeComponent();
            //
            ClientOptions.PostLogoutRedirectUri = ClientOptions.RedirectUri;
            //
            //Colocar método para chamada do login
        }
        //
        private void SairButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        private void VerificarUsuário(Cliente cliente)
        {
            if (cliente.ClienteAtivo) { LABS_PDV_MAIN.IniciarApp<LabsMainApp>(true); return; }
            //Se não for cliente ativo
            Modais.MostrarAviso("Que Pena!\nParece Que você Ainda não é um Cliente Labs!");
            //
            RealizarLogin();
        }
        private static async Task<bool> VerificarAdmin(AdminLabs admin)
        {
            if (admin.AdminAtivo && admin.PermLevel > 0) { LABS_PDV_MAIN.IniciarDependencia<DatabaseConfig>(); await Task.Delay(1); return true; }
            //Se não for cliente ativo
            Modais.MostrarAviso("Você Não é Um Técnico Labs ou Não Possui o Nível de Permissão Necessário");
            //
            await Task.Delay(1);
            return false;
        }
        //
        public static async Task<bool> RealizarLoginAdmin()
        {
            LoginResult result = await Client.LoginAsync();
            if (result.IsError) { return false; }
            //
            foreach (Claim claim in result.User.Claims)
            {
                if (claim.Type == "sub")
                {
                    AdminLabs admin = await CloudDataBase.GetAdminLabsAsync(claim.Value);
                    if (admin == null) { admin = new(claim.Value, false); CloudDataBase.RegisterAdminLabs(admin); }
                    //Verifica o usuário
                    return await VerificarAdmin(admin);
                }
            }
            return false;
        }
        //
        private async void RealizarLogin()
        {
            LoginResult result = await Client.LoginAsync(extraParameters: extraParameters);
            if (result.IsError) { this.Close(); return; }
            // Se o Cliente Não existir no nosso Banco de Dados, Adicionamos, mas não Ativamos de imediato
            //
            foreach (Claim claim in result.User.Claims)
            {
                if(claim.Type == "sub")
                {
                    Cliente cliente = await CloudDataBase.GetClienteAsync(claim.Value);
                    if(cliente == null) { cliente = new(claim.Value,false); CloudDataBase.RegisterClienteAsync(cliente); }
                    //Verifica o usuário
                    VerificarUsuário(cliente);
                    break;
                }
            }
        }
    }
}

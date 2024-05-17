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
using static Labs.LABS_PDV.Modelos;

namespace Labs
{
    public partial class Login
    {
        readonly static Auth0ClientOptions ClientOptions = new() { Domain = "solucaeslab.us.auth0.com", LoadProfile = true, Scope = "read:role", ClientId = "00hKMs1GjChBpf0bMo9WVrPNXXe2ycGv"};
        readonly static Auth0Client Client = new (ClientOptions);
        //
        public Login()
        {
            //
            ClientOptions.PostLogoutRedirectUri = ClientOptions.RedirectUri;
            //
        }
        //
        private async Task<bool> VerificarAdmin(AdminLabs admin)
        {
            if (admin.AdminAtivo && admin.PermLevel > 0) { await Task.Delay(1); return true; }
            //Se não for cliente ativo
            Modais.MostrarAviso("Você Não é Um Técnico Labs ou Não Possui o Nível de Permissão Necessário");
            //
            await Task.Delay(1);
            return false;
        }
        //
        public async Task<bool> RealizarLoginAdmin()
        {
            LoginResult result = await Client.LoginAsync();
            if (result.IsError) { Modais.MostrarErro($"ERRO DURANTE LOGIN\n{result.Error}"); return false; }
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
        /// <summary>
        /// Realiza o Login Do Cliente, Caso não exista retorna Nulo
        /// </summary>
        /// <returns>Objeto de Cliente</returns>
        public async Task<Cliente> RealizarLoginCliente()
        {
            LoginResult result = await Client.LoginAsync();
            if (result.IsError) { return null!; }
            // Se o Cliente Não existir no nosso Banco de Dados, Adicionamos, mas não Ativamos de imediato
            //
            foreach (Claim claim in result.User.Claims)
            {
                if(claim.Type == "sub")
                {
                    Cliente cliente = await CloudDataBase.GetClienteAsync(claim.Value);
                    if(cliente == null) 
                    { 
                        cliente = new(claim.Value); 
                        CloudDataBase.RegisterClienteAsync(cliente);
                    }
                    //Verifica o usuário
                    return cliente;
                }
            }
            return null!;
        }
    }
}

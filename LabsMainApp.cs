using Labs.Janelas;
using Labs.Janelas.Configuracoes;
using Labs.Janelas.LabsEstoque;
using Labs.Janelas.LabsPDV;
using Labs.Janelas.LabsPDV.Dependencias;
using Labs.LABS_PDV;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Labs.LABS_PDV.Modelos;

namespace Labs
{
	public partial class LabsMainApp : Form //Herda de FormBase porque Não queremos que ela se mova
	{
		//Referencia de Instância
		public static LabsMainApp App { get; private set; } = null!; //Declaramos como anulável já que de inicio não terá uma intância na memória.
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
		public LabsMainApp()
		{
			InitializeComponent();
			if (App == null) { App = this; }
			//Caso tenha uma instância rodando, fechamos esta janela e jogamos um erro
			//Dando a informação para que se o erro persistir, entrar em contato com o suporte técnico.
			else { this.Close(); MessageBox.Show("ERRO-800 \n UMA INSTÂNCIA DO APLICATIVO JÁ ESTÁ EM EXECUCÃO\n Caso o erro persista recomendamos entrar em contato com o suporte.", "ERRO CRÍTICO-cod:800", MessageBoxButtons.OK, MessageBoxIcon.Error); }
			//Carrega a imagem de fundo/
			//
			LoadConfigs();
		}
        //
        bool VerifyDataBases()
		{
			bool canProceed = true;
			if(!CloudDataBase.CheckDataBaseConnection(out bool LocalOK, out bool CloudOK, out bool LabsCloudOK))
			{
				Modais.MostrarErro("ERRO CRÍTICO\nNão Foi Possivel Estabelecer Conexão com os Bancos de Dados!");
                LabsPDV.Enabled = false;
                LabsEstoqueButton.Enabled = false;
				canProceed = false;
            }
			return canProceed;
		}
		//
		void LoadConfigs()
		{
            //Aqui Lemos as Configs do App.Config;
            if (Utils.TryParseToInt(ConfigurationManager.AppSettings["QMDP"]!, out int value))
            {
                QMDP = value;
            }
			//Aqui Puxamos as Impressoras Configuradas Para uso do sistema (Configuradas pelo painel de Configs);
			ImpressoraTermica = ConfigurationManager.AppSettings["ImpressoraTermica"]!;
			ImpressoraA4 = ConfigurationManager.AppSettings["ImpressoraA4"]!;
			//
        }
		//
		static async void VerificaEstoqueOnLoad()
		{
			var JDC = LABS_PDV_MAIN.IniciarApp<JanelaCarregamento>(true);
			//
			var ProdutosCount = await CloudDataBase.GetProdutosCountAsync();
			var PBFDS = await CloudDataBase.GetProdutosAsync();
			int ProdutosEmBaixa = 0;
			//
			JDC.SetTextoFrontEnd("VERIFICANDO ESTOQUE");
			JDC.ConfigBarraDeCarregamento(0, (int)ProdutosCount);
			JDC.BringToFront();
			//
			foreach (Produto produto in PBFDS)
			{
				if(produto.Quantidade <= QMDP) {  ProdutosEmBaixa++; }
				JDC.AumentarBarraDeCarregamento(1);
				await Task.Delay(1);
			}
			//
			if (ProdutosEmBaixa > 0) { Modais.MostrarAviso("UM OU MAIS PRODUTOS ESTÃO EM BAIXA NO ESTOQUE!"); }
			JDC.Close();
		}
		//
		//---------------------------//
		// EVENTOS
		//---------------------------//
		private void OnLabsEstoqueClick(object sender, EventArgs e)
		{
			//Iniciamos a Janela de Controle de estoque caso o usuário tenha permissão para isso;
			LABS_PDV_MAIN.IniciarApp<LabsEstoque>(false);
		}
		//
		private void PdvLogin() //Realiza o login do Operador se possível, caso contrário, não permite a abertura
		{
            LABS_PDV_MAIN.IniciarApp<LabsPDV>(true);
        }
		//
		private void OnLabsPDVClick(object sender, EventArgs e)
		{
			//Iniciamos a Janela Labs PDV. //Não precisa de permissão
			//Depois fazer função caixa remoto!
			//Aqui definimos essa janela como persistente
			LABS_PDV_MAIN.IniciarApp<LabsPDV>(true);
		}


		private void OnLabsConfigClick(object sender, EventArgs e)
		{
			LABS_PDV_MAIN.IniciarApp<LabsConfig>(false);
		}

		private void SairButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void LabsMainApp_Load(object sender, EventArgs e)
		{
			if (!VerifyDataBases()) { ModoSegurança = true; Modais.MostrarAviso("MODO DE SEGURANÇA HABILITADO!\nPara Sair Desse Modo, Os Conflitos Devem ser Resolvidos\ne Logo Após o Sistema Deve Ser Reiniciado!"); return; }
            VerificaEstoqueOnLoad();
		}
	}
}

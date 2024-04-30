﻿using Labs.Janelas;
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
		/// <summary>
		/// Habilitado Somente Caso Algum Erro Crítico de Inicialização for Detectado
		/// </summary>
		public static bool ModoSegurança { get; private set; } = false;
		public LabsMainApp()
		{
			InitializeComponent();
			if (App == null) { App = this; }
			//Caso tenha uma instância rodando, fechamos esta janela e jogamos um erro
			//Dando a informação para que se o erro persistir, entrar em contato com o suporte técnico.
			else { this.Close(); MessageBox.Show("ERRO-800 \n UMA INSTÂNCIA DO APLICATIVO JÁ ESTÁ EM EXECUCÃO\n Caso o erro persista recomendamos entrar em contato com o suporte.", "ERRO CRÍTICO-cod:800", MessageBoxButtons.OK, MessageBoxIcon.Error); }
			//Carrega a imagem de fundo
			//
			LoadConfigs();
		}
		//
		bool VerifyDataBases()
		{
			bool canProceed = true;
			if(LABS_PDV_MAIN.CloudDataBase == null) 
			{ 
				Modais.MostrarErro("ERRO CRÍTICO!\nNão foi Possível Encontrar o Caminho Para a DataBase Remota!");
				LabsPDV.Enabled = false;
				LabsEstoqueButton.Enabled = false;
				canProceed = false;
			}
			if(LABS_PDV_MAIN.LocalDataBase == null) 
			{
				Modais.MostrarErro("ERRO CRÍTICO\nNão Foi Possível Encontrar o Caminho Para a Database Local!");
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
		//
		//PREVENÇÃO DE MOVIMENTO DE JANELA // Qualquer janela que tiver a propriedade de prevenção de movimento deve herdar esse Método
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x0112;
			const int SC_MOVE = 0xF010;

			switch (m.Msg)
			{
				case WM_SYSCOMMAND:
					int command = m.WParam.ToInt32() & 0xfff0;
					if (command == SC_MOVE)
						return;
					break;
			}

			base.WndProc(ref m);
		}

		private void LabsMainApp_Load(object sender, EventArgs e)
		{
			if (!VerifyDataBases()) { ModoSegurança = true; Modais.MostrarAviso("MODO DE SEGURANÇA HABILITADO!\nPara Sair Desse Modo, Os Conflitos Devem ser Resolvidos\ne Logo Após o Sistema Deve Ser Reiniciado!"); return; }
            VerificaEstoqueOnLoad();
		}
	}
}

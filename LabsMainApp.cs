using Labs.Janelas.LabsEstoque;
using Labs.Janelas.LabsPDV;
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

namespace Labs
{
	public partial class LabsMainApp : Form //Herda de FormBase porque Não queremos que ela se mova
	{
		//Referencia de Instância
		public static LabsMainApp App { get; private set; } = null!; //Declaramos como anulável já que de inicio não terá uma intância na memória.
		public static string AppName { get; private set; } = "JanelaPrincipal"; //Determinado o nome dessa janela para iteração caso necessário.
		public LabsMainApp()
		{
			InitializeComponent();
			if (App == null) { App = this; }
			//Caso tenha uma instância rodando, fechamos esta janela e jogamos um erro
			//Dando a informação para que se o erro persistir, entrar em contato com o suporte técnico.
			else { this.Close(); MessageBox.Show("ERRO-800 \n UMA INSTÂNCIA DO APLICATIVO JÁ ESTÁ EM EXECUCÃO\n Caso o erro persista recomendamos entrar em contato com o suporte.", "ERRO CRÍTICO-cod:800", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}
		//
		//---------------------------//
		// EVENTOS
		//---------------------------//
		private void OnLabsEstoqueClick(object sender, EventArgs e)
		{
			//Iniciamos a Janela de Controle de estoque caso o usuário tenha permissão para isso;
			LABS_PDV_MAIN.IniciarApp<LabsEstoque>();
		}

		private void OnLabsPDVClick(object sender, EventArgs e)
		{
			//Iniciamos a Janela Labs PDV. //Não precisa de permissão
			//Depois fazer função caixa remoto!
			LABS_PDV_MAIN.IniciarApp<LabsPDV>();
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
	}
}

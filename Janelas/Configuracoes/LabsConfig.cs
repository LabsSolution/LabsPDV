using Labs.Janelas.Configuracoes.Dependencias;
using Labs.Janelas.LabsPDV.Dependencias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Janelas.Configuracoes
{
	public partial class LabsConfig : Form
	{
		public LabsConfig()
		{
			InitializeComponent();
		}
		//---------------------------------//
		//			   EVENTOS
		//---------------------------------//
		// Essa classe só dispõe de Paineis de Configuração, então aqui só terá chamadas de evento
		private void MeiosDePagamentoConfigButton_Click(object sender, EventArgs e)
		{
			LABS_PDV_MAIN.IniciarDependencia<MeiosDePagamentoConfig>();
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

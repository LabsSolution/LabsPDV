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
	public partial class LabsMainApp : Form
	{
		//Referencia de Instância
		public static LabsMainApp app { get; private set; } = null!; //Declaramos como anulável já que de inicio não terá uma intância na memória.
		//
		public LabsMainApp()
		{
			InitializeComponent();
			if(app == null) { app = this; }
			//Caso tenha uma instância rodando, fechamos esta janela e jogamos um erro
			//Dando a informação para que se o erro persistir, entrar em contato com o suporte técnico.
			else { this.Close(); MessageBox.Show("ERRO-800 \n UMA INSTÂNCIA DO APLICATIVO JÁ ESTÁ EM EXECUCÃO\n Caso o erro persista recomendamos entrar em contato com o suporte.","ERRO CRÍTICO-cod:800",MessageBoxButtons.OK,MessageBoxIcon.Error); }
		}
		//---------------------------//
		// EVENTOS
		//---------------------------//

		//
	}
}

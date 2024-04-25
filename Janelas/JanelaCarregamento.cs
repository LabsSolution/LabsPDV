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
	public partial class JanelaCarregamento : Form
	{
		public JanelaCarregamento()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Define o Texto que irá aparecer na tela de carregamento
		/// </summary>
		/// <param name="texto"></param>
		public void SetTextoFrontEnd(string texto)
		{
			TituloLabel.Text = texto;
		}
		/// <summary>
		/// Configura a Barra de Carregamento da Janela
		/// </summary>
		/// <param name="Min">Valor Minimo da barra</param>
		/// <param name="Max">Valor Máximo da barra</param>
		public void ConfigBarraDeCarregamento(int Min, int Max)
		{
			LoadingBar.Minimum = Min;
			LoadingBar.Maximum = Max;
		}
		//
		public void AumentarBarraDeCarregamento(int valor)
		{
			LoadingBar.Value += valor;
		}
	}
}

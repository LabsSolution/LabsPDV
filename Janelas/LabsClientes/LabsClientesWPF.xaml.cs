using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Labs.Janelas.LabsClientes
{
	/// <summary>
	/// Lógica interna para LabsClientesWPF.xaml
	/// </summary>
	public partial class LabsClientesWPF : Window
	{
		public LabsClientesWPF()
		{
			InitializeComponent();
		}
		//Evento de saida da página
		private void VoltarButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }
}

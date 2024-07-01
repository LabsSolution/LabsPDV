using Labs.Main;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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

namespace Labs.Janelas.Configuracoes.Dependencias
{
	/// <summary>
	/// Lógica interna para ImpressorasConfigWPF.xaml
	/// </summary>
	public partial class ImpressorasConfigWPF : Window
	{
		public ImpressorasConfigWPF()
		{
			InitializeComponent();
		}

		public void ListarImpressoras()
		{
			ImpressoraTermicaComboBox.Items.Clear();
			ImpressoraA4ComboBox.Items.Clear();
			//
			if(ImpressoraTermicaComboBox != null)
			{
				foreach (string printer in PrinterSettings.InstalledPrinters)
				{
					ImpressoraTermicaComboBox.Items.Add(printer);
				}
			}
			//
			if(ImpressoraA4ComboBox != null)
			{
				foreach (string printer in PrinterSettings.InstalledPrinters)
				{
					ImpressoraA4ComboBox.Items.Add(printer);
				}
			}
			//
			ImpressoraA4ComboBox!.Text = LabsConfigs.GetConfigValue("ImpressoraA4");
			ImpressoraTermicaComboBox!.Text = LabsConfigs.GetConfigValue("ImpressoraTermica");
		}

		private void SalvarButton_Click(object sender, RoutedEventArgs e)
		{
			if (ImpressoraTermicaComboBox.SelectedIndex == -1) { LabsConfigs.SalvarConfig("ImpressoraTermica","N/A"); }
			else { LabsConfigs.SalvarConfig("ImpressoraTermica",ImpressoraTermicaComboBox.Text); }
			//
			if (ImpressoraA4ComboBox.SelectedIndex == -1) { LabsConfigs.SalvarConfig("ImpressoraA4","N/A"); }
			else { LabsConfigs.SalvarConfig("ImpressoraA4", ImpressoraA4ComboBox.Text); }
			//
			Modais.MostrarInfo("Configurações Salvas Com Sucesso!\nReinicie o Sistema para as alterações serem aplicadas!");
			this.Close();
        }

		private void SairButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

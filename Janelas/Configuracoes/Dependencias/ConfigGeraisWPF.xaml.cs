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
	public partial class ConfigGeraisWPF : Window
	{
		public ConfigGeraisWPF()
		{
			InitializeComponent();
		}

		public void CarregarConfigs()
		{
			NomeEmpresaInputBox.Text = LabsConfigs.GetConfigValue("NomeEmpresa");
			EndereçoEmpresaInputBox.Text = LabsConfigs.GetConfigValue("EnderecoEmpresa");
			//
		}

		private void SalvarButton_Click(object sender, RoutedEventArgs e)
		{
			if (NomeEmpresaInputBox.Text.IsNullOrEmpty()) { LabsConfigs.SalvarConfig("NomeEmpresa","N/A"); }
			else { LabsConfigs.SalvarConfig("NomeEmpresa", NomeEmpresaInputBox.Text); }
			//
			if (EndereçoEmpresaInputBox.Text.IsNullOrEmpty()) { LabsConfigs.SalvarConfig("EnderecoEmpresa","N/A"); }
			else { LabsConfigs.SalvarConfig("EnderecoEmpresa", EndereçoEmpresaInputBox.Text); }
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

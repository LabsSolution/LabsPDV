using Labs.Main;
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
using Unimake.Business.DFe.Servicos;

namespace Labs.Janelas.LabsEstoque.Dependencias
{
	/// <summary>
	/// Lógica interna para CadastrarInfosFiscais.xaml
	/// </summary>
	public partial class CadastrarInfosFiscais : Window
	{
		public delegate void OnInfosApply(CadastrarInfosFiscais Janela,Dictionary<string, string> Infos);
		public event OnInfosApply OnInfosApplied = null!;
		public CadastrarInfosFiscais()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Atualiza o Nome do Produto que está sendo editado na Janela
		/// </summary>
		/// <param name="Desc">Descrição do Produto</param>
		public void INIT(string Desc)
		{
			DescricaoProdutoBox.Text = Desc;
			//
			if (Desc.IsNullOrEmpty()) { DescricaoProdutoBox.Text = "NÃO INFORMADO";}
			//
			BaseCalculoICMS_ComboBox.Items.Clear();
			foreach (string Modalidade in Enum.GetNames<ModalidadeBaseCalculoICMS>())
			{
				BaseCalculoICMS_ComboBox.Items.Add(Modalidade);
			}
			//
			BaseCalculoICMSST_ComboBox.Items.Clear();
			foreach (string Modalidade in Enum.GetNames<ModalidadeBaseCalculoICMSST>())
			{
				BaseCalculoICMSST_ComboBox.Items.Add(Modalidade);
			}
			//
			MotivoDesoneracaoICMS_ComboBox.Items.Clear();
			foreach (string Motivo in Enum.GetNames<MotivoDesoneracaoICMS>())
			{
				MotivoDesoneracaoICMS_ComboBox.Items.Add(Motivo);
			}
		}

		private void AplicarInfosButton_Click(object sender, RoutedEventArgs e)
		{
			//Antes de Adicionar Verificamos a validade dos Campos
			if (!Utils.IsValidBarCode(NCMInputBox.Text)) 
			{ Modais.MostrarAviso("O Valor informado para o NCM é Inválido"); return; }
			if (!Utils.IsValidBarCode(CSTInputBox.Text)) 
			{ Modais.MostrarAviso("O Valor informado para o CST é Inválido"); return; }
			if (!Utils.IsValidBarCode(CFOPInputBox.Text)) 
			{ Modais.MostrarAviso("O Valor informado para o CFOP é Inválido"); return; }
			//
			if(!VICMSDESONInputBox.Text.IsNullOrEmpty() && !Utils.TryParseToDouble(VICMSDESONInputBox.Text,out var _)) 
			{ Modais.MostrarAviso("Valor Incorreto Informado no Campo ( Val. do ICMS Deson. )"); return; }
			//
			if(!PICMSInputBox.Text.IsNullOrEmpty() && !Utils.TryParseToDouble(PICMSInputBox.Text,out var _)) 
			{ Modais.MostrarAviso("Valor Incorreto Informado no Campo ( Porcentagem ICMS )"); return; }
			//
			if(!PICMSSTInputBox.Text.IsNullOrEmpty() && !Utils.TryParseToDouble(PICMSSTInputBox.Text,out var _)) 
			{ Modais.MostrarAviso("Valor Incorreto Informado no Campo ( Porcentagem ICMS ST )"); return; }
			//
			if(!PMVASTInputBox.Text.IsNullOrEmpty() && !Utils.TryParseToDouble(PMVASTInputBox.Text,out var _)) 
			{ Modais.MostrarAviso("Valor Incorreto Informado no Campo ( Porcentagem MVA ST )"); return; }
			//
			if(!PFCPInputBox.Text.IsNullOrEmpty() && !Utils.TryParseToDouble(PFCPInputBox.Text,out var _)) 
			{ Modais.MostrarAviso("Valor Incorreto Informado no Campo ( Porcentagem FCP )"); return; }
			//
			if(!PRedBCSTInputBox.Text.IsNullOrEmpty() && !Utils.TryParseToDouble(PRedBCSTInputBox.Text,out var _)) 
			{ Modais.MostrarAviso("Valor Incorreto Informado no Campo ( Porcentagem RedBC )"); return; }
			//
			if(!PRedBCInputBox.Text.IsNullOrEmpty() && !Utils.TryParseToDouble(PRedBCInputBox.Text,out var _)) 
			{ Modais.MostrarAviso("Valor Incorreto Informado no Campo ( Porcentagem RedBC ST )"); return; }
			//
			if(!PICMSDifInputBox.Text.IsNullOrEmpty() && !Utils.TryParseToDouble(PICMSDifInputBox.Text,out var _)) 
			{ Modais.MostrarAviso("Valor Incorreto Informado no Campo ( Porcentagem ICMS Dif )"); return; }
			//
			if(!PCredSNInputBox.Text.IsNullOrEmpty() && !Utils.TryParseToDouble(PCredSNInputBox.Text,out var _)) 
			{ Modais.MostrarAviso("Valor Incorreto Informado no Campo ( Porcentagem de Cred. SN )"); return; }
			//Checagem dos ComboBox
			if(BaseCalculoICMS_ComboBox.SelectedIndex == -1) 
			{ Modais.MostrarAviso("Nenhuma Base de Cálculo de ICMS Foi Informada!\n(Esse Campo é usado de acordo com o CST Informado)"); return; }
			//
			if(BaseCalculoICMSST_ComboBox.SelectedIndex == -1)
			{ Modais.MostrarAviso("Nenhuma Base de Cálculo de ICMS ST Foi Informada!\n(Esse Campo é usado de acordo com o CST Informado)"); return; }
			//
			if(MotivoDesoneracaoICMS_ComboBox.SelectedIndex == -1)
			{ Modais.MostrarAviso("Nenhum Motivo de Desoneração foi Informado!\n(Esse Campo é usado de acordo com o CST Informado)"); return; }
			//Passou em Todas? Seguiremos adiante
			var Infos = new Dictionary<string, string>
			{
				{"NCM",NCMInputBox.Text},
				{"CST",CSTInputBox.Text},
				{"CFOP",CFOPInputBox.Text},
				{"CBENEF",!CBENEFInputBox.Text.IsNullOrEmpty()? CBENEFInputBox.Text : "" },
				{"VICMSDESON",!VICMSDESONInputBox.Text.IsNullOrEmpty()? VICMSDESONInputBox.Text : "0"},
				{"PICMS",!PICMSInputBox.Text.IsNullOrEmpty()? PICMSInputBox.Text : "0" },
				{"PICMSST",!PICMSSTInputBox.Text.IsNullOrEmpty()? PICMSSTInputBox.Text : "0" },
				{"PMVAST",!PMVASTInputBox.Text.IsNullOrEmpty()? PMVASTInputBox.Text : "0" },
				{"PFCP",! PFCPInputBox.Text.IsNullOrEmpty() ? PFCPInputBox.Text : "0" },
				{"PredBC",! PRedBCInputBox.Text.IsNullOrEmpty() ? PRedBCInputBox.Text : "0" },
				{"PredBCST",! PRedBCSTInputBox.Text.IsNullOrEmpty() ? PRedBCSTInputBox.Text : "0" },
				{"PICMSDIF",! PICMSDifInputBox.Text.IsNullOrEmpty()? PICMSDifInputBox.Text : "0"},
				{"PCredSN",! PCredSNInputBox.Text.IsNullOrEmpty() ? PCredSNInputBox.Text : "0" },
				{"BaseDeCalculoICMS",$"{BaseCalculoICMS_ComboBox.SelectedIndex}"},
				{"BaseDeCalculoICMSST",$"{BaseCalculoICMSST_ComboBox.SelectedIndex}"},
				{"MotivoDesoneracaoICMS",$"{MotivoDesoneracaoICMS_ComboBox.SelectedIndex}"}
			};
			//Aqui Geramos um Dicionário Contendo as Informações e Devolvemos em Um Evento em Disparo Único.
			//Disparo
			OnInfosApplied?.Invoke(this,Infos);
			Modais.MostrarInfo("Informações Aplicadas Com Sucesso!");
			this.Close();
		}

		private void SairButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		//

	}
}

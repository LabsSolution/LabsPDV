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
		public delegate void OnInfosApply(CadastrarInfosFiscais Janela,Produto produto, List<Produto> Produtos);
		public event OnInfosApply OnInfosApplied = null!;
		//
		Produto Produto { get; set; } = null!;
		List<Produto> Produtos { get; set; } = null!;
		//
		int iterador = 0;
		//
		public CadastrarInfosFiscais()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Atualiza o Nome do Produto que está sendo editado na Janela
		/// </summary>
		/// <param name="produto">produto que vai receber a atualização</param>
		public void InitSingle(Produto produto)
		{
			Produto = produto;
			//
			DescricaoProdutoBox.Text = produto.Descricao;
			//
			if (produto.Descricao.IsNullOrEmpty()) { DescricaoProdutoBox.Text = "NÃO INFORMADO";}
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
		public void InitMany(List<Produto> produtos)
		{
			Produto = null!;
			Produtos = produtos;
			//
			DescricaoProdutoBox.Text = Produtos[0].Descricao + $"{iterador+1}/{produtos.Count} Restantes";
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
		//
		public void ResetInterface()
		{
			//Campos para serem Resetados
			NCMInputBox.Text = null!;
			CSTInputBox.Text = null!;
			CFOPInputBox.Text = null!;
			CBENEFInputBox.Text = null!;
			VICMSDESONInputBox.Text = null!;
			PICMSInputBox.Text = null!;
			PICMSSTInputBox.Text = null!;
			PMVASTInputBox.Text = null!;
			PFCPInputBox.Text = null!;
			PRedBCInputBox.Text = null!;
			PRedBCSTInputBox.Text = null!;
			PICMSDifInputBox.Text = null!;
			PCredSNInputBox.Text = null!;
			//
			BaseCalculoICMS_ComboBox.SelectedIndex = -1;
			BaseCalculoICMSST_ComboBox.SelectedIndex = -1;
			MotivoDesoneracaoICMS_ComboBox.SelectedIndex = -1;
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
			if(Produto != null)
			{
				Produto.NCM = NCMInputBox.Text;
				Produto.CST = CSTInputBox.Text;
				Produto.CFOP = CFOPInputBox.Text;
				Produto.CBENEF = !CBENEFInputBox.Text.IsNullOrEmpty()? CBENEFInputBox.Text : "";
				Produto.VICMSDESON = !VICMSDESONInputBox.Text.IsNullOrEmpty() ? double.Parse(VICMSDESONInputBox.Text) : 0;
				Produto.PICMS = !PICMSInputBox.Text.IsNullOrEmpty() ? double.Parse(PICMSInputBox.Text) : 0;
				Produto.PICMSST = !PICMSSTInputBox.Text.IsNullOrEmpty() ? double.Parse(PICMSSTInputBox.Text) : 0;
				Produto.PMVAST = !PMVASTInputBox.Text.IsNullOrEmpty() ? double.Parse(PMVASTInputBox.Text) : 0;
				Produto.PFCP = !PFCPInputBox.Text.IsNullOrEmpty() ? double.Parse(PFCPInputBox.Text) : 0;
				Produto.PRedBC = !PRedBCInputBox.Text.IsNullOrEmpty() ? double.Parse(PRedBCInputBox.Text) : 0;
				Produto.PRedBCST = !PRedBCSTInputBox.Text.IsNullOrEmpty() ? double.Parse(PRedBCSTInputBox.Text) : 0;
				Produto.PICMSDIF = !PICMSDifInputBox.Text.IsNullOrEmpty() ? double.Parse(PICMSDifInputBox.Text) : 0;
				Produto.PCredSN = !PCredSNInputBox.Text.IsNullOrEmpty() ? double.Parse(PCredSNInputBox.Text) : 0;
				//
				Produto.BaseDeCalculoICMS = BaseCalculoICMS_ComboBox.SelectedIndex;
				Produto.BaseDeCalculoICMSST = BaseCalculoICMSST_ComboBox.SelectedIndex;
				Produto.MotivoDesoneracaoICMS = MotivoDesoneracaoICMS_ComboBox.SelectedIndex;
				//
				Produto.PossuiInfosFiscais = true;
				//Disparo
				OnInfosApplied?.Invoke(this, Produto!, null!);
				Modais.MostrarInfo("Informações Aplicadas Com Sucesso!");
				this.Close();
			}
			//
			if(Produtos != null)
			{
				if(Produtos.Count > 0)
				{
					var prod = Produtos[iterador];
					//
					prod.NCM = NCMInputBox.Text;
					prod.CST = CSTInputBox.Text;
					prod.CFOP = CFOPInputBox.Text;
					prod.CBENEF = !CBENEFInputBox.Text.IsNullOrEmpty() ? CBENEFInputBox.Text : "";
					prod.VICMSDESON = !VICMSDESONInputBox.Text.IsNullOrEmpty() ? double.Parse(VICMSDESONInputBox.Text) : 0;
					prod.PICMS = !PICMSInputBox.Text.IsNullOrEmpty() ? double.Parse(PICMSInputBox.Text) : 0;
					prod.PICMSST = !PICMSSTInputBox.Text.IsNullOrEmpty() ? double.Parse(PICMSSTInputBox.Text) : 0;
					prod.PMVAST = !PMVASTInputBox.Text.IsNullOrEmpty() ? double.Parse(PMVASTInputBox.Text) : 0;
					prod.PFCP = !PFCPInputBox.Text.IsNullOrEmpty() ? double.Parse(PFCPInputBox.Text) : 0;
					prod.PRedBC = !PRedBCInputBox.Text.IsNullOrEmpty() ? double.Parse(PRedBCInputBox.Text) : 0;
					prod.PRedBCST = !PRedBCSTInputBox.Text.IsNullOrEmpty() ? double.Parse(PRedBCSTInputBox.Text) : 0;
					prod.PICMSDIF = !PICMSDifInputBox.Text.IsNullOrEmpty() ? double.Parse(PICMSDifInputBox.Text) : 0;
					prod.PCredSN = !PCredSNInputBox.Text.IsNullOrEmpty() ? double.Parse(PCredSNInputBox.Text) : 0;
					//
					prod.BaseDeCalculoICMS = BaseCalculoICMS_ComboBox.SelectedIndex;
					prod.BaseDeCalculoICMSST = BaseCalculoICMSST_ComboBox.SelectedIndex;
					prod.MotivoDesoneracaoICMS = MotivoDesoneracaoICMS_ComboBox.SelectedIndex;
					//
					prod.PossuiInfosFiscais = true;
					// Seta a descrição para o próximo produto e incrementa o iterador
					if(iterador < Produtos.Count)
					{ 
						iterador++;
						DescricaoProdutoBox.Text = Produtos[iterador].Descricao + $"{iterador + 1}/{Produtos.Count} Restantes";
					}
					else
					{
						//Disparo
						OnInfosApplied?.Invoke(this, null!, Produtos);
						Modais.MostrarInfo($"Informações Aplicadas Para ({Produtos.Count}) Com Sucesso!");
						this.Close();
					}
				}
			}
		}

		private void SairButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		//

	}
}

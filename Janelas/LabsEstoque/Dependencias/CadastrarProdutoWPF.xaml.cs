using Labs.Main;
using Lucene.Net.Documents;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Unimake.Business.DFe.Xml.CTe;

namespace Labs.Janelas.LabsEstoque.Dependencias
{
    /// <summary>
    /// Lógica interna para CadastrarProdutoWPF.xaml
    /// </summary>
    public partial class CadastrarProdutoWPF : Window
    {
        public CadastrarProdutoWPF()
        {
            InitializeComponent();
            InitiateComboBox();
		}
        //
        private Dictionary<string, string> InfosFiscais = null!;
        //
        private async void InitiateComboBox()
        {
            //Inicia as Medidas
            Type type = typeof(UnidadesDeMedida);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Static | BindingFlags.Public);
            //
            foreach (var prop in properties)
            {
                if(prop.PropertyType == typeof(UnidadeDeMedida))
                {
                    UnidadeDeMedida Uni = (UnidadeDeMedida)prop.GetValue(null)!;
                    UnidadeDeMedidaComboBox.Items.Add(Uni);
                    UnidadeDeMedidaComboBox.DisplayMemberPath = "Descricao";
                }
            }
            //Inicia a lista de fornecedores
            var Fornecedores = await CloudDataBase.GetManyLocalAsync<Fornecedor>(Collections.Fornecedores,_ => true);
            //
            if(Fornecedores != null)
            {
                foreach (var fornecedor in Fornecedores)
                {
                    FornecedorComboBox.Items.Add(fornecedor);
                    FornecedorComboBox.DisplayMemberPath = "NomeEmpresa";
                }
            }
        }
        //
        private void ResetInterface()
        {
            DescricaoInputBox.Text = null!;
            EstoqueMinimoInputBox.Text = null!;
            CodBarrasInputBox.Text = null!;
            //
            UnidadeDeMedidaComboBox.Text = null!;
            UnidadeDeMedidaComboBox.SelectedItem = null!;
            //
            FornecedorComboBox.Text = null!;
            FornecedorComboBox.SelectedItem = null!;
        }

        //METODOS
        private async void CadastrarProduto(string Desc, int QTD, UnidadeDeMedida Unidade,Fornecedor fornecedor, string CodBarras)
        {
            Produto produto = new()
            {
                CodBarras = CodBarras,
                Descricao = Desc,
                Fornecedor = fornecedor,
                QuantidadeMin = QTD,
                UnidadeDeMedida = Unidade,
                Status = "OK",
            };
            if(InfosFiscais != null)
            {
                produto.NCM = InfosFiscais["NCM"];
                produto.CST = InfosFiscais["CST"];
                produto.CFOP = InfosFiscais["CFOP"];
                produto.CBENEF = InfosFiscais["CBENEF"];
                produto.VICMSDESON = double.Parse(InfosFiscais["VICMSDESON"]);
                produto.PICMS = double.Parse(InfosFiscais["PICMS"]);
                produto.PICMSST = double.Parse(InfosFiscais["PICMSST"]);
                produto.PMVAST = double.Parse(InfosFiscais["PMVAST"]);
                produto.PFCP = double.Parse(InfosFiscais["PFCP"]);
                produto.PRedBC = double.Parse(InfosFiscais["PredBC"]);
                produto.PRedBCST = double.Parse(InfosFiscais["PredBCST"]);
                produto.PICMSDIF = double.Parse(InfosFiscais["PICMSDIF"]);
                produto.PCredSN = double.Parse(InfosFiscais["PCredSN"]);
                produto.BaseDeCalculoICMS = int.Parse(InfosFiscais["BaseDeCalculoICMS"]);
                produto.BaseDeCalculoICMSST = int.Parse(InfosFiscais["BaseDeCalculoICMSST"]);
                produto.MotivoDesoneracaoICMS = int.Parse(InfosFiscais["MotivoDesoneracaoICMS"]);
            }
            //
            if (LabsMain.Cliente.PossuiPlanoCloud) { await CloudDataBase.RegisterCloudAsync(Collections.Produtos, produto); }
            //
            await CloudDataBase.RegisterLocalAsync(Collections.Produtos,produto);
            //
            var prod = CloudDataBase.GetLocalAsync<Produto>(Collections.Produtos, x => x.CodBarras == produto.CodBarras);
            //
            LabsMainAppWPF.IndexarProdutos();
            //
            Modais.MostrarInfo("Produto cadastrado com sucesso!");
            //
            ResetInterface();
        } 
        //Eventos
        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            string Desc = DescricaoInputBox.Text;
			var isQTD = Utils.TryParseToInt(EstoqueMinimoInputBox.Text, out int QTD);
			string CodBarras = CodBarrasInputBox.Text; 
            //Validação dos Campos
            if (UnidadeDeMedidaComboBox.SelectedItem is not UnidadeDeMedida Uni) { Modais.MostrarAviso("Não é possível registrar um produto sem Unidade de Medida!"); return; }
            if (FornecedorComboBox.SelectedItem is not Fornecedor fornecedor) { Modais.MostrarAviso("Você não selecionou um fornecedor para o produto."); return; }
            //
            if (Desc.IsNullOrEmpty()) { Modais.MostrarAviso("Não é possível registrar um produto sem nome!"); return; }
            if (!isQTD || QTD < 0) { Modais.MostrarAviso("Você deve inserir uma quantidade válida"); return; }
            if (!Utils.IsValidBarCode(CodBarras)) { Modais.MostrarAviso("Você deve inserir um código válido"); return; }
            //Passou em todos os validadores?
            CadastrarProduto(Desc,QTD,Uni,fornecedor,CodBarras);
        }
        private void LimparTudoButton_Click(object sender, RoutedEventArgs e)
        {
            ResetInterface();
        }
        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //
		private void CadastroInfoFiscalButton_Click(object sender, RoutedEventArgs e)
		{
            LabsMain.IniciarDependencia<CadastrarInfosFiscais>(app =>
            {
                app.INIT(DescricaoInputBox.Text);
				app.OnInfosApplied += OnInfosFiscaisApply;
            });
        }

		private void OnInfosFiscaisApply(CadastrarInfosFiscais Janela,Dictionary<string, string> Infos)
		{
            InfosFiscais = Infos;
            Janela.OnInfosApplied -= OnInfosFiscaisApply;
		}
	}
}

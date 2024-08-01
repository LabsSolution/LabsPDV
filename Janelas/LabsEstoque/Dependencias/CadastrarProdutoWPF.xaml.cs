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
        private Produto Produto { get; set; } = null!;

        public CadastrarProdutoWPF()
        {
            InitializeComponent();
            Initiate();
		}
        //
        private async void Initiate()
        {
            //Inicia as Medidas
            Type type = typeof(UnidadesDeMedida);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Static | BindingFlags.Public);
            //
            this.Produto = new();
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
            CodInternoInputBox.Text = null!;
            //
            UnidadeDeMedidaComboBox.Text = null!;
            UnidadeDeMedidaComboBox.SelectedItem = null!;
            //
            FornecedorComboBox.Text = null!;
            FornecedorComboBox.SelectedItem = null!;
        }

        //METODOS
        private async void CadastrarProduto(string Desc, int QTD, UnidadeDeMedida Unidade,Fornecedor fornecedor, string CodInterno, string CodBarras)
        {
            Produto.CodInterno = CodInterno;
            Produto.CodBarras = CodBarras;
            Produto.Descricao = Desc;
            Produto.Fornecedor = fornecedor;
            Produto.QuantidadeMin = QTD;
            Produto.UnidadeDeMedida = Unidade;
            Produto.Status = "OK";
            //
            if (LabsMain.Cliente.PossuiPlanoCloud && LabsMainAppWPF.IsConnectedToInternet) { await CloudDataBase.RegisterCloudAsync(Collections.Produtos, Produto); }
            //
            await CloudDataBase.RegisterLocalAsync(Collections.Produtos,Produto);
            //
            //var prod = CloudDataBase.GetLocalAsync<Produto>(Collections.Produtos, x => x.CodBarras == Produto.CodBarras);
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
            string CodInterno = CodInternoInputBox.Text;
            //Validação dos Campos
            if (UnidadeDeMedidaComboBox.SelectedItem is not UnidadeDeMedida Uni) { Modais.MostrarAviso("Não é possível registrar um produto sem Unidade de Medida!"); return; }
            //
            if (FornecedorComboBox.SelectedItem is not Fornecedor fornecedor) { Modais.MostrarAviso("Você não selecionou um fornecedor para o produto."); return; }
            //
            if (Desc.IsNullOrEmpty()) { Modais.MostrarAviso("Não é possível registrar um produto sem nome!"); return; }
            //
            if (!isQTD || QTD < 0) { Modais.MostrarAviso("Você deve inserir uma quantidade mínima de estoque válida"); return; }
            //
            if (!Utils.IsValidBarCode(CodInterno)) { Modais.MostrarAviso("Você deve inserir um código válido"); return; }
            //
            if (!CodBarrasInputBox.Text.IsNullOrEmpty())
            {
                if (!Utils.IsValidGtin13(CodBarras)) { Modais.MostrarAviso("O Código GTIN informado é inválido"); return; }
            }
            else { CodBarras = "SEM GTIN"; }
            //Passou em todos os validadores?
            CadastrarProduto(Desc,QTD,Uni,fornecedor,CodInterno,CodBarras);
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
                app.InitSingle(Produto);
				app.OnInfosApplied += OnInfosFiscaisApply;
            });
        }

		private void OnInfosFiscaisApply(CadastrarInfosFiscais Janela,Produto produto, List<Produto> produtos)
		{
            if (produto != null) { this.Produto = produto; this.Produto.PossuiInfosFiscais = true; };
            Janela.OnInfosApplied -= OnInfosFiscaisApply;
		}
	}
}

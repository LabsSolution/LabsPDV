using Labs.Main;
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
using static Labs.Main.Modelos;

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
            //
            if (LabsMain.Cliente.PossuiPlanoCloud) { await CloudDataBase.RegisterCloudAsync(Collections.Produtos, produto); }
            //
            await CloudDataBase.RegisterLocalAsync(Collections.Produtos,produto);
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
    }
}

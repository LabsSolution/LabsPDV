using Labs.Main;
using MongoDB.Driver;
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
    /// Lógica interna para AtualizarProdutoWPF.xaml
    /// </summary>
    public partial class AtualizarProdutoWPF : Window
    {
		private Produto Produto { get; set; } = null!;
        public AtualizarProdutoWPF()
        {
            InitializeComponent();
		}
		private async Task InitiateComboBox()
		{
			//Inicia as Medidas
			Type type = typeof(UnidadesDeMedida);
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Static | BindingFlags.Public);
			//
			foreach (var prop in properties)
			{
				if (prop.PropertyType == typeof(UnidadeDeMedida))
				{
					UnidadeDeMedida Uni = (UnidadeDeMedida)prop.GetValue(null)!;
					UnidadeDeMedidaComboBox.Items.Add(Uni);
					UnidadeDeMedidaComboBox.DisplayMemberPath = "Descricao";
				}
			}
			//Inicia a lista de fornecedores
			var Fornecedores = await CloudDataBase.GetManyLocalAsync<Fornecedor>(Collections.Fornecedores, _ => true);
			//
			if (Fornecedores != null)
			{
				foreach (var fornecedor in Fornecedores)
				{
					FornecedorComboBox.Items.Add(fornecedor);
					FornecedorComboBox.DisplayMemberPath = "NomeEmpresa";
				}
			}
			//
		}
		//
		//
		//
		public async void CarregarAtributos(Produto Produto)
        {
            this.Produto = Produto;
			//
			//Antes chamamos o InitiateComboBox
			await InitiateComboBox();
			//Agora inicia os selecionados
			if (UnidadeDeMedidaComboBox.Items.Count > 0 && FornecedorComboBox.Items.Count > 0)
			{
				foreach (UnidadeDeMedida item in UnidadeDeMedidaComboBox.Items) { if (item.Unidade == Produto.UnidadeDeMedida.Unidade) { UnidadeDeMedidaComboBox.Text = item.Descricao; UnidadeDeMedidaComboBox.SelectedItem = item; } }
				foreach (Fornecedor item in FornecedorComboBox.Items)
				{
					if (item.ID == Produto.Fornecedor.ID) { FornecedorComboBox.Text = item.NomeEmpresa; FornecedorComboBox.SelectedItem = item; }
				}
			}
			//
			DescricaoInputBox.Text = Produto.Descricao;
            CodBarrasInputBox.Text = Produto.CodBarras;
            EstoqueMinimoInputBox.Text = $"{Produto.QuantidadeMin}";
        }
        private async void AtualizarProduto(string Desc, int QTD,UnidadeDeMedida Uni, Fornecedor fornecedor, string CodBarras)
        {
            Produto.Descricao = Desc;
            Produto.QuantidadeMin = QTD;
            Produto.UnidadeDeMedida = Uni;
            Produto.Fornecedor = fornecedor;
            Produto.CodBarras = CodBarras;
            //
            if (LabsMain.Cliente.PossuiPlanoCloud)
            {
                await CloudDataBase.RegisterCloudAsync(Collections.Produtos, Produto, Builders<Produto>.Filter.Eq("ID", Produto.ID));
			}
            //
            await CloudDataBase.RegisterLocalAsync(Collections.Produtos, Produto, Builders<Produto>.Filter.Eq("ID",Produto.ID));
            //
            Modais.MostrarInfo("Produto Atualizado com sucesso!");
            //
        }
        //
        private void AtualizarButton_Click(object sender, RoutedEventArgs e)
        {
            string Desc = DescricaoInputBox.Text;
            var isQTD = Utils.TryParseToInt(EstoqueMinimoInputBox.Text, out int QTD);
            string CodBarras = CodBarrasInputBox.Text;
			//Validação dos Campos
			if (UnidadeDeMedidaComboBox.SelectedItem is not UnidadeDeMedida Uni) { Modais.MostrarAviso("Não é possível registrar um produto sem Unidade de Medida!"); return; }
			if (FornecedorComboBox.SelectedItem is not Fornecedor fornecedor) { Modais.MostrarAviso("Você não selecionou um fornecedor para o produto."); return; }
			//
			if (Desc.IsNullOrEmpty()) { Modais.MostrarAviso("Não é possível Atualizar um produto sem nome!"); return; }
            if (!isQTD || QTD < 0) { Modais.MostrarAviso("Você deve inserir uma quantidade válida"); return; }
            if (!Utils.IsValidBarCode(CodBarras)) { Modais.MostrarAviso("Você deve inserir um código válido"); return; }
            //Passou em todos os validadores?
            AtualizarProduto(Desc, QTD,Uni,fornecedor,CodBarras);
        }
        //
        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

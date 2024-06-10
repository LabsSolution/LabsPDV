using Labs.LABS_PDV;
using MongoDB.Driver;
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
using static Labs.LABS_PDV.Modelos;
namespace Labs.Janelas.LabsEstoque.Dependencias
{
    /// <summary>
    /// Lógica interna para AtualizarProdutoWPF.xaml
    /// </summary>
    public partial class AtualizarProdutoWPF : Window
    {
        public AtualizarProdutoWPF()
        {
            InitializeComponent();
        }
        //
        private Produto Produto { get; set; } = null!;
        //
        public void CarregarAtributos(Produto Produto)
        {
            this.Produto = Produto;
            DescricaoInputBox.Text = Produto.Descricao;
            QuantidadeInputBox.Text = $"{Produto.Quantidade}";
            PrecoInputBox.Text = Utils.FormatarValor(Produto.Preco);
            CodigoInputBox.Text = Produto.CodBarras;
        }
        private async void AtualizarProduto(string Desc, int QTD, double Preco, string CodBarras)
        {
            Produto.CodBarras = CodBarras;
            Produto.Descricao = Desc;
            Produto.Preco = Preco;
            Produto.Quantidade = QTD;
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
            var isQTD = Utils.TryParseToInt(QuantidadeInputBox.Text, out int QTD);
            var isPreco = Utils.TryParseToDouble(PrecoInputBox.Text, out double Preco);
            string CodBarras = CodigoInputBox.Text;
            //Validação dos Campos
            if (Desc.IsNullOrEmpty()) { Modais.MostrarAviso("Não é possível Atualizar um produto sem nome!"); return; }
            if (!isQTD || QTD < 0) { Modais.MostrarAviso("Você deve inserir uma quantidade válida"); return; }
            if (!isPreco || Preco < 0) { Modais.MostrarAviso("Você deve inserir um Preço válido"); return; }
            if (!Utils.IsValidBarCode(CodBarras)) { Modais.MostrarAviso("Você deve inserir um código válido"); return; }
            //Passou em todos os validadores?
            AtualizarProduto(Desc, QTD, Preco, CodBarras);
        }
        //
        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

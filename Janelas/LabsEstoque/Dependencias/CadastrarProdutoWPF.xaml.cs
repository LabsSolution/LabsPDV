using Labs.LABS_PDV;
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
    /// Lógica interna para CadastrarProdutoWPF.xaml
    /// </summary>
    public partial class CadastrarProdutoWPF : Window
    {
        public CadastrarProdutoWPF()
        {
            InitializeComponent();
        }
        //
        private void ResetInterface()
        {
            DescricaoInputBox.Text = null!;
            QuantidadeInputBox.Text = null!;
            PrecoInputBox.Text = null!;
            CodigoInputBox.Text = null!;
        }
        //METODOS
        private async void CadastrarProduto(string Desc, int QTD, double Preco, string CodBarras)
        {
            Produto produto = new()
            {
                CodBarras = CodBarras,
                Descricao = Desc,
                Preco = Preco,
                Quantidade = QTD,
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
            var isQTD = Utils.TryParseToInt(QuantidadeInputBox.Text,out int QTD);
            var isPreco = Utils.TryParseToDouble(PrecoInputBox.Text,out double Preco);
            string CodBarras = CodigoInputBox.Text; 
            //Validação dos Campos
            if (Desc.IsNullOrEmpty()) { Modais.MostrarAviso("Não é possível registrar um produto sem nome!"); return; }
            if (!isQTD || QTD < 0) { Modais.MostrarAviso("Você deve inserir uma quantidade válida"); return; }
            if (!isPreco || Preco < 0) { Modais.MostrarAviso("Você deve inserir um Preço válido"); return; }
            if (!Utils.IsValidBarCode(CodBarras)) { Modais.MostrarAviso("Você deve inserir um código válido"); return; }
            //Passou em todos os validadores?
            CadastrarProduto(Desc,QTD,Preco,CodBarras);
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

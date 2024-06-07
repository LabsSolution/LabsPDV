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

namespace Labs.Janelas.LabsPDV.Dependencias
{
    /// <summary>
    /// Lógica interna para JanelaDeDevolucao.xaml
    /// </summary>
    public partial class JanelaDeDevolucaoWPF : Window
    {
        public JanelaDeDevolucaoWPF()
        {
            InitializeComponent();
        }
        //
        private async void RealizarDevolucao()
        {
            string CodBarras = CodigoInputBox.Text;
            if (!Utils.IsValidBarCode(CodBarras)) { Modais.MostrarAviso("Informe um Código Válido!"); return; }
            //
            Produto produto = await CloudDataBase.GetLocalAsync<Produto>(Collections.Produtos,x => x.CodBarras == CodBarras);
            //
            if(produto == null) { Modais.MostrarAviso("Produto Não Cadastrado no Estoque!"); return; }
            //
            //Obriga o operador a escolher uma opção
            if(DevolucaoCheckBox.IsChecked == false && DefeitoCheckBox.IsChecked == false) { Modais.MostrarAviso("Você deve marcar pelo menos uma opção!"); return; }
            //
            if(DevolucaoCheckBox.IsChecked == true) { produto.Status = "Devolução"; }
            if(DefeitoCheckBox.IsChecked == true) { produto.ComDefeito = true; produto.Status = "Produto com Defeito"; }
            //Antes de seguir configuramos o objeto para ter 1 unidade
            PrecoLabel.Content = $"Preço Unitário: R$ {Utils.FormatarValor(produto.Preco)}";
            //
            produto.Quantidade = 1;
            //Setamos a descrição para que o produto seja apresentado
            DescricaoProdutoBox.Text = produto.Descricao;
            //
        }
        //-------------------------
        //EVENTOS
        //-------------------------
        private void DevolucaoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VoltarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemoverProduto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CodigoInputBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                RealizarDevolucao();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(sender == DevolucaoCheckBox) { DefeitoCheckBox.IsChecked = false; }
            if(sender == DefeitoCheckBox) { DevolucaoCheckBox.IsChecked = false; }
        }
    }
}

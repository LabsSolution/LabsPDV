using Labs.LABS_PDV;
using SharpVectors.Dom.Svg;
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
            //Remover
            IniciarJanela();
        }
        //
        //
        public void IniciarJanela()
        {
            //Prepara a combobox de meio de devolução
            SetMeios();
        }
        //
        private async void SetMeios()
        {
            MeiosPagamento meios = await CloudDataBase.GetLocalAsync<MeiosPagamento>(Collections.MeiosDePagamento,_ => true);
            if (meios == null && LabsMain.Cliente.PossuiPlanoCloud) { meios = await CloudDataBase.GetCloudAsync<MeiosPagamento>(Collections.MeiosDePagamento, _ => true); }
            //
            if(meios != null)
            {
                foreach (var meio in meios.Meios)
                {
					MeiosDevolucaoComboBox.Items.Add(meio.Item1);
				}
                MeiosDevolucaoComboBox.SelectedIndex = 0;
            }
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
            if (MotivoTextBox.Text.IsNullOrEmpty()) { Modais.MostrarAviso("Você deve informar o motivo"); return; }
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
            Devolucao dev = new(MeiosDevolucaoComboBox.Text, MotivoTextBox.Text, produto.Preco,$"{DateTime.Now:dd/MM/yyyy}",$"{DateTime.Now:HH/mm/ss}");
            // Após gerarmos o Objeto de devolução, guardamos na database e geramos a nota
            //Geramos a nota
            //Guardamos no banco de dados
            await CloudDataBase.RegisterLocalAsync(Collections.Devol)

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

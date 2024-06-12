using Labs.Main;
using MongoDB.Driver;
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
using static Labs.Main.Modelos;

namespace Labs.Janelas.LabsPDV.Dependencias
{
    /// <summary>
    /// Lógica interna para JanelaDeDevolucao.xaml
    /// </summary>
    public partial class JanelaDeDevolucaoWPF : Window
    {
        Produto Produto { get; set; } = null!;
        public JanelaDeDevolucaoWPF()
        {
            InitializeComponent();
            //Remover
            IniciarJanela();
        }
        //
        private void Reset()
        {
            CodigoInputBox.Text = null!;
            DescricaoProdutoBox.Text = null!;
            DevolucaoCheckBox.IsChecked = false;
            DefeitoCheckBox.IsChecked = false;
            SetMeios();
            //
            MotivoTextBox.Text = null!;
        }
        //
        public void IniciarJanela()
        {
            //Prepara a combobox de meio de devolução
            SetMeios();
        }
        //
        private async void SetMeios()
        {
            //Garantimos que os meios serão resetados já que essa função também é chamada no Reset();
            MeiosDevolucaoComboBox.Items.Clear();
            //
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
        private async void RegistrarProdutoParaDevoloucao()
        {
            string CodBarras = CodigoInputBox.Text;
            if (!Utils.IsValidBarCode(CodBarras)) { Modais.MostrarAviso("Informe um Código Válido!"); return; }
            //
            Produto = await CloudDataBase.GetLocalAsync<Produto>(Collections.Produtos,x => x.CodBarras == CodBarras);
            //
            if(Produto == null) { Modais.MostrarAviso("Produto Não Cadastrado no Estoque!"); return; }
            //Antes de seguir configuramos o objeto para ter 1 unidade
            PrecoLabel.Content = $"Preço Unitário: R$ {Utils.FormatarValor(Produto.Preco)}";
            //
            Produto.Quantidade = 1;
            //Setamos a descrição para que o produto seja apresentado
            DescricaoProdutoBox.Text = Produto.Descricao;
        }
        //-------------------------
        //EVENTOS
        //-------------------------
        private async void RealizarDevolucao()
        {
            if(Produto == null) { Modais.MostrarAviso("Você deve informar o produto para devolução"); return; }
			//Obriga o operador a escolher uma opção
			if (DevolucaoCheckBox.IsChecked == false && DefeitoCheckBox.IsChecked == false) { Modais.MostrarAviso("Você deve marcar pelo menos uma opção!"); return; }
			//
            //
			if (MotivoTextBox.Text.IsNullOrEmpty()) { Modais.MostrarAviso("Você deve informar o motivo"); return; }
			//
			// Após gerarmos o Objeto de devolução, guardamos na database e geramos a nota
			//Geramos a nota
			//Guardamos no banco de dados
			if (DevolucaoCheckBox.IsChecked == true) 
            { 
                Produto.Status = "Devolução"; 
			    Devolucao dev = new(Produto.Descricao,MeiosDevolucaoComboBox.Text, MotivoTextBox.Text, Produto, $"{DateTime.Now:dd/MM/yyyy}", $"{DateTime.Now:HH:mm:ss}");
			    await CloudDataBase.RegisterLocalAsync(Collections.Devolucoes, dev);
            }
            //
			if (DefeitoCheckBox.IsChecked == true)
            { 
                Produto.ComDefeito = true; 
                Produto.Status = "Produto com Defeito";
                //Nesse caso o produto fica relacionado com a devolução.
                //Gera a devolução
                Devolucao dev = new(Produto.Descricao,MeiosDevolucaoComboBox.Text,MotivoTextBox.Text,Produto, $"{DateTime.Now:dd/MM/yyyy}", $"{DateTime.Now:HH:mm:ss}");
			    await CloudDataBase.RegisterLocalAsync(Collections.Devolucoes, dev);
                //Salva na coleção de produtos com defeito (Atualiza a quantidade caso o produto seja o mesmo)
                var sProd = await CloudDataBase.GetLocalAsync<Produto>(Collections.ProdutosComDefeito,x => x.ID == Produto.ID);
                if(sProd != null) { Produto.Quantidade += sProd.Quantidade; }
				await CloudDataBase.RegisterLocalAsync(Collections.ProdutosComDefeito,Produto,Builders<Produto>.Filter.Eq("ID",Produto.ID));
            }
            Modais.MostrarInfo("Devolução Registrada com Sucesso!");
            //
            Reset();
		}
        //
        private void DevolucaoButton_Click(object sender, RoutedEventArgs e)
        {
            RealizarDevolucao();
		}

        private void VoltarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CodigoInputBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                RegistrarProdutoParaDevoloucao();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(sender == DevolucaoCheckBox) { DefeitoCheckBox.IsChecked = false; }
            if(sender == DefeitoCheckBox) { DevolucaoCheckBox.IsChecked = false; }
        }
    }
}

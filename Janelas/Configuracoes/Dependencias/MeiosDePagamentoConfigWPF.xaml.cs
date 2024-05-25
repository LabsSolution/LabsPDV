﻿using Labs.LABS_PDV;
using System.Windows;
using System.Windows.Controls;
using static Labs.LABS_PDV.Modelos;

namespace Labs.Janelas.Configuracoes.Dependencias
{
    /// <summary>
    /// Lógica interna para MeiosDePagamentoConfigWPF.xaml
    /// </summary>
    public partial class MeiosDePagamentoConfigWPF : Window
    {
        MeiosPagamento Meios { get; set; } = null!;
        public MeiosDePagamentoConfigWPF()
        {
            InitializeComponent();
            // Tenta puxar da database, caso não consiga ele simplesmente cria um novo
            LoadFromDataBase();
        }
        //
        private bool Save()
        {
            //LOCAL
            CloudDataBase.RegisterLocalAsync(Collections.MeiosDePagamento, Meios);
            //ESPELHAMENTO CLOUD
            CloudDataBase.RegisterCloudAsync(Collections.MeiosDePagamento, Meios);
            return true;
        }
        //
        private async void LoadFromDataBase()
        {
            //Tentamos realizar a atribuição
            Meios = await CloudDataBase.GetCloudAsync<MeiosPagamento>(Collections.MeiosDePagamento, _ => true);
            // Se não conseguir do cloud pega do local
            Meios ??= await CloudDataBase.GetLocalAsync<MeiosPagamento>(Collections.MeiosDePagamento, _ => true);
            // A partir daqui Mostramos os Meios já registrados, caso não tenha criamos um novo objeto para a edição;
            Meios ??= new();
            // Por segurança verificamos se o meio é um objeto válido
            if (Meios != null)
            {
                List<Meio> mMeios = Meios.Meios;
                //Por precaução limpamos a lista primeiro
                ListaMeiosRegistrados.Items.Clear();
                foreach (Meio meio in mMeios)
                {
                    ListaMeiosRegistrados.Items.Add(meio);
                }
            }
        }

        private void AdicionarButton_Click(object sender, RoutedEventArgs e)
        {
            string NomeDoMeio = MeioPagamentoInputBox.Text;
            bool? SLDV = SLDVCheckBox.IsChecked;
            if (NomeDoMeio.IsNullOrEmpty()) { Modais.MostrarAviso("Você Precisa Digitar o Nome do Meio de Pagamento!"); return; }
            //
            Meio mPag = new(NomeDoMeio, (bool)SLDV!);
            //
            ListaMeiosRegistrados.Items.Add(mPag);
            Meios.Meios.Add(mPag);
            //
            MeioPagamentoInputBox.Text = null!;
            SLDVCheckBox.IsChecked = false;
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            Meio? meio = ListaMeiosRegistrados.SelectedItem as Meio;
            if (meio != null)
            {
                var r = Modais.MostrarPergunta("Deseja Remover o Meio Selecionado?");
                if (r == System.Windows.Forms.DialogResult.Yes)
                {
                    var index = ListaMeiosRegistrados.SelectedIndex;
                    //
                    if (index == 0) { Modais.MostrarAviso("Não é Possível Remover um Meio Protegido Internamente!"); return; }
                    //
                    ListaMeiosRegistrados.Items.Remove(meio);
                    Meios.Meios.Remove(meio);
                }
                //
            }
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            //Salvamos as configs por precaução e com espelhamento
            if (Save())
            {
                this.Close();
            }
        }
        //





    }

}

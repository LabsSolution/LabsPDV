using Labs.Main;
using MongoDB.Driver;
using System.Windows;
using System.Windows.Controls;
using Unimake.Business.DFe.Servicos;

namespace Labs.Janelas.Configuracoes.Dependencias
{
    /// <summary>
    /// Lógica interna para MeiosDePagamentoConfigWPF.xaml
    /// </summary>
    public partial class MeiosDePagamentoConfigWPF : Window
    {
        MeiosPagamentoNotaFiscal Meios { get; set; } = null!;
        public MeiosDePagamentoConfigWPF()
        {
            InitializeComponent();
            // Tenta puxar da database, caso não consiga ele simplesmente cria um novo
            LoadFromDataBase();
        }
        //
        private async Task<bool> Save()
        {
            //LOCAL
            await CloudDataBase.RegisterLocalAsync(Collections.MeiosDePagamento, Meios,Builders<MeiosPagamentoNotaFiscal>.Filter.Eq("ID",Meios.ID));
            //Espelhamento Cloud
            if (LabsMain.Cliente.PossuiPlanoCloud && LabsMainAppWPF.IsConnectedToInternet)
            {
                //Só fazenos o salvamento cloud caso o cliente tenha internet (caso contrário será apresentado um erro grotesco)
                await CloudDataBase.RegisterCloudAsync(Collections.MeiosDePagamento, Meios,Builders<MeiosPagamentoNotaFiscal>.Filter.Eq("ID",Meios.ID)); 
            }
            return true;
        }
        //
        private async void LoadFromDataBase()
        {
            //Tentamos realizar a atribuição
            if (LabsMain.Cliente.PossuiPlanoCloud && LabsMainAppWPF.IsConnectedToInternet)
            {
                Meios = await CloudDataBase.GetCloudAsync<MeiosPagamentoNotaFiscal>(Collections.MeiosDePagamento, _ => true);
            }
            // Se não conseguir do cloud pega do local
            Meios ??= await CloudDataBase.GetLocalAsync<MeiosPagamentoNotaFiscal>(Collections.MeiosDePagamento, _ => true);
            // A partir daqui Mostramos os Meios já registrados, caso não tenha criamos um novo objeto para a edição;
            Meios ??= new();
            // Por segurança verificamos se o meio é um objeto válido
            if (Meios != null)
            {
                List<MeioPagamentoNotaFiscal> mMeios = Meios.Meios;
                //Por precaução limpamos a lista primeiro
                ListaMeiosRegistrados.Items.Clear();
                foreach (MeioPagamentoNotaFiscal meio in mMeios)
                {
                    ListaMeiosRegistrados.Items.Add(meio);
                }
			}
            // Após carregar da database, adicionamos os meios para edição
			LoadAllMeios();
		}
		//
		private void LoadAllMeios()
		{
			//
			ListaMeiosDisponiveis.Items.Clear();
			//
			foreach (int Meio in Enum.GetValues(typeof(MeioPagamento)))
			{
				MeioPagamentoNotaFiscal meio = new((MeioPagamento)Meio, false, null!, false);
                if (Meio != 1 && Meio != 99 && Meio != 14 && Meios.Meios.Find(x => x.MeioPagamento == meio.MeioPagamento) == null!)
                { 
                    ListaMeiosDisponiveis.Items.Add(meio);
                }
			}
		}
		//
		// Pega da lista de disponiveis e passa para a lista de registrados
		private void AdicionarMeioPagamento_Click(object sender, RoutedEventArgs e)
		{
            if(ListaMeiosDisponiveis.SelectedItem is not MeioPagamentoNotaFiscal Meio) { return; }
            //
            ListaMeiosRegistrados.Items.Add(Meio);
            Meios.Meios.Add(Meio);
            //
            ListaMeiosDisponiveis.Items.Remove(Meio);
        }
        // Pega da lista de registrados e passa para a lista de disponíveis
        private void RemoverMeioPagamento_Click(object sender, RoutedEventArgs e)
		{
            if(ListaMeiosRegistrados.SelectedItem is not MeioPagamentoNotaFiscal Meio) { return; }
            if(ListaMeiosRegistrados.SelectedIndex == 0) { Modais.MostrarAviso("Não é Possível Remover um Meio Protegido Internamente!"); return; }
            //
            ListaMeiosDisponiveis.Items.Add(Meio);
            //
            ListaMeiosRegistrados.Items.Remove(Meio);
            Meios.Meios.Remove(Meio);
		}
		//Adicionar Meios de pagamento Customizados
		private void AdicionarButton_Click(object sender, RoutedEventArgs e)
        {
            string NomeDoMeio = MeioPagamentoInputBox.Text;
            bool? SLDV = SLDVCheckBox.IsChecked;
            bool? AceitaParcelas = ParcelasCheckBox.IsChecked;
            if (NomeDoMeio.IsNullOrEmpty()) { Modais.MostrarAviso("Você Precisa Digitar o Nome do Meio de Pagamento!"); return; }
            //
            MeioPagamentoNotaFiscal mPag = new(MeioPagamento.Outros,(bool)AceitaParcelas!,NomeDoMeio,(bool)SLDV!);
            //
            ListaMeiosRegistrados.Items.Add(mPag);
            Meios.Meios.Add(mPag);
            //
            MeioPagamentoInputBox.Text = null!;
            SLDVCheckBox.IsChecked = false;
            ParcelasCheckBox.IsChecked = false;
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListaMeiosRegistrados.SelectedItem is MeioPagamentoNotaFiscal meio)
            {
                var r = Modais.MostrarPergunta("Deseja Remover o Meio Selecionado?");
                if (r == MessageBoxResult.Yes)
                {
                    var index = ListaMeiosRegistrados.SelectedIndex;
                    //
                    if (index == 0) { Modais.MostrarAviso("Não é Possível Remover um Meio Protegido Internamente!"); return; }
                    if (meio.NomeDoMeio.IsNullOrEmpty()) { Modais.MostrarAviso("Para remover um meio pré-registrado utilize as setas acima!"); return; }
                    //
                    ListaMeiosRegistrados.Items.Remove(meio);
                    Meios.Meios.Remove(meio);
                }
                //
            }
        }

        private async void SairButton_Click(object sender, RoutedEventArgs e)
        {
            //Salvamos as configs por precaução e com espelhamento
            if (await Save())
            {
                this.Close();
            }
        }

		//





	}

}

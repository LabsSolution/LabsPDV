using Labs.LABS_PDV;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Labs.LABS_PDV.Modelos;

namespace Labs.Janelas.Configuracoes.Dependencias
{
    public partial class MeiosDePagamentoConfig : Form
    {
        //
        public const string MeiosCollection = "MeiosPagamento";
        //
        MeiosPagamento Meios { get; set; } = null!;



        public MeiosDePagamentoConfig()
        {
            InitializeComponent();
            // Tenta puxar da database, caso não consiga ele simplesmente cria um novo
            LoadFromDataBase();
        }
        private bool Save()
        {
            //LOCAL
            CloudDataBase.RegisterLocalAsync(MeiosCollection, Meios);
            //ESPELHAMENTO CLOUD
            CloudDataBase.RegisterCloudAsync(MeiosCollection, Meios);
            return true;
        }
        //
        private async void LoadFromDataBase()
        {
            //Tentamos realizar a atribuição
            Meios = await CloudDataBase.GetCloudAsync<MeiosPagamento>(MeiosCollection, _ => true);
            // Se não conseguir do cloud pega do local
            Meios ??= await CloudDataBase.GetLocalAsync<MeiosPagamento>(MeiosCollection, _ => true);
            // A partir daqui Mostramos os Meios já registrados, caso não tenha criamos um novo objeto para a edição;
            Meios ??= new();
            // Por segurança verificamos se o meio é um objeto válido
            if (Meios != null)
            {
                List<Meio> mMeios = Meios.Meios;
                //Por precaução limpamos a lista primeiro
                ListaMeiosRegistrados.Items.Clear();
                foreach (var meio in mMeios)
                {
                    ListViewItem item = new([meio.Item1]);
                    ListaMeiosRegistrados.Items.Add(item);
                }
            }
        }
        //
        private void AdicionarButton_Click(object sender, EventArgs e)
        {
            string NomeDoMeio = MeioDePagamentoBoxInput.Text;
            bool SLDV = SemLimiteDeValor.Checked;
            if (NomeDoMeio.IsNullOrEmpty()) { Modais.MostrarAviso("Você Precisa Digitar o Nome do Meio de Pagamento!"); return; }
            //
            Meio mPag = new(NomeDoMeio,SLDV);
            //
            ListViewItem item = new([NomeDoMeio]);
            ListaMeiosRegistrados.Items.Add(item);
            Meios.Meios.Add(mPag);
            //
            MeioDePagamentoBoxInput.Text = null!;
            SemLimiteDeValor.Checked = false;
        }
        //
        private void RemoverButton_Click(object sender, EventArgs e)
        {
            if(ListaMeiosRegistrados.SelectedIndices.Count > 0)
            {
                var r = Modais.MostrarPergunta("Deseja Remover o Meio Selecionado?");
                if(r == DialogResult.Yes)
                {
                    var item = ListaMeiosRegistrados.SelectedItems[0];
                    var index = ListaMeiosRegistrados.SelectedIndices[0];
                    //
                    if(index == 0) { Modais.MostrarAviso("Não é Possível Remover um Meio Protegido Internamente!"); return; }
                    //
                    ListaMeiosRegistrados.Items.Remove(item);
                    Meios.Meios.RemoveAt(index);
                }
                //
            }
        }
        //
        private void SairButton_Click(object sender, EventArgs e)
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

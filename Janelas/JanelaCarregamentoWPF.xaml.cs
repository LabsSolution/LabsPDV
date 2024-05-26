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

namespace Labs.Janelas
{
    /// <summary>
    /// Lógica interna para JanelaCarregamentoWPF.xaml
    /// </summary>
    public partial class JanelaCarregamentoWPF : Window
    {
        public JanelaCarregamentoWPF()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Define o Texto que irá aparecer na tela de carregamento
        /// </summary>
        /// <param name="texto"></param>
        public void SetTextoFrontEnd(string texto)
        {
            Titulo.Text = texto;
        }
        /// <summary>
        /// Configura a Barra de Carregamento da Janela
        /// </summary>
        /// <param name="Min">Valor Minimo da barra</param>
        /// <param name="Max">Valor Máximo da barra</param>
        public void ConfigBarraDeCarregamento(int Min, int Max)
        {
            LoadingBar.Minimum = Min;
            LoadingBar.Maximum = Max;
        }
        //
        public void AumentarBarraDeCarregamento(int valor)
        {
            LoadingBar.Value += valor;
        }
        //
        public void SetarValor(int valor)
        {
            LoadingBar.Value = valor;
        }
    }
}

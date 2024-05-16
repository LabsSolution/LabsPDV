using Labs.LABS_PDV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs
{
    public partial class TesteDeImpressora : Form
    {
        public TesteDeImpressora()
        {
            InitializeComponent();
            LoadImpressoras();
        }

        private void LoadImpressoras()
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                ImpressorasComboBox.Items.Add(printer);
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {

        }
    }
}

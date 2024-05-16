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

        //
        //
        private Size Papel58mm = new(58,210);

        private Size Papel76mm = new(76,210);

        private Size Papel80mm = new(80,210);
        //Usado para cálculo interno
        int LarguraPapel = 210; // largura em mm (usado como limitador) (Por algum motivo o segundo valor é o que vale) (o porque eu não sei)
        int LimiteNomeProduto = 30;

        private Font Bold = new (FontFamily.GenericSansSerif, 10, FontStyle.Bold);
        //
        private Font Regular = new (FontFamily.GenericSansSerif, 8, FontStyle.Regular);
        //
        private Font RegularPedido = new (FontFamily.GenericSansSerif, 7, FontStyle.Regular);
        //
        private Font RegularEndereco = new (FontFamily.GenericSansSerif, 6, FontStyle.Regular);
        //
        private Font RegularItens = new (FontFamily.GenericSansSerif, 6, FontStyle.Regular);

        //

        private void LoadImpressoras()
        {
            ImpressorasComboBox.Items.Clear();//
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                ImpressorasComboBox.Items.Add(printer);
            }
        }
        //
        // Criar função para cálculo de tamanho de texto
        //
        private void PrintButton_Click(object sender, EventArgs e)
        {
            //
            if (ImpressorasComboBox.Text.IsNullOrEmpty()) { Modais.MostrarAviso("Selecione uma Impressora!"); return; }
            //
            
        }
        public void ImprimirCupomNaoFiscal()
        {
            using (var pd = new PrintDocument())
            {
                pd.PrinterSettings.PrinterName = ImpressorasComboBox.Text;
                pd.PrintPage += printPage;
                pd.Print();
            }
        }
        //
        private void printPage(object send, PrintPageEventArgs e)
        {
            Graphics? graphics = e.Graphics;
            if (graphics == null) { return; }
            float yPos = 0;
            //
            //print header
            graphics.DrawString("NOME DA EMPRESA", Bold, Brushes.Black,0, yPos);
            yPos += 15;
            //
            var endereco = Utils.FormatarTexto("ENDEREÇO DA EMPRESA" + " Nº " + "NUMERO DA EMPRESA",RegularEndereco,graphics,LarguraPapel,out float yEndereco); 
            // Usamos o limite gerado pela função de formatação e printamos a linha limite :D
            graphics.DrawString(endereco, RegularEndereco, Brushes.Black, 0, yPos);
            yPos += yEndereco; // A Cada Impressão Atualizamos a posição Vertical da Agulha
            //
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            graphics.DrawString("CUPOM NÃO FISCAL", Bold, Brushes.Black, 0, yPos);
            yPos += 15;
            //
            //
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            graphics.DrawString("PEDIDO / VENDA: " + "ID PEDIDO / VENDA", RegularPedido, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 30;
            //
            //Cabeçalho dos Itens
            graphics.DrawString("PROD | R$ UNI | QTD | TOTAL R$", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 10;

            //itens de venda
            //foreach (var iv in venda.items)
            //{
            for (int i = 0; i < 2; i++)
            {
                string produtoDesc = $"{"NOME DO PRODUTO MUITO GRANDE N°: " + i}";
                produtoDesc = produtoDesc.Length > LimiteNomeProduto ? $"{produtoDesc[..LimiteNomeProduto]}..." : produtoDesc; // se o produto tiver mais de 20 de tamanho, cortamos o nome e colocamos ... no final
                graphics.DrawString(produtoDesc, RegularItens, Brushes.Black, 0, yPos);
                yPos += RegularItens.Size + 1;
                //
                string valores = $"R$ Uni: 2,00 Qtd: 10 Total R$ 20,00";
                graphics.DrawString(valores, RegularItens, Brushes.Black, 0, yPos);
                yPos += 10;
                graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
                yPos += 10;
            }
            //}
            yPos += 15;
            //
            graphics.DrawString($"TOTAL R$: {20,00}", Bold, Brushes.Black, 0, yPos);
            yPos += 15;

            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 5;

            //bottom
            graphics.DrawString($"Data: {DateTime.Now:dd/MM/yyyy} Hora: {DateTime.Now:HH:mm:ss}", RegularItens, Brushes.Black, 0, yPos);
            yPos += 10;
            graphics.DrawString("© Lab Soluções ©",RegularItens,Brushes.Black,0,yPos);
            e.HasMorePages = false;
        }
    }
}

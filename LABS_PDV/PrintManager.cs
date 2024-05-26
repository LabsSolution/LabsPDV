using Labs.Janelas.LabsPDV.Dependencias;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static Labs.LABS_PDV.Modelos;
using Brushes = System.Drawing.Brushes;
using FontFamily = System.Drawing.FontFamily;
using FontStyle = System.Drawing.FontStyle;
using Size = System.Drawing.Size;

namespace Labs.LABS_PDV
{
	public class PrintManager : PrintDocument
	{
        //
        //public const string ImpressoraDefault = "POS-58 Baisec";
        public const string ImpressoraDefault = "HP LaserJet Professional P1102w";
        //
        private Size Papel58mm = new(58, 210);
        //
        private Size Papel76mm = new(76, 210);
        //
        private Size Papel80mm = new(80, 210);
        //Holders
        //Para a impressão de Cupons ( e depois emissão de nota fiscal )
        VendaRealizada Venda = null!;
        FechamentoDeCaixa Fechamento = null!;
        //
        int LarguraPapel = 210; // largura em mm (usado como limitador) (Por algum motivo o segundo valor é o que vale) (o porque eu não sei)
        int LimiteNomeProduto = 30;

        private Font Bold = new(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
        //
        private Font Regular = new(FontFamily.GenericSansSerif, 8, FontStyle.Regular);
        //
        private Font RegularPedido = new(FontFamily.GenericSansSerif, 7, FontStyle.Regular);
        //
        private Font RegularEndereco = new(FontFamily.GenericSansSerif, 6, FontStyle.Regular);
        //
        private Font RegularItens = new(FontFamily.GenericSansSerif, 6, FontStyle.Regular);
        //
        // Criar função para cálculo de tamanho de texto
        //
        public void ImprimirCupomNaoFiscalLoja(string Impressora,VendaRealizada Venda)
        {
            //Realizamos as configs iniciais
            PrinterSettings.PrinterName = Impressora;
            this.Venda = Venda;
            //Começamos o processo de Print
            PrintPage += ICNFLoja;
            EndPrint += OnEndPrinting;
            // Mostra a pré-visualização antes de imprimir
            Print();
        }
        //
        public void ImprimirCupomNaoFiscalCliente(string Impressora,VendaRealizada Venda)
        {
            //Realizamos as configs iniciais
            PrinterSettings.PrinterName = Impressora;
            this.Venda = Venda;
            //Começamos o processo de Print
            PrintPage += ICNFCliente;
            EndPrint += OnEndPrinting;
            // Mostra a pré-visualização antes de imprimir
            Print();
        }
        //
        public void ImprimirCupomFechamentoDeCaixa(string Impressora,FechamentoDeCaixa Fechamento)
        {
            //Realizamos as configs iniciais
            PrinterSettings.PrinterName = Impressora;
            //Configs Necessárias para o cupom
            this.Fechamento = Fechamento; 
            //Começamos o processo de Print
            PrintPage += ICNFFechamento;
            EndPrint += OnEndPrinting;
            // Mostra a pré-visualização antes de imprimir
            Print();
        }
        // Quando Terminam de Printar, Deserdam do evento
        private void OnEndPrinting(object sender, PrintEventArgs e)
        {
            //Automaticamente deserdam do evento para não sobreescrever
            PrintPage -= ICNFCliente;
            PrintPage -= ICNFLoja;
            PrintPage -= ICNFFechamento;
            EndPrint -= OnEndPrinting;
        }
        //
        #region Impressão Cupom Fechamento de Caixa
        private void ICNFFechamento(object send, PrintPageEventArgs e)
        {
            Graphics? graphics = e.Graphics;
            if (graphics == null) { return; }
            float yPos = 0; // Variável responsável por determinar a posição da agulha
            //
            //print header
            graphics.DrawString("FECHAMENTO DE CAIXA", Bold, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 15;
            graphics.DrawString($"ID: {Fechamento.FechamentoID}", Bold, Brushes.Black, 0, yPos);
            yPos += 30;
            //
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            graphics.DrawString($"DATA: {DateTime.Now:dd/MM/yyyy}", RegularPedido, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"HORA: {DateTime.Now:HH:mm:ss}", RegularPedido, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 15;
            //
            graphics.DrawString($"Valor de Abertura R$: {Utils.FormatarValor(Fechamento.ValorDeAbertura)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Fundo de Caixa R$: {Utils.FormatarValor(Fechamento.FundoDeCaixa)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            graphics.DrawString($"MEIOS RECEBIDOS", Bold, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 5;
            foreach (ValorFechado MeioRecebido in Fechamento.Recebimentos)
            {
                graphics.DrawString($"{MeioRecebido.Nome}", RegularItens, Brushes.Black, 0, yPos);
                yPos += 15;
                graphics.DrawString($"R$: {Utils.FormatarValor(MeioRecebido.ValorAferido)}", RegularItens, Brushes.Black, 0, yPos);
                yPos += 15;
                graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            }
            yPos += 15;
            graphics.DrawString($"Ganhos Totais. R$: {Utils.FormatarValor(Fechamento.GanhosTotais)}", Regular, Brushes.Black, 0, yPos);
            yPos += 20;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 5;
            //bottom
            graphics.DrawString(LABS_PDV_MAIN_WPF.TradeMark, RegularItens, Brushes.Black, 0, yPos);
            e.HasMorePages = false;
        }
        #endregion

        #region Impressão Cupom Não Fiscal Loja
        //
        private void ICNFLoja(object send, PrintPageEventArgs e)
        {
            Graphics? graphics = e.Graphics;
            if (graphics == null) { return; }
            float yPos = 0; // Variável responsável por determinar a posição da agulha
            //
            //print header
            graphics.DrawString("CONTROLE INTERNO", Bold, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 30;
            //
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            graphics.DrawString($"PEDIDO / VENDA: {Venda.IDVenda}", RegularPedido, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 15;
            //
            graphics.DrawString($"Total R$: {Utils.FormatarValor(Venda.Total)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Desconto: {Utils.FormatarValor(Venda.Desconto)}%", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Total c. Desc. R$: {Utils.FormatarValor(Venda.TotalComDesconto)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Valor Pago R$: {Utils.FormatarValor(Venda.ValorPago)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Troco R$: {Utils.FormatarValor(Venda.Troco)}", Regular, Brushes.Black, 0, yPos);
            yPos += 20;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            graphics.DrawString($"MEIO(S) DE PAGAMENTO", Bold, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 5;
            foreach (var Pagamento in Venda.PagamentosEfetuados)
            {
                graphics.DrawString($"{Pagamento.DescPagamento} R$: {Utils.FormatarValor(Pagamento.Valor)}", RegularItens, Brushes.Black, 0, yPos);
                yPos += 15;
            }
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 5;

            //bottom
            graphics.DrawString($"Data: {DateTime.Now:dd/MM/yyyy} Hora: {DateTime.Now:HH:mm:ss}", RegularItens, Brushes.Black, 0, yPos);
            yPos += 10;
            graphics.DrawString(LABS_PDV_MAIN_WPF.TradeMark, RegularItens, Brushes.Black, 0, yPos);
            e.HasMorePages = false;
        }
        #endregion
        //
        #region Impressão Cupom Não Fiscal Cliente
        private void ICNFCliente(object send, PrintPageEventArgs e)
        {
            Graphics? graphics = e.Graphics;
            if (graphics == null) { return; }
            float yPos = 0; // Variável responsável por determinar a posição da agulha
            //
            //print header
            graphics.DrawString("NOME DA EMPRESA", Bold, Brushes.Black, 0, yPos);
            yPos += 15;
            //
            var endereco = Utils.FormatarTexto("ENDEREÇO DA EMPRESA" + " Nº " + "NUMERO DA EMPRESA", RegularEndereco, graphics, LarguraPapel, out float yEndereco);
            // Usamos o limite gerado pela função de formatação e printamos a linha limite :D
            graphics.DrawString(endereco, RegularEndereco, Brushes.Black, 0, yPos);
            yPos += yEndereco;
            yPos += 20; // A Cada Impressão Atualizamos a posição Vertical da Agulha
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            //
            graphics.DrawString("CUPOM NÃO FISCAL", Bold, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 30;
            //
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            graphics.DrawString($"PEDIDO / VENDA: {Venda.IDVenda}", RegularPedido, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 30;
            //
            //Cabeçalho dos Itens
            graphics.DrawString("PROD | R$ UNI | QTD | TOTAL R$", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 10;
            //
            //itens de venda
            foreach (var Produto in Venda.Produtos)
            {
                string produtoDesc = $"{Produto.Descricao}";
                produtoDesc = produtoDesc.Length > LimiteNomeProduto ? $"{produtoDesc[..LimiteNomeProduto]}..." : produtoDesc; // se o produto tiver mais de 20 de tamanho, cortamos o nome e colocamos ... no final
                graphics.DrawString(produtoDesc, RegularItens, Brushes.Black, 0, yPos);
                yPos += 10;
                //
                string valores = $"Uni R$: {Utils.FormatarValor(Produto.Preco)} Qtd: {Produto.Quantidade} Total R$ {Utils.FormatarValor(Math.Round(Produto.Quantidade * Produto.Preco, 2))}";
                graphics.DrawString(valores, RegularItens, Brushes.Black, 0, yPos);
                yPos += 10;
                graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
                yPos += 5;
            }
            yPos += 5;
            //
            graphics.DrawString($"Total R$: {Utils.FormatarValor(Venda.Total)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Desconto: {Utils.FormatarValor(Venda.Desconto)}%", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Total c. Desc. R$: {Utils.FormatarValor(Venda.TotalComDesconto)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Valor Pago R$: {Utils.FormatarValor(Venda.ValorPago)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Troco R$: {Utils.FormatarValor(Venda.Troco)}", Regular, Brushes.Black, 0, yPos);
            yPos += 20;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            graphics.DrawString($"MEIO(S) DE PAGAMENTO", Bold, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 5;
            foreach (var Pagamento in Venda.PagamentosEfetuados)
            {
                graphics.DrawString($"{Pagamento.DescPagamento} R$: {Utils.FormatarValor(Pagamento.Valor)}", RegularItens, Brushes.Black, 0, yPos);
                yPos += 15;
            }
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 5;

            //bottom
            graphics.DrawString($"Data: {DateTime.Now:dd/MM/yyyy} Hora: {DateTime.Now:HH:mm:ss}", RegularItens, Brushes.Black, 0, yPos);
            yPos += 10;
            graphics.DrawString(LABS_PDV_MAIN_WPF.TradeMark, RegularItens, Brushes.Black, 0, yPos);
            e.HasMorePages = false;
        }
        #endregion
    }
}

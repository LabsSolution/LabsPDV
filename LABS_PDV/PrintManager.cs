﻿using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labs.LABS_PDV.Modelos;

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
        List<Produto> Produtos { get; set; } = [];
        //
        string MeioPagamento = null!;
        //
        double ValorTotalComDesconto = 0;
        double ValorTotalSemDesconto = 0;
        double ValorPago = 0;
        double ValorTroco = 0;
        double Desconto = 0;

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
        public void ImprimirCupomNaoFiscal(string Impressora,List<Produto> Produtos, double ValorTotalComDesconto,double ValorTotalSemDesconto, double ValorPago,double Desconto,double ValorTroco, string MeioPagamento)
        {
            //Realizamos as configs iniciais
            PrinterSettings.PrinterName = Impressora;
            this.Produtos = Produtos;
            this.ValorTotalComDesconto = ValorTotalComDesconto;
            this.ValorTotalSemDesconto = ValorTotalSemDesconto;
            this.ValorPago = ValorPago;
            this.ValorTroco = ValorTroco;
            this.Desconto = Desconto;
            this.MeioPagamento = MeioPagamento;
            //Começamos o processo de Print
            PrintPage += ICNF;
            Print();
        }
        //
        private void ICNF(object send, PrintPageEventArgs e)
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
            yPos += yEndereco + 20; // A Cada Impressão Atualizamos a posição Vertical da Agulha
            //
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            graphics.DrawString("CUPOM NÃO FISCAL", Bold, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            //
            yPos += 15;
            //
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
            //
            //itens de venda
            foreach (var Produto in Produtos)
            {
                string produtoDesc = $"{Produto.Descricao}";
                produtoDesc = produtoDesc.Length > LimiteNomeProduto ? $"{produtoDesc[..LimiteNomeProduto]}..." : produtoDesc; // se o produto tiver mais de 20 de tamanho, cortamos o nome e colocamos ... no final
                graphics.DrawString(produtoDesc, RegularItens, Brushes.Black, 0, yPos);
                yPos += 10;
                //
                string valores = $"Uni R$: {Utils.FormatarValor(Produto.Preco)} Qtd: {Produto.Quantidade} Total R$ {Utils.FormatarValor(Math.Round(Produto.Quantidade * Produto.Preco,2))}";
                graphics.DrawString(valores, RegularItens, Brushes.Black, 0, yPos);
                yPos += 10;
                graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
                yPos += 5;
            }
            yPos += 15;
            //
            graphics.DrawString($"Total R$: {Utils.FormatarValor(ValorTotalSemDesconto)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Desconto: {Utils.FormatarValor(Desconto)}%", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Total c. Desc. R$: {Utils.FormatarValor(ValorTotalComDesconto)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Valor Pago R$: {Utils.FormatarValor(ValorPago)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"Troco R$: {Utils.FormatarValor(ValorTroco)}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            graphics.DrawString($"MEIO DE PAGAMENTO", Bold, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawString($"{MeioPagamento}", Regular, Brushes.Black, 0, yPos);
            yPos += 15;
            graphics.DrawLine(Pens.Black, 0, yPos, LarguraPapel, yPos);
            yPos += 5;

            //bottom
            graphics.DrawString($"Data: {DateTime.Now:dd/MM/yyyy} Hora: {DateTime.Now:HH:mm:ss}", RegularItens, Brushes.Black, 0, yPos);
            yPos += 10;
            graphics.DrawString("© Lab Soluções ©", RegularItens, Brushes.Black, 0, yPos);
            e.HasMorePages = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labs.LABS_PDV.Modelos;

namespace Labs.LABS_PDV
{
    public class CaixaLabs
    {
        public OperadorCaixa OperadorCaixa { get; private set; }
        /// <summary>
        /// Valor de Abertura do caixa (Com Quanto Dinheiro o caixa abriu inicialmente no dia)
        /// </summary>
        public double ValorAbertura { get { return this.ValorAbertura; } private set { Math.Round(value, 2); } }
        /// <summary>
        /// Valor Total contido no caixa.
        /// </summary>
        public double ValorTotalNoCaixa { get { return Math.Round(this.ValorTotalNoCaixa,2); } private set { Math.Round(value, 2); } }
        //
        //Agora Registramos os Modos de pagamento Existentes;
        public Dictionary<string, RIDP> RegistroInternoDePagamentos = new()
        {
            //O valor de Chave deve ser igual ao do RIDP
            {MeioPagamento.DINHEIRO,new(MeioPagamento.DINHEIRO,0)},
            //
            {MeioPagamento.PIX,new(MeioPagamento.PIX,0)},
            //
            {MeioPagamento.CARTAODEBITO,new(MeioPagamento.CARTAODEBITO,0)},
            //
            {MeioPagamento.CARTAOCREDITO,new(MeioPagamento.CARTAOCREDITO,0)}
        };
        public CaixaLabs(double ValorDeAbertura, OperadorCaixa operadorCaixa)
        {
            this.ValorAbertura = ValorDeAbertura;
            OperadorCaixa = operadorCaixa;
        }
        //----------------------------//
        //----------MÉTODOS-----------//
        //----------------------------//
        //Fazemos a requisição de Abertura

        public void RealizarAbertura() //Como o construtor obriga a realização de instância, não precisamos de um referenciador interno
        {
            RegistroInternoDePagamentos[MeioPagamento.DINHEIRO].CapitalDeGiro = ValorAbertura;
        }
        //
        public void AdicionarCapital()
        {

        }
    }
}

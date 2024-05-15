﻿using Labs.Janelas.Configuracoes.Dependencias;
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
        //
        //
        public OperadorCaixa OperadorCaixa { get; private set; }
        /// <summary>
        /// Valor de Abertura do caixa (Com Quanto Dinheiro o caixa abriu inicialmente no dia)
        /// </summary>
        public double FundoDeCaixa { get; private set; }
        /// <summary>
        /// Valor Total Contido no Caixa (Somatório de Todos os Ganhos + Valor de Abertura)
        /// </summary>
        public double ValorTotal { get; private set; }
        /// <summary>
        /// Valor Total contido no caixa em dinheiro
        /// </summary>
        public double ValorTotalEmDinheiro { get; private set; }
        /// <summary>
        /// Ganhos totais do Caixa (Juntando todos os Meios de Pagamento)
        /// </summary>
        public double GanhosTotais { get; private set; }
        //
        //Agora Registramos os Modos de pagamento Existentes;
        public Dictionary<string, RIDP> RegistroInternoDePagamentos = new();

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="ValorDeAbertura"> Valor de Abertura Inicial</param>
        /// <param name="operadorCaixa">Referência a quem é o operador deste caixa</param>
        public CaixaLabs(double ValorDeAbertura, OperadorCaixa operadorCaixa)
        {
            this.FundoDeCaixa = ValorDeAbertura;
            OperadorCaixa = operadorCaixa;
        }
        //
        public MeiosPagamento Meios { get; private set; } = null!;
        /// <summary>
        /// Carrega os meios direto da database seguindo o espelhamento padrão
        /// </summary>
        /// <returns> Returna uma Task nula de Espera</returns>
        private async Task LoadFromDataBase()
        {
            //Tentamos realizar a atribuição
            Meios = await CloudDataBase.GetCloudAsync<MeiosPagamento>(MeiosDePagamentoConfig.MeiosCollection, _ => true);
            // Se não conseguir do cloud pega do local
            Meios ??= await CloudDataBase.GetLocalAsync<MeiosPagamento>(MeiosDePagamentoConfig.MeiosCollection, _ => true);
            // A partir daqui Mostramos os Meios já registrados, caso não tenha criamos um novo objeto para a edição;
            Meios ??= new();
            //
            if(Meios != null)
            {
                RegistroInternoDePagamentos.Clear();//Limpamos o registro como forma de proteção
                //
                for (int i = 0; i < Meios.Meios.Count; i++)
                {
                    string NomeMeio = Meios.Meios[i].Item1;
                    bool SLDV = Meios.Meios[i].Item2;
                    //
                    RegistroInternoDePagamentos.Add(i.ToString(), new(NomeMeio, 0, SLDV)); // O Index é usado como chave do dicionário;
                }
                //
            }
        }
        //-----------------------------------------------------------------//

        //----------------------------//
        //----------MÉTODOS-----------//
        //----------------------------//

        /// <summary>
        /// Retorna o Valor Total no Caixa em Dinheiro (Cédula)
        /// </summary>
        public double GetValorTotalNoCaixa()
        {
            // "0" é o index padrão do Registro DINHEIRO
            ValorTotalEmDinheiro = RegistroInternoDePagamentos["0"].CapitalDeGiro;
            //
            return ValorTotalEmDinheiro;
        }

        //

        public void AtualizarCaixa()
        {
            //Atualiza o Valor total No caixa em Cédula
            ValorTotalEmDinheiro = RegistroInternoDePagamentos["0"].CapitalDeGiro;
            //Agora atualiza os ganhos Totais//
            GanhosTotais = 0;
            //
            foreach (var RegistroInterno in RegistroInternoDePagamentos.Values)
            {
                GanhosTotais += RegistroInterno.CapitalDeGiro;
            }
            //
            ValorTotal = GanhosTotais + FundoDeCaixa;
        }

        //

        public async void RealizarAbertura() //Como o construtor obriga a realização de instância, não precisamos de um referenciador interno
        {
            //Carregamos da database os meios
            await LoadFromDataBase(); // Precisamos que o método finalize para que possamos seguir
            //Index 0 é o meio padrão para a adição de Fundo de caixa
            RegistroInternoDePagamentos["0"].CapitalDeGiro = FundoDeCaixa;
        }
        //
        /// <summary>
        /// Adiciona um capital ao meio definido pelo ID
        /// </summary>
        /// <param name="Index">Index do Meio para adicionar o capital ( 0 ) - DINHEIRO</param>
        /// <param name="valor">Valor de capital a ser adicionado</param>
        public void AdicionarCapitalAoMeio(int Index,double valor)
        {
            if (RegistroInternoDePagamentos.ContainsKey($"{Index}"))
            {
                RegistroInternoDePagamentos[$"{Index}"].AdicionarCapital(valor);
            }
        }
        //
        /// <summary>
        /// Remove um valor do registro usando o index do meio
        /// </summary>
        /// <param name="Index">Index do Meio para remover o capital ( 0 ) - DINHEIRO</param>
        /// <param name="valor">Valor que foi retirado</param>
        public void RemoverCapitalDoMeio(int Index, double valor)
        {
            if (RegistroInternoDePagamentos.ContainsKey($"{Index}"))
            {
                RegistroInternoDePagamentos[$"{Index}"].RetirarValor(valor);
            }
            //
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.LABS_PDV
{
    /// <summary>
    /// Registro Interno De Pagamentos (RIDP).
    /// </summary>
    public class RIDP(string NomeDoRegistro,double CapitalDeGiroInicial = 0,bool SemLimiteDeValor = false)
    {
        //
        public bool SemLimiteDeValor { get; private set; } = SemLimiteDeValor;
        //
        public string NomeDoRegistro { get; set; } = NomeDoRegistro;
        //
        private double _capitalDeGiro = CapitalDeGiroInicial;
        public double CapitalDeGiro { get { return Math.Round(_capitalDeGiro, 2); } set { _capitalDeGiro = Math.Round(value, 2); } }
        //
        public void AdicionarCapital(double valor)
        {
            CapitalDeGiro += valor;
        }
        //
        public void RetirarValor(double valor)
        {
            CapitalDeGiro -= valor;
        }
    }
}

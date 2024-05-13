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
        public string NomeDoRegistro { get; set; } = NomeDoRegistro;
        public double CapitalDeGiro { get { return Math.Round(CapitalDeGiro, 2); } set => Math.Round(CapitalDeGiroInicial, 2); }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.LABS_PDV
{
	public class Modelos
	{
		//MODELOS DE Objetos (Structs)
		public struct ModoDePagamento(string Modo, bool Ativo, int Parcelas = 1, double Taxa = 0.0)
		{
			public string Modo { get; private set; } = Modo;
			public bool Ativo { get; private set; } = Ativo;
			public int Parcelas { get; private set; } = Parcelas;
			public double Taxa { get; private set; } = Taxa;
		}
		//
		//
		//
		public struct Produto
		{
			public int ID { get; set; }
			public string Descricao { get; set; }
			public int Quantidade { get; set; }
			public string Preco {  get; set; }
			public string CodBarras {  get; set; }
			//Métodos
			public readonly double GetPrecoAsDouble()
			{
				if(Utils.TryParseToDouble(this.Preco,out double value)) { return value; }
				return 0.0;
			}
		}
		//
	}
}

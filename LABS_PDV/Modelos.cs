using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.LABS_PDV
{
	internal class Modelos
	{
		//MODELOS DE Objetos (Structs)
		public struct Produto
		{
			public int ID { get; set; }
			public string Nome { get; set; }
			public int Quantidade { get; set; }
			public double Preco {  get; set; }
			public string CodBarras {  get; set; }
		}
		//
	}
}

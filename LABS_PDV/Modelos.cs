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
		public struct MeioDePagamento(string Meio, bool Ativo = false, List<ModoDePagamento> Modos = default!)
		{
			/// <summary>
			/// Nome do Meio de Pagamento (Grupo Utilizado como biblioteca para o Modo)
			/// </summary>
			public string Meio { get; private set; } = Meio;
			/// <summary>
			/// Indica se esse meio de pagamento está ativo
			/// </summary>
			public bool Ativo { get; private set; } = Ativo;
			//
			public List<ModoDePagamento> Modos { get; private set; } = Modos;
		}
		//
		public struct ModoDePagamento(string Modo,string Bandeira, bool Ativo,bool PossuiParcelas = false, int Parcelas = 1, double Taxa = 0.0)
		{
			/// <summary>
			/// Nome do Modo de Pagamento (Usado Para a Biblioteca de Meios de Pagamento)
			/// </summary>
			public string Modo { get; private set; } = Modo;
			/// <summary>
			/// Bandeira Representante do Modo de Pagamento (Usado em Cartões)
			/// </summary>
			public string Bandeira {  get; private set; } = Bandeira;
			/// <summary>
			/// Indica se o Modo está ativo ou não
			/// </summary>
			public bool Ativo { get; private set; } = Ativo;
			/// <summary>
			/// Indicador se Possui Parcelas
			/// </summary>
			public bool PossuiParcelas { get; private set; } = PossuiParcelas;
			/// <summary>
			/// Quantidade de Parcelas do Modo de pagamento
			/// </summary>
			public int Parcelas { get; private set; } = Parcelas;
			/// <summary>
			/// Taxa Adicional do Modo de Pagamento (Geralmente Cartão de Crédito faz isso).
			/// </summary>
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

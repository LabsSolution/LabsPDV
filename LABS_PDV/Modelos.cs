﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.LABS_PDV
{
	public class Modelos
	{
		public struct PagamentoEfetuado(string DescPagamento, double valor)
		{
			/// <summary>
			/// Descrição do pagamento efetuado
			/// </summary>
			public string DescPagamento { get; private set; } = DescPagamento;
			/// <summary>
			/// Valor do pagamento efetuado
			/// </summary>
			public double Valor { get; private set; } = valor;
		}
		//MODELOS DE Objetos (Structs)
		public class MeioDePagamento(string Meio, bool PodeUltrapassarOValorTotal = false, bool PossuiModos = false, List<ModoDePagamento> Modos = default!)
		{
			[BsonId]
			[BsonRepresentation(BsonType.ObjectId)]
			public string ID { get; set; } = null!;
			/// <summary>
			/// Nome do Meio de Pagamento (Grupo Utilizado como biblioteca para o Modo)
			/// </summary>
			public string Meio { get; set; } = Meio;
			/// <summary>
			/// Determinador se esse meio de pagamento possui mais de um modo.
			/// </summary>
			public bool PossuiModos { get; set; } = PossuiModos;
			public bool PodeUltrapassarOValorTotal { get; set; } = PodeUltrapassarOValorTotal;
			public List<ModoDePagamento> Modos { get; set; } = Modos;
		}
		//
		public class ModoDePagamento(string Modo,bool PossuiBandeira = false,List<string> Bandeiras = default!,bool PossuiParcelas = false, int Parcelas = 0, double Taxa = 0.0)
		{
			[BsonId]
			[BsonRepresentation(BsonType.ObjectId)]
			public string ID { get; set; } = null!;
			/// <summary>
			/// Nome do Modo de Pagamento (Usado Para a Biblioteca de Meios de Pagamento)
			/// </summary>
			public string Modo { get; set; } = Modo;
			/// <summary>
			/// Diz se esse modo de pagamento Possui Bandeira
			/// </summary>
			public bool PossuiBandeira { get; set; } = PossuiBandeira;
			/// <summary>
			/// Bandeira Representante do Modo de Pagamento (Usado em Cartões)
			/// </summary>
			public List<string> Bandeiras {  get; set; } = Bandeiras;
			/// <summary>
			/// Indicador se Possui Parcelas
			/// </summary>
			public bool PossuiParcelas { get; set; } = PossuiParcelas;
			/// <summary>
			/// Quantidade de Parcelas do Modo de pagamento
			/// </summary>
			public int Parcelas { get; set; } = Parcelas;
			/// <summary>
			/// Taxa Adicional do Modo de Pagamento (Geralmente Cartão de Crédito faz isso).
			/// </summary>
			public double Taxa { get; set; } = Taxa;
		}
		//
		//
		//
		public class Produto(string Descricao = null!, int Quantidade = 0, double Preco = 0, string CodBarras = null!)
		{
			//Identificador na database
			[BsonId]
			[BsonRepresentation(BsonType.ObjectId)]
			public string ID { get; set; } = null!;
			//Parâmetros
			public string Descricao { get; set; } = Descricao;
			public int Quantidade { get; set; } = Quantidade;
			public double Preco { get; set; } = Preco;
			public string CodBarras { get; set; } = CodBarras;
		}
		//
	}
}

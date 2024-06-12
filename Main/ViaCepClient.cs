using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static Labs.Main.Modelos;

namespace Labs.Main
{
	public class ViaCepClient
	{
		private static readonly HttpClient client = new();

		public async Task<Endereco> GetEnderecoAsync(string cep)
		{
			if (string.IsNullOrWhiteSpace(cep))
			{
				throw new ArgumentException("O CEP não pode ser nulo ou vazio.", nameof(cep));
			}

			string url = $"https://viacep.com.br/ws/{cep}/json/";

			HttpResponseMessage response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();
				Endereco? address = JsonConvert.DeserializeObject<Endereco>(jsonResponse);

				return address == null ? throw new Exception("Não foi possível desserializar a resposta JSON.") : address;
			}
			else
			{
				throw new HttpRequestException($"Erro ao obter o endereço. Status code: {response.StatusCode}");
			}
		}
	}
}

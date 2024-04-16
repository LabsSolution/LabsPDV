using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Labs.LABS_PDV
{

	//Leitor GTIN (Comunicação Web Service com o Portal Fiscal ) (AINDA SENDO FEITO)
	//Parado por falta d Certificado Digital Válido
	//Requerido para comunicação com a receita
	internal class LabsGTIN
	{
		private const string Url = "https://dfe-servico.svrs.rs.gov.br/ws/ccgConsGTIN/ccgConsGTIN.asmx";
		private const string Action = "http://www.portalfiscal.inf.br/ccg/ConsGTIN";

		public static async Task<string> SearchBarCode(string CodBarras)
		{
			var soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ccg=""http://www.portalfiscal.inf.br/ccg"">
            <soapenv:Header/>
            <soapenv:Body>
                <ccg:ConsGTIN>
                    <ccg:GTIN>{CodBarras}</ccg:GTIN>
                </ccg:ConsGTIN>
            </soapenv:Body>
			</soapenv:Envelope>";

			using (var handler = new HttpClientHandler())
			{
				// Adicione aqui o certificado digital
				X509Certificate2 certificate = new X509Certificate2(@"C:\AssinadorRS\Certificados\Associacao de Moradores.pfx");
				handler.ClientCertificates.Add(certificate);
				handler.ClientCertificateOptions = ClientCertificateOption.Manual;

				using (var client = new HttpClient(handler))
				{
					var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
					content.Headers.Clear();
					content.Headers.Add("SOAPAction", Action);

					var response = await client.PostAsync(Url, content);
					return await response.Content.ReadAsStringAsync();
				}
			}
		}
	}
}

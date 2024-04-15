using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Unimake.Business.DFe.Servicos;
using Unimake.Business.DFe.Servicos.CCG;
using Unimake.Business.DFe.Xml.CCG;
using Unimake.Business.Security;

namespace LabsPDV.LABS_PDV
{

	//Leitor GTIN (Comunicação Web Service com o Portal Fiscal ) (AINDA SENDO FEITO)
	internal class LabsGTIN
	{
		static CertificadoDigital Certificado = new ();

		public static string SearchBarCode(string CodBarras)
		{
			var cert = Certificado.CarregarCertificadoDigitalA1(@".\Certificado\labsCert.pfx","12345678");
			var config = new Configuracao
			{
				TipoDFe = TipoDFe.CCG,
				CertificadoDigital = cert,
			};
			var xml = new ConsGTIN()
			{
				Versao = "1.00",
				GTIN = CodBarras,
			};
			try
			{
				var cggConsGTIN = new CcgConsGTIN(xml,config);
				cggConsGTIN.Executar();
				return cggConsGTIN.Result.XProd;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimake.Unidanfe;
using Unimake.Unidanfe.Configurations;

namespace Labs.Main.ReceitaFederal
{
	public class DANFE
	{
		public static void ConfigsDANFE()
		{
			UnidanfeServices.ShowConfigurationScreen();
		}
		//
		public static void ImprimirDANFE_NFCE(string IDVenda)
		{
			//
			var Config = new UnidanfeConfiguration
			{
				Arquivo = $"Nfe/Autorizadas/NFCE-{IDVenda}.xml",
				Copias = 1,
				Visualizar = false,
				Imprimir = true,
				Impressora = LabsMainAppWPF.ImpressoraTermica
			};
			UnidanfeServices.Execute(Config);
		}




		public static void teste()
		{
			var config = new UnidanfeConfiguration
			{
				Arquivo = @"C:\Users\Pc\source\LabsSolution\LabsPDV\bin\Debug\net8.0-windows\NFe\Autorizadas\33240754781393000147650010000000271831726743-procnfe.xml",
				Copias = 1,
				Visualizar = true,
				Imprimir = false,
				Impressora = LabsMainAppWPF.ImpressoraTermica
			};
			UnidanfeServices.Execute(config);
		}
	}
}

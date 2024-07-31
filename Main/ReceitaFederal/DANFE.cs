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
		public static void ImprimirDANFE_NFCE(string IDVenda,int Copias = 1, bool Visualize = false, bool Imprimir = true)
		{
			//
			var Config = new UnidanfeConfiguration
			{
				Arquivo = $"Nfe/Autorizadas/NFCE-{IDVenda}.xml",
				Copias = Copias,
				Visualizar = Visualize,
				Imprimir = Imprimir,
				Impressora = LabsMainAppWPF.ImpressoraTermica
			};
			UnidanfeServices.Execute(Config);
		}
		//
		public static void ImprimirDANFE_NFCE_Path(string Path,int Copias = 1, bool Visualize = false, bool Imprimir = true)
		{
			//
			var Config = new UnidanfeConfiguration
			{
				Arquivo = Path,
				Copias = Copias,
				Visualizar = Visualize,
				Imprimir = Imprimir,
				Impressora = LabsMainAppWPF.ImpressoraTermica
			};
			UnidanfeServices.Execute(Config);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unimake.Business.DFe.Xml.NFe;

namespace Labs.LABS_PDV
{
	public static class JsonManager
	{
		private static readonly string ConfigFolder = @".\Config";
		private static readonly string ExtensaoArquivo = ".Labs";
		private static readonly string SelfConfigFile = @"\Init";
		//
		private static List<string> RegisteredPaths = new();
		//
		public static bool InitializeJsonManager()
		{
			if (!Directory.Exists(ConfigFolder)) 
			{
				Modais.MostrarAviso("O Sistema não Conseguiu Encontrar a Diretório de Configurações.\n\n" +
					"Uma Nova Pasta Com os Arquivos Necessários Será Criada.\n\n" +
					"Se Essa Mensagem Aparecer Em Todas as Inicializações Recomendamos Chamar um Técnico Da Nossa Equipe.");
				//Após Exibir o aviso Indicamos a Criação do diretório
				var dir = Directory.CreateDirectory(ConfigFolder);
				if (dir.Exists)
				{
					Modais.MostrarInfo("Diretório de Configuração Criado Com Sucesso!\n");
					return CheckupDeArquivos();
				}
			}
			return CheckupDeArquivos();
		}
		//
		private static bool CheckupDeArquivos()
		{
			if (!File.Exists(ConfigFolder + SelfConfigFile + ExtensaoArquivo))
			{
				string json = JsonConvert.SerializeObject(RegisteredPaths,Formatting.Indented);
				File.WriteAllText(ConfigFolder + SelfConfigFile + ExtensaoArquivo,json);
				Modais.MostrarInfo("Arquivo de Primeira Inicialização Criado!"); 
				return true;
			}
			//
			string jsonContent = File.ReadAllText(ConfigFolder + SelfConfigFile + ExtensaoArquivo);
			RegisteredPaths = JsonConvert.DeserializeObject<List<string>>(jsonContent)!;
			// Se for nulo reportamos o erro e encerramos a verificação
			if(RegisteredPaths == null) { Modais.MostrarErro("NÃO FOI POSSÍVEL CARREGAR O ARQUIVO DE INICIALIZAÇÃO!"); return false; }
			// se a listagem de arquivos existir, seguimos felizes
			//
			bool AllListedFilesExist = true;
			foreach (string path in RegisteredPaths) 
			{
				if (!File.Exists(path)) 
				{ 
					AllListedFilesExist = false;
					Modais.MostrarErro($"{path}\n O Caminho Especificado Não Foi Encontrado!");
					//
				}
			}
			return AllListedFilesExist;
		}
		//Registrador de Caminhos de Configuração
		private static void OnNewPathAdded() // Esse evento atualiza o arquivo de inicicalização toda vez que um novo arquivo de config é Adicionado;
		{
			if(File.Exists(ConfigFolder + SelfConfigFile + ExtensaoArquivo))
			{
				string json = JsonConvert.SerializeObject(RegisteredPaths,Formatting.Indented);
				File.WriteAllText(ConfigFolder + SelfConfigFile + ExtensaoArquivo,json);
			}
		}
		//------------------------//
		// Métodos Publicos
		//------------------------//
		/// <summary>
		/// Salva uma configuração usando o tipo de objeto como parâmetro
		/// </summary>
		/// <typeparam name="T">Tipo de objeto da configuração</typeparam>
		/// <param name="target">Objeto Alvo do Tipo Especificado</param>
		/// <param name="NomeDoArquivo">Nome do Arquivo de Configuração que será Lido</param>
		/// <returns></returns>
		public static bool SalvarConfig<T>(T target,string NomeDoArquivo)
		{
			//
			if(target != null)
			{
				string json = JsonConvert.SerializeObject(target, Formatting.Indented);
				if(json.Length > 0)
				{
					var path = $@"{ConfigFolder}\{NomeDoArquivo}{ExtensaoArquivo}";
					// se o arquivo for novo, registramos e adicionamos na lista de inicialização
					if (!RegisteredPaths.Contains(path)) { RegisteredPaths.Add(path); OnNewPathAdded(); }
					//
					File.WriteAllText(path, json);
				}
			}
			return false;
		}
		/// <summary>
		/// Carrega um Arquivo de Configuração devolvendo um Objeto do tipo Especificado
		/// </summary>
		/// <typeparam name="T">Tipo de Objeto de Retorno</typeparam>
		/// <param name="NomeDoArquivo">Nome do Arquivo de Configuração</param>
		/// <returns>Retorna a Configuração dentro do parâmetro de Tipo de Objeto</returns>
		public static T CarregarConfig<T>(string NomeDoArquivo)
		{
			var path = $@"{ConfigFolder}\{NomeDoArquivo}{ExtensaoArquivo}";
			if (File.Exists(path))
			{
				string json = File.ReadAllText(path);
				T res = JsonConvert.DeserializeObject<T>(json)!;
				return res;
			}
			return default!;
		}
		/// <summary>
		/// Verifica se o arquivo de Configuração especificado está presente na pasta config
		/// </summary>
		/// <param name="NomeDoArquivo">Nome do arquivo a ser verificado</param>
		/// <returns>Retorna verdadeiro se o arquivo existir</returns>
		public static bool ChecarConfig(string NomeDoArquivo)
		{
			var path = $@"{ConfigFolder}\{NomeDoArquivo}{ExtensaoArquivo}";
			return RegisteredPaths.Contains(path);
		}
	}
}

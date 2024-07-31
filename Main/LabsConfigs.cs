﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Main
{
    public class LabsConfigs
    {
        /// <summary>
        /// Salva uma Config Diretamente no arquivo De Configs
        /// </summary>
        /// <param name="NomeDaConfig">Nome da Config</param>
        /// <param name="Valor">Valor da Config</param>
        public static void SalvarConfig(string NomeDaConfig, string Valor)
        {
            //// Abre o arquivo de configuração do aplicativo
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //// Adiciona um novo valor ou atualiza um existente
            //if (config.AppSettings.Settings[NomeDaConfig] != null) { config.AppSettings.Settings[NomeDaConfig].Value = Valor; }
            //else
            //{
            //    config.AppSettings.Settings.Add(NomeDaConfig, Valor);
            //}
            //// Salva as alterações
            //config.Save(ConfigurationSaveMode.Modified);
            //// Força uma recarga das seções alteradas do arquivo de configuração
            //ConfigurationManager.RefreshSection("AppSettings");
            //
            JsonManager.SalvarConfig(Valor,NomeDaConfig);
            //
        }
        /// <summary>
        /// Retorna uma config Diretamente do arquivo De Configs
        /// </summary>
        /// <param name="NomeDaConfig">Nome da Config</param>
        /// <returns>Valor da Config</returns>
        public static string GetConfigValue(string NomeDaConfig)
        {
            if (JsonManager.ChecarConfig(NomeDaConfig))
            {
                return JsonManager.CarregarConfig<string>(NomeDaConfig);
            }
            return null!;
        }
    }
}

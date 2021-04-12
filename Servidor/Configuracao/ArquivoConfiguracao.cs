namespace Servidor.Configuracao
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public static class ArquivoConfiguracao
    {
        public static IConfigurationRoot Configuracoes { get; set; }

        public static void Carrega()
        {
            Configuracoes = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"configuracao.json")
                .Build();
        }
    }
}
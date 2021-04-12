namespace Servidor.Configuracao
{
    using Serilog;
    using System;
    using static Configuracao.ArquivoConfiguracao;

    public static class Log
    {
        public static ILogger Loga;

        public static void Configura() =>
            Loga = new LoggerConfiguration().ReadFrom.Configuration(Configuracoes).CreateLogger();
    }
}

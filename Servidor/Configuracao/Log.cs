namespace Servidor.Configuracao
{
    using Serilog.Events;
    using Serilog;
    using System;
    using static Configuracao.ArquivoConfiguracao;

    public static class Log
    {
        public static ILogger Loga;

        public static void Configura() 
        {
            Loga = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .ReadFrom
                .Configuration(Configuracoes)
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (_, e) => 
                Loga.Error($"Ocorreu um não tratado:\n\"{e.ExceptionObject}\".");
        }
    }
}

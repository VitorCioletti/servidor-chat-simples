namespace Servidor.WebSocket
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public class Configuracao
    {
        public Configuracao(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment _)
        {
            app.UseWebSockets();
            app.Use(Processador.Processa);
        }
    }
}
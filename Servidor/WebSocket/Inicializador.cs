namespace Servidor.WebSocket
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Inicializador 
    {
        public void Inicializa(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Configuracao>())
                .Build()
                .Run();
        }
    }
}
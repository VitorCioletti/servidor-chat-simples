
namespace Servidor.Configuracao.WebSocket
{
    using System;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;

    public static class WebSocket 
    {
        public static void Inicializa(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(w => w.Configure(_configura))
                .Build()
                .Run();
        }

        private static void _configura(IApplicationBuilder app) 
        {
            app.UseWebSockets();
            app.Use(Processa);
        }

        public static async Task Processa(HttpContext context, Func<Task> next)
        {
            if (context.Request.Path == "/")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                    await EnviaEco(context, webSocket);
                }
                else
                    context.Response.StatusCode = 400;
            }
            else
                await next();
        }

        private static async Task EnviaEco(HttpContext context, System.Net.WebSockets.WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = 
                await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, result.Count), 
                    result.MessageType, 
                    result.EndOfMessage, 
                    CancellationToken.None
                );

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
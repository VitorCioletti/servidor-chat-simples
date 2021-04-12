
namespace Servidor.Configuracao
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;
    using Protocolo.Entidades.Requisicao;
    using Protocolo.Entidades;
    using Protocolo;
    using Regras;
    using Serilog;
    using System.Collections.Generic;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading.Tasks;
    using System.Threading;
    using System;
    using static Configuracao.Log;

    public static class WebSocket 
    {
        public static Dictionary<string, System.Net.WebSockets.WebSocket> _conexoes;

        public static void Inicializa(string[] args)
        {
            _conexoes = new Dictionary<string, System.Net.WebSockets.WebSocket>();

            Loga.Information("Inicializado servidor WebSocket.");

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(w => w.Configure(_configura))
                .UseSerilog(Loga)
                .Build()
                .Run();
        }

        private static void _configura(IApplicationBuilder app) 
        {
            Loga.Information("Configurando servidor WebSocket.");

            app.UseWebSockets();
            app.Use(Processa);

            Loga.Information("Configurado servidor WebSocket.");
        }

        public static async Task Processa(HttpContext context, Func<Task> next)
        {
            if (context.Request.Path == "/")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                    await _recebeRequisicao(context, webSocket);
                }
                else
                    context.Response.StatusCode = 400;
            }
            else
                await next();
        }

        private static async Task _recebeRequisicao(HttpContext context, System.Net.WebSockets.WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var respostaSocket = await _aguardaResposta(webSocket, buffer);

            while (!respostaSocket.CloseStatus.HasValue)
            {
                var jsonRequisicao = Encoding.Default.GetString(buffer, 0, respostaSocket.Count);

                Loga.Debug($"Recebida requisição \"{jsonRequisicao}\".");

                var requisicao = Parser.Deserializa(jsonRequisicao);

                var resposta = await Regras.Processa(requisicao);

                if (resposta.Sucesso && requisicao.Contexto == Contexto.Usuario)
                {
                    if (requisicao.Acao == Acao.Entra)
                        _conexoes.Add(resposta.Corpo.Conexao, webSocket);

                    else if (requisicao.Acao == Acao.Sai)
                        _conexoes.Remove(resposta.Corpo.Conexao);
                }

                var jsonResposta = Parser.Serializa(resposta);

                await _enviaMensagem(webSocket, jsonResposta);

                respostaSocket = await _aguardaResposta(webSocket, buffer);

            }

            await webSocket.CloseAsync(
                respostaSocket.CloseStatus.Value, respostaSocket.CloseStatusDescription, CancellationToken.None);
        }

        public static async Task EnviaMensagem(string idConexao, Mensagem mensagem)
        {
            var conexao = _conexoes[idConexao];

            var jsonMensagem = Parser.Serializa<Mensagem>(mensagem);
            await _enviaMensagem(conexao, jsonMensagem);
        }

        private static async Task _enviaMensagem(System.Net.WebSockets.WebSocket conexao, string mensagem)
        {
            var arraySegment = new ArraySegment<byte>(Encoding.Default.GetBytes(mensagem));

            await conexao.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);

            Loga.Debug($"Enviada mensagem \"{mensagem}\".");
        }

        private static async Task<WebSocketReceiveResult> _aguardaResposta(
            System.Net.WebSockets.WebSocket webSocket, byte[] buffer)
        {
            Loga.Debug($"Aguardando resposta do cliente.");
            return await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }
    }
}
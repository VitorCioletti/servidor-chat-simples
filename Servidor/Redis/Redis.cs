namespace Servidor.Redis
{
    using EstruturasDado.Tipos;
    using ServiceStack.Redis;
    using System;
    using static Configuracao.ArquivoConfiguracao;
    using static Configuracao.Log;

    public class Redis : IDisposable
    {
        public Set UsuariosCadastrados { get; private set; }

        public Set UsuariosSala { get; private set; }

        public Set SalasAtivas { get; private set; }

        public Hash UsuariosOnline { get; private set; }

        private IRedisClient _conexao; 

        public Redis()
        {
            var endereco = Configuracoes.GetSection("Redis:Endereco").Value;
            var porta = int.Parse(Configuracoes.GetSection("Redis:Porta").Value);

            Loga.Information("Inicializando conexão com Redis.");

            _conexao = new RedisClient(endereco, porta);

            UsuariosOnline = new Hash(_conexao, "usuarios-online");
            UsuariosCadastrados = new Set(_conexao, "usuarios-cadastrados");
            UsuariosSala = new Set(_conexao, "usuarios-sala:{0}");

            SalasAtivas = new Set(_conexao, "salas-ativas");

            Loga.Information($"Inicializada conexão com Redis em {endereco}:{porta}.");

        }

        public void Dispose()
        {
            _conexao.Dispose(); 
            Loga.Information("Finalizada conexão com Redis.");
        }
    }
}
namespace Servidor.Redis
{
    using EstruturasDado.Tipos;
    using ServiceStack.Redis;
    using System;

    public class Conexao : IDisposable
    {
        public Set UsuariosSala { get; private set; }

        public Set SalasAtivas { get; private set; }

        public Set UsuariosOnline { get; private set; }

        public Set ChatsPrivados {get; private set; }

        public List MensagensSala { get; private set; }

        public List MensagensChatPrivado { get; private set; }

        public Set UsuariosChatPrivado { get; private set; }

        private IRedisClient _conexao; 

        public Conexao()
        {
            var endereco = "";
            var porta = 4000;

            _conexao = new RedisClient(endereco, porta);

            UsuariosOnline = new Set(_conexao, "usuarios-ativos");
            SalasAtivas = new Set(_conexao, "salas-ativas");

            UsuariosSala = new Set(_conexao, "usuarios-sala:{0}");
            MensagensSala = new List(_conexao, "mensagens-sala:{0}");

            ChatsPrivados = new Set(_conexao, "chats-privados");
            UsuariosChatPrivado = new Set(_conexao, "usuarios-chat-privado:{0}");
            MensagensChatPrivado = new List(_conexao, "mensagens-chat-privado:{0}");
        }

        public void Dispose() => _conexao.Dispose(); 
    }
}
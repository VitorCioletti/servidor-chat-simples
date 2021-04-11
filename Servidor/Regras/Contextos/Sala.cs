namespace Servidor.Regras.Contextos
{
    using Configuracao.WebSocket;
    using Protocolo.Entidades;
    using Excecoes.Contextos.Sala;
    using Excecoes.Contextos.Usuario;
    using Protocolo.Entidades.Requisicao;
    using Protocolo.Entidades.Resposta;
    using Redis;
    using System.Threading.Tasks;

    public static class Sala
    {
        public async static Task<Resposta> ProcessaRequisicaoAsync(Requisicao requisicao)
        {
            using (var redis = new Redis())
            {
                var salasAtivas = redis.SalasAtivas;
                var usuariosSala = redis.UsuariosSala;
                var usuariosOnline = redis.UsuariosOnline;

                var idSala = requisicao.Corpo.IdSala;
                var apelidoUsuario = requisicao.Corpo.ApelidoUsuario;

                if (!usuariosOnline.Existe(apelidoUsuario))
                    throw new UsuarioOfflineException();

                switch (requisicao.Acao)
                {
                    case Acao.Adiciona:
                        if (!salasAtivas.Existe(idSala))
                            salasAtivas.Adiciona(idSala);
                        else
                            throw new SalaJaExisteException();

                        break;

                    case Acao.Remove:
                        if (!salasAtivas.Existe(idSala))
                            salasAtivas.Remove(idSala);
                        else
                            throw new SalaNaoExisteException();
                        
                        break;
                    
                    case Acao.Entra:
                        if (salasAtivas.Existe(idSala))
                            if (!usuariosSala.Existe(idSala, apelidoUsuario))
                                usuariosSala.Adiciona(idSala, apelidoUsuario);
                            else
                                throw new UsuarioNaSalaException();

                        else
                            throw new SalaNaoExisteException();
                        
                        break;

                    case Acao.Sai:
                        if (salasAtivas.Existe(idSala))
                            if (usuariosSala.Existe(idSala, apelidoUsuario))
                                usuariosSala.Remove(idSala, apelidoUsuario);
                            else
                                throw new UsuarioNaoEstaNaSalaException();
                        else
                            throw new SalaNaoExisteException();
                        
                        break;

                    case Acao.EnviaMensagem:
                        var usuarios = usuariosSala.ObtemTodos(idSala);
                        usuarios.Remove(apelidoUsuario);

                        foreach (var usuario in usuarios)
                        {
                            var textoMensagem = requisicao.Corpo.Texto;
                            var idConexao = usuariosOnline.Obtem(usuario);

                            var mensagem = new Mensagem(apelidoUsuario, idSala, textoMensagem);

                            await WebSocket.EnviaMensagem(idConexao, mensagem);
                        }

                        break; 

                    default:
                        break;
                }
            }

            return new Resposta(true);
        }
    }
}
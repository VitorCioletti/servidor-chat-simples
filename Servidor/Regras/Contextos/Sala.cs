namespace Servidor.Regras.Contextos
{
    using Configuracao;
    using Excecoes.Contextos.Sala;
    using Excecoes.Contextos.Usuario;
    using Protocolo.Entidades.Requisicao;
    using Protocolo.Entidades.Resposta;
    using Protocolo.Entidades;
    using Redis;
    using System.Threading.Tasks;
    using static Configuracao.Log;

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
                        {
                            salasAtivas.Adiciona(idSala);
                            Loga.Information($"Sala \"{idSala}\" criada por \"{apelidoUsuario}\".");
                        }
                        else
                            throw new SalaJaExisteException();

                        break;

                    case Acao.Remove:
                        if (!salasAtivas.Existe(idSala))
                        {
                            salasAtivas.Remove(idSala);
                            Loga.Information($"Sala \"{idSala}\" removida por \"{apelidoUsuario}\".");
                        }
                        else
                            throw new SalaNaoExisteException();
                        
                        break;
                    
                    case Acao.Entra:
                        if (salasAtivas.Existe(idSala))
                            if (!usuariosSala.Existe(idSala, apelidoUsuario))
                            {
                                usuariosSala.Adiciona(idSala, apelidoUsuario);
                                Loga.Information($"Usuário \"{apelidoUsuario}\" entrou na sala \"{idSala}\".");
                            }
                            else
                                throw new UsuarioNaSalaException();

                        else
                            throw new SalaNaoExisteException();
                        
                        break;

                    case Acao.Sai:
                        if (salasAtivas.Existe(idSala))
                            if (usuariosSala.Existe(idSala, apelidoUsuario))
                            {
                                usuariosSala.Remove(idSala, apelidoUsuario);
                                Loga.Information($"Usuário \"{apelidoUsuario}\" saiu da sala \"{idSala}\".");
                            }
                            else
                                throw new UsuarioNaoEstaNaSalaException();
                        else
                            throw new SalaNaoExisteException();
                        
                        break;

                    case Acao.EnviaMensagem:
                        var usuarios = usuariosSala.ObtemTodos(idSala);
                        usuarios.Remove(apelidoUsuario);

                        var textoMensagem = requisicao.Corpo.Texto;
                        var mensagem = new Mensagem(apelidoUsuario, idSala, textoMensagem);

                        foreach (var usuario in usuarios)
                        {
                            var idConexao = usuariosOnline.Obtem(usuario);

                            if (idConexao != null)
                                await WebSocket.EnviaMensagem(idConexao, mensagem);
                        }

                        Loga.Information(
                            $"Usuário \"{apelidoUsuario}\" enviou mensagem \"{mensagem.Id}\" para sala \"{idSala}\".");

                        break; 

                    default:
                        break;
                }
            }

            return new Resposta(true);
        }
    }
}
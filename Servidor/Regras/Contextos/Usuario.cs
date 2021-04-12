namespace Servidor.Regras.Contextos
{
    using Excecoes.Contextos.Usuario;
    using Protocolo.Entidades.Requisicao;
    using Protocolo.Entidades.Resposta;
    using Redis;
    using System.Threading.Tasks;
    using System;
    using static Configuracao.Log;

    public static class Usuario
    {
        public async static Task<Resposta> ProcessaRequisicaoAsync(Requisicao requisicao)
        {
            var resposta = new Resposta(true);

            using (var redis = new Redis())
            {
                var usuariosCadastrados = redis.UsuariosCadastrados;
                var usuariosOnline = redis.UsuariosOnline;

                var apelidoUsuario = requisicao.Corpo.ApelidoUsuario;

                switch (requisicao.Acao)
                {
                    case Acao.Adiciona:

                        if (!usuariosCadastrados.Existe(apelidoUsuario))
                        {
                            usuariosCadastrados.Adiciona(apelidoUsuario);
                            Loga.Information($"Criado usuário \"{apelidoUsuario}\".");
                        }
                        else 
                            throw new UsuarioJaExisteException(); 

                        break;

                    case Acao.Entra:
                        if (!usuariosOnline.Existe(apelidoUsuario))
                        {
                            var idConexao = Guid.NewGuid().ToString();
                            resposta.Corpo.Conexao = idConexao; 

                            usuariosOnline.Adiciona(apelidoUsuario, idConexao);
                            Loga.Information($"Usuário \"{apelidoUsuario}\" ficou online.");
                        }
                        else
                            throw new UsuarioJaOnlineException();

                        break;

                    case Acao.Remove:
                        if (!usuariosCadastrados.Existe(apelidoUsuario))
                        {
                            usuariosCadastrados.Remove(apelidoUsuario);
                            usuariosOnline.Remove(apelidoUsuario);

                            Loga.Information($"Removido usuário \"{apelidoUsuario}\".");
                        }
                        else
                            throw new UsuarioNaoEncontradoException();

                        break;

                    case Acao.Sai:
                        if (usuariosOnline.Existe(apelidoUsuario))
                        {
                            usuariosOnline.Remove(apelidoUsuario);
                            Loga.Information($"Usuário \"{apelidoUsuario}\" ficou offline.");
                        }
                        else
                            throw new UsuarioOfflineException();

                        break;

                    default:
                        break;
                }
            }

            return resposta;
        }
    }
}
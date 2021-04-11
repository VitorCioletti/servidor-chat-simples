
namespace Servidor.Regras.Excecoes.Contextos.Usuario
{
    public class UsuarioNaoEncontradoException : ErroRegraException
    {
        public UsuarioNaoEncontradoException(string idErro = "apelido-nao-encontrado") : base(idErro) { }
    }
}
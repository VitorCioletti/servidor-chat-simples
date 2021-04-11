
namespace Servidor.Regras.Excecoes.Contextos.Usuario
{
    public class UsuarioNaoEstaNaSalaException : ErroRegraException
    {
        public UsuarioNaoEstaNaSalaException(string idErro = "usuario-nao-esta-na-sala") : base(idErro) { }
    }
}
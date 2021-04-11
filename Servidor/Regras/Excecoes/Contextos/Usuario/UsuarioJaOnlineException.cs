namespace Servidor.Regras.Excecoes.Contextos.Usuario
{
    public class UsuarioJaOnlineException : ErroRegraException
    {
        public UsuarioJaOnlineException(string idErro = "usuario-ja-online") : base(idErro) { }
    }
}
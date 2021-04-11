namespace Servidor.Regras.Excecoes.Contextos.Usuario
{
    public class UsuarioOfflineException : ErroRegraException
    {
        public UsuarioOfflineException(string idErro = "usuario-offline") : base(idErro) { }
    }
}
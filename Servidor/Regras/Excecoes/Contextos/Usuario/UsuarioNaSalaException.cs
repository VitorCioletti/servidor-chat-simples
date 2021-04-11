namespace Servidor.Regras.Excecoes.Contextos.Usuario
{
    public class UsuarioNaSalaException : ErroRegraException
    {
        public UsuarioNaSalaException(string idErro = "usuario-na-sala") : base(idErro) { }
    }
}
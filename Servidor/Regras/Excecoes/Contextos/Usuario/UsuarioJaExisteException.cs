namespace Servidor.Regras.Excecoes.Contextos.Usuario
{
    public class UsuarioJaExisteException : ErroRegraException
    {
        public UsuarioJaExisteException(string idErro = "apelido-ja-existe") : base(idErro) { }
    }
}

namespace Servidor.Regras.Excecoes.Contextos.Sala
{
    public class SalaJaExisteException : ErroRegraException
    {
        public SalaJaExisteException(string idErro = "sala-ja-existe-exception") : base(idErro) { }
    }
}
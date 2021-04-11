namespace Servidor.Regras.Excecoes.Contextos.Sala
{
    public class SalaNaoExisteException : ErroRegraException
    {
        public SalaNaoExisteException(string idErro = "sala-nao-existe") : base(idErro) { }
    }
}
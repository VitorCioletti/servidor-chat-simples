namespace Servidor.Protocolo.Entidades.Requisicao
{
    public class Requisicao
    {
        public Contexto Contexto { get; private set; }

        public Acao Acao { get; private set; }

        public Corpo Corpo { get; private set; }
    }
}
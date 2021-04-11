namespace Servidor.Protocolo.Entidades.Resposta
{
    public class Resposta
    {
        public bool Sucesso { get; set; }

        public Corpo Corpo { get; set; }

        public Resposta(bool sucesso)
        {
            Sucesso = sucesso;
            Corpo = new Corpo();
        }
    }
}
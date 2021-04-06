namespace Servidor.Entidades
{
    using System;

    public class Mensagem
    {
        public string Id { get; set; }

        public string IdSala { get; set; }

        public string ApelidoUsuario { get; set; }

        public DateTime DataHoraEnvio { get; set; }

        public string Conteudo { get; set; }

        public Mensagem(string apelidoUsuario, string idSala, string conteudo)
        {
            Id = Guid.NewGuid().ToString();
            DataHoraEnvio = DateTime.UtcNow;

            IdSala = idSala;

            ApelidoUsuario = apelidoUsuario;
            Conteudo = conteudo;
        }
    }
}
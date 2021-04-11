namespace Servidor.Protocolo.Entidades
{
    using System;

    public class Mensagem
    {
        public string Id { get; set; }

        public string IdSala { get; set; }

        public string ApelidoUsuario { get; set; }

        public DateTime DataHoraEnvio { get; set; }

        public string Texto { get; set; }

        public Mensagem(string apelidoUsuario, string idSala, string texto)
        {
            Id = Guid.NewGuid().ToString();
            DataHoraEnvio = DateTime.UtcNow;

            IdSala = idSala;

            ApelidoUsuario = apelidoUsuario;
            Texto = texto;
        }
    }
}
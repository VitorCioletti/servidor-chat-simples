namespace Servidor.Protocolo.Entidades
{
    using System;

    public class Mensagem
    {
        public string Id { get; set; }

        public string IdSala { get; set; }

        public string ApelidoUsuario { get; set; }

        public long DataHoraEnvio { get; set; }

        public string Texto { get; set; }

        public Mensagem(string apelidoUsuario, string idSala, string texto)
        {
            Id = Guid.NewGuid().ToString();
            DataHoraEnvio = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

            IdSala = idSala;

            ApelidoUsuario = apelidoUsuario;
            Texto = texto;
        }
    }
}
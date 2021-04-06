namespace Servidor.Regras.Entidades
{
    using System.Collections.Generic;

    public class Usuario
    {
        public string Apelido { get; set; }

        public List<Sala> Salas { get; set; }

        public Sala CriaSala(string idSala)
        {
            return new Sala(this, idSala);
        }

        public override string ToString() => Apelido;

        public override bool Equals(object obj) => obj is Usuario jogador && this == jogador; 

        public static bool operator ==(Usuario usuario1, Usuario usuario2) => usuario1.Apelido == usuario2.Apelido;

        public static bool operator !=(Usuario usuario1, Usuario usuario2) => usuario1.Apelido != usuario2.Apelido;
    }
}
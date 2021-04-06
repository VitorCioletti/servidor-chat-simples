namespace Servidor.Regras.Entidades
{
    using System;
    using System.Collections.Generic;

    public class Sala
    {
        public string Id { get; private set; }

        public string ApelidoUsuarioCriador { get; private set; }

        public DateTime DataHoraInicio { get; private set; }

        public DateTime DataHoraFim { get; private set; }

        public HashSet<Usuario> Usuarios { get; private set; }

        public Sala(Usuario criador, string id)
        {
            Id = id; 
            DataHoraInicio = DateTime.UtcNow;
            Usuarios = new HashSet<Usuario>{criador};
            ApelidoUsuarioCriador = criador.Apelido;
        }

        public void RemoveUsuario(Usuario usuario)
        {
            if (Usuarios.Contains(usuario))
                Usuarios.Remove(usuario);
            else
                throw new Exception("Usuário não está na sala.");
        }

        public void AdicionaNovoUsuario(Usuario usuario)
        {
            if (!Usuarios.Contains(usuario))
                Usuarios.Add(usuario);
            else
                throw new Exception("Usuári já está na sala");
        }
    }
}
namespace Servidor.Regras.Excecoes
{
    using Protocolo.Entidades.Resposta;
    using System.Collections.Generic;
    using System;

    public class ErroRegraException : Exception
    {
        private string _idErro;

        private Dictionary<string, string> _erros;

        public ErroRegraException(string idErro) : base(idErro) 
        { 
            _idErro = idErro;

            _erros = new Dictionary<string, string>
            {
                { "apelido-ja-existe", "Apelido de usuário já existe." },
                { "apelido-nao-encontrado", "Apelido não encontrado." },
                { "sala-nao-existe", "Sala não existe." },
                { "usuario-ja-online", "Usuário já está online." },
                { "usuario-na-sala", "Usuário já está na sala." },
                { "usuario-nao-esta-na-sala", "Usuário não está na sala." },
                { "usuario-offline", "Usuário offline." },
            };
        }

        public Resposta ObtemResposta()
        {
            return new Resposta(false)
            {
                Corpo = new Corpo
                {
                    IdErro = _idErro,
                    MensagemErro = _erros[_idErro],
                },
            };
        }
    }
}
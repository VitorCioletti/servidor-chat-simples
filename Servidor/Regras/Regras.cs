namespace Servidor.Regras
{
    using Contextos;
    using Excecoes;
    using Protocolo.Entidades.Requisicao;
    using Protocolo.Entidades.Resposta;
    using Protocolo;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    public static class Regras
    {
        private static Dictionary<Contexto, Func<Requisicao, Task<Resposta>>> _contextos;

        static Regras()
        {
            _contextos = new Dictionary<Contexto, Func<Requisicao, Task<Resposta>>>
            {
                { Contexto.Sala, async (r) => await Sala.ProcessaRequisicaoAsync(r)},
                { Contexto.Usuario, async (r) => await Usuario.ProcessaRequisicaoAsync(r)},
            };
        }

        public static async Task<Resposta> Processa(Requisicao requisicao)
        {
            try
            {
                return await _contextos[requisicao.Contexto](requisicao);
            }
            catch (ErroRegraException e)
            {
                return e.ObtemResposta();
            }
            catch (Exception e)
            {
                var resposta = new Resposta(false);

                resposta.Sucesso = false;
                resposta.Corpo.IdErro = "erro-interno-servidor";
                resposta.Corpo.MensagemErro = "Erro interno de servidor.";

                return resposta;
            }
        }
    }
}
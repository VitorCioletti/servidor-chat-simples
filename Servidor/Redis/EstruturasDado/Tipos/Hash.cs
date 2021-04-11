
namespace Servidor.Redis.EstruturasDado.Tipos
{
    using ServiceStack.Redis;
    using EstruturasDado;

    public class Hash : EstruturaDado
    {
        public Hash(IRedisClient conexao, string chave) : base(conexao, chave) {}

        public bool Existe(string id) => _conexao.HashContainsEntry(_chave, id);

        public void Adiciona(string id, string registro) => _conexao.SetEntryInHash(_chave, id, registro);

        public string Obtem(string id) => _conexao.GetValueFromHash(_chave, id);

        public void Remove(string id) => _conexao.RemoveEntryFromHash(_chave, id);
    }
}
namespace Servidor.Redis.EstruturasDado.Tipos
{
    using ServiceStack.Redis;
    using EstruturasDado;

    public class Set : EstruturaDado
    {
        public Set(IRedisClient conexao, string chave) : base(conexao, chave) {}

        public bool Existe(string registro) => _conexao.SetContainsItem(_chave, registro);

        public bool Existe(string id, string registro) =>
            _conexao.SetContainsItem(_montaChave(id), registro);

        public void Registra(string registro) => _conexao.AddItemToSet(_chave, registro);

        public void Registra(string id, string registro) => 
            _conexao.AddItemToSet(_montaChave(id), registro);

        public void Remove(string registro) => _conexao.RemoveItemFromSet(_chave, registro);

        public void Remove(string id, string registro) => 
            _conexao.RemoveItemFromSet(_montaChave(id), registro);
    }
}
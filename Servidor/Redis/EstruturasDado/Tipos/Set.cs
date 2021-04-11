namespace Servidor.Redis.EstruturasDado.Tipos
{
    using EstruturasDado;
    using ServiceStack.Redis;
    using System.Collections.Generic;

    public class Set : EstruturaDado
    {
        public Set(IRedisClient conexao, string chave) : base(conexao, chave) {}

        public bool Existe(string registro) => _conexao.SetContainsItem(_chave, registro);

        public HashSet<string> ObtemTodos(string id) => _conexao.GetAllItemsFromSet(_montaChave(id));

        public bool Existe(string id, string registro) =>
            _conexao.SetContainsItem(_montaChave(id), registro);

        public void Adiciona(string registro) => _conexao.AddItemToSet(_chave, registro);

        public void Adiciona(string id, string registro) => 
            _conexao.AddItemToSet(_montaChave(id), registro);

        public void Remove(string registro) => _conexao.RemoveItemFromSet(_chave, registro);

        public void Remove(string id, string registro) => 
            _conexao.RemoveItemFromSet(_montaChave(id), registro);
    }
}

namespace Servidor.Redis.EstruturasDado.Tipos
{
    using ServiceStack.Redis;
    using EstruturasDado;

    public class List : EstruturaDado
    {
        public List(IRedisClient conexao, string chave) : base(conexao, chave) {}

        public void Adiciona(string id, string registro) =>
            _conexao.AddItemToList(_montaChave(id), registro);

        public void Remove(string id, string registro) =>
            _conexao.RemoveItemFromList(_montaChave(id), registro);
    }
}
namespace Servidor.Redis.EstruturasDado
{
    using ServiceStack.Redis;

    public abstract class EstruturaDado
    {
        protected string _chave;

        protected IRedisClient _conexao; 

        public EstruturaDado(IRedisClient conexao, string chave)
        {
            _conexao = conexao;
            _chave = chave;
        }

        protected string _montaChave(string id) => string.Format(_chave, id);
    }
}
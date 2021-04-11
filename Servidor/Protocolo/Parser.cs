namespace Servidor.Protocolo
{
    using Entidades.Requisicao;
    using Entidades.Resposta;
    using ServiceStack.Text;

    public static class Parser
    {
        public static Requisicao Deserializa(string requisicao) =>
            JsonSerializer.DeserializeFromString<Requisicao>(requisicao);

        public static string Serializa<T>(T t) => JsonSerializer.SerializeToString<T>(t);
    }
}
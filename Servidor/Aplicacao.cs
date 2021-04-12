namespace Servidor
{
    using Configuracao;

    public class Aplicacao
    {
        public static void Main(string[] args)
        {
            ArquivoConfiguracao.Carrega();
            Log.Configura();
            WebSocket.Inicializa(args);
        }
    }
}
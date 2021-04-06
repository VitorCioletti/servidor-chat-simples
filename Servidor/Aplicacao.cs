namespace Servidor
{
    using WebSocket;

    public class Aplicacao
    {
        public static void Main(string[] args)
        {
            var inicializadorWebSocket = new Inicializador();
            inicializadorWebSocket.Inicializa(args);
        }

    }
}

using System;

namespace ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            var teste = "usuario-teste:{0}";
            var apelidoUsuario = "batataFeliz";

            var resultado = string.Format(teste, apelidoUsuario);

            Console.WriteLine("Hello World!");
        }
    }
}

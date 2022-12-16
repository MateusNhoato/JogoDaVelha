using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System.Drawing;
namespace JogoDaVelha
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor= ConsoleColor.Black;
            Console.Title = "Jogo da Velha";
            Console.Clear();

            Registro.AbrirDadosDosJogadoresAoIniciar();
            Menu.MostrarMenu();
        }
    }
}

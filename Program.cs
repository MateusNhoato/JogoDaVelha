using JogoDaVelha.Controllers;
using JogoDaVelha.Entities;
using JogoDaVelha.Repositories;

namespace JogoDaVelha
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor= ConsoleColor.Black;
            Console.Title = "Jogo da Velha";
            Console.Clear();

            
            Registro.AbrirDadosDosJogadoresAoIniciar();
            Menu.MostrarMenu();
        }
    }
}

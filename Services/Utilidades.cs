using JogoDaVelha.Views;
using System.Globalization;

namespace JogoDaVelha.Services
{
    // classe para funções utilitárias
    internal static class Utilidades
    {
        // função para passar um nome para pascal case
        internal static string LerNomeEPassarParaPascalCase()
        {
            TextInfo textInfo = new CultureInfo("pt-br", false).TextInfo;
            string nome = Console.ReadLine();
            nome = textInfo.ToTitleCase(nome);

            return nome;
        }

        // funções utilitária para comunicação com usuário
        internal static void EntradaInvalida()
        {
            Console.WriteLine(Arte.linha);
            Console.WriteLine("  Entrada inválida.");
            Thread.Sleep(1200);
            Console.Clear();
        }
        internal static void AperteEnterParaContinuar()
        {
            Console.WriteLine(Arte.linha);
            Console.WriteLine("  Aperte enter para continuar");
            Console.ReadLine();
            Console.Clear();
        }
    }
}

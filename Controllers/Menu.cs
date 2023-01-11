using JogoDaVelha.Repositories;
using JogoDaVelha.Views;
using System.Globalization;

namespace JogoDaVelha.Controllers
{
    internal class Menu
    {
        // função para mostrar o menu principal
        internal static void MostrarMenu()
        {
            string? opcao;
            do
            {
                Tela.ImprimirMenuPrincipal();
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "0":
                        break;

                    case "1":
                        PassarJogadoresParaJogo();
                        break;

                    case "2":
                        DadosJogadores.CadastrarJogador();
                        break;

                    case "3":
                        Tela.ImprimirListaDosJogadores(DadosJogadores.Jogadores);
                        break;

                    case "4":
                        Registro.MostrarHistoricoDePartidas(null);
                        break;

                    case "5":
                        Tela.ImprimirHistoricoDeJogador(true);
                        Registro.MostrarHistoricoDePartidas(LerNomeEPassarParaPascalCase());
                        break;

                    case "6":
                        Tela.ImprimirHallDaFama(DadosJogadores.Jogadores);
                        break;

                    default:
                        EntradaInvalida();
                        break;

                }

            } while (opcao != "0");

        }

        // validar e passar jogadores válidos para função jogar na classe jogo
        private static void PassarJogadoresParaJogo()
        {
            Tela.ImprimirJogar();

            Tela.ImprimirJogadores("X");
            string? nomePrimeiroJogador = LerNomeEPassarParaPascalCase();
            Tela.ImprimirJogadores("O");
            string? nomeSegundoJogador = LerNomeEPassarParaPascalCase();

            DadosJogadores.AcharJogadoresParaJogo(nomePrimeiroJogador, nomeSegundoJogador);

        }       
      

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

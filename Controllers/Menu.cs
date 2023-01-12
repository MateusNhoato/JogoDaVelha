using JogoDaVelha.Repositories;
using JogoDaVelha.Services;
using JogoDaVelha.Views;
using System.Globalization;

namespace JogoDaVelha.Controllers
{
    public static class Menu
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
                        Registro.MostrarHistoricoDePartidas(Utilidades.LerNomeEPassarParaPascalCase());
                        break;

                    case "6":
                        Tela.ImprimirHallDaFama(DadosJogadores.Jogadores);
                        break;

                    default:
                        Utilidades.EntradaInvalida();
                        break;

                }

            } while (opcao != "0");

        }

        // validar e passar jogadores válidos para função jogar na classe jogo
        private static void PassarJogadoresParaJogo()
        {
            Tela.ImprimirJogar();

            Tela.ImprimirJogadores("X");
            string? nomePrimeiroJogador = Utilidades.LerNomeEPassarParaPascalCase();
            Tela.ImprimirJogadores("O");
            string? nomeSegundoJogador = Utilidades.LerNomeEPassarParaPascalCase();

            DadosJogadores.AcharJogadoresParaJogo(nomePrimeiroJogador, nomeSegundoJogador);
        }


       
    }
}

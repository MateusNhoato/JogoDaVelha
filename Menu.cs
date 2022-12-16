using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha
{
    internal class Menu
    {
        // função para mostrar o menu principal
        internal static void MostrarMenu()
        {
            string? opcao;
            do
            {
                Console.Clear();

                Console.WriteLine("1 - Jogar");
                Console.WriteLine("2 - Adicionar jogadores");
                Console.WriteLine("3 - Listar jogadores");
                Console.WriteLine("4 - Histórico de partidas");
                Console.WriteLine("5 - Histórico do Jogador");
                Console.WriteLine("6 - Hall da fama");
                Console.WriteLine("0 - Encerrar o programa");
                Console.Write("Digite a opção deseja: ");
                opcao = Console.ReadLine();

                switch(opcao) 
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
                        DadosJogadores.ListarTodosJogadores();
                        break;

                    case "4":
                        break;

                    case "5":
                        break;

                    case "6":
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
            Console.Clear();
            // passar os nomes para pascal case
            TextInfo textInfo = new CultureInfo("pt-br", false).TextInfo;
            
            Console.Write("Primeiro jogador (X): ");
            string? nomePrimeiroJogador = Console.ReadLine();
            nomePrimeiroJogador = textInfo.ToTitleCase(nomePrimeiroJogador);
            Jogador? primeiroJogador = DadosJogadores.Jogadores.Find(x => x.Nome == nomePrimeiroJogador);

            Console.Write("Segundo jogador (O): ");
            string? nomeSegundoJogador = Console.ReadLine();
            nomeSegundoJogador = textInfo.ToTitleCase(nomeSegundoJogador);
            Jogador? segundoJogador = DadosJogadores.Jogadores.Find(x => x.Nome == nomeSegundoJogador);

            if(primeiroJogador != null && segundoJogador != null && primeiroJogador != segundoJogador) 
            {
                Jogo.Jogar(primeiroJogador, segundoJogador);
            }
            else
            {
                Console.WriteLine("\nJogador(es) inválido(s)");
                AperteEnterParaContinuar();
            }
        }



        // funções utilitária para comunicação com usuário
        internal static void EntradaInvalida()
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Entrada inválida.");
            System.Threading.Thread.Sleep(1200);
            Console.Clear();
         }

        internal static void AperteEnterParaContinuar() 
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Aperte enter para continuar");
            Console.ReadLine();
            Console.Clear();
        }
    }
}

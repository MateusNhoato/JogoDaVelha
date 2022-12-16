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
                Console.WriteLine(Arte.titulo + "\n");

                Console.WriteLine("1 - Jogar");
                Console.WriteLine("2 - Adicionar jogadores");
                Console.WriteLine("3 - Listar jogadores");
                Console.WriteLine("4 - Histórico de partidas");
                Console.WriteLine("5 - Histórico do Jogador");
                Console.WriteLine("6 - Hall da fama");
                Console.WriteLine("0 - Encerrar o programa");
                Console.Write("\nDigite a opção deseja: ");
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
                        Registro.MostrarHistoricoDePartidas(null);
                        break;

                    case "5":
                        MostrarHistoricoDeJogador();
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
            
            Console.Write("Player 1 (X): ");
            string? nomePrimeiroJogador = Console.ReadLine();
            nomePrimeiroJogador = textInfo.ToTitleCase(nomePrimeiroJogador);
            Jogador? primeiroJogador = DadosJogadores.Jogadores.Find(x => x.Nome == nomePrimeiroJogador);

            Console.Write("Player 2 (O): ");
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

        // função para validar se o jogador passado existe,  passando para a função na classe registro
        private static void MostrarHistoricoDeJogador()
        {
            Console.Clear();
            Console.WriteLine(Arte.historico + "\n");
            Console.Write("Digitar o nome do jogador: ");
            string nome = Console.ReadLine();

            // passar o nome para pascal case
            TextInfo textInfo = new CultureInfo("pt-br", false).TextInfo;
            nome = textInfo.ToTitleCase(nome);

           Jogador jogador = DadosJogadores.Jogadores.Find(x => x.Nome == nome);
            if(jogador != null)           
                Registro.MostrarHistoricoDePartidas(nome);
            
            else
            {
                Console.WriteLine("Jogador não encontrado.");
                AperteEnterParaContinuar();
            }
        }


        // funções utilitária para comunicação com usuário
        internal static void EntradaInvalida()
        {
            Console.WriteLine(Arte.linha);
            Console.WriteLine("Entrada inválida.");
            System.Threading.Thread.Sleep(1200);
            Console.Clear();
         }

        internal static void AperteEnterParaContinuar() 
        {
            Console.WriteLine(Arte.linha);
            Console.WriteLine("Aperte enter para continuar");
            Console.ReadLine();
            Console.Clear();
        }
    }
}

using JogoDaVelha.Controllers;
using JogoDaVelha.Entities;
using JogoDaVelha.Repositories;

namespace JogoDaVelha.Views
{
    internal static class Tela
    {
        // função para imprimir o menu principal
        internal static void ImprimirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine(Arte.titulo + "\n");

            Console.WriteLine("  1 - Jogar");
            Console.WriteLine("  2 - Adicionar jogadores");
            Console.WriteLine("  3 - Listar jogadores");
            Console.WriteLine("  4 - Histórico de partidas");
            Console.WriteLine("  5 - Histórico do Jogador");
            Console.WriteLine("  6 - Hall da fama");
            Console.WriteLine("  0 - Encerrar o programa");
            Console.Write("\n  Digite a opção desejada: ");
            
        }
        // função para imprimir cadastro de jogador
        internal static void ImprimirCadastrarJogador()
        {
            Console.Clear();
            Console.WriteLine(Arte.adicionarJogador + "\n");
            Console.Write("  Digite o nome do jogador: ");
        }
        // funções para imprimir Jogar
        internal static void ImprimirJogar()
        {
            Console.Clear();
            Console.WriteLine(Arte.jogar);
        }
        internal static void ImprimirJogadores(string jogada) 
        {
            Console.Write($"  Player {jogada}: ");
        }
        // função para imprimir histórico do jogador
        internal static void ImprimirHistoricoDeJogador(bool historicoDeJogador)
        {
            Console.Clear();
            Console.WriteLine(Arte.historico + "\n");
            if(historicoDeJogador)
                Console.Write("  Digitar o nome do jogador: ");
        }

        // função para imrpimir uma partida, usada para ver o histórico
        internal static void ImprimirPartida(Partida partida)
        {
            Console.WriteLine(Arte.linha);
            Console.WriteLine(partida);

            // imprimindo tabuleiro            
            for (int i = 0; i < partida.Tabuleiro.TamanhoDoTabuleiro; i++)
            {
                Console.Write("  ");
                for (int j = 0; j < partida.Tabuleiro.TamanhoDoTabuleiro; j++)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    if (partida.Tabuleiro.MatrizTabuleiro[i,j].Trim() == "X")
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    else if (partida.Tabuleiro.MatrizTabuleiro[i,j].Trim() == "O")
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    if (partida.Tabuleiro.MatrizTabuleiro[i, j].Trim() == "1")
                        Console.Write("   ");
                    else
                        Console.Write(partida.Tabuleiro.MatrizTabuleiro[i, j]);
                    Console.ForegroundColor = aux;
                }
                Console.WriteLine();
            }
            string resultado = partida.Resultado;

            if (resultado == "X")
                resultado = partida.Jogador1;
            else if (resultado == "O")
                resultado = partida.Jogador2;

            if (resultado == "Velha")
                Console.WriteLine("\n  " + resultado);
            else
                Console.WriteLine($"\n  Vencedor: {resultado}");
        }


        // função para listar os jogadores
        internal static void ImprimirListaDosJogadores(List<Jogador> jogadores)
        {
            Console.Clear();
            Console.WriteLine(Arte.jogadores + "\n");
            if (jogadores.Count > 0)
                foreach (Jogador jogador in jogadores)
                    Console.WriteLine(jogador);

            else
                Console.WriteLine("  Nenhum jogador cadastrado.");

            Console.WriteLine("\n  Obs: Vitórias = 3pts | Empates = 1pt | Derrotas = -1pt.");
            Menu.AperteEnterParaContinuar();
        }

        // função para imprimir o hall da fama
        internal static void ImprimirHallDaFama(List<Jogador> jogadores)
        {
            Console.Clear();
            Console.WriteLine(Arte.hallDaFama + "\n");
            Console.WriteLine("  Top 3 melhores jogadores de todos os tempos");
            // lista em ordem de pontuação
            List<Jogador> jogadoresPorPontos = jogadores.
                Select(x => new Jogador(x.Nome, x.QuantidadeVitorias, x.QuantidadeEmpates, x.QuantidadeDerrotas))
                .OrderByDescending(x => x.Pontuacao)
                .ThenBy(x => x.QuantidadeVitorias)
                .ThenByDescending(x => x.QuantidadeDerrotas).ToList();

            int index = jogadoresPorPontos.Count;

            for (int i = 0; i < index; i++)
            {
                Jogador jogador = jogadoresPorPontos[i];

                if (jogador.Pontuacao <= 0)
                    continue;

                Console.WriteLine($"  Top {i + 1}: {jogador.Nome} | {jogador.Pontuacao} pontos | {jogador.QuantidadeVitorias}V/{jogador.QuantidadeEmpates}E/{jogador.QuantidadeDerrotas}D\n");
                if (i >= 2)
                    break;
            }
            Menu.AperteEnterParaContinuar();
        }



        // função para imprimir o MatrizTabuleiro
        internal static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            Console.WriteLine("\n");
            for (int i = 0; i < tabuleiro.TamanhoDoTabuleiro; i++)
            {
                Console.Write("   ");
                for (int j = 0; j < tabuleiro.TamanhoDoTabuleiro; j++)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    if (tabuleiro.MatrizTabuleiro[i, j].Trim() == "X")
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    else if (tabuleiro.MatrizTabuleiro[i, j].Trim() == "O")
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.Write(tabuleiro.MatrizTabuleiro[i, j]);
                    Console.ForegroundColor = aux;
                }
                Console.WriteLine();
            }
        }



    }
}

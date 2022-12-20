﻿
using JogoDaVelha.Controllers;
using JogoDaVelha.Entities;
using JogoDaVelha.Repositories;

namespace JogoDaVelha.Services
{
    internal class Jogo
    {
        // função de jogada de cada jogador
        internal static bool Jogada(string jogada, string jogador, Tabuleiro tabuleiro)
        {

            for (int i = 0; i < tabuleiro.TamanhoDoTabuleiro; i++)
            {
                for (int j = 0; j < tabuleiro.TamanhoDoTabuleiro; j++)
                {
                    if (tabuleiro.tabuleiro[i, j].Trim() == jogada)
                    {
                        tabuleiro.tabuleiro[i, j] = jogador;
                        return true;
                    }
                }
            }
            return false;
        }


        // função para checkar se alguém ganhou ou deu velha
        internal static string? CheckarVitoriaOuVelha(Tabuleiro tabuleiro)
        {
            // para haver um vencedor precisamos de pelo menos (x * 2 - 1) jogadas, o que é o tamanho do tabuleiro.
            // logo, se não houve esse numero de jogadas ainda não houve vencedor, retorno null
            if (tabuleiro.jogadasPossiveis.Count >= tabuleiro.TamanhoDoTabuleiro)              
                return null;
                         

            int tamanhoAuxiliar = (tabuleiro.TamanhoDoTabuleiro + 1) / 2;

            string[] valoresNaDiagonalPrincipal = new string[tamanhoAuxiliar];
            string[] valoresNaDiagonalSecundaria = new string[tamanhoAuxiliar];

            int diagonalSecundariaAuxiliar = tamanhoAuxiliar + 1;
            if (tamanhoAuxiliar % 2 == 0)
                diagonalSecundariaAuxiliar = tamanhoAuxiliar + 2;

            // lista das colunas
            List<string[]> colunas = new List<string[]>();
            for (int i = 0; i < tabuleiro.TamanhoDoTabuleiro + 1; i += 2)
            {
                colunas.Add(new string[tamanhoAuxiliar]);
            }

            // loop de checkagem
            for (int i = 0; i < tabuleiro.TamanhoDoTabuleiro; i += 2)
            {

                string[] valoresNaLinha = new string[tamanhoAuxiliar];

                for (int j = 0; j < tabuleiro.TamanhoDoTabuleiro; j += 2)
                {
                    // utilizando o .Trim() pois usei espaços no X e na O para ficar com espaçamento
                    string valor = tabuleiro.tabuleiro[i, j].Trim();

                    int posicaoAuxiliarParaVetoresJ = (int)Math.Floor(j / 2.0);
                    int posicaoAuxiliarParaVetoresI = (int)Math.Floor(i / 2.0);

                    valoresNaLinha[posicaoAuxiliarParaVetoresJ] = valor;

                    // adicionando valores na diagonal principal
                    if (i == j)
                        valoresNaDiagonalPrincipal[posicaoAuxiliarParaVetoresJ] = valor;

                    // adicionando valores na diagonal secundária
                    if (j == diagonalSecundariaAuxiliar)
                    {
                        valoresNaDiagonalSecundaria[posicaoAuxiliarParaVetoresJ] = valor;
                        diagonalSecundariaAuxiliar -= 2;
                    }

                    // adicionando os valores nas colunas                   
                    colunas[posicaoAuxiliarParaVetoresI][posicaoAuxiliarParaVetoresJ] = tabuleiro.tabuleiro[j, i].Trim();
                }
                // checkando os valores únicos da linha
                valoresNaLinha = valoresNaLinha.Distinct().ToArray();
                if (valoresNaLinha.Length == 1)
                    return valoresNaLinha[0];

            }
            // checkando a diagonal principal
            valoresNaDiagonalPrincipal = valoresNaDiagonalPrincipal.Distinct().ToArray();
            if (valoresNaDiagonalPrincipal.Length == 1)
                return valoresNaDiagonalPrincipal[0];

            // checkando a diagonal secundária
            valoresNaDiagonalSecundaria = valoresNaDiagonalSecundaria.Distinct().ToArray();
            if (valoresNaDiagonalSecundaria.Length == 1)
                return valoresNaDiagonalSecundaria[0];

            // checkando as colunas
            for (int i = 0; i < colunas.Count; i++)
            {
                string[] coluna = colunas[i];
                coluna = coluna.Distinct().ToArray();
                if (coluna.Length == 1)
                    return coluna[0];
            }

            // checkando velha
            if (tabuleiro.jogadasPossiveis.Count == 0)
                return "Velha";


            // caso ninguém ganhou e não deu velha
            return null;

        }

        // função principal de jogar
        internal static void Jogar(Jogador jogador1, Jogador jogador2)
        {
            int tamanho;
            string? vencedor;
            do
            {
                Console.Write("\n  Digite o tamanho do jogo (3 a 10): ");
                if (int.TryParse(Console.ReadLine(), out tamanho))
                {
                    if (tamanho >= 3 && tamanho <= 10)
                    {
                        Console.Clear();
                        break;
                    }
                }
            } while (true);

            // gerando o tabuleiro
            Tabuleiro tabuleiro = new Tabuleiro(tamanho);

            // começando pelo jogador1 como X
            Jogador jogador = jogador1;
            string jogada = " X ";
            while (true)
            {
                // enquanto o jogador tentar colocar uma posição inválida, o jogo não continua
                string posicao;
                do
                {
                    Console.Clear();
                    tabuleiro.MostrarTabuleiro();
                    Console.WriteLine($"\n  Vez de {jogador.Nome}\n");
                    Console.Write($"  Digite a posição da jogada ({jogada.Trim()}): ");
                    posicao = Console.ReadLine();
                } while (!tabuleiro.jogadasPossiveis.Contains(posicao));

                // removendo a jogada das jogadas possíveis e chamando a função Jogada que muda o tabuleiro
                tabuleiro.jogadasPossiveis.Remove(posicao);
                Jogada(posicao, jogada, tabuleiro);

                // chamando a função de checkar vitória após cada jogada válida
                vencedor = CheckarVitoriaOuVelha(tabuleiro);
                if (vencedor != null)
                {
                    Console.Clear();
                    tabuleiro.MostrarTabuleiro();

                    string jog1 = jogador1.Nome;
                    string jog2 = jogador2.Nome;

                    // CheckarVitoriaOuVelha retorou velha 
                    if (vencedor == "Velha")
                    {
                        Console.WriteLine("\n  Deu velha. Empate.");
                        jogador1.QuantidadeEmpates += 1;
                        jogador2.QuantidadeEmpates += 1;

                    }
                    // se retornar algo que não seja null e nem velha, teve um vencedor (x ou o)
                    else
                    {
                        Console.WriteLine($"\n  Vencedor: {jogador.Nome} ({vencedor}).");
                        jogador.QuantidadeVitorias += 1;

                        // alterando quantidade de derrotas do perdedor
                        if (jogador == jogador1)
                            jogador2.QuantidadeDerrotas += 1;

                        else
                            jogador1.QuantidadeDerrotas += 1;

                    }
                    // salvando os dados dos jogadores
                    Registro.SalvarDadosDosJogadores();

                    // salvando a partida
                    Registro.SalvarResultadoDaPartida(tabuleiro.TamanhoDoTabuleiro,tabuleiro,jog1, jog2, vencedor);

                    Menu.AperteEnterParaContinuar();
                    break;
                }

                // trocando o jogador, alternando as jogadas
                if (jogador == jogador1)
                {
                    jogador = jogador2;
                    jogada = " O ";
                }
                else
                {
                    jogador = jogador1;
                    jogada = " X ";
                }
            }
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha.Entities
{
    internal struct Tabuleiro
    {
        internal static string[,] tabuleiro;
        internal static List<string> jogadasPossiveis;
        private static int _tamanhoDoTabuleiro;

        internal static int TamanhoDoTabuleiro
        {
            get { return _tamanhoDoTabuleiro; }
            set
            {
                if (value >= 3 && value <= 100)
                    _tamanhoDoTabuleiro = value * 2 - 1;

                else
                    Console.WriteLine("  Tamanho inválido.");
            }
        }


        // função para gerar tabuleiro de 3 até 10 
        internal static void GerarTabuleiro()
        {
            int cont = 1;
            tabuleiro = new string[TamanhoDoTabuleiro, TamanhoDoTabuleiro];

            for (int i = 0; i < TamanhoDoTabuleiro; i++)
            {
                for (int j = 0; j < TamanhoDoTabuleiro; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            if (cont > 9)
                                tabuleiro[i, j] = $" {cont}";
                            else
                                tabuleiro[i, j] = $" {cont} ";
                            cont++;
                        }
                        else
                            tabuleiro[i, j] = "|";
                    }
                    else
                    {
                        if (j % 2 != 0)
                            tabuleiro[i, j] = "+";
                        else
                            tabuleiro[i, j] = "---";
                    }

                }
            }
        }

        // função para criar lista de jogadas possíveis
        internal static void ListarJogadasPossiveis()
        {
            jogadasPossiveis = new List<string>();
            for (int i = 1; i <= Math.Pow((TamanhoDoTabuleiro + 1) / 2, 2); i++)
            {
                jogadasPossiveis.Add($"{i}");
            }
        }

        // função para imprimir o tabuleiro
        internal static void MostrarTabuleiro()
        {
            Console.WriteLine("\n");
            for (int i = 0; i < TamanhoDoTabuleiro; i++)
            {
                Console.Write("   ");
                for (int j = 0; j < TamanhoDoTabuleiro; j++)
                {
                    Console.Write(tabuleiro[i, j]);
                }
                Console.WriteLine();
            }
        }

        // função para fazer uma string do tabuleiro para adicioná-lo no registro da partida
        public static string TabuleiroParaRegistro()
        {
            string resultado = "";
            foreach (string s in tabuleiro)
            {
                resultado += $"{s},";
            }
            return resultado;
        }
    }
}

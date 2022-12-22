namespace JogoDaVelha.Entities
{
    internal class Tabuleiro
    {
        internal string[,] tabuleiro { get; private set; }
        internal List<string> jogadasPossiveis { get; private set; }
        private static int _tamanhoDoTabuleiro;



        public Tabuleiro(int tamanho)
        {
           // configurando o tabuleiro e gerando lista de jogadas possíveis
            TamanhoDoTabuleiro = tamanho;
            GerarTabuleiro();
            ListarJogadasPossiveis();

        }

        internal int TamanhoDoTabuleiro
        {
            get { return _tamanhoDoTabuleiro; }
            set {_tamanhoDoTabuleiro = value * 2 - 1;}
        }


        // função para gerar tabuleiro de 3 até 10 
        internal void GerarTabuleiro()
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
                            else if (cont > 99)
                                tabuleiro[i, j] = $"{cont}";
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
        internal void ListarJogadasPossiveis()
        {
            jogadasPossiveis = new List<string>();
            for (int i = 1; i <= Math.Pow((TamanhoDoTabuleiro + 1) / 2, 2); i++)
            {
                jogadasPossiveis.Add($"{i}");
            }
        }

        // função para imprimir o tabuleiro
        internal void MostrarTabuleiro()
        {
            Console.WriteLine("\n");
            for (int i = 0; i < TamanhoDoTabuleiro; i++)
            {
                Console.Write("   ");
                for (int j = 0; j < TamanhoDoTabuleiro; j++)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    if (tabuleiro[i, j].Trim() == "X")
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    else if (tabuleiro[i, j].Trim() == "O")
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.Write(tabuleiro[i, j]);
                    Console.ForegroundColor = aux;
                }
                Console.WriteLine();
            }
        }   
    }
}

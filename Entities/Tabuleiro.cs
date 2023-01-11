namespace JogoDaVelha.Entities
{
    internal class Tabuleiro
    {
        internal string[,] MatrizTabuleiro { get; private set; }
        public List<string> JogadasPossiveis { get; private set; }
        private static int _tamanhoDoTabuleiro;


        // construtor para tabuleiro novo
        public Tabuleiro(int tamanho)
        {
            // configurando o tabuleiro e gerando lista de jogadas possíveis
            TamanhoDoTabuleiro = tamanho;
            GerarTabuleiro();
            ListarJogadasPossiveis();

        }
        // construtor para tabuleiro de registro
        public Tabuleiro(string[,] matrizTabuleiro, int tamanhoDoTabuleiro)
        {
            MatrizTabuleiro = matrizTabuleiro;
            TamanhoDoTabuleiro = (tamanhoDoTabuleiro +1) /2;
        }

        internal int TamanhoDoTabuleiro
        {
            get { return _tamanhoDoTabuleiro; }
            set { _tamanhoDoTabuleiro = value * 2 - 1; }
        }


        // função para gerar o tabuleiro de 3 até 10 
        private void GerarTabuleiro()
        {
            int cont = 1;
            MatrizTabuleiro = new string[TamanhoDoTabuleiro, TamanhoDoTabuleiro];

            for (int i = 0; i < TamanhoDoTabuleiro; i++)
            {
                for (int j = 0; j < TamanhoDoTabuleiro; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            if (cont > 9)
                                MatrizTabuleiro[i, j] = $" {cont}";
                            else if (cont > 99)
                                MatrizTabuleiro[i, j] = $"{cont}";
                            else
                                MatrizTabuleiro[i, j] = $" {cont} ";
                            cont++;
                        }
                        else
                            MatrizTabuleiro[i, j] = "|";
                    }
                    else
                    {
                        if (j % 2 != 0)
                            MatrizTabuleiro[i, j] = "+";
                        else
                            MatrizTabuleiro[i, j] = "---";
                    }

                }
            }
        }

        // função para criar lista de jogadas possíveis
        private void ListarJogadasPossiveis()
        {
            JogadasPossiveis = new List<string>();
            for (int i = 1; i <= Math.Pow((TamanhoDoTabuleiro + 1) / 2, 2); i++)
            {
                JogadasPossiveis.Add($"{i}");
            }
        }


        // função para fazer uma string do tabuleiro para adicioná-lo no registro da partida
        public static string TabuleiroParaRegistro(Tabuleiro tabuleiro)
        {
            string resultado = "";
            foreach (string s in tabuleiro.MatrizTabuleiro)
            {
                int n;
                if (int.TryParse(s, out n))
                    resultado += "1,";
                else
                    resultado += $"{s},";
            }
            return resultado;
        }
        // função para transformar um tabuleiro de registro (string[] em um objeto da classe Tabuleiro)
        public static Tabuleiro TransformarTabuleiroDeRegistroEmTabuleiro(string[] tabuleiroDeRegistro, int tamanhoDoTabuleiro)
        {
            string[,] matrizTabuleiro = new string[tamanhoDoTabuleiro, tamanhoDoTabuleiro];
            int cont = 0;
            for(int i=0; i<tamanhoDoTabuleiro; i++)
            {
                for(int j=0; j<tamanhoDoTabuleiro; j++)
                {
                    matrizTabuleiro[i, j] = tabuleiroDeRegistro[cont];
                    cont++;
                }
            }
            return new Tabuleiro(matrizTabuleiro, tamanhoDoTabuleiro);
        }
       
    }
}

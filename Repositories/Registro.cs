using JogoDaVelha.Entities;
using JogoDaVelha.Views;
using JogoDaVelha.Controllers;

namespace JogoDaVelha.Repositories
{
    internal static class Registro
    {
        // paths para os arquivos
        private static string pathDadosJogadores = @"..\..\..\Data\DadosDosJogadores.txt";
        private static string pathRegistroPartidas = @"..\..\..\Data\RegistroDasPartidas.txt";
        private static string pathNumeroPartida = @"..\..\..\Data\QuantidadeDePartidasJogadas.txt";


        // função para checkar se há dados no arquivo e iniciar com eles caso existam
        internal static void AbrirDadosDosJogadoresAoIniciar()
        {
            try
            {
                using (StreamReader sr = new StreamReader(pathDadosJogadores))
                {
                    string linha = sr.ReadLine();
                    while (!string.IsNullOrEmpty(linha))
                    {
                        // separando os dados
                        string[] dados = linha.Split(';');

                        string nome = dados[0];
                        int vitorias = int.Parse(dados[1]);
                        int empates = int.Parse(dados[2]);
                        int derrotas = int.Parse(dados[3]);

                        // adicionando jogadora na lista jogadores
                        Jogador jogador = new Jogador(nome, vitorias, empates, derrotas);
                        DadosJogadores.Jogadores.Add(jogador);

                        linha = sr.ReadLine();
                    }
                }

            }
            catch (FileNotFoundException)
            {
                File.Create(pathDadosJogadores).Close();
            }

        }

        // função para salvar os dados de todos os jogadores
        internal static void SalvarDadosDosJogadores()
        {

            List<Jogador> jogadores = DadosJogadores.Jogadores;
            File.WriteAllText(pathDadosJogadores, string.Empty);

            using (StreamWriter sw = new StreamWriter(pathDadosJogadores))
            {
                foreach (Jogador jogador in jogadores)
                {
                    sw.WriteLine($"{jogador.Nome};{jogador.QuantidadeVitorias};{jogador.QuantidadeEmpates};{jogador.QuantidadeDerrotas}");
                }
            }
        }

        // função para salvar os resultados da partida (tabuleiro, jogadores, resultado)
        internal static void SalvarResultadoDaPartida(int tamanhoTabuleiro,Tabuleiro tabuleiro ,string jogador1, string jogador2, string resultado)
        {            
            int partidaDeNumero = 0;

            // abrindo e lendo o arquivo
            try
            {
                using (StreamReader sr = new StreamReader(pathNumeroPartida))
                {
                    partidaDeNumero = int.Parse(sr.ReadLine());
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(pathNumeroPartida).Close();

            }
            // caso o arquivo esteja criado, porém vazio
            catch (FormatException) { }

            // aumentando para a próxima partida
            using (StreamWriter sw = new StreamWriter(pathNumeroPartida, false))
            {
                sw.WriteLine(partidaDeNumero + 1);
            }

            // aumentando a variável para registrar a seguir
            partidaDeNumero++;

            // registrando resutados
            using (StreamWriter sw = new StreamWriter(pathRegistroPartidas, true))
            {
                sw.WriteLine($"{partidaDeNumero};{tamanhoTabuleiro};{TabuleiroParaRegistro(tabuleiro)};{jogador1};{jogador2};{resultado}");
            }

        }

        // função para mostrar o resultado das partidas anteriores
        internal static void MostrarHistoricoDePartidas(string? nome)
        {
            Console.Clear();
            Console.WriteLine(Arte.historico + "\n");

            try
            {
                // abrindo arquivo do histórico
                using (StreamReader sr = new StreamReader(pathRegistroPartidas))
                {
                    string linha = sr.ReadLine();
                    while (!string.IsNullOrEmpty(linha))
                    {
                        // separando os dados
                        string[] dados = linha.Split(';');
                        int numeroDaPartida = int.Parse(dados[0]);
                        int tamanhoTabuleiro = int.Parse(dados[1]);
                        string[] tabuleiro = dados[2].Split(',');
                        string jogador1 = dados[3];
                        string jogador2 = dados[4];
                        string resultado = dados[5];

                        // caso a função seja chamada com um nome específico
                        if (nome != null)
                        {
                            if (jogador1 != nome && jogador2 != nome)
                            {
                                linha = sr.ReadLine();
                                continue;
                            }
                        }
                        // imprimindo os resultados
                        Console.WriteLine(Arte.linha);
                        Console.WriteLine($"\n  Partida: {numeroDaPartida}- {jogador1}(X) vs {jogador2}(O)\n");

                        // imprimindo tabuleiro
                        int cont = 0;
                        for (int i = 0; i < tamanhoTabuleiro; i++)
                        {
                            Console.Write("  ");
                            for (int j = 0; j < tamanhoTabuleiro; j++)
                            {
      
                                ConsoleColor aux = Console.ForegroundColor;
                                if (tabuleiro[cont].Trim() == "X")
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                else if (tabuleiro[cont].Trim() == "O")
                                    Console.ForegroundColor = ConsoleColor.DarkRed;

                                if (tabuleiro[cont].Trim() == "1")
                                    Console.Write("   ");
                                else
                                    Console.Write(tabuleiro[cont]);
                                Console.ForegroundColor = aux;
                                cont++;
                            }
                            Console.WriteLine();
                        }

                        if (resultado == "X")
                            resultado = jogador1;
                        else if (resultado == "O")
                            resultado = jogador2;

                        if (resultado == "Velha")
                            Console.WriteLine("\n  " + resultado);
                        else
                            Console.WriteLine($"\n  Vencedor: {resultado}");

                        linha = sr.ReadLine();
                    }

                }
            }

            // se o arquivo não existe, crio um e fecho ele
            catch (FileNotFoundException)
            {
                File.Create(pathRegistroPartidas).Close();
            }
            Menu.AperteEnterParaContinuar();
        }


        // função para fazer uma string do tabuleiro para adicioná-lo no registro da partida
        public static string TabuleiroParaRegistro(Tabuleiro tabuleiro)
        {
            string resultado = "";
            foreach (string s in tabuleiro.tabuleiro)
            {
                int n;
                if (int.TryParse(s, out n))
                    resultado += "1,";
                else
                    resultado += $"{s},";
            }
            return resultado;
        }
    }
}

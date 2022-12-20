using System.Globalization;
using JogoDaVelha.Views;
using JogoDaVelha.Entities;
using JogoDaVelha.Controllers;

namespace JogoDaVelha.Repositories
{
    internal class DadosJogadores
    {
        // lista de jogadores
        public static List<Jogador> Jogadores { get; private set; } =  new List<Jogador>();


        // função para cadastrar jogador novo
        internal static void CadastrarJogador()
        {
            Console.Clear();
            Console.WriteLine(Arte.adicionarJogador + "\n");
            Console.Write("  Digite o nome do jogador: ");
            string? nome = Console.ReadLine();

            if (nome.Length > 1 && nome.Length <= 60)
            {
                string[] input = nome.Split();
                foreach (string s in input)
                {
                    foreach (char c in s)
                    {
                        if (!char.IsLetter(c))
                        {
                            Console.WriteLine("  Nome do usuário só pode conter letras.");
                            Menu.EntradaInvalida();
                            return;
                        }
                    }
                }

                // após passar a validação, passar o nome para pascal case
                TextInfo textInfo = new CultureInfo("pt-br", false).TextInfo;
                nome = textInfo.ToTitleCase(nome);
                foreach (Jogador jogador in Jogadores)
                {
                    if (jogador.Nome == nome)
                    {
                        Console.WriteLine("Jogador já cadastrado.");
                        Menu.AperteEnterParaContinuar();
                        return;
                    }
                }
                Jogador novoJogador = new Jogador(nome);
                Jogadores.Add(novoJogador);
                Registro.SalvarDadosDosJogadores();
                Console.WriteLine("\n  Jogador cadastrado com sucesso!");
                Menu.AperteEnterParaContinuar();
            }
            else
            {
                Console.WriteLine("  Nomes devem conter entre 2 a 60 letras.");
                Menu.AperteEnterParaContinuar();
            }

        }
        // função para listar os jogadores
        internal static void ListarTodosJogadores()
        {
            Console.Clear();
            Console.WriteLine(Arte.jogadores + "\n");
            if (Jogadores.Count > 0)
                foreach (Jogador jogador in Jogadores)
                    Console.WriteLine(jogador);

            else
                Console.WriteLine("  Nenhum jogador cadastrado.");

            Console.WriteLine("\n  Obs: Vitórias = 3pts | Empates = 1pt | Derrotas = -1pt.");
            Menu.AperteEnterParaContinuar();
        }

        // função hall da fama
        internal static void HallDaFama()
        {
            Console.Clear();
            Console.WriteLine(Arte.hallDaFama + "\n");

            // lista em ordem de pontuação
            List<Jogador> jogadoresPorPontos = Jogadores.OrderBy(x => x.Pontuacao).ToList();

            int index = jogadoresPorPontos.Count - 1;
            if (index > 2)
            {
                for (int i = index, j = 1; i > index - 3; i--, j++)
                {
                    Jogador jogador = jogadoresPorPontos[i];

                    if (jogador.Pontuacao <= 0)
                        continue;

                    Console.WriteLine($"  Top {j}: {jogador.Nome} | {jogador.Pontuacao} pontos | {jogador.QuantidadeVitorias}V/{jogador.QuantidadeEmpates}E/{jogador.QuantidadeDerrotas}D\n");
                }
            }
            else
            {
                for (int i = index, j = 1; i >= 0; i--, j++)
                {
                    Jogador jogador = jogadoresPorPontos[i];

                    if (jogador.Pontuacao <= 0)
                        continue;

                    Console.WriteLine($"  Top {j}: {jogador.Nome} | {jogador.Pontuacao} pontos | {jogador.QuantidadeVitorias}V/{jogador.QuantidadeEmpates}E/{jogador.QuantidadeDerrotas}D\n");
                }
            }
            Menu.AperteEnterParaContinuar();
        }
    }
}

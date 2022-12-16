using System.Globalization;

namespace JogoDaVelha
{
    internal class DadosJogadores
    {
        // lista de jogadores
        public static List<Jogador> Jogadores = new List<Jogador>(); 



        // função para cadastrar jogador novo
        internal static void CadastrarJogador()
        {
            Console.Clear();
            Console.Write("Digite o nome do jogador: ");
            string? nome = Console.ReadLine();

            if (nome.Length > 1 && nome.Length <= 60)
            {
                string[] input = nome.Split();
                foreach(string s in input)
                {
                    foreach (char c in s)
                    {
                        if (!char.IsLetter(c))
                        {
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
                    if(jogador.Nome == nome)
                    {
                        Console.WriteLine("Jogador já cadastrado.");
                        Menu.AperteEnterParaContinuar();
                        return;
                    }
                }
                Jogador novoJogador = new Jogador(nome);
                Jogadores.Add(novoJogador);
                Console.WriteLine("\nJogador cadastrado com sucesso!");
                Menu.AperteEnterParaContinuar();
            }
            else
            {
                Console.WriteLine("Nomes devem conter entre 2 e 60 letras.");
                Menu.AperteEnterParaContinuar();
            }
           
        }
        // Função para listar os jogadores
        internal static void ListarTodosJogadores() 
        {
            Console.Clear();
            Console.WriteLine("Jogadores:\n");
            if (Jogadores.Count > 0 )
                foreach (Jogador jogador in Jogadores)
                    Console.WriteLine(jogador);
               
            else
                Console.WriteLine("Nenhum jogador cadastrado.");

            Console.WriteLine("\nObs: Vitórias = 3pts, Empates = 1pt, Derrotas = -1pt.");
            Menu.AperteEnterParaContinuar();
        }
    }
}

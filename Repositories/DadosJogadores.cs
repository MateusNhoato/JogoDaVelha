using JogoDaVelha.Controllers;
using JogoDaVelha.Entities;
using JogoDaVelha.Services;
using JogoDaVelha.Views;

namespace JogoDaVelha.Repositories
{
    // classe para manipular a lista de jogadores
    public static class DadosJogadores
    {
        // lista de jogadores
        private static List<Jogador> _jogadores = new List<Jogador>();

        public static List<Jogador> Jogadores { get { return _jogadores; } }


        // função para cadastrar jogador novo
        public static void CadastrarJogador()
        {
            Tela.ImprimirCadastrarJogador();
            string? nome = Utilidades.LerNomeEPassarParaPascalCase();

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
                            Utilidades.EntradaInvalida();
                            return;
                        }
                    }
                }

                foreach (Jogador jogador in _jogadores)
                {
                    if (jogador.Nome == nome)
                    {
                        Console.WriteLine("Jogador já cadastrado.");
                        Utilidades.AperteEnterParaContinuar();
                        return;
                    }
                }
                Jogador novoJogador = new Jogador(nome);
                _jogadores.Add(novoJogador);
                Registro.SalvarDadosDosJogadores();
                Console.WriteLine("\n  Jogador cadastrado com sucesso!");
                Utilidades.AperteEnterParaContinuar();
            }
            else
            {
                Console.WriteLine("  Nomes devem conter entre 2 a 60 letras.");
                Utilidades.AperteEnterParaContinuar();
            }

        }
        // sobrecarga da função CadastrarJogador, usada para colocar jogadores que já estavam salvos no arquivo txt na lista de jogadores
        public static void CadastrarJogador(Jogador jogador) => _jogadores.Add(jogador);
        

        // função para achar jogadores pelos seus nomes para Jogar
        public static void AcharJogadoresParaJogo(string nomePrimeiroJogador, string nomeSegundoJogador)
        {
            Jogador? primeiroJogador = _jogadores.Find(x => x.Nome == nomePrimeiroJogador);
            Jogador? segundoJogador = _jogadores.Find(x => x.Nome == nomeSegundoJogador);

            if (primeiroJogador != null && segundoJogador != null)
            {
                if (primeiroJogador != segundoJogador)
                    Jogo.Jogar(primeiroJogador, segundoJogador);
                else
                {
                    Console.WriteLine("\n  Um Jogador não pode jogar contra si mesmo.");
                    Utilidades.AperteEnterParaContinuar();
                }
            }
            else
            {
                Console.WriteLine("\n  Jogador(es) inválido(s)");
                Utilidades.AperteEnterParaContinuar();
            }
        }
    }
}

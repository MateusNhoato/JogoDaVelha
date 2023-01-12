using JogoDaVelha.Entities;

namespace JogoDaVelha.Repositories
{
    // classe para registrar as Partidas
    public class Partida
    {
        public int? NumeroDaPartidaId { get; private set; }
        public int TamanhoDoTabuleiro { get; private set; }
        public Tabuleiro Tabuleiro { get; private set; }
        public string Jogador1 { get; private set; }
        public string Jogador2 { get; private set; }

        public string Resultado { get; private set; }

        public Partida(int numeroDaPartida, int tamanhoDoTabuleiro, Tabuleiro tabuleiro, string jogador1, string jogador2, string resultado)
        {
            NumeroDaPartidaId = numeroDaPartida;
            TamanhoDoTabuleiro = tamanhoDoTabuleiro;
            Tabuleiro = tabuleiro;
            Jogador1 = jogador1;
            Jogador2 = jogador2;
            Resultado = resultado;
        }

        public Partida(int tamanhoDoTabuleiro, Tabuleiro tabuleiro, string jogador1, string jogador2, string resultado)
        {
            TamanhoDoTabuleiro = tamanhoDoTabuleiro;
            Tabuleiro = tabuleiro;
            Jogador1 = jogador1;
            Jogador2 = jogador2;
            Resultado = resultado;
        }

        public override string ToString()
        {
            return $"\n  Partida: {NumeroDaPartidaId}- {Jogador1}(X) vs {Jogador2}(O)\n";
        }
    }
}

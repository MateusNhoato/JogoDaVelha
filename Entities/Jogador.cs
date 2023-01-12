namespace JogoDaVelha.Entities
{
    public class Jogador
    {
        public string Nome { get; private set; }
        internal int QuantidadeVitorias { get; private set; }
        internal int QuantidadeDerrotas { get; private set; }
        internal int QuantidadeEmpates { get; private set; }

        public int Pontuacao
        {
            get { return QuantidadeVitorias * 3 + QuantidadeEmpates - QuantidadeDerrotas; }
        }

        // construtor simples, para adicionar um jogador novo
        public Jogador(string nome)
        {
            Nome = nome;
        }

        // construtor completo, para colocar o jogador do arquivo txt na lista de jogadores
        public Jogador(string nome, int vitorias, int empates, int derrotas)
        {
            Nome = nome;
            QuantidadeVitorias = vitorias;
            QuantidadeEmpates = empates;
            QuantidadeDerrotas = derrotas;
        }

        public void AumentarVitorias() => QuantidadeVitorias++;

        public void AumentarDerrotas() => QuantidadeDerrotas++;

        public void AumentarEmpates() => QuantidadeEmpates++;

        public override string ToString()
        {
            return $"  {Nome} | Pontuação: {Pontuacao} | {QuantidadeVitorias}V/{QuantidadeEmpates}E/{QuantidadeDerrotas}D\n";
        }
    }
}

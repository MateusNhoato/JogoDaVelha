namespace JogoDaVelha.Entities
{
    internal class Jogador
    {
        public string Nome { get; private set; }
        internal int QuantidadeVitorias { get; set; }
        internal int QuantidadeDerrotas { get; set; }
        internal int QuantidadeEmpates { get; set; }



        public int Pontuacao
        {
            get { return QuantidadeVitorias * 3 + QuantidadeEmpates + QuantidadeDerrotas * -1; }
        }

        // construtor simples, para adicionar um jogador novo
        internal Jogador(string nome)
        {
            Nome = nome;
        }

        // construtor mais completo, para colocar o jogador do arquivo txt na lista de jogadores
        internal Jogador(string nome, int vitorias, int empates, int derrotas)
        {
            Nome = nome;
            QuantidadeVitorias = vitorias;
            QuantidadeEmpates = empates;
            QuantidadeDerrotas = derrotas;
        }


        public override string ToString()
        {
            return $"  Jogador: {Nome} | Pontuação: {Pontuacao} | {QuantidadeVitorias}V/{QuantidadeEmpates}E/{QuantidadeDerrotas}D\n";
        }
    }
}

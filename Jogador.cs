namespace JogoDaVelha
{
    internal class Jogador
    {
        private string? _nome;
        internal int QuantidadeVitorias = 0;
        internal int QuantidadeDerrotas = 0;
        internal int QuantidadeEmpates = 0;

        public string? Nome
        {
            get { return _nome; }
        }

        public int Pontuacao
        {
            get { return QuantidadeVitorias * 3 + QuantidadeEmpates + QuantidadeDerrotas * -1; }
        }


        internal Jogador(string nome)
            {
                _nome = nome;
            }

       

        public override string ToString()
        {
            return $"Jogador: {_nome}\n" +
                $"Vitórias: {QuantidadeVitorias}\n" +
                $"Derrotas: {QuantidadeDerrotas}\n" +
                $"Empates : {QuantidadeEmpates}\n" +
                $"Pontuacao: {Pontuacao}\n";

        }

    }

}

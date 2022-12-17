﻿namespace JogoDaVelha.Entities
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

        // construtor simples, para adicionar um jogo novo
        internal Jogador(string nome)
        {
            _nome = nome;
        }

        // construtor mais completo, para colocar na lista de jogadores os jogadores do arquivo txt
        internal Jogador(string nome, int vitorias, int empates, int derrotas)
        {
            _nome = nome;
            QuantidadeVitorias = vitorias;
            QuantidadeEmpates = empates;
            QuantidadeDerrotas = derrotas;
        }


        public override string ToString()
        {
            return $"  Jogador: {_nome} | Pontuação: {Pontuacao} | {QuantidadeVitorias}V/{QuantidadeEmpates}E/{QuantidadeDerrotas}D\n";
        }
    }
}
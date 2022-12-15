using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha
{
    internal struct Jogador
    {
        string nome;
        int pontuacao;
        int quantidadeVitorias;
        int quantidadeDerrotas;
        int quantidadeEmpates;



        public override string ToString()
        {
            return $"Jogador: {nome}\n" +
                $"Quantidade de vitórias: {quantidadeVitorias}\n" +
                $"Quantidade de derrotas: {quantidadeDerrotas}\n" +
                $"Quantidade de empates: {quantidadeEmpates}\n" +
                $"\nPontuação total: {pontuacao}\n";

        }

    }

}

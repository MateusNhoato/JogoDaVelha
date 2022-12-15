
using System.Diagnostics.CodeAnalysis;

namespace JogoDaVelha
{
    internal class Jogo
    {
        // função de jogada de cada jogador
        internal static bool Jogada(string jogada, string jogador)
        {

            for (int i = 0; i < Tabuleiro.TamanhoDoTabuleiro; i++)
            {
                for (int j = 0; j < Tabuleiro.TamanhoDoTabuleiro; j++)
                {
                    if (Tabuleiro.tabuleiro[i, j].Trim() == jogada)
                    {
                        Tabuleiro.tabuleiro[i, j] = jogador;
                        return true;
                    }
                }
            }
            return false;
        }
    
    
        // função para checkar se alguém ganhou ou deu velha
        internal static string CheckarVitoriaOuVelha()
        {
            int tamanhoAuxiliar = (Tabuleiro.TamanhoDoTabuleiro + 1) / 2;
            

            string[] valoresNaDiagonalPrincipal = new string[tamanhoAuxiliar];
            string[] valoresNaDiagonalSecundaria = new string[tamanhoAuxiliar];

            int diagonalSecundariaAuxiliar = tamanhoAuxiliar + 1;

            // lista das colunas
            List<string[]> colunas = new List<string[]>();
            for (int i=0; i < Tabuleiro.TamanhoDoTabuleiro+1; i+=2)
            {
                colunas.Add( new string[tamanhoAuxiliar]);
            }

            // loop de checkagem
            for (int i=0; i< Tabuleiro.TamanhoDoTabuleiro; i+=2)
            {
                
                string[] valoresNaLinha = new string[tamanhoAuxiliar];

                for(int j=0; j<Tabuleiro.TamanhoDoTabuleiro; j+=2) 
                {
                    string valor = Tabuleiro.tabuleiro[i, j].Trim();
                    int posicaoAuxiliarParaVetoresJ = (int)Math.Floor(j / 2.0);
                    int posicaoAuxiliarParaVetoresI = (int)Math.Floor(i / 2.0);

                    valoresNaLinha[posicaoAuxiliarParaVetoresJ] = valor;

                    // adicionando valores na diagonal principal
                    if(i == j)
                        valoresNaDiagonalPrincipal[posicaoAuxiliarParaVetoresJ] = valor;
                    
                    // adicionando valores na diagonal secundária
                    if(j == diagonalSecundariaAuxiliar)
                    {
                        valoresNaDiagonalSecundaria[posicaoAuxiliarParaVetoresJ] = valor;
                        diagonalSecundariaAuxiliar-=2;
                    }
                    
                    // adicionando os valores nas colunas                   
                    colunas[posicaoAuxiliarParaVetoresI][posicaoAuxiliarParaVetoresJ] = Tabuleiro.tabuleiro[j,i].Trim(); 
                }
                // checkando os valores únicos da linha
                valoresNaLinha = valoresNaLinha.Distinct().ToArray();
                if(valoresNaLinha.Length == 1)
                   return "Vencedor: " + valoresNaLinha[0];
                
            }
            // checkando a diagonal principal
            valoresNaDiagonalPrincipal = valoresNaDiagonalPrincipal.Distinct().ToArray();
            if (valoresNaDiagonalPrincipal.Length == 1)
                return "Vencedor: " + valoresNaDiagonalPrincipal[0];

            // checkando a diagonal secundária
            valoresNaDiagonalSecundaria = valoresNaDiagonalSecundaria.Distinct().ToArray();
            if (valoresNaDiagonalSecundaria.Length == 1)
                return "Vencedor: " + valoresNaDiagonalSecundaria[0];

            // checkando as colunas
            for(int i=0; i <colunas.Count; i++)
            {
                string[] coluna = colunas[i];
                coluna = coluna.Distinct().ToArray();
                if (coluna.Length == 1)
                    return "Vencedor: " + coluna[0];
            }

            // checkando velha
            if (Tabuleiro.jogadasPossiveis.Count == 0)
                return "Deu velha";

            // caso ninguém ganhou e não deu velha
            return " ";
            
        }
    
    }
}

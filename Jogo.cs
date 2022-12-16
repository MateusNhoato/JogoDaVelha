
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

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
        internal static string? CheckarVitoriaOuVelha()
        {
            int tamanhoAuxiliar = (Tabuleiro.TamanhoDoTabuleiro + 1) / 2;
            

            string[] valoresNaDiagonalPrincipal = new string[tamanhoAuxiliar];
            string[] valoresNaDiagonalSecundaria = new string[tamanhoAuxiliar];

            int diagonalSecundariaAuxiliar = tamanhoAuxiliar + 1;
            if(tamanhoAuxiliar % 2 == 0)
                diagonalSecundariaAuxiliar = tamanhoAuxiliar + 2;

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
                   return valoresNaLinha[0];
                
            }
            // checkando a diagonal principal
            valoresNaDiagonalPrincipal = valoresNaDiagonalPrincipal.Distinct().ToArray();
            if (valoresNaDiagonalPrincipal.Length == 1)
                return  valoresNaDiagonalPrincipal[0];

            // checkando a diagonal secundária
            valoresNaDiagonalSecundaria = valoresNaDiagonalSecundaria.Distinct().ToArray();
            if (valoresNaDiagonalSecundaria.Length == 1)
                return valoresNaDiagonalSecundaria[0];

            // checkando as colunas
            for(int i=0; i <colunas.Count; i++)
            {
                string[] coluna = colunas[i];
                coluna = coluna.Distinct().ToArray();
                if (coluna.Length == 1)
                    return coluna[0];
            }

            // checkando velha
            if (Tabuleiro.jogadasPossiveis.Count == 0)                
                return "Velha";
            

            // caso ninguém ganhou e não deu velha
            return null;
            
        }
    
        // função principal de jogar
        internal static void Jogar(Jogador jogador1, Jogador jogador2)
        {
            int tamanho;
            string? vencedor;
            do
            {
                Console.Write("\n  Digite o tamanho do jogo (3 a 10): ");
                if (int.TryParse(Console.ReadLine(), out tamanho))
                {
                    if (tamanho >= 3 && tamanho <= 10)
                    {
                        Console.Clear();
                        break;
                    }
                }                                
            } while (true);
            
            // configurando o tabuleiro e gerando lista de jogadas possíveis
            Tabuleiro.TamanhoDoTabuleiro = tamanho;
            Tabuleiro.GerarTabuleiro();
            Tabuleiro.ListarJogadasPossiveis();
            Console.Clear();
            Tabuleiro.MostrarTabuleiro();

            Jogador jogador = jogador1;
            string jogada = " X ";
            while (true)
            {               
                string posicao;
                do
                {
                    Console.WriteLine($"\n  Vez de {jogador.Nome}\n");
                    Console.Write($"  Digite a posição da jogada ({jogada.Trim()}): ");
                    posicao = Console.ReadLine();
                } while (!Tabuleiro.jogadasPossiveis.Contains(posicao));
                Tabuleiro.jogadasPossiveis.Remove(posicao);
                Jogo.Jogada(posicao, jogada);
                Console.Clear();
                Tabuleiro.MostrarTabuleiro();

                // chamando a função de checkar vitória após cada jogada
                vencedor = Jogo.CheckarVitoriaOuVelha();
                if (vencedor != null)
                {
                    string jog1 = jogador1.Nome;
                    string jog2 = jogador2.Nome;

                    // CheckarVitoriaOuVelha retorou velha 
                    if(vencedor == "Velha")
                    {
                        Console.WriteLine("\n  Deu velha. Empate.");
                        jogador1.QuantidadeEmpates += 1;
                        jogador2.QuantidadeEmpates += 1;
                        
                    }
                    // se retornar algo que não seja null nem velha, teve um vencedor (x ou o)
                    else
                    {
                        Console.WriteLine($"\n  Vencedor: {jogador.Nome} ({vencedor}).");
                        jogador.QuantidadeVitorias += 1;
                        
                        // alterando quantidade de derrotas do perdedor
                        if (jogador == jogador1)                           
                            jogador2.QuantidadeDerrotas += 1;    
                        
                        else                       
                            jogador1.QuantidadeDerrotas += 1;
                            
                    }
                    // salvando os dados dos jogadores
                    Registro.SalvarDadosDosJogadores();
                    // salvando a partida
                    Registro.SalvarResultadoDaPartida(Tabuleiro.TamanhoDoTabuleiro,jog1, jog2, vencedor);

                    Menu.AperteEnterParaContinuar();
                    break;
                }
                if(jogador == jogador1)
                {
                    jogador = jogador2;
                    jogada = " O ";
                }
                else
                {
                    jogador = jogador1;
                    jogada = " X ";
                }
            }
        }




    }
}

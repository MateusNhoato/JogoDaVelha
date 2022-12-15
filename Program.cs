using System.Security.Cryptography.X509Certificates;

namespace JogoDaVelha
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Digite o tamanho do jogo: ");
            int tamanho = int.Parse(Console.ReadLine());
            Tabuleiro.TamanhoDoTabuleiro = tamanho;
            Tabuleiro.GerarTabuleiro();
            Tabuleiro.ListarJogadasPossiveis();
            string vencedor;
            while(true)
            {
                Tabuleiro.MostrarTabuleiro();
                string jogada; 
                string jogador = " X ";
                do
                {
                    Console.Write("Digite a posição da jogada(x): ");
                    jogada = Console.ReadLine();
                } while (!Tabuleiro.jogadasPossiveis.Contains(jogada));
                Tabuleiro.jogadasPossiveis.Remove(jogada);
                Jogo.Jogada(jogada, jogador);
                Console.Clear();
                Tabuleiro.MostrarTabuleiro();

                vencedor = Jogo.CheckarVitoriaOuVelha();
                if (vencedor != " ")
                {
                    Console.WriteLine(vencedor);
                    break;
                }


                jogador = " O ";
                do
                {
                    Console.Write("Digite a posição da jogada(o): ");
                    jogada = Console.ReadLine();
                } while (!Tabuleiro.jogadasPossiveis.Contains(jogada));
                Tabuleiro.jogadasPossiveis.Remove(jogada);
                Jogo.Jogada(jogada, jogador);
                Console.Clear();

                vencedor = Jogo.CheckarVitoriaOuVelha();
                if (vencedor != " ")
                {
                    Console.WriteLine(vencedor);
                    break;
                }
            }           
            


            

            


           

            

        }
    }
}

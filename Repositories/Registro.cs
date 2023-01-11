using JogoDaVelha.Controllers;
using JogoDaVelha.Entities;
using JogoDaVelha.Views;

namespace JogoDaVelha.Repositories
{
    // classe para manipular os arquivos txt
    internal static class Registro
    {
        // paths para os arquivos
        private static string _pathDadosJogadores = @"..\..\..\Repositories\Data\DadosDosJogadores.txt";
        private static string _pathRegistroPartidas = @"..\..\..\Repositories\Data\RegistroDasPartidas.txt";
        

        // função para checkar se há dados no arquivo e iniciar com eles caso existam
        internal static void AbrirDadosDosJogadoresAoIniciar()
        {
            try
            {
                using (StreamReader sr = new StreamReader(_pathDadosJogadores))
                {
                    string? linha = sr.ReadLine();
                    while (!string.IsNullOrEmpty(linha))
                    {
                        // separando os dados
                        string[] dados = linha.Split(';');

                        string nome = dados[0];
                        int vitorias = int.Parse(dados[1]);
                        int empates = int.Parse(dados[2]);
                        int derrotas = int.Parse(dados[3]);

                        // adicionando jogadora na lista jogadores
                        Jogador jogador = new Jogador(nome, vitorias, empates, derrotas);
                        DadosJogadores.CadastrarJogador(jogador);

                        linha = sr.ReadLine();
                    }
                }

            }
            // caso o arquivo não esteja criado
            catch (FileNotFoundException)
            {
                File.Create(_pathDadosJogadores).Close();
            }

        }

        // função para salvar os dados de todos os jogadores
        internal static void SalvarDadosDosJogadores()
        {

            List<Jogador> jogadores = DadosJogadores.Jogadores;
            File.WriteAllText(_pathDadosJogadores, string.Empty);

            using (StreamWriter sw = new StreamWriter(_pathDadosJogadores))
            {
                foreach (Jogador jogador in jogadores)
                {
                    sw.WriteLine($"{jogador.Nome};{jogador.QuantidadeVitorias};{jogador.QuantidadeEmpates};{jogador.QuantidadeDerrotas}");
                }
            }
        }

        // função para salvar os resultados da partida
        internal static void SalvarResultadoDaPartida(Partida partida)
        {
            int partidaDeNumero = 0;

           try
            {
                string[] file = File.ReadAllLines(_pathRegistroPartidas);
                partidaDeNumero += file.Length;
            }
            
            // caso o arquivo não esteja criado
            catch (FileNotFoundException)
            {
                File.Create(_pathDadosJogadores).Close();
            }

            // aumentando a variável para registrar a seguir
            partidaDeNumero++;

            // registrando resutados
            using (StreamWriter sw = new StreamWriter(_pathRegistroPartidas, true))
            {
                sw.WriteLine($"{partidaDeNumero};{partida.Tabuleiro.TamanhoDoTabuleiro};{Tabuleiro.TabuleiroParaRegistro(partida.Tabuleiro)};{partida.Jogador1};{partida.Jogador2};{partida.Resultado}");
            }

        }

        // função para mostrar o resultado das partidas anteriores
        internal static void MostrarHistoricoDePartidas(string? nome)
        {
            Tela.ImprimirHistoricoDeJogador(false);

            Jogador? jogador = DadosJogadores.Jogadores.Find(x => x.Nome == nome);
            if (jogador == null && nome != null)
            {
                Console.WriteLine("  Jogador não encontrado.");
                Menu.AperteEnterParaContinuar();
                return;
            }

            try
            {
                // abrindo arquivo do histórico
                using (StreamReader sr = new StreamReader(_pathRegistroPartidas))
                {
                    string? linha = sr.ReadLine();
                    // variável auxiliar para partidas de histórico de um jogador específico
                    int contAux = 0;
                    if(nome != null)
                        Console.WriteLine($"   Histórico de {nome}");


                    while (!string.IsNullOrEmpty(linha))
                    {
                        // separando os dados
                        string[] dados = linha.Split(';');
                        int numeroDaPartida = int.Parse(dados[0]);
                        int tamanhoTabuleiro = int.Parse(dados[1]);
                        string[] tabuleiro = dados[2].Split(',');
                        string jogador1 = dados[3];
                        string jogador2 = dados[4];
                        string resultado = dados[5];

                        // caso a função seja chamada com um nome específico
                        if (nome != null)
                        { 
                            if (jogador1 != nome && jogador2 != nome)
                            {
                                linha = sr.ReadLine();
                                continue;
                            }
                            contAux++;
                            numeroDaPartida = contAux;
                        }
                        Partida partida = new Partida(numeroDaPartida, tamanhoTabuleiro, Tabuleiro.TransformarTabuleiroDeRegistroEmTabuleiro(tabuleiro, tamanhoTabuleiro), jogador1, jogador2, resultado);
                        // imprimindo os resultados
                        Tela.ImprimirPartida(partida);                     
                        linha = sr.ReadLine();
                    }

                }
            }

            // se o arquivo não existe, crio um e fecho ele
            catch (FileNotFoundException)
            {
                File.Create(_pathRegistroPartidas).Close();
            }
            Menu.AperteEnterParaContinuar();
        }


       
    }
}

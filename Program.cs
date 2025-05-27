using ScreenSound.Modelos;
using ScreenSound.Menus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; // Necessário para async/await

internal class Program
{
    // O método Main agora é assíncrono para permitir chamadas await
    private static async Task Main(string[] args)
    {
        // CRIACAO DO OBJETO DA BANDA
        Banda theBeatles = new("The Beatles");
        theBeatles.AdicionarNota(new Avaliacao(10));
        theBeatles.AdicionarNota(new Avaliacao(8));
        theBeatles.AdicionarNota(new Avaliacao(10));

        // Dicionário para armazenar as bandas registradas
        Dictionary<string, Banda> bandasRegistradas = new();
        bandasRegistradas.Add(theBeatles.Nome, theBeatles);

        // Dicionário de opções de menu
        Dictionary<int, Menu> opcoes = new();
        opcoes.Add(1, new MenuRegistrarBanda());
        opcoes.Add(2, new MenuRegistrarAlbum());
        opcoes.Add(3, new MenuMostrarBandasRegistradas());
        opcoes.Add(4, new MenuAvaliarBandas());
        opcoes.Add(5, new MenuAvaliarAlbum());
        opcoes.Add(6, new MenuExibirDetalhes());
        opcoes.Add(-1, new MenuSair());
                
        // Método para exibir o logo
        void ExibirLogo()
        {
            Console.WriteLine(@"

            ░██████╗░█████╗░██████╗░███████╗███████╗███╗░░██╗  ░██████╗░█████╗░██╗░░░██╗███╗░░██╗██████╗░
            ██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝████╗░██║  ██╔════╝██╔══██╗██║░░░██║████╗░██║██╔══██╗
            ╚█████╗░██║░░╚═╝██████╔╝█████╗░░█████╗░░██╔██╗██║  ╚█████╗░██║░░██║██║░░░██║██╔██╗██║██║░░██║
            ░╚═══██╗██║░░██╗██╔══██╗██╔══╝░░██╔══╝░░██║╚████║  ░╚═══██╗██║░░██║██║░░░██║██║╚████║██║░░██║
            ██████╔╝╚█████╔╝██║░░██║███████╗███████╗██║░╚███║  ██████╔╝╚█████╔╝╚██████╔╝██║░╚███║██████╔╝
            ╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚═╝░░╚══╝  ╚═════╝░░╚════╝░░╚═════╝░╚═╝░░╚══╝╚═════╝░
            ");
            Console.WriteLine("Boas vindas ao Screen Sound 2.0!");
        }

        // Método para exibir as opções do menu
        // Agora também é assíncrono
        async Task ExibirOpcoesDoMenu()
        {
            ExibirLogo();
            Console.WriteLine("\nDigite 1 para registrar uma banda");
            Console.WriteLine("Digite 2 para registrar o álbum de uma banda");
            Console.WriteLine("Digite 3 para mostrar todas as bandas");
            Console.WriteLine("Digite 4 para avaliar uma banda");
            Console.WriteLine("Digite 5 para avaliar um album");
            Console.WriteLine("Digite 6 para exibir os detalhes de uma banda");
            Console.WriteLine("Digite -1 para sair");

            Console.Write("\nDigite a sua opção: ");
            string opcaoEscolhida = Console.ReadLine()!;
            int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

            if (opcoes.ContainsKey(opcaoEscolhidaNumerica))
            {
                Menu menuASerExibido = opcoes[opcaoEscolhidaNumerica];
                // Chama o método Executar do menu de forma assíncrona
                await menuASerExibido.Executar(bandasRegistradas);
                // Continua exibindo o menu se a opção não for sair
                if(opcaoEscolhidaNumerica > 0) await ExibirOpcoesDoMenu();    
            }
            else
            {
                Console.WriteLine("Opcao invalida");
            }
        }

        // Inicia a exibição do menu
        await ExibirOpcoesDoMenu();
    }
}
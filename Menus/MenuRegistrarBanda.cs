using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarBanda : Menu
{
    public override async Task Executar(Dictionary<string, Banda> bandasRegistradas)
    {
        await base.Executar(bandasRegistradas); // Calls the base Execute to clear the console
        ExibirTituloDaOpcao("Registrar Banda");
        Console.Write("Digite o nome da banda que deseja registrar: ");
        string nomeDaBanda = Console.ReadLine()!;
        Banda banda = new(nomeDaBanda);
        
        // ************* AI Call here ***************
        Console.WriteLine($"\nGerando descrição com IA para {nomeDaBanda}, aguarde...");
        await banda.GerarDescricaoIA(); // Calls the asynchronous method to generate the summary
        // ************* End of AI Call ***************

        bandasRegistradas.Add(nomeDaBanda, banda);

        Console.WriteLine($"A banda {nomeDaBanda} foi registrada com sucesso!");
        // LINHA ONDE A DESCRIÇÃO É EXIBIDA APÓS O REGISTRO
        if (!string.IsNullOrEmpty(banda.Resumo))
        {
            Console.WriteLine($"Descrição gerada pela IA: {banda.Resumo}");
        }
        else
        {
            Console.WriteLine("Nenhuma descrição gerada pela IA.");
        }
        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}


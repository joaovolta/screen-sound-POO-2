using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarBandasRegistradas : Menu
{
   public override async Task Executar(Dictionary<string, Banda> bandasRegistradas)
    {
        await base.Executar(bandasRegistradas);
        ExibirTituloDaOpcao("Bandas Registradas");
        foreach (string banda in bandasRegistradas.Keys)
        {
            Console.WriteLine($"Banda: {banda}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}

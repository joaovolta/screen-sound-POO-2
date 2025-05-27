using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override Task Executar(Dictionary<string, Banda> bandasRegistradas)
    {
        Console.WriteLine("Programa encerrado...");
        return Task.CompletedTask;
    }
}
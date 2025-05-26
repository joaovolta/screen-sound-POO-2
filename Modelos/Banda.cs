namespace ScreenSound.Modelos;

internal class Banda : IAvaliavel 
{
    private List<Album> albuns = new List<Album>();
    private List<Avaliacao> lstNotas = new ();

    public Banda(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; }
    // CRIAR UMA CONDICAO PARA RETORNAR A MEDIA DAS AVALIACOES
    public double Media 
    {
        get
        {
            if (lstNotas.Count == 0) return 0;
            else return lstNotas.Average(a => a.Nota);
        }
    }
    public List<Album> Albuns => albuns;

    public void AdicionarAlbum(Album album) 
    { 
        albuns.Add(album);
    }

    public void AdicionarNota(Avaliacao nota)
    {
        lstNotas.Add(nota);
    }

    public void ExibirDiscografia()
    {
        Console.WriteLine($"Discografia da banda {Nome}");
        foreach (Album album in albuns)
        {
            Console.WriteLine($"Álbum: {album.Nome} ({album.DuracaoTotal})");
        }
    }
}
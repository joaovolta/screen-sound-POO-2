using Mscc.GenerativeAI; // Para usar a biblioteca do Gemini
using System;
using System.Collections.Generic;
using System.Linq; // Necessário para .Average()
using System.Threading.Tasks; // Necessário para async/await

namespace ScreenSound.Modelos;

internal class Banda : IAvaliavel
{
    private List<Album> albuns = new List<Album>();
    private List<Avaliacao> lstNotas = new();

    public Banda(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; }
    
    public double Media
    {
        get
        {
            if (lstNotas.Count == 0) return 0;
            else return lstNotas.Average(a => a.Nota);
        }
    }
    public IEnumerable<Album> Albuns => albuns;

    // Nova propriedade para armazenar o resumo gerado pela IA
    public string Resumo { get; private set; } = string.Empty;

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

    // Novo método assíncrono para gerar a descrição usando a IA
    public async Task GerarDescricaoIA()
    {
        // Pega a chave da API da variável de ambiente
        string apiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY_GEMINI");

        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("Erro: A chave de API do Gemini não está configurada na variável de ambiente 'GOOGLE_API_KEY_GEMINI'.");
            Resumo = "Não foi possível gerar um resumo. Chave de API ausente.";
            return;
        }

        try
        {
            var googleAI = new GoogleAI(apiKey: apiKey);
            // REVERTIDO AQUI: Usando o modelo gemini-pro novamente
            var model = googleAI.GenerativeModel(model: Model.Gemini15Flash); 

            string prompt = $"Crie uma breve descrição sobre a banda/artista {Nome}. Mencione seu gênero musical e alguns dos seus maiores sucessos ou características marcantes, se possível. Limite a descrição a aproximadamente 80 palavras.";

            // Gera o conteúdo de forma assíncrona
            var response = await model.GenerateContent(prompt);

            if (response != null && !string.IsNullOrEmpty(response.Text))
            {
                Resumo = response.Text.Trim(); // Atribui o texto gerado à propriedade
                Console.WriteLine($"Descrição IA para '{Nome}' gerada com sucesso!");
            }
            else
            {
                Resumo = "Não foi possível gerar um resumo detalhado para esta banda/artista.";
                Console.WriteLine($"Aviso: A resposta da IA para '{Nome}' estava vazia ou nula.");
            }
        }
        catch (Exception ex)
        {
            // Em caso de erro na chamada da API, armazena uma mensagem de erro no resumo
            Resumo = $"Erro ao gerar resumo com IA: {ex.Message}";
            Console.WriteLine($"Erro ao gerar descrição para '{Nome}': {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Detalhes do erro: {ex.InnerException.Message}");
            }
        }
    }
}
namespace ScreenSound.Modelos;

internal class Avaliacao
{
    public Avaliacao(int nota)
    {
        if(nota >= 0 && nota <= 10)  Nota = nota;
        else if(nota < 0) Nota = 0;
        else Nota = 10;
    }

    public int Nota { get; }

    // 1 - Métodos e propriedades static: pertencem à classe, não a instâncias.
    // 2 - Instâncias: são objetos criados a partir da classe e têm suas próprias propriedades e métodos que não são static.
    // 3 - Uso de static: é útil para funções que não precisam acessar dados de instâncias específicas.
    public static Avaliacao Parse(string texto)
    {
        int nota = int.Parse(texto);
        return new Avaliacao(nota); 
    }
}

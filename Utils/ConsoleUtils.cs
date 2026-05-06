namespace conta_bancaria_csharp.Utils;

/// <summary>
/// Classe utilitária responsável por auxiliar na leitura de dados pelo console.
/// </summary>
public static class ConsoleUtils
{
    public static int LerInteiro(string mensagem)
    {
        Console.Write(mensagem);
        return int.Parse(Console.ReadLine()!);
    }

    public static float LerFloat(string mensagem)
    {
        Console.Write(mensagem);
        return float.Parse(Console.ReadLine()!);
    }

    public static string LerTexto(string mensagem)
    {
        Console.Write(mensagem);
        return Console.ReadLine()!;
    }
}
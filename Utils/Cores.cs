namespace conta_bancaria_csharp.Utils;

/// <summary>
/// Classe utilitária responsável por aplicar cores nas mensagens do console.
/// </summary>
public static class Cores
{
    public const ConsoleColor Borda = ConsoleColor.DarkCyan;
    public const ConsoleColor Titulo = ConsoleColor.Cyan;
    public const ConsoleColor Opcao = ConsoleColor.Yellow;
    public const ConsoleColor Destaque = ConsoleColor.Magenta;
    public const ConsoleColor Texto = ConsoleColor.Gray;
    public const ConsoleColor Sucesso = ConsoleColor.Green;
    public const ConsoleColor Erro = ConsoleColor.Red;
    public const ConsoleColor Aviso = ConsoleColor.DarkYellow;

    public static void Escrever(string mensagem, ConsoleColor cor)
    {
        ExecutarComCor(cor, () => Console.Write(mensagem));
    }

    public static void EscreverLinha(string mensagem, ConsoleColor cor)
    {
        ExecutarComCor(cor, () => Console.WriteLine(mensagem));
    }

    private static void ExecutarComCor(ConsoleColor cor, Action acao)
    {
        ConsoleColor corOriginal = Console.ForegroundColor;

        try
        {
            Console.ForegroundColor = cor;
            acao();
        }
        finally
        {
            Console.ForegroundColor = corOriginal;
        }
    }
}

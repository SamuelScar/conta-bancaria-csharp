using System.Globalization;

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

    public static decimal LerDecimal(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string? entrada = Console.ReadLine();

            if (decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal valor))
                return valor;

            ConsolePrinter.ExibirErro("Valor inválido. Use ponto como separador decimal. Exemplo: 10.50");
        }
    }

    public static string LerTexto(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string texto = Console.ReadLine()!.Trim();

            if (!string.IsNullOrWhiteSpace(texto))
                return texto;

            ConsolePrinter.ExibirErro("Texto inválido. O campo não pode ficar vazio.");
        }
    }
}

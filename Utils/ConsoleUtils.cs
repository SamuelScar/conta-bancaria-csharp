using System.Globalization;
using conta_bancaria_csharp.Exceptions;

namespace conta_bancaria_csharp.Utils;

/// <summary>
/// Classe utilitária responsável por auxiliar na leitura de dados pelo console.
/// </summary>
public static class ConsoleUtils
{
    public const string ComandoCancelar = "CANCELAR";

    public static int LerInteiro(string mensagem)
    {
        while (true)
        {
            string entrada = LerEntrada(mensagem);

            if (int.TryParse(entrada, out int valor))
                return valor;

            ConsolePrinter.ExibirErro("Número inválido. Digite um número inteiro válido.");
        }
    }

    public static decimal LerDecimal(string mensagem)
    {
        while (true)
        {
            string entrada = LerEntrada(mensagem);

            if (decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal valor))
                return valor;

            ConsolePrinter.ExibirErro("Valor inválido. Use ponto como separador decimal. Exemplo: 10.50");
        }
    }

    public static string LerTexto(string mensagem)
    {
        while (true)
        {
            string texto = LerEntrada(mensagem).Trim();

            if (!string.IsNullOrWhiteSpace(texto))
                return texto;

            ConsolePrinter.ExibirErro("Texto inválido. O campo não pode ficar vazio.");
        }
    }

    private static string LerEntrada(string mensagem)
    {
        Console.Write(mensagem);
        string entrada = Console.ReadLine() ?? string.Empty;

        if (entrada.Trim().Equals(ComandoCancelar, StringComparison.OrdinalIgnoreCase))
            throw new OperacaoCanceladaException();

        return entrada;
    }
}

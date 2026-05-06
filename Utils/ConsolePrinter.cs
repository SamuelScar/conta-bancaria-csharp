using System;
using System.Threading;
using conta_bancaria_csharp.Models;

namespace conta_bancaria_csharp.Utils;

/// <summary>
/// Classe utilitária responsável por centralizar as impressões no console.
/// </summary>
public static class ConsolePrinter
{
    /// <summary>
    /// Exibe o cabeçalho principal do sistema.
    /// </summary>
    public static void ExibirCabecalho()
    {
        Console.WriteLine("*****************************************************");
        Console.WriteLine("                                                     ");
        Console.WriteLine("          BANCO BRODASCO CONTA BANCÁRIA              ");
        Console.WriteLine("                                                     ");
        Console.WriteLine("*****************************************************");
    }

    /// <summary>
    /// Exibe as opções disponíveis no menu principal.
    /// </summary>
    public static void ExibirOpcoesMenu()
    {
        Console.WriteLine("*****************************************************");
        Console.WriteLine("                                                     ");
        Console.WriteLine("            1 - Cadastrar Conta                     ");
        Console.WriteLine("            2 - Listar todas as Contas              ");
        Console.WriteLine("            3 - Procurar Conta por Número           ");
        Console.WriteLine("            4 - Atualizar Dados da Conta            ");
        Console.WriteLine("            5 - Deletar Conta                       ");
        Console.WriteLine("            6 - Sacar                               ");
        Console.WriteLine("            7 - Depositar                           ");
        Console.WriteLine("            8 - Transferir valores entre Contas     ");
        Console.WriteLine("            9 - Sair                                ");
        Console.WriteLine("                                                     ");
        Console.WriteLine("*****************************************************");
    }

    /// <summary>
    /// Exibe a mensagem de despedida ao encerrar o sistema.
    /// </summary>
    public static void ExibirDespedida()
    {
        Console.Clear();
        Console.WriteLine("Obrigado por utilizar o sistema do Brodasco Conta Bancária!");
        Thread.Sleep(2500);
    }

    /// <summary>
    /// Exibe os dados da conta após uma operação realizada com sucesso.
    /// </summary>
    /// <param name="acao">Ação realizada na conta.</param>
    /// <param name="conta">Conta relacionada à operação.</param>
    public static void ExibirContaComSucesso(string acao, Conta conta)
    {
        ExibirDadosConta(
            mensagem: $"Conta {acao} com sucesso!",
            conta: conta
        );
    }

    /// <summary>
    /// Exibe os dados principais de uma conta.
    /// </summary>
    /// <param name="conta">Conta que será exibida.</param>
    public static void ExibirConta(Conta conta)
    {
        ExibirDadosConta(
            mensagem: "Dados da conta:",
            conta: conta
        );
    }

    /// <summary>
    /// Exibe os dados principais de uma conta.
    /// </summary>
    /// <param name="mensagem">Mensagem inicial que será exibida.</param>
    /// <param name="conta">Conta que terá os dados exibidos.</param>
    private static void ExibirDadosConta(string mensagem, Conta conta)
    {
        Console.WriteLine(
            $"\n{mensagem}" +
            $"\nNúmero: {conta.getNumero()}" +
            $"\nAgência: {conta.getAgencia()}" +
            $"\nTitular: {conta.getTitular()}" +
            $"\nSaldo: {conta.getSaldo()}"
        );
    }

    /// <summary>
    /// Exibe uma mensagem de erro no console.
    /// </summary>
    /// <param name="mensagem">Mensagem que será exibida.</param>
    public static void ExibirErro(string mensagem)
    {
        Console.WriteLine(mensagem);
    }

    /// <summary>
    /// Exibe uma mensagem simples no console.
    /// </summary>
    /// <param name="mensagem">Mensagem que será exibida.</param>
    public static void ExibirMensagem(string mensagem)
    {
        Console.WriteLine(mensagem);
    }

    /// <summary>
    /// Exibe um título de seção no console.
    /// </summary>
    /// <param name="titulo">Título que será exibido.</param>
    public static void ExibirTitulo(string titulo)
    {
        Console.WriteLine($"{titulo.ToUpper()}\n");
    }

    /// <summary>
    /// Exibe uma mensagem de sucesso no console.
    /// </summary>
    /// <param name="mensagem">Mensagem que será exibida.</param>
    public static void ExibirSucesso(string mensagem)
    {
        Console.WriteLine(mensagem);
    }

    /// <summary>
    /// Aguarda o usuário pressionar uma tecla para continuar.
    /// </summary>
    public static void AguardarTecla()
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
using System;
using System.Threading;
using conta_bancaria_csharp.Constants;
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
        Console.WriteLine("              BANCO CONTA BANCÁRIA                  ");
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
        Console.WriteLine("Obrigado por utilizar o sistema do Banco Conta Bancária!");
        Thread.Sleep(2500);
    }

    /// <summary>
    /// Exibe a mensagem de sucesso após o cadastro de uma conta.
    /// </summary>
    /// <param name="descricaoTipo">Descrição do tipo da conta cadastrada.</param>
    /// <param name="conta">Conta cadastrada.</param>
    public static void ExibirContaCadastrada(string descricaoTipo, Conta conta)
    {
        Console.WriteLine(
            $"\n{descricaoTipo} cadastrada com sucesso!" +
            $"\nNúmero: {conta.getNumero()}" +
            $"\nAgência: {conta.getAgencia()}" +
            $"\nTitular: {conta.getTitular()}" +
            $"\nSaldo inicial: {conta.getSaldo()}"
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

    public static void aguardarTecla()
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
using System;
using conta_bancaria_csharp.Controllers;
using conta_bancaria_csharp.Utils;
using conta_bancaria_csharp.Constants;
using conta_bancaria_csharp.Models;

namespace conta_bancaria_csharp.Menus;

/// <summary>
/// Classe principal responsável por exibir o menu inicial da aplicação.
/// </summary>
public class Menu
{
    private static ContaController contaController = new ContaController();

    public static void Exibir()
    {
        exibirMenu();
    }

    private static void exibirMenu()
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();

            ConsolePrinter.ExibirCabecalho();
            ConsolePrinter.ExibirOpcoesMenu();

            int opcao = solicitarOpcao();

            Console.WriteLine();

            continuar = executarOpcao(opcao);

            if (continuar) ConsolePrinter.AguardarTecla();
        }

        ConsolePrinter.ExibirDespedida();
    }

    private static int solicitarOpcao()
    {
        Console.Write("Selecione a opção desejada: ");

        string? entrada = Console.ReadLine();

        if (int.TryParse(entrada, out int opcao))
        {
            return opcao;
        }

        return -1;
    }

    private static bool executarOpcao(int opcao)
    {
        switch (opcao)
        {
            case 1: cadastrarConta(); return true;
            case 2: listarTodasContas(); return true;
            case 3: procurarContaPorNumero(); return true;
            case 4: atualizarConta(); return true;
            case 5: Console.WriteLine("Deletar Conta ainda não implementado."); return true;
            case 6: Console.WriteLine("Sacar ainda não implementado."); return true;
            case 7: Console.WriteLine("Depositar ainda não implementado."); return true;
            case 8: Console.WriteLine("Transferir valores entre Contas ainda não implementado."); return true;
            case 9: ConsolePrinter.ExibirMensagem("Sistema finalizado."); return false;
            default: ConsolePrinter.ExibirErro("Opção inválida."); return true;
        }
    }

    private static void cadastrarConta()
    {
        ConsolePrinter.ExibirTitulo("Cadastro de Conta");

        int tipo = ConsoleUtils.LerInteiro("Tipo da conta (1 - Corrente | 2 - Poupança): ");
        if (tipo is < 1 or > 2) { ConsolePrinter.ExibirErro("Tipo de conta inválido."); return; }

        string titular = ConsoleUtils.LerTexto("Nome do titular: ");

        Conta conta;

        switch (tipo)
        {
            case TipoConta.Corrente:
                float limite = ConsoleUtils.LerFloat("Limite da conta corrente: ");
                conta = new ContaCorrente(tipo, titular, limite);
                break;

            case TipoConta.Poupanca:
                int aniversario = ConsoleUtils.LerInteiro("Aniversário da conta poupança: ");
                conta = new ContaPoupanca(tipo, titular, aniversario);
                break;

            default:
                ConsolePrinter.ExibirErro("Tipo de conta inválido.");
                return;
        }

        if (contaController.cadastrar(conta))
            ConsolePrinter.ExibirContaComSucesso("cadastrada", conta);
        else
            ConsolePrinter.ExibirErro("Não foi possível cadastrar a conta.");
    }

    private static void atualizarConta()
    {
        ConsolePrinter.ExibirTitulo("Atualizar Dados da Conta");
        int numero = ConsoleUtils.LerInteiro("Número da conta: ");

        Conta? conta = contaController.procurarPorNumero(numero);
        if (conta == null) { ConsolePrinter.ExibirErro("Conta não encontrada."); return; }

        Console.WriteLine("\nO que deseja atualizar?");
        Console.WriteLine("1 - Titular");

        if (conta.getTipo() == TipoConta.Corrente)
            Console.WriteLine("2 - Limite");

        if (conta.getTipo() == TipoConta.Poupanca)
            Console.WriteLine("2 - Aniversário");

        int opcao = ConsoleUtils.LerInteiro("\nSelecione a opção desejada: ");

        switch (conta.getTipo())
        {
            case TipoConta.Corrente:
                ContaCorrente contaCorrente = (ContaCorrente)conta;

                switch (opcao)
                {
                    case 1: contaCorrente.setTitular(ConsoleUtils.LerTexto("Novo nome do titular: ")); break;
                    case 2: contaCorrente.setLimite(ConsoleUtils.LerFloat("Novo limite da conta corrente: ")); break;
                    default: ConsolePrinter.ExibirErro("Opção inválida."); return;
                }

                break;

            case TipoConta.Poupanca:
                ContaPoupanca contaPoupanca = (ContaPoupanca)conta;

                switch (opcao)
                {
                    case 1: contaPoupanca.setTitular(ConsoleUtils.LerTexto("Novo nome do titular: ")); break;
                    case 2: contaPoupanca.setAniversario(ConsoleUtils.LerInteiro("Novo aniversário da conta poupança: ")); break;
                    default: ConsolePrinter.ExibirErro("Opção inválida."); return;
                }

                break;

            default:
                ConsolePrinter.ExibirErro("Tipo de conta inválido.");
                return;
        }

        if (contaController.atualizar(conta))
            ConsolePrinter.ExibirContaComSucesso("atualizada", conta);
        else
            ConsolePrinter.ExibirErro("Não foi possível atualizar a conta.");
    }

    /// <summary>
    /// Procura uma conta pelo número informado pelo usuário.
    /// </summary>
    private static void procurarContaPorNumero()
    {
        ConsolePrinter.ExibirTitulo("Procurar Conta por Número");

        int numero = ConsoleUtils.LerInteiro("Número da conta: ");

        Conta? conta = contaController.procurarPorNumero(numero);

        if (conta == null)
        {
            ConsolePrinter.ExibirErro("Conta não encontrada.");
            return;
        }

        ConsolePrinter.ExibirConta(conta);
    }

    /// <summary>
    /// Lista todas as contas cadastradas no sistema.
    /// </summary>
    private static void listarTodasContas()
    {
        ConsolePrinter.ExibirTitulo("Listar Todas as Contas");
        contaController.listarTodas();
    }

}
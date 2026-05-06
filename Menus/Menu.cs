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

            if (continuar) ConsolePrinter.aguardarTecla();
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
            case 1:
                cadastrarConta();
                return true;

            case 2:
                return true;

            case 3:
                Console.WriteLine("Procurar Conta por Número ainda não implementado.");
                return true;

            case 4:
                atualizarConta();
                return true;

            case 5:
                Console.WriteLine("Deletar Conta ainda não implementado.");
                return true;

            case 6:
                Console.WriteLine("Sacar ainda não implementado.");
                return true;

            case 7:
                Console.WriteLine("Depositar ainda não implementado.");
                return true;

            case 8:
                Console.WriteLine("Transferir valores entre Contas ainda não implementado.");
                return true;

            case 9:
                ConsolePrinter.ExibirMensagem("Sistema finalizado.");
                return false;

            default:
                ConsolePrinter.ExibirErro("Opção inválida.");
                return true;
        }
    }

    private static void cadastrarConta()
    {
        Console.WriteLine("CADASTRO DE CONTA\n");

        int tipo = ConsoleUtils.LerInteiro("Tipo da conta (1 - Corrente | 2 - Poupança): ");
        if (tipo is < 1 or > 2) { ConsolePrinter.ExibirErro("Tipo de conta inválido."); return; }
        string titular = ConsoleUtils.LerTexto("Nome do titular: ");

        switch (tipo)
        {
            case TipoConta.Corrente:
                float limite = ConsoleUtils.LerFloat("Limite da conta corrente: ");
                ContaCorrente contaCorrente = new ContaCorrente(tipo, titular, limite);

                if( contaController.cadastrar(contaCorrente) )
                    ConsolePrinter.ExibirContaCadastrada(TipoConta.DescricaoCorrente, contaCorrente);
                else
                    ConsolePrinter.ExibirErro("Não foi possível cadastrar a conta.");

                break;

            case TipoConta.Poupanca:
                int aniversario = ConsoleUtils.LerInteiro("Aniversário da conta poupança: ");
                ContaPoupanca contaPoupanca = new ContaPoupanca(tipo, titular, aniversario);

                if( contaController.cadastrar(contaPoupanca) )
                    ConsolePrinter.ExibirContaCadastrada(TipoConta.DescricaoPoupanca, contaPoupanca);
                else
                    ConsolePrinter.ExibirErro("Não foi possível cadastrar a conta.");

                break;

            default:
                ConsolePrinter.ExibirErro("Tipo de conta inválido.");
                break;
        }
    }

    private static void atualizarConta()
    {
        Console.WriteLine("ATUALIZAR DADOS DA CONTA\n");

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
            ConsolePrinter.ExibirMensagem("Conta atualizada com sucesso.");
        else
            ConsolePrinter.ExibirErro("Não foi possível atualizar a conta.");
    }

}
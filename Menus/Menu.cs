using System;
using conta_bancaria_csharp.Controllers;
using conta_bancaria_csharp.Utils;
using conta_bancaria_csharp.Constants;
using conta_bancaria_csharp.Models;
using conta_bancaria_csharp.Exceptions;

namespace conta_bancaria_csharp.Menus;

/// <summary>
/// Classe principal responsável por exibir o menu inicial da aplicação.
/// </summary>
public class Menu
{
    private static readonly ContaController contaController = new ContaController();

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
            case 1: executarOperacaoEmLoop("Cadastro de Conta", "Cadastrar nova conta", cadastrarConta); return true;
            case 2: executarOperacaoEmLoop("Listar Todas as Contas", "Listar contas cadastradas", listarTodasContas); return true;
            case 3: executarOperacaoEmLoop("Procurar Conta por Número", "Procurar conta", procurarContaPorNumero); return true;
            case 4: executarOperacaoEmLoop("Atualizar Dados da Conta", "Atualizar conta", atualizarConta); return true;
            case 5: executarOperacaoEmLoop("Deletar Conta", "Deletar conta", deletarConta); return true;
            case 6: executarOperacaoEmLoop("Sacar", "Realizar saque", sacar); return true;
            case 7: executarOperacaoEmLoop("Depositar", "Realizar depósito", depositar); return true;
            case 8: executarOperacaoEmLoop("Transferir Valores entre Contas", "Realizar transferência", transferir); return true;
            case 9: ConsolePrinter.ExibirSucesso("Sistema finalizado."); return false;
            default:
                ConsolePrinter.ExibirErro("Opção inválida.");
                ConsolePrinter.AguardarTecla();
                return true;
        }
    }

    /// <summary>
    /// Exibe um submenu em loop para executar uma operação até o usuário voltar ao menu principal.
    /// </summary>
    /// <param name="titulo">Título exibido no submenu.</param>
    /// <param name="descricaoOperacao">Descrição da opção que executa a operação.</param>
    /// <param name="operacao">Operação executada quando a opção principal é selecionada.</param>
    private static void executarOperacaoEmLoop(string titulo, string descricaoOperacao, Action operacao)
    {
        bool voltarAoMenuPrincipal = false;

        while (!voltarAoMenuPrincipal)
        {
            Console.Clear();

            ConsolePrinter.ExibirTela(titulo);
            Console.WriteLine($"1 - {descricaoOperacao}");
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.WriteLine();

            int opcao = solicitarOpcao();

            Console.WriteLine();

            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    try
                    {
                        operacao();
                    }
                    catch (OperacaoCanceladaException)
                    {
                        ConsolePrinter.ExibirMensagem("Operação cancelada. Voltando ao submenu.");
                    }

                    ConsolePrinter.AguardarTecla();
                    break;

                case 0:
                    voltarAoMenuPrincipal = true;
                    break;

                default:
                    ConsolePrinter.ExibirErro("Opção inválida.");
                    ConsolePrinter.AguardarTecla();
                    break;
            }
        }
    }

    private static void cadastrarConta()
    {
        exibirTituloOperacaoCancelavel("Cadastro de Conta");

        int tipo = ConsoleUtils.LerInteiro("Tipo da conta (1 - Corrente | 2 - Poupança): ");
        if (tipo is < 1 or > 2) { ConsolePrinter.ExibirErro("Tipo de conta inválido."); return; }

        string titular = ConsoleUtils.LerTexto("Nome do titular: ");

        Conta conta;

        switch (tipo)
        {
            case TipoConta.Corrente:
                decimal limite = lerLimiteContaCorrente("Limite da conta corrente (use ponto. Ex: 1000.50): ");
                conta = new ContaCorrente(tipo, titular, limite);
                break;

            case TipoConta.Poupanca:
                int aniversario = lerAniversarioContaPoupanca("Aniversário da conta poupança: ");
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
        exibirTituloOperacaoCancelavel("Atualizar Dados da Conta");
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
                    case 2: contaCorrente.setLimite(lerLimiteContaCorrente("Novo limite da conta corrente (use ponto. Ex: 1000.50): ")); break;
                    default: ConsolePrinter.ExibirErro("Opção inválida."); return;
                }

                break;

            case TipoConta.Poupanca:
                ContaPoupanca contaPoupanca = (ContaPoupanca)conta;

                switch (opcao)
                {
                    case 1: contaPoupanca.setTitular(ConsoleUtils.LerTexto("Novo nome do titular: ")); break;
                    case 2: contaPoupanca.setAniversario(lerAniversarioContaPoupanca("Novo aniversário da conta poupança: ")); break;
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
        exibirTituloOperacaoCancelavel("Procurar Conta por Número");

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
        ConsolePrinter.ExibirTela("Listar Todas as Contas");
        contaController.listarTodas();
    }

    /// <summary>
    /// Deleta uma conta pelo número informado pelo usuário.
    /// </summary>
    private static void deletarConta()
    {
        exibirTituloOperacaoCancelavel("Deletar Conta");

        int numero = ConsoleUtils.LerInteiro("Número da conta: ");

        Conta? conta = contaController.procurarPorNumero(numero);

        if (conta == null)
        {
            ConsolePrinter.ExibirErro("Conta não encontrada.");
            return;
        }

        if (contaController.deletar(numero))
            ConsolePrinter.ExibirContaComSucesso("deletada", conta);
        else
            ConsolePrinter.ExibirErro("Não foi possível deletar a conta.");
    }

    /// <summary>
    /// Realiza o depósito em uma conta informada pelo usuário.
    /// </summary>
    private static void depositar()
    {
        exibirTituloOperacaoCancelavel("Depositar");

        int numero = ConsoleUtils.LerInteiro("Número da conta: ");
        decimal valor = ConsoleUtils.LerDecimal("Valor do depósito (use ponto. Ex: 100.50): ");

        if (!contaController.depositar(numero, valor))
            return;

        Conta? conta = contaController.procurarPorNumero(numero);

        if (conta == null)
        {
            ConsolePrinter.ExibirErro("Depósito realizado, mas não foi possível carregar os dados atualizados da conta.");
            return;
        }

        ConsolePrinter.ExibirSucesso("Depósito realizado com sucesso.");
        ConsolePrinter.ExibirConta(conta);
    }

    /// <summary>
    /// Realiza o saque em uma conta informada pelo usuário.
    /// </summary>
    private static void sacar()
    {
        exibirTituloOperacaoCancelavel("Sacar");

        int numero = ConsoleUtils.LerInteiro("Número da conta: ");
        decimal valor = ConsoleUtils.LerDecimal("Valor do saque (use ponto. Ex: 100.50): ");

        if (!contaController.sacar(numero, valor))
            return;

        Conta? conta = contaController.procurarPorNumero(numero);

        if (conta == null)
        {
            ConsolePrinter.ExibirErro("Saque realizado, mas não foi possível carregar os dados atualizados da conta.");
            return;
        }

        ConsolePrinter.ExibirSucesso("Saque realizado com sucesso.");
        ConsolePrinter.ExibirConta(conta);
    }

    /// <summary>
    /// Realiza a transferência de valores entre duas contas.
    /// </summary>
    private static void transferir()
    {
        exibirTituloOperacaoCancelavel("Transferir Valores entre Contas");

        int numeroOrigem = ConsoleUtils.LerInteiro("Número da conta de origem: ");
        int numeroDestino = ConsoleUtils.LerInteiro("Número da conta de destino: ");
        decimal valor = ConsoleUtils.LerDecimal("Valor da transferência (use ponto. Ex: 100.50): ");

        if (!contaController.transferir(numeroOrigem, numeroDestino, valor))
            return;

        Conta? contaOrigem = contaController.procurarPorNumero(numeroOrigem);

        if (contaOrigem == null)
        {
            ConsolePrinter.ExibirErro("Transferência realizada, mas não foi possível carregar os dados atualizados da conta de origem.");
            return;
        }

        ConsolePrinter.ExibirSucesso("Transferência realizada com sucesso.");
        ConsolePrinter.ExibirMensagem("\nConta de origem:");
        ConsolePrinter.ExibirConta(contaOrigem);
    }

    /// <summary>
    /// Exibe o título da operação e a instrução única para cancelamento.
    /// </summary>
    /// <param name="titulo">Título da operação atual.</param>
    private static void exibirTituloOperacaoCancelavel(string titulo)
    {
        ConsolePrinter.ExibirTela(titulo);
        ConsolePrinter.ExibirMensagem($"Digite {ConsoleUtils.ComandoCancelar} em qualquer campo para cancelar a ação atual.");
        Console.WriteLine();
    }

    /// <summary>
    /// Lê e valida o limite de uma conta corrente.
    /// </summary>
    /// <param name="mensagem">Mensagem exibida ao usuário.</param>
    /// <returns>Retorna um limite maior ou igual a zero.</returns>
    private static decimal lerLimiteContaCorrente(string mensagem)
    {
        while (true)
        {
            decimal limite = ConsoleUtils.LerDecimal(mensagem);

            if (limite >= 0)
                return limite;

            ConsolePrinter.ExibirErro("O limite da conta corrente não pode ser negativo.");
        }
    }

    /// <summary>
    /// Lê e valida o dia de aniversário de uma conta poupança.
    /// </summary>
    /// <param name="mensagem">Mensagem exibida ao usuário.</param>
    /// <returns>Retorna um dia entre 1 e 31.</returns>
    private static int lerAniversarioContaPoupanca(string mensagem)
    {
        while (true)
        {
            int aniversario = ConsoleUtils.LerInteiro(mensagem);

            if (aniversario is >= 1 and <= 31)
                return aniversario;

            ConsolePrinter.ExibirErro("O aniversário da conta poupança deve estar entre 1 e 31.");
        }
    }

}

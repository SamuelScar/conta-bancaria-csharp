using System;
using System.Collections.Generic;
using conta_bancaria_csharp.Data;
using conta_bancaria_csharp.Models;
using conta_bancaria_csharp.Repositories;
using conta_bancaria_csharp.Constants;
using conta_bancaria_csharp.Utils;

namespace conta_bancaria_csharp.Controllers;

/// <summary>
/// Classe responsável por implementar as operações definidas na interface ContaRepository.
/// </summary>
public class ContaController : ContaRepository
{
    private readonly ContaData contaData = new ContaData();

    /// <summary>
    /// Procura uma conta pelo número.
    /// </summary>
    /// <param name="numero">Número da conta pesquisada.</param>
    /// <returns>Retorna a conta encontrada ou null caso não exista.</returns>
    public Conta? procurarPorNumero(int numero)
    {
        if (numero <= 0)
        {
            ConsolePrinter.ExibirErro("Número da conta inválido.");
            return null;
        }

        try
        {
            return contaData.procurarPorNumero(numero);
        }
        catch (Exception erro)
        {
            ConsolePrinter.ExibirErro($"Erro ao procurar conta: {erro.Message}");
            return null;
        }
    }

    /// <summary>
    /// Lista todas as contas cadastradas.
    /// </summary>
    public void listarTodas()
    {
        try
        {
            List<Conta> contas = contaData.listarTodas();

            if (contas.Count == 0)
            {
                ConsolePrinter.ExibirErro("Nenhuma conta cadastrada.");
                return;
            }

            foreach (Conta conta in contas)
            {
                ConsolePrinter.ExibirConta(conta);
                Console.WriteLine("\n------------------------");
            }
        }
        catch (Exception erro)
        {
            ConsolePrinter.ExibirErro($"Erro ao listar contas: {erro.Message}");
        }
    }

    /// <summary>
    /// Cadastra uma nova conta.
    /// </summary>
    /// <param name="conta">Conta que será cadastrada.</param>
    /// <returns>Retorna true se o cadastro for realizado com sucesso.</returns>
    public bool cadastrar(Conta conta)
    {
        if (conta == null)
        {
            ConsolePrinter.ExibirErro("Conta inválida.");
            return false;
        }

        try
        {
            return contaData.cadastrar(conta);
        }
        catch (Exception erro)
        {
            ConsolePrinter.ExibirErro($"Erro ao cadastrar conta: {erro.Message}");
            return false;
        }
    }

    /// <summary>
    /// Atualiza os dados de uma conta.
    /// </summary>
    /// <param name="conta">Conta com os dados atualizados.</param>
    /// <returns>Retorna true se a atualização for realizada com sucesso.</returns>
    public bool atualizar(Conta conta)
    {
        if (conta == null)
        {
            ConsolePrinter.ExibirErro("Conta inválida.");
            return false;
        }

        try
        {
            return contaData.atualizar(conta);
        }
        catch (Exception erro)
        {
            ConsolePrinter.ExibirErro($"Erro ao atualizar conta: {erro.Message}");
            return false;
        }
    }

    /// <summary>
    /// Deleta uma conta pelo número.
    /// </summary>
    /// <param name="numero">Número da conta que será deletada.</param>
    /// <returns>Retorna true se a conta for deletada com sucesso.</returns>
    public bool deletar(int numero)
    {
        if (numero <= 0)
        {
            ConsolePrinter.ExibirErro("Número da conta inválido.");
            return false;
        }

        try
        {
            return contaData.deletar(numero);
        }
        catch (Exception erro)
        {
            ConsolePrinter.ExibirErro($"Erro ao deletar conta: {erro.Message}");
            return false;
        }
    }

    /// <summary>
    /// Realiza um depósito em uma conta.
    /// </summary>
    /// <param name="numero">Número da conta que receberá o depósito.</param>
    /// <param name="valor">Valor que será depositado.</param>
    /// <returns>Retorna true se o depósito for realizado com sucesso.</returns>
    public bool depositar(int numero, float valor)
    {
        if (numero <= 0)
        {
            ConsolePrinter.ExibirErro("Número da conta inválido.");
            return false;
        }

        if (valor <= 0)
        {
            ConsolePrinter.ExibirErro("Valor de depósito inválido.");
            return false;
        }

        try
        {
            Conta? conta = contaData.procurarPorNumero(numero);

            if (conta == null)
            {
                ConsolePrinter.ExibirErro("Conta não encontrada.");
                return false;
            }

            if (!conta.depositar(valor))
            {
                ConsolePrinter.ExibirErro("Não foi possível realizar o depósito.");
                return false;
            }

            return contaData.atualizarSaldos(new List<Conta> { conta });
        }
        catch (Exception erro)
        {
            ConsolePrinter.ExibirErro($"Erro ao depositar: {erro.Message}");
            return false;
        }
    }

    /// <summary>
    /// Realiza um saque em uma conta.
    /// </summary>
    /// <param name="numero">Número da conta que realizará o saque.</param>
    /// <param name="valor">Valor que será sacado.</param>
    /// <returns>Retorna true se o saque for realizado com sucesso.</returns>
    public bool sacar(int numero, float valor)
    {
        if (numero <= 0)
        {
            ConsolePrinter.ExibirErro("Número da conta inválido.");
            return false;
        }

        if (valor <= 0)
        {
            ConsolePrinter.ExibirErro("Valor de saque inválido.");
            return false;
        }

        try
        {
            Conta? conta = contaData.procurarPorNumero(numero);

            if (conta == null)
            {
                ConsolePrinter.ExibirErro("Conta não encontrada.");
                return false;
            }

            if (!conta.sacar(valor))
            {
                ConsolePrinter.ExibirErro("Saldo insuficiente para realizar o saque.");
                return false;
            }

            return contaData.atualizarSaldos(new List<Conta> { conta });
        }
        catch (Exception erro)
        {
            ConsolePrinter.ExibirErro($"Erro ao sacar: {erro.Message}");
            return false;
        }
    }

    /// <summary>
    /// Transfere um valor entre duas contas.
    /// </summary>
    /// <param name="numeroOrigem">Número da conta de origem.</param>
    /// <param name="numeroDestino">Número da conta de destino.</param>
    /// <param name="valor">Valor que será transferido.</param>
    /// <returns>Retorna true se a transferência for realizada com sucesso.</returns>
    public bool transferir(int numeroOrigem, int numeroDestino, float valor)
    {
        if (numeroOrigem <= 0 || numeroDestino <= 0)
        {
            ConsolePrinter.ExibirErro("Número da conta inválido.");
            return false;
        }

        if (numeroOrigem == numeroDestino)
        {
            ConsolePrinter.ExibirErro("A conta de origem e destino não podem ser iguais.");
            return false;
        }

        if (valor <= 0)
        {
            ConsolePrinter.ExibirErro("Valor de transferência inválido.");
            return false;
        }

        try
        {
            Conta? contaOrigem = contaData.procurarPorNumero(numeroOrigem);
            Conta? contaDestino = contaData.procurarPorNumero(numeroDestino);

            if (contaOrigem == null)
            {
                ConsolePrinter.ExibirErro("Conta de origem não encontrada.");
                return false;
            }

            if (contaDestino == null)
            {
                ConsolePrinter.ExibirErro("Conta de destino não encontrada.");
                return false;
            }

            if (!contaOrigem.sacar(valor))
            {
                ConsolePrinter.ExibirErro("Saldo insuficiente para realizar a transferência.");
                return false;
            }

            if (!contaDestino.depositar(valor))
            {
                ConsolePrinter.ExibirErro("Não foi possível creditar o valor na conta de destino.");
                return false;
            }

            return contaData.atualizarSaldos(new List<Conta> { contaOrigem, contaDestino });
        }
        catch (Exception erro)
        {
            ConsolePrinter.ExibirErro($"Erro ao transferir: {erro.Message}");
            return false;
        }
    }
}
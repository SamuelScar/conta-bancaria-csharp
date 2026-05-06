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
            Console.WriteLine("Conta inválida.");
            return false;
        }

        try
        {
            return contaData.cadastrar(conta);
        }
        catch (Exception erro)
        {
            Console.WriteLine($"Erro ao cadastrar conta: {erro.Message}");
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
            Console.WriteLine("Conta inválida.");
            return false;
        }

        try
        {
            return contaData.atualizar(conta);
        }
        catch (Exception erro)
        {
            Console.WriteLine($"Erro ao atualizar conta: {erro.Message}");
            return false;
        }
    }

    public void deletar(int numero) { }

    public void sacar(int numero, float valor) { }

    public void depositar(int numero, float valor) { }

    public void transferir(int numeroOrigem, int numeroDestino, float valor) { }
}
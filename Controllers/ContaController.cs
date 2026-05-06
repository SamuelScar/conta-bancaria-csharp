using System;
using conta_bancaria_csharp.Data;
using conta_bancaria_csharp.Models;
using conta_bancaria_csharp.Repositories;

namespace conta_bancaria_csharp.Controllers;

/// <summary>
/// Classe responsável por implementar as operações definidas na interface ContaRepository.
/// </summary>
public class ContaController : ContaRepository
{
    private readonly ContaData contaData = new ContaData();

    public void procurarPorNumero(int numero) { }

    public void listarTodas() { }

    /// <summary>
    /// Cadastra uma nova conta corrente.
    /// </summary>
    /// <param name="contaCorrente">Conta corrente que será cadastrada.</param>
    /// <returns>Retorna true se o cadastro for realizado com sucesso.</returns>
    public bool cadastrar(ContaCorrente contaCorrente)
    {
        if (contaCorrente == null)
        {
            Console.WriteLine("Conta corrente inválida.");
            return false;
        }

        try
        {
            return contaData.cadastrar(contaCorrente);
        }
        catch (Exception erro)
        {
            Console.WriteLine($"Erro ao cadastrar conta corrente: {erro.Message}");
            return false;
        }
    }

    /// <summary>
    /// Cadastra uma nova conta poupança.
    /// </summary>
    /// <param name="contaPoupanca">Conta poupança que será cadastrada.</param>
    /// <returns>Retorna true se o cadastro for realizado com sucesso.</returns>
    public bool cadastrar(ContaPoupanca contaPoupanca)
    {
        if (contaPoupanca == null)
        {
            Console.WriteLine("Conta poupança inválida.");
            return false;
        }

        try
        {
            return contaData.cadastrar(contaPoupanca);
        }
        catch (Exception erro)
        {
            Console.WriteLine($"Erro ao cadastrar conta poupança: {erro.Message}");
            return false;
        }
    }

    public void atualizar(Conta conta) { }

    public void deletar(int numero) { }

    public void sacar(int numero, float valor) { }

    public void depositar(int numero, float valor) { }

    public void transferir(int numeroOrigem, int numeroDestino, float valor) { }
}
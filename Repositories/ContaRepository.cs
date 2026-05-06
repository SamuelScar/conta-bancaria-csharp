using conta_bancaria_csharp.Models;

namespace conta_bancaria_csharp.Repositories;

/// <summary>
/// Define o contrato das operações disponíveis para gerenciamento de contas.
/// </summary>
public interface ContaRepository
{
    void procurarPorNumero(int numero);
    void listarTodas();

    /// <summary>
    /// Cadastra uma nova conta corrente.
    /// </summary>
    /// <param name="contaCorrente">Conta corrente que será cadastrada.</param>
    bool cadastrar(ContaCorrente contaCorrente);

    /// <summary>
    /// Cadastra uma nova conta poupança.
    /// </summary>
    /// <param name="contaPoupanca">Conta poupança que será cadastrada.</param>
    bool cadastrar(ContaPoupanca contaPoupanca);

    void atualizar(Conta conta);
    void deletar(int numero);
    void sacar(int numero, float valor);
    void depositar(int numero, float valor);
    void transferir(int numeroOrigem, int numeroDestino, float valor);
}
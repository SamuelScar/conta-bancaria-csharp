using conta_bancaria_csharp.Models;

namespace conta_bancaria_csharp.Repositories;

/// <summary>
/// Define o contrato das operações disponíveis para gerenciamento de contas.
/// </summary>
public interface ContaRepository
{
    Conta? procurarPorNumero(int numero);
    void listarTodas();

    bool cadastrar(Conta conta);
    bool atualizar(Conta conta);

    void deletar(int numero);
    void sacar(int numero, float valor);
    void depositar(int numero, float valor);
    void transferir(int numeroOrigem, int numeroDestino, float valor);
}
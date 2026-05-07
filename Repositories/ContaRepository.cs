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
    bool deletar(int numero);
    bool sacar(int numero, decimal valor);
    bool depositar(int numero, decimal valor);
    bool transferir(int numeroOrigem, int numeroDestino, decimal valor);
}
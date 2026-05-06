using conta_bancaria_csharp.Models;
using Npgsql;

namespace conta_bancaria_csharp.Data;

/// <summary>
/// Classe responsável pela persistência de contas no banco de dados.
/// </summary>
public class ContaData
{
    private readonly DatabaseExecutor databaseExecutor = new DatabaseExecutor();

    /// <summary>
    /// Cadastra uma conta corrente no banco de dados.
    /// </summary>
    /// <param name="contaCorrente">Conta corrente que será cadastrada.</param>
    /// <returns>Retorna true se o cadastro for realizado com sucesso.</returns>
    public bool cadastrar(ContaCorrente contaCorrente)
    {
        return cadastrarConta(contaCorrente, contaCorrente.getLimite(), null);
    }

    /// <summary>
    /// Cadastra uma conta poupança no banco de dados.
    /// </summary>
    /// <param name="contaPoupanca">Conta poupança que será cadastrada.</param>
    /// <returns>Retorna true se o cadastro for realizado com sucesso.</returns>
    public bool cadastrar(ContaPoupanca contaPoupanca)
    {
        return cadastrarConta(contaPoupanca, null, contaPoupanca.getAniversario());
    }

    /// <summary>
    /// Cadastra os dados comuns de uma conta na tabela contas.
    /// </summary>
    /// <param name="conta">Conta que será cadastrada.</param>
    /// <param name="limite">Limite da conta corrente, quando existir.</param>
    /// <param name="aniversario">Aniversário da conta poupança, quando existir.</param>
    /// <returns>Retorna true se o cadastro for realizado com sucesso.</returns>
    private bool cadastrarConta(Conta conta, float? limite, int? aniversario)
    {
        string sql = @"
            INSERT INTO contas
                (tipo, titular, saldo, limite, aniversario)
            VALUES
                (@tipo, @titular, @saldo, @limite, @aniversario)
            RETURNING numero, agencia;
        ";

        Dictionary<string, object?> parametros = new Dictionary<string, object?>
        {
            { "@tipo", conta.getTipo() },
            { "@titular", conta.getTitular() },
            { "@saldo", conta.getSaldo() },
            { "@limite", limite },
            { "@aniversario", aniversario }
        };

        return databaseExecutor.executarComandoComRetorno(sql, parametros, reader =>
        {
            conta.setNumero(reader.GetInt32(reader.GetOrdinal("numero")));
            conta.setAgencia(reader.GetInt32(reader.GetOrdinal("agencia")));
        });
    }
}
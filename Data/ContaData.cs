using Npgsql;
using conta_bancaria_csharp.Models;
using conta_bancaria_csharp.Constants;

namespace conta_bancaria_csharp.Data;

/// <summary>
/// Classe responsável pela persistência de contas no banco de dados.
/// </summary>
public class ContaData
{
    private readonly DatabaseExecutor databaseExecutor = new DatabaseExecutor();

    /// <summary>
    /// Cadastra uma conta no banco de dados.
    /// </summary>
    /// <param name="conta">Conta que será cadastrada.</param>
    /// <returns>Retorna true se o cadastro for realizado com sucesso.</returns>
    public bool cadastrar(Conta conta)
    {
        if (conta == null)
            return false;

        float? limite = null;
        int? aniversario = null;

        if (conta.getTipo() == TipoConta.Corrente && conta is ContaCorrente contaCorrente)
            limite = contaCorrente.getLimite();
        else if (conta.getTipo() == TipoConta.Poupanca && conta is ContaPoupanca contaPoupanca)
            aniversario = contaPoupanca.getAniversario();
        else
            return false;

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

    /// <summary>
    /// Atualiza os dados de uma conta no banco de dados.
    /// </summary>
    /// <param name="conta">Conta que será atualizada.</param>
    /// <returns>Retorna true se a atualização for realizada com sucesso.</returns>
    public bool atualizar(Conta conta)
    {
        if (conta == null)
            return false;

        float? limite = null;
        int? aniversario = null;

        if (conta.getTipo() == TipoConta.Corrente && conta is ContaCorrente contaCorrente)
            limite = contaCorrente.getLimite();
        else if (conta.getTipo() == TipoConta.Poupanca && conta is ContaPoupanca contaPoupanca)
            aniversario = contaPoupanca.getAniversario();
        else
            return false;

        string sql = @"
        UPDATE contas
        SET
            titular = @titular,
            limite = @limite,
            aniversario = @aniversario
        WHERE numero = @numero;
    ";

        Dictionary<string, object?> parametros = new Dictionary<string, object?>
    {
        { "@numero", conta.getNumero() },
        { "@titular", conta.getTitular() },
        { "@limite", limite },
        { "@aniversario", aniversario }
    };

        return databaseExecutor.executarComando(sql, parametros);
    }


    /// <summary>
    /// Procura uma conta pelo número no banco de dados.
    /// </summary>
    /// <param name="numero">Número da conta que será pesquisada.</param>
    /// <returns>Retorna a conta encontrada ou null caso não exista.</returns>
    public Conta? procurarPorNumero(int numero)
    {
        Conta? contaEncontrada = null;

        string sql = @"
        SELECT
            numero,
            agencia,
            tipo,
            titular,
            saldo,
            limite,
            aniversario
        FROM contas
        WHERE numero = @numero;
    ";

        Dictionary<string, object?> parametros = new Dictionary<string, object?>
    {
        { "@numero", numero }
    };

        databaseExecutor.executarComandoComRetorno(sql, parametros, reader =>
        {
            int tipo = reader.GetInt32(reader.GetOrdinal("tipo"));
            string titular = reader.GetString(reader.GetOrdinal("titular"));

            if (tipo == TipoConta.Corrente)
            {
                float limite = reader.GetFloat(reader.GetOrdinal("limite"));
                ContaCorrente contaCorrente = new ContaCorrente(tipo, titular, limite);

                preencherDadosConta(contaCorrente, reader);
                contaEncontrada = contaCorrente;
            }

            if (tipo == TipoConta.Poupanca)
            {
                int aniversario = reader.GetInt32(reader.GetOrdinal("aniversario"));
                ContaPoupanca contaPoupanca = new ContaPoupanca(tipo, titular, aniversario);

                preencherDadosConta(contaPoupanca, reader);
                contaEncontrada = contaPoupanca;
            }
        });

        return contaEncontrada;
    }

    /// <summary>
    /// Preenche os dados comuns de uma conta recuperada do banco.
    /// </summary>
    /// <param name="conta">Conta que receberá os dados.</param>
    /// <param name="reader">Leitor com os dados retornados do banco.</param>
    private void preencherDadosConta(Conta conta, NpgsqlDataReader reader)
    {
        conta.setNumero(reader.GetInt32(reader.GetOrdinal("numero")));
        conta.setAgencia(reader.GetInt32(reader.GetOrdinal("agencia")));
        conta.setSaldo(reader.GetFloat(reader.GetOrdinal("saldo")));
    }
}
using Npgsql;

namespace conta_bancaria_csharp.Data;

/// <summary>
/// Classe responsável por executar comandos SQL genéricos no banco de dados.
/// </summary>
public class DatabaseExecutor
{
    private readonly DatabaseConnection databaseConnection = new DatabaseConnection();

    /// <summary>
    /// Executa comandos SQL de escrita, como INSERT, UPDATE e DELETE.
    /// </summary>
    /// <param name="sql">Comando SQL que será executado.</param>
    /// <param name="parametros">Parâmetros utilizados no comando SQL.</param>
    /// <returns>Retorna true se alguma linha for afetada.</returns>
    public bool executarComando(string sql, Dictionary<string, object?> parametros)
    {
        using NpgsqlConnection conexao = databaseConnection.criarConexao();

        conexao.Open();

        using NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);

        adicionarParametros(comando, parametros);

        int linhasAfetadas = comando.ExecuteNonQuery();

        return linhasAfetadas > 0;
    }

    /// <summary>
    /// Executa um comando SQL que retorna dados, como INSERT com RETURNING.
    /// </summary>
    /// <param name="sql">Comando SQL que será executado.</param>
    /// <param name="parametros">Parâmetros utilizados no comando SQL.</param>
    /// <param name="processarRetorno">Função responsável por processar os dados retornados.</param>
    /// <returns>Retorna true se o comando retornar algum registro.</returns>
    public bool executarComandoComRetorno(
        string sql,
        Dictionary<string, object?> parametros,
        Action<NpgsqlDataReader> processarRetorno
    )
    {
        using NpgsqlConnection conexao = databaseConnection.criarConexao();

        conexao.Open();

        using NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);

        adicionarParametros(comando, parametros);

        using NpgsqlDataReader reader = comando.ExecuteReader();

        if (!reader.Read())
            return false;

        processarRetorno(reader);

        return true;
    }

    /// <summary>
    /// Executa uma consulta SQL que pode retornar vários registros.
    /// </summary>
    /// <param name="sql">Comando SQL que será executado.</param>
    /// <param name="parametros">Parâmetros utilizados no comando SQL.</param>
    /// <param name="processarRegistro">Função responsável por processar cada registro retornado.</param>
    public void executarConsulta(
        string sql,
        Dictionary<string, object?> parametros,
        Action<NpgsqlDataReader> processarRegistro
    )
    {
        using NpgsqlConnection conexao = databaseConnection.criarConexao();

        conexao.Open();

        using NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);

        adicionarParametros(comando, parametros);

        using NpgsqlDataReader reader = comando.ExecuteReader();

        while (reader.Read())
        {
            processarRegistro(reader);
        }
    }

    /// <summary>
    /// Executa vários comandos SQL dentro de uma mesma transação.
    /// </summary>
    /// <param name="comandos">Lista de comandos SQL com seus respectivos parâmetros.</param>
    /// <returns>Retorna true se todos os comandos forem executados com sucesso.</returns>
    public bool executarComandosEmTransacao(
        List<(string sql, Dictionary<string, object?> parametros)> comandos
    )
    {
        if (comandos == null || comandos.Count == 0)
            return false;

        using NpgsqlConnection conexao = databaseConnection.criarConexao();

        conexao.Open();

        using NpgsqlTransaction transacao = conexao.BeginTransaction();

        try
        {
            foreach ((string sql, Dictionary<string, object?> parametros) in comandos)
            {
                using NpgsqlCommand comando = new NpgsqlCommand(sql, conexao, transacao);

                adicionarParametros(comando, parametros);
                int linhasAfetadas = comando.ExecuteNonQuery();

                if (linhasAfetadas == 0)
                {
                    transacao.Rollback();
                    return false;
                }
            }

            transacao.Commit();
            return true;
        }
        catch
        {
            transacao.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Adiciona os parâmetros recebidos em um comando SQL.
    /// </summary>
    /// <param name="comando">Comando SQL que receberá os parâmetros.</param>
    /// <param name="parametros">Parâmetros que serão adicionados ao comando.</param>
    private void adicionarParametros(NpgsqlCommand comando, Dictionary<string, object?> parametros)
    {
        foreach (KeyValuePair<string, object?> parametro in parametros)
        {
            comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
        }
    }
}
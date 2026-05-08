using Npgsql;

namespace conta_bancaria_csharp.Data;

/// <summary>
/// Classe responsável por criar e testar a conexão com o banco de dados PostgreSQL.
/// </summary>
public class DatabaseConnection
{
    private const string NomeVariavelConnectionString = "DATABASE_CONNECTION_STRING";

    /// <summary>
    /// Cria uma nova conexão com o banco de dados a partir da variável de ambiente configurada.
    /// </summary>
    /// <returns>Retorna uma conexão PostgreSQL.</returns>
    public NpgsqlConnection criarConexao()
    {
        string? connectionString = Environment.GetEnvironmentVariable(NomeVariavelConnectionString);

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException($"Variável de ambiente {NomeVariavelConnectionString} não configurada.");

        return new NpgsqlConnection(connectionString);
    }

    /// <summary>
    /// Testa se a conexão com o banco de dados está funcionando.
    /// </summary>
    /// <returns>Retorna true se conectar com sucesso; caso contrário, false.</returns>
    public bool testarConexao()
    {
        try
        {
            using NpgsqlConnection conexao = criarConexao();
            conexao.Open();
            return true;
        }
        catch (Exception erro)
        {
            Console.WriteLine($"Erro ao conectar com o banco de dados: {erro.Message}");
            return false;
        }
    }
}
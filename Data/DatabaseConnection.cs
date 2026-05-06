using Npgsql;

/// <summary>
/// Classe responsável por criar e testar a conexão com o banco de dados PostgreSQL.
/// </summary>
public class DatabaseConnection
{
    private const string connectionString =
        "Host=localhost;Port=5432;Database=conta_bancaria;Username=postgres;Password=postgres";

    /// <summary>
    /// Cria uma nova conexão com o banco de dados.
    /// </summary>
    /// <returns>Retorna uma conexão PostgreSQL.</returns>
    public NpgsqlConnection criarConexao()
    {
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
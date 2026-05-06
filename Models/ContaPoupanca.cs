namespace conta_bancaria_csharp.Models;


/// <summary>
/// Representa uma conta poupança.
/// </summary>
public class ContaPoupanca : Conta
{
    private int aniversario;

    public int getAniversario() { return aniversario; }

    public void setAniversario(int aniversario)
    {
        this.aniversario = aniversario;
    }

    public void visualizar() { }

    /// <summary>
    /// Cria uma nova conta poupança.
    /// </summary>
    /// <param name="tipo">Tipo da conta.</param>
    /// <param name="titular">Nome do titular da conta.</param>
    /// <param name="aniversario">Dia de aniversário da conta poupança.</param>
    public ContaPoupanca(int tipo, string titular, int aniversario)
        : base(tipo, titular)
    {
        setAniversario(aniversario);
    }
}
namespace conta_bancaria_csharp.Models;


/// <summary>
/// Representa uma conta corrente.
/// </summary>
public class ContaCorrente : Conta
{
    private decimal limite;

    public decimal getLimite() { return limite; }

    public void setLimite(decimal limite)
    {
        this.limite = limite;
    }

    public void visualizar() { }

    /// <summary>
    /// Cria uma nova conta corrente.
    /// </summary>
    /// <param name="tipo">Tipo da conta.</param>
    /// <param name="titular">Nome do titular da conta.</param>
    /// <param name="limite">Limite disponível para a conta corrente.</param>
    public ContaCorrente(int tipo, string titular, decimal limite)
        : base(tipo, titular)
    {
        setLimite(limite);
    }
}
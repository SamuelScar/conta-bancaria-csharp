namespace conta_bancaria_csharp.Models;

public abstract class Conta
{

    private int numero;
    private int agencia;
    private int tipo;
    private string titular = string.Empty;
    private decimal saldo;

    public int getNumero() { return numero; }
    public int getAgencia() { return agencia; }
    public int getTipo() { return tipo; }
    public string getTitular() { return titular; }
    public decimal getSaldo() { return saldo; }

    public void setNumero(int numero) { this.numero = numero; }
    public void setAgencia(int agencia) { this.agencia = agencia; }
    public void setTipo(int tipo) { this.tipo = tipo; }
    public void setTitular(string titular) { this.titular = titular?.Trim() ?? string.Empty; }
    public void setSaldo(decimal saldo) { this.saldo = saldo; }
    public void setSaldoInicial() { this.saldo = 0.0m; }

    public Conta(int tipo, string titular)
    {
        setTipo(tipo);
        setTitular(titular);
        setSaldoInicial();
    }

    /// <summary>
    /// Realiza o saque na conta, caso o valor seja válido e exista saldo suficiente.
    /// </summary>
    /// <param name="valor">Valor que será sacado da conta.</param>
    /// <returns>Retorna true se o saque for realizado com sucesso; caso contrário, false.</returns>
    public bool sacar(decimal valor)
    {
        if (valor <= 0 || valor > saldo)
            return false;
        saldo -= valor;
        return true;
    }

    public bool depositar(decimal valor)
    {
        if (valor <= 0)
            return false;

        this.saldo += valor;
        return true;
    }

}

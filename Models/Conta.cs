/// <summary>
/// Representa a estrutura base para uma conta bancária.
/// </summary>
public abstract class Conta
{

    /// <summary>
    /// Em projetos C# atualmente também é comum utilizar propriedades públicas com set protegido,
    /// como: public int Numero { get; protected set; }
    /// 
    /// Os atributos foram definidos como privados para seguir fielmente a representação do diagrama UML.
    /// 
    /// Porém, neste projeto, a prioridade é manter a implementação alinhada ao UML fornecido.
    /// </summary>

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
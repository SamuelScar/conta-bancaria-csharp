using System;
using System.Threading;
using conta_bancaria_csharp.Constants;
using conta_bancaria_csharp.Models;

namespace conta_bancaria_csharp.Utils;

/// <summary>
/// Classe utilitária responsável por centralizar as impressões no console.
/// </summary>
public static class ConsolePrinter
{
    private const string Linha = "*****************************************************";
    private const string Espaco = "                                                     ";

    /// <summary>
    /// Exibe o cabeçalho principal do sistema.
    /// </summary>
    public static void ExibirCabecalho()
    {
        ExibirLinha();
        Console.WriteLine(Espaco);
        Cores.EscreverLinha("          BANCO BRODASCO CONTA BANCÁRIA              ", Cores.Titulo);
        Console.WriteLine(Espaco);
        ExibirLinha();
    }

    /// <summary>
    /// Exibe as opções disponíveis no menu principal.
    /// </summary>
    public static void ExibirOpcoesMenu()
    {
        ExibirLinha();
        Console.WriteLine(Espaco);
        ExibirOpcaoMenu(1, "Cadastrar Conta");
        ExibirOpcaoMenu(2, "Listar todas as Contas");
        ExibirOpcaoMenu(3, "Procurar Conta por Número");
        ExibirOpcaoMenu(4, "Atualizar Dados da Conta");
        ExibirOpcaoMenu(5, "Deletar Conta");
        ExibirOpcaoMenu(6, "Sacar");
        ExibirOpcaoMenu(7, "Depositar");
        ExibirOpcaoMenu(8, "Transferir valores entre Contas");
        ExibirOpcaoMenu(9, "Sair");

        Console.WriteLine(Espaco);
        ExibirLinha();
    }

    /// <summary>
    /// Exibe a mensagem de despedida ao encerrar o sistema.
    /// </summary>
    public static void ExibirDespedida()
    {
        Console.Clear();
        Cores.EscreverLinha("Obrigado por utilizar o sistema do Brodasco Conta Bancária!", Cores.Sucesso);
        Thread.Sleep(2500);
    }

    /// <summary>
    /// Exibe os dados da conta após uma operação realizada com sucesso.
    /// </summary>
    /// <param name="acao">Ação realizada na conta.</param>
    /// <param name="conta">Conta relacionada à operação.</param>
    public static void ExibirContaComSucesso(string acao, Conta conta) =>
        ExibirDadosConta($"Conta {acao} com sucesso!", conta, Cores.Sucesso);

    /// <summary>
    /// Exibe os dados principais de uma conta.
    /// </summary>
    /// <param name="conta">Conta que será exibida.</param>
    public static void ExibirConta(Conta conta) =>
        ExibirDadosConta("Dados da conta:", conta, Cores.Destaque);

    /// <summary>
    /// Exibe os dados principais de uma conta.
    /// </summary>
    /// <param name="mensagem">Mensagem inicial que será exibida.</param>
    /// <param name="conta">Conta que terá os dados exibidos.</param>
    private static void ExibirDadosConta(string mensagem, Conta conta, ConsoleColor corMensagem)
    {
        Console.WriteLine();
        Cores.EscreverLinha(mensagem, corMensagem);

        ExibirCampo("Número", conta.getNumero());
        ExibirCampo("Agência", conta.getAgencia());
        ExibirCampo("Tipo", ObterDescricaoTipo(conta));
        ExibirCampo("Titular", conta.getTitular());
        ExibirCampo("Saldo", FormatarValor(conta.getSaldo()));

        if (conta is ContaCorrente contaCorrente)
            ExibirCampo("Limite", FormatarValor(contaCorrente.getLimite()));

        if (conta is ContaPoupanca contaPoupanca)
            ExibirCampo("Aniversário", $"{contaPoupanca.getAniversario()}º dia");
    }

    /// <summary>
    /// Exibe uma mensagem de erro no console.
    /// </summary>
    /// <param name="mensagem">Mensagem que será exibida.</param>
    public static void ExibirErro(string mensagem) =>
        Cores.EscreverLinha(mensagem, Cores.Erro);

    /// <summary>
    /// Exibe uma mensagem simples no console.
    /// </summary>
    /// <param name="mensagem">Mensagem que será exibida.</param>
    public static void ExibirMensagem(string mensagem) =>
        Cores.EscreverLinha(mensagem, Cores.Texto);

    /// <summary>
    /// Exibe um título de seção no console.
    /// </summary>
    /// <param name="titulo">Título que será exibido.</param>
    public static void ExibirTitulo(string titulo)
    {
        Cores.EscreverLinha(titulo.ToUpper(), Cores.Titulo);
        Console.WriteLine();
    }

    /// <summary>
    /// Exibe uma mensagem de sucesso no console.
    /// </summary>
    /// <param name="mensagem">Mensagem que será exibida.</param>
    public static void ExibirSucesso(string mensagem) =>
        Cores.EscreverLinha(mensagem, Cores.Sucesso);

    /// <summary>
    /// Aguarda o usuário pressionar uma tecla para continuar.
    /// </summary>
    public static void AguardarTecla()
    {
        Cores.EscreverLinha("\nPressione qualquer tecla para continuar...", Cores.Aviso);
        Console.ReadKey();
    }

    private static void ExibirLinha()
    {
        Cores.EscreverLinha(Linha, Cores.Borda);
    }

    private static void ExibirOpcaoMenu(int numero, string descricao)
    {
        Console.Write("            ");
        Cores.Escrever(numero.ToString(), Cores.Opcao);
        Console.WriteLine($" - {descricao}");
    }

    private static void ExibirCampo(string nome, object valor)
    {
        Cores.Escrever($"{nome}: ", Cores.Destaque);
        Cores.EscreverLinha(valor.ToString() ?? string.Empty, Cores.Texto);
    }

    private static string FormatarValor(decimal valor)
    {
        return $"R$ {valor:F2}";
    }

    private static string ObterDescricaoTipo(Conta conta)
    {
        return conta.getTipo() switch
        {
            TipoConta.Corrente => "Conta Corrente",
            TipoConta.Poupanca => "Conta Poupança",
            _ => "Tipo desconhecido"
        };
    }
}

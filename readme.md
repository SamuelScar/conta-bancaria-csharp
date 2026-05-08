# Banco Brodasco

Projeto de conta bancária em C# desenvolvido como parte de um desafio prático, com foco em aplicar conceitos de orientação a objetos em uma aplicação console.

A implementação segue a estrutura proposta no desafio e documenta as principais decisões tomadas durante o desenvolvimento.

## Sumário

- [Observação sobre a implementação](#observação-sobre-a-implementação)
- [Como rodar o projeto](#como-rodar-o-projeto)
- [Decisões do Projeto](#decisões-do-projeto)

## Observação sobre a implementação

Durante o desenvolvimento deste projeto, a implementação busca manter a estrutura próxima ao modelo UML proposto, com ajustes pontuais necessários para funcionamento, validação e persistência.

A escolha de manter a estrutura próxima ao UML tem como objetivo facilitar a leitura e o entendimento do código, principalmente para pessoas que ainda não conhecem a implementação interna do sistema. Como o UML é utilizado justamente para modelar e representar a estrutura do projeto de forma visual, manter o código alinhado a esse modelo ajuda a tornar a aplicação mais compreensível.

Mesmo existindo formas mais otimizadas, mais flexíveis ou visualmente mais “elegantes” de implementar algumas funcionalidades, a decisão neste momento foi priorizar a fidelidade ao modelo apresentado. Dessa forma, as alterações realizadas no código serão feitas apenas quando forem essenciais para alcançar o resultado final esperado e garantir o funcionamento correto da aplicação.

## Como rodar o projeto

### Pré-requisitos

- .NET 8 SDK
- PostgreSQL

### Configurar o banco de dados

O arquivo `schema.sql` contém a estrutura da tabela `contas`.

```bash
createdb conta_bancaria
psql -d conta_bancaria -f schema.sql
```

### Configurar a conexão

A aplicação lê a conexão do banco pela variável `DATABASE_CONNECTION_STRING`. Para configurar localmente, copie o arquivo `.env.example` para `.env` e ajuste usuário e senha conforme o seu PostgreSQL.

```bash
cp .env.example .env
```

Exemplo de valor esperado:

```env
DATABASE_CONNECTION_STRING=Host=localhost;Port=5432;Database=conta_bancaria;Username=seu_usuario;Password=sua_senha
```

Ao iniciar, a aplicação carrega o arquivo `.env` usando o pacote `DotNetEnv`.

### Executar a aplicação

```bash
dotnet run
```

## Decisões do Projeto

### Cadastro por tipo de conta

O cadastro identifica o tipo de conta informado pelo usuário para criar a instância correta: `ContaCorrente` ou `ContaPoupanca`.

Essa separação é necessária porque cada tipo possui um dado específico. A conta corrente possui `limite`, enquanto a conta poupança possui `aniversario`.

Depois que a conta específica é criada, ela é tratada pelo sistema através da classe base `Conta`. Dessa forma, o fluxo de cadastro continua simples e genérico no `ContaController` e na persistência, mas sem perder as diferenças entre os tipos de conta.

### Métodos de acesso

Mesmo sendo comum em C# utilizar propriedades, como `Numero { get; set; }`, o projeto mantém métodos `get` e `set` para ficar mais próximo da estrutura apresentada no UML do desafio.

### Saldo na classe `Conta`

O atributo `saldo` foi definido na classe `Conta`, pois saldo é uma informação comum a todos os tipos de conta. Com isso, tanto `ContaCorrente` quanto `ContaPoupanca` herdam esse atributo e podem reutilizar os mesmos comportamentos relacionados a saldo.

### Método `sacar`

O método `sacar` foi mantido apenas na classe `Conta`, pois a regra de saque atual é comum para todos os tipos de conta. Como `ContaCorrente` já herda esse comportamento de `Conta`, não faria sentido duplicar o método enquanto não houver uma regra específica para saque em conta corrente.

### Saldo inicial

Toda conta cadastrada inicia com saldo `0`. Por esse motivo, o saldo não é informado pelo usuário no momento do cadastro, sendo definido automaticamente pela própria classe `Conta`.

### Fluxo de navegação

As operações do menu foram organizadas em submenus para permitir que o usuário repita a mesma ação quantas vezes precisar antes de voltar ao menu principal.

Também foi adicionado o comando `CANCELAR` nos campos das operações, permitindo cancelar a ação atual e retornar ao submenu sem concluir a operação.

### Transferências

As transferências atualizam o saldo da conta de origem e da conta de destino dentro de uma mesma transação no banco de dados. Essa decisão garante que a operação seja concluída por inteiro ou desfeita em caso de erro, evitando inconsistências entre as contas.

### Valores monetários com `decimal`

Embora o UML indique `float`, os valores monetários foram implementados com `decimal`, por ser um tipo mais adequado para dinheiro e reduzir riscos de imprecisão em cálculos com casas decimais.

Na entrada pelo console, foi adotado o ponto como separador decimal. Entradas com vírgula são consideradas inválidas para manter um padrão único de leitura dos valores.

### Aniversário da conta poupança

O atributo `aniversario` da `ContaPoupanca` foi considerado como o dia do mês em que a conta faz aniversário. Por isso, o sistema aceita apenas valores entre `1` e `31`, garantindo que o dado represente um dia válido dentro do mês.

### Banco de dados

Foi escolhido o PostgreSQL para realizar a persistência dos dados do sistema.

As contas são armazenadas em uma única tabela chamada `contas`, pois `ContaCorrente` e `ContaPoupanca` compartilham vários dados em comum, como número, agência, tipo, titular e saldo. Os campos específicos de cada tipo, como `limite` e `aniversario`, são tratados como campos opcionais, com restrições no banco para garantir que cada tipo de conta possua apenas os dados correspondentes.

A conexão com o banco é configurada pela variável de ambiente `DATABASE_CONNECTION_STRING`. Essa decisão evita deixar usuário e senha fixos no código-fonte e permite que cada ambiente configure suas próprias credenciais.

### Camada de dados

Além do `ContaController` e da interface `ContaRepository`, a persistência foi separada em classes próprias.

A classe `ContaData` concentra as operações relacionadas à tabela de contas, `DatabaseExecutor` centraliza a execução dos comandos SQL e `DatabaseConnection` fica responsável pela criação da conexão com o banco. Essa separação evita concentrar responsabilidades no controller e deixa o acesso aos dados mais organizado.

### Número da conta e agência

O número da conta é gerado pelo banco de dados no momento do cadastro, e a agência recebe um valor padrão. Essa decisão evita a necessidade de gerar valores aleatórios na aplicação e depois verificar manualmente se já existem no banco.

O campo `numero` foi definido como identificador único da conta e será utilizado como chave primária no banco de dados. Por isso, as operações de busca, atualização, exclusão e movimentação utilizam apenas o número da conta como referência principal.

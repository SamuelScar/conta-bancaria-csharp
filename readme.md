## Observação sobre a implementação

Durante o desenvolvimento deste projeto, a implementação está sendo feita buscando seguir fielmente o modelo UML proposto.

A escolha de manter a estrutura próxima ao UML tem como objetivo facilitar a leitura e o entendimento do código, principalmente para pessoas que ainda não conhecem a implementação interna do sistema. Como o UML é utilizado justamente para modelar e representar a estrutura do projeto de forma visual, manter o código alinhado a esse modelo ajuda a tornar a aplicação mais compreensível.

Mesmo existindo formas mais otimizadas, mais flexíveis ou visualmente mais “elegantes” de implementar algumas funcionalidades, a decisão neste momento foi priorizar a fidelidade ao modelo apresentado. Dessa forma, as alterações realizadas no código serão feitas apenas quando forem essenciais para alcançar o resultado final esperado e garantir o funcionamento correto da aplicação.

## Decisões do Projeto

### Cadastro separado por tipo de conta

O cadastro foi separado por tipo de conta porque `ContaCorrente` e `ContaPoupanca` possuem dados específicos que precisam ser tratados de formas diferentes.

Mesmo utilizando um método genérico recebendo `Conta` e verificando o tipo real com `is`, ainda seria necessário criar lógicas diferentes para persistir cada tipo de conta, já que a conta corrente possui `limite` e a conta poupança possui `aniversario`.

Dessa forma, utilizar métodos específicos/sobrecarregados deixa essa diferença explícita na assinatura dos métodos, evitando esconder essa decisão dentro de um método genérico e tornando o código mais claro, direto e coerente com as regras de cada tipo de conta.

### Saldo na classe `Conta`

O atributo `saldo` foi definido na classe `Conta`, pois saldo é uma informação comum a todos os tipos de conta. Com isso, tanto `ContaCorrente` quanto `ContaPoupanca` herdam esse atributo e podem reutilizar os mesmos comportamentos relacionados a saldo.

### Método `sacar`

O método `sacar` foi mantido apenas na classe `Conta`, pois a regra de saque atual é comum para todos os tipos de conta. Como `ContaCorrente` já herda esse comportamento de `Conta`, não faria sentido duplicar o método enquanto não houver uma regra específica para saque em conta corrente.

### Saldo inicial

Toda conta cadastrada inicia com saldo `0`. Por esse motivo, o saldo não é informado pelo usuário no momento do cadastro, sendo definido automaticamente pela própria classe `Conta`.

### Valores monetários com `decimal`

Embora o UML indique o uso de `float` para valores como saldo, limite, saque, depósito e transferência, foi adotado o tipo `decimal` na implementação.

Essa decisão foi tomada porque esses campos representam valores monetários. Em C#, `float` trabalha com ponto flutuante binário e pode gerar pequenas imprecisões em cálculos com casas decimais, o que não é adequado para dinheiro. Já `decimal` possui maior precisão decimal e é mais indicado para operações financeiras, reduzindo riscos de arredondamentos inesperados.

Dessa forma, a estrutura geral do projeto continua seguindo o UML, mas esse tipo específico foi ajustado por necessidade técnica e para garantir mais segurança nas regras de negócio envolvendo saldo e movimentações bancárias.

### Aniversário da conta poupança

O atributo `aniversario` da `ContaPoupanca` foi considerado como o dia do mês em que a conta faz aniversário. Por isso, o sistema aceita apenas valores entre `1` e `31`, garantindo que o dado represente um dia válido dentro do mês.

### Banco de dados

Foi escolhido o PostgreSQL para realizar a persistência dos dados do sistema.

As contas são armazenadas em uma única tabela chamada `contas`, pois `ContaCorrente` e `ContaPoupanca` compartilham vários dados em comum, como número, agência, tipo, titular e saldo. Os campos específicos de cada tipo, como `limite` e `aniversario`, são tratados como campos opcionais, com restrições no banco para garantir que cada tipo de conta possua apenas os dados correspondentes.

### Número da conta e agência

O número da conta e a agência são gerados pelo banco de dados no momento do cadastro, e não diretamente pelo código C#. Essa decisão evita a necessidade de gerar valores aleatórios na aplicação e depois verificar manualmente se já existem no banco.

Foi utilizada uma restrição de unicidade composta entre `agencia` e `numero`, garantindo que a combinação entre agência e número da conta seja única.


# TODO
- [ ] Adicionar sistema de login
- [ ] Adicionar opção de voltar ao menu inicial cancelando qualquer ação a qualquer momento

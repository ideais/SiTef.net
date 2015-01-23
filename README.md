SiTef.net
=========

API para acesso as funcionalidades da *LibSiTef*.

### Configurando a LibSiTef

Você vai precisar da _LibSiTef.dll_ para utilizar o projeto. Ela é fornecida pela [Software Express](http://www.softwareexpress.com.br),
sob licença de uso, entre em contato com eles para obter uma cópia.

Em específico estamos usando a _LibSiTef.dll_ de **64 bits**. Para configura-la no projeto faça o seguinte:

#### Windows

1. Crie uma pasta _C:\LibSiTef_
2. Copie a _LibSiTef.dll_ para lá
3. Adicione essa pasta ao **PATH** do Windows

#### Linux / Mono

Ajuda é bem vinda à quem estiver disposto a fazer o projeto funcionar no Linux com Mono.

### Utilizando o SiTef.net

Exemplo de operação de consulta às informações do cartão do usuário:

```C#

    using SiTef.net.Action;
    using SiTef.net.Action.Model;
    using SiTef.net.Type;
    
...

    TerminalFactory factory = new TerminalFactory("127.0.0.1","00000000");
    using (var term = factory.NewInstance())
    {
        ConsultaCartaoAction action = new ConsultaCartaoAction(term);

        ConsultaCartaoResponse response = action.Execute(
            new ConsultaCartaoRequest(
                new NumeroDoCartao("4000000000000044"),
                new DataDeVencimento(12,15)
            )
        );
        foreach (var field in response.GetFields())
            System.Console.WriteLine(field);
    }
    
```

Changelog
---------

Versão **0.0.0.15**

* BugFix - Correção no tratamento da conversão do campo _Valor_ para Int

Versão **0.0.0.11**

* BugFix - DadosDeConfirmação precisa ter um construtor que receba o valor String. 

Versão **0.0.0.10**

* Campo _Valor_ herda de _NumericField_ agora.

Versão **0.0.0.9**

* Ação de Venda
* Ação de Finaliza Tranzação
 * Confirma Venda
 * Cancela Venda

Versão **0.0.0.8**

* Refactoring de Field, agora tipificado com Generics e criação de tipos Base comuns
* ActionException lançada quando alguma ação tem falha no seu retorno
* Vários outros refactorings e melhorias

Versão **0.0.0.7**

Versão **0.0.0.6**

* Correção - TerminalException estava com a visibilidade errada. 

Versão --0.0.0.5--

Versão **0.0.0.4**

* Correções nas ações
 * ConsultaCartao
 * PreCaptura
 * Captura
 * Estorno

Versão **0.0.0.3**

* Terminal implementando IDisposable, para ser finalizado automaticamente
* TerminalFactory inicializa novos terminais para o uso
* Ação de Estorno de Pré-Autorização de Cartão de Crédito

Versão **0.0.0.2**

* Mudando métodos e classes para o português para diminuir confusão
* Modeladas ações de:
 * Pré-Autorização
 * Captura

Versão **0.0.0.1**:

* Modelo de Tipos de Entrada/Saída com validação
* Abstração de Ação, com Requisição e Resposta

Backlog
-------

* Testes unitários para cada um dos tipos de campos que extendem Field
* Agrupar as implementações de Field em tipos básicos, Date, Time, Numeric, etc...
* Validação nos construtores das Actions
* Validação da resposta das Actions, criação de uma ActionException para encapsular erros de negócio
* Modelar o restante das Ações definidas pelo SiTef
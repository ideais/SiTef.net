SiTef.net
=========

API para acesso as funcionalidades da *LibSiTef*.

### Configurando a LibSiTef

Voc� vai precisar da _LibSiTef.dll_ para utilizar o projeto. Ela � fornecida pela [Software Express](http://www.softwareexpress.com.br),
sob licen�a de uso, entre em contato com eles para obter uma c�pia.

Em espec�fico estamos usando a _LibSiTef.dll_ de **64 bits**. Para configura-la no projeto fa�a o seguinte:

#### Windows

1. Crie uma pasta _C:\LibSiTef_
2. Copie a _LibSiTef.dll_ para l�
3. Adicione essa pasta ao **PATH** do Windows

#### Linux / Mono

Ajuda � bem vinda � quem estiver disposto a fazer o projeto funcionar no Linux com Mono.

### Utilizando o SiTef.net

Exemplo de opera��o de consulta �s informa��es do cart�o do usu�rio:

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

Vers�o **0.0.0.15**

* BugFix - Corre��o no tratamento da convers�o do campo _Valor_ para Int

Vers�o **0.0.0.11**

* BugFix - DadosDeConfirma��o precisa ter um construtor que receba o valor String. 

Vers�o **0.0.0.10**

* Campo _Valor_ herda de _NumericField_ agora.

Vers�o **0.0.0.9**

* A��o de Venda
* A��o de Finaliza Tranza��o
 * Confirma Venda
 * Cancela Venda

Vers�o **0.0.0.8**

* Refactoring de Field, agora tipificado com Generics e cria��o de tipos Base comuns
* ActionException lan�ada quando alguma a��o tem falha no seu retorno
* V�rios outros refactorings e melhorias

Vers�o **0.0.0.7**

Vers�o **0.0.0.6**

* Corre��o - TerminalException estava com a visibilidade errada. 

Vers�o --0.0.0.5--

Vers�o **0.0.0.4**

* Corre��es nas a��es
 * ConsultaCartao
 * PreCaptura
 * Captura
 * Estorno

Vers�o **0.0.0.3**

* Terminal implementando IDisposable, para ser finalizado automaticamente
* TerminalFactory inicializa novos terminais para o uso
* A��o de Estorno de Pr�-Autoriza��o de Cart�o de Cr�dito

Vers�o **0.0.0.2**

* Mudando m�todos e classes para o portugu�s para diminuir confus�o
* Modeladas a��es de:
 * Pr�-Autoriza��o
 * Captura

Vers�o **0.0.0.1**:

* Modelo de Tipos de Entrada/Sa�da com valida��o
* Abstra��o de A��o, com Requisi��o e Resposta

Backlog
-------

* Testes unit�rios para cada um dos tipos de campos que extendem Field
* Agrupar as implementa��es de Field em tipos b�sicos, Date, Time, Numeric, etc...
* Valida��o nos construtores das Actions
* Valida��o da resposta das Actions, cria��o de uma ActionException para encapsular erros de neg�cio
* Modelar o restante das A��es definidas pelo SiTef
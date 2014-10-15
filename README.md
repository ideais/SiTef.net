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
                new DataDeVencimento("1215")
            )
        );
        foreach (var field in response.GetFields())
            System.Console.WriteLine(field);
    }
    
```

Changelog
---------

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
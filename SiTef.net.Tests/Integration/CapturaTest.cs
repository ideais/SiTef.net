using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Action;
using SiTef.net.Action.Model;
using SiTef.net.Type;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class CapturaTest
    {
        static TerminalFactory factory;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            factory = new TerminalFactory("127.0.0.1","00000000");
        }

        [TestMethod]
        public void ExecutaCapturaTest()
        {
            PreAutorizacaoResponse autorizacao;
            
            using (var term = factory.NewInstance())
            {
                var preautoriza = new PreAutorizacaoAction(term);
                autorizacao = preautoriza.Execute(new PreAutorizacaoRequest(
                    null,
                    new DataFiscal(new DateTime()),
                    null,
                              // MASTER 5486906003474434
                    new NumeroDoCartao("4485022036287910"),
                    new DataDeVencimento(12,15),
                    new Valor(100.00),
                    null,
                    new CodigoDeSeguranca("1234")
                ));

                Console.WriteLine("-------------------------");
                foreach (var field in autorizacao.GetFields())
                    Console.WriteLine(field);

            }
            

            using(var term = factory.NewInstance()){
                var captura = new CapturaPreAutorizacaoAction(term);
                var result = captura.Execute(new CapturaRequest(
                        new NumeroDoCartao("4929208425739710"),
                        new DataDeVencimento(12,15),
                        new Valor(100.00),
                        new DataDaTransacao(new DateTime()),
                        autorizacao.NumeroAutorizacao,
                        autorizacao.NsuHost,
                        null,
                        new TipoDeFinanciamento(1),
                        null,
                        new CodigoDeSeguranca("123")
                    ));
                Console.WriteLine("-------------------------");
                foreach (var field in result.GetFields())
                    System.Console.WriteLine(field);
            }
        }

    }
}

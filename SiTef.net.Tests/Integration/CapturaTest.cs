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
            factory = new TerminalFactory("10.166.58.10","00000000");
        }

        [TestMethod]
        public void ExecutaCapturaTest()
        {
            using (var term = factory.NewInstance())
            {
                var preautoriza = new PreAutorizacaoAction(term);
                var autorizacao = preautoriza.Execute(new PreAutorizacaoRequest(
                    null,
                    new DataFiscal(new DateTime()),
                    null,
                    new NumeroDoCartao("4929208425739710"),
                    new DataDeVencimento("1215"),
                    new Valor("10000"),
                    null,
                    new CodigoDeSeguranca("123")
                ));

                var captura = new CapturaAction(term);
                var result = captura.Execute(new CapturaRequest(
                        new NumeroDoCartao("4929208425739710"),
                        new DataDeVencimento("1215"),
                        new Valor("10000"),
                        new DataDaTransacao(new DateTime()),
                        autorizacao.autorizacao,
                        autorizacao.nsuHost,
                        null,
                        new TipoDeFinanciamento("1"),
                        null,
                        new CodigoDeSeguranca("123")
                    ));

                foreach (var field in result.GetFields())
                    System.Console.WriteLine(field);
            }
        }

    }
}

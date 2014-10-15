using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Type;
using SiTef.net.Action.Model;
using SiTef.net.Action;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class EstornoPreAutorizacaoTest
    {
        static TerminalFactory factory;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            factory = new TerminalFactory("127.0.0.1", "00000000");
        }

        [TestMethod]
        public void ExecuteEstornoTest()
        {

            var cartao = new NumeroDoCartao("4929208425739710");
            var vencimento = new DataDeVencimento("1215");
            var valor = new Valor("10000");
            var cvv = new CodigoDeSeguranca("123");

            PreAutorizacaoResponse autorizacao;
            using (var term = factory.NewInstance())
            {
                var preautoriza = new PreAutorizacaoAction(term);
                autorizacao = preautoriza.Execute(new PreAutorizacaoRequest(
                    null,
                    new DataFiscal(DateTime.Now),
                    null,
                    cartao,
                    vencimento,
                    valor,
                    null,
                    cvv
                ));
            }

            using (var term = factory.NewInstance())
            {
                var estorno = new EstornoPreAutorizacaoAction(term);
                var response = estorno.Execute(
                    new EstornoRequest(
                        TipoDeTransacao.CANCELAMENTO_GENERICO_CIAGROUP_GIFT,
                        cartao,
                        vencimento,
                        valor,
                        new DataDaTransacao(DateTime.Now),
                        autorizacao.autorizacao,
                        autorizacao.nsuHost,
                        cvv
                    ));
                foreach (var field in response.GetFields())
                    System.Console.WriteLine(field);
            }
        }
    }
}

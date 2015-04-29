using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Pool;
using SiTef.net.Action.Model;
using SiTef.net.Action;
using SiTef.net.Type;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class CancelaVendaTest
    {
        static TerminalFactory factory;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            factory = new TerminalFactory("127.0.0.1", "00000000");
        }

        [TestMethod]
        public void ExecuteCancelaVendaTest()
        {
            var valor = new Random().Next(20, 120) * 100;

            // Primeiro executa uma venda

            VendaResponse venda;
            var term = factory.NewInstance();
            VendaAction action = new VendaAction(term);
            VendaRequest request = new VendaRequest(
                null,//new DataFiscal(DateTime.Now.AddDays(2)),
                null,//new HoraFiscal(DateTime.Now),
                null,//new NumeroDeParcelas(1),
                null,//new TipoDeFinanciamento(2),
                new NumeroDoCartao("4024007122405250"),
                new DataDeVencimento(12, 15),
                new CodigoDeSeguranca("123"),
                new Valor(valor)
                );

            venda = action.Execute(request);

            foreach (var field in venda.GetFields())
                System.Console.WriteLine(field);

            if (!venda.Failure())
                new FinalizaTransacaoAction(term).Execute(new  ConfirmaVenda(venda.DadosDeConfirmacao));

            Assert.IsFalse(venda.Failure());

            System.Console.WriteLine("\n-----------------------------------------\n");

            var nsuHost = venda.NSUHost.Value;

            CancelaVendaResponse cancelResponse;
            CancelaVendaAction cancelAction = new CancelaVendaAction(term);
            CancelaVendaRequest cancelRequest = new CancelaVendaRequest
                (
                    Rede.CIELO,
                    new NumeroDoCartao("402400******5250"),
                    new NSUHost(nsuHost),
                    new Data(new DateTime(2015,4,29)),
                    new Valor(valor)
                );

            cancelResponse = cancelAction.Execute(cancelRequest);


            foreach (var field in cancelResponse.GetFields())
                System.Console.WriteLine(field);

            if (!cancelResponse.Failure())
                new FinalizaTransacaoAction(term).Execute(new ConfirmaVenda(cancelResponse.DadosDeConfirmacao));

            Assert.IsFalse(cancelResponse.Failure());

        }
    }
}

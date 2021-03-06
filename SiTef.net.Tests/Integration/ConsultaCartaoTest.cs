﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiTef.net.Action;
using SiTef.net.Action.Model;
using SiTef.net.Type;
using SiTef.net.Pool;

namespace SiTef.net.Tests.Integration
{
    [TestClass]
    public class ConsultaCartaoTest
    {
        static TerminalFactory factory;

        [ClassInitialize]
        static public void Init(TestContext context)
        {
            factory = new TerminalFactory("127.0.0.1", "00000000");
        }

        [TestMethod]
        public void ExecuteConsultaCartaoTest()
        {
            var term = factory.NewInstance();
            ConsultaCartaoAction action = new ConsultaCartaoAction(term);

            ConsultaCartaoResponse response = action.Execute(
                new ConsultaCartaoRequest(
                    new NumeroDoCartao("4024007122405250"),
                    new DataDeVencimento(12, 15)
                )
            );
            foreach (var field in response.GetFields())
                System.Console.WriteLine(field);
        }
    }
}

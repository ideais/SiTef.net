﻿using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class ConsultaCartaoResponse : AbstractActionModel
    {

        public ConsultaCartaoResponse(Terminal terminal)
        {
            _fields = new List<IField>{
                new Rede(terminal),
                new CodigoDeRespostaSiTef(terminal),
                new TextoParaExibicao(terminal),
                new BandeiraDoCartao(terminal),
                new StringField(24, 1, terminal), //Valida Embosso
                new StringField(25, 4, terminal), //Codigo Validacao
                new StringField(26, 1, terminal), //Tipo Senha
                new TaxaServico(terminal),
                new StringField(28, 2, terminal), //No Min Parcela
                new StringField(29, 2, terminal), //No Max Parcela
                new StringField(30, 4, terminal), // Percentual Máximo da Taxa de Servico
                new DateField(31, terminal){Pattern = "ddMMyyyy", Label = "Data Limite Pre Datado"}, // Data Limite Pre Datado
                new DateField(32, terminal){Pattern = "ddMMyyyy", Label = "Data Limite 1a parcela"}, // Data Limite 1a parcela
                new CapturaCodigoSeguranca(terminal),
                new ZeroOrOneField(34, terminal){Label ="Garantia Pre-Datado"},
                new ZeroOrOneField(35, terminal), // Transacao com Chip
                new ZeroOrOneField(36, terminal), // Venda a Vista
                new ZeroOrOneField(37, terminal), // Venda Parcelada
                new ZeroOrOneField(38, terminal), // Venda Parcelada C/ Juros Administradora
                new ZeroOrOneField(39, terminal), // Venda Pro-Rata a Vista
                new ZeroOrOneField(40, terminal), // Venda Pro-Rata parcelada
                new ZeroOrOneField(41, terminal), // Cancelamento (tr.36h/ 37h) e Estorno de Captura de Pré-Autorização (tr. 12h)
                new ZeroOrOneField(42, terminal){Label = "Pré-autorização"}, // Pré-autorização
                new ZeroOrOneField(43, terminal), // Consulta venda Parcelada
                new ZeroOrOneField(44, terminal), // Cancelamento de Pre-Autorizacao
                new ZeroOrOneField(45, terminal), // Captura de Pre-Autorizacao
                new ZeroOrOneField(46, terminal), // Consulta AVS
                new StringField(155, 128, terminal), // Opcoes Variaveis com Prefixo
                /*
                new Field(163, 99, terminal),
                new Field(164, 99, terminal),
                new Field(165, 99, terminal),
                new Field(166, 99, terminal),
                new Field(167, 99, terminal),
                new Field(168, 99, terminal),
                new Field(169, 99, terminal),
                new Field(170, 99, terminal),
                new Field(171, 99, terminal),
                new Field(172, 99, terminal),
                new Field(173, 99, terminal),
                new Field(174, 99, terminal),
                new Field(175, 99, terminal),
                new Field(176, 99, terminal),
                new Field(177, 99, terminal),
                new Field(178, 99, terminal),
                new Field(179, 99, terminal),
                new Field(180, 99, terminal),
                new Field(181, 99, terminal),
                new Field(182, 99, terminal),
                new Field(236, 99, terminal),
                new Field(237, 99, terminal),
                new Field(239, 99, terminal),
                new Field(241, 99, terminal),
                new Field(242, 99, terminal),
                new Field(243, 99, terminal),
                new Field(244, 99, terminal),*/
                new NumericField(245, 2, terminal), //Numero Maximo de Parcelas Loja
                /*
                new Field(246, 99, terminal),
                new Field(350, 99, terminal),
                new Field(351, 99, terminal),
                new Field(352, 99, terminal),
                new Field(353, 99, terminal),
                new Field(354, 99, terminal),
                new Field(561, 99, terminal),
                new Field(562, 99, terminal),
                new Field(563, 99, terminal),
                new Field(564, 99, terminal),
                new Field(578, 99, terminal),
                new Field(579, 99, terminal),*/
            };
        }

    }
}

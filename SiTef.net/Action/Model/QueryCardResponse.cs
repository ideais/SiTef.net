using SiTef.net.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiTef.net.Action.Model
{
    public class QueryCardResponse : AbstractActionModel
    {

        public QueryCardResponse(Terminal terminal)
        {
            _fields = new List<Field>{
                new Field(1, 99, terminal),
                new Field(10, 99, terminal),
                new Field(11, 99, terminal),
                new Field(23, 99, terminal),
                new Field(24, 99, terminal),
                new Field(25, 99, terminal),
                new Field(26, 99, terminal),
                new Field(27, 99, terminal),
                new Field(28, 99, terminal),
                new Field(29, 99, terminal),
                new Field(30, 99, terminal),
                new Field(31, 99, terminal),
                new Field(32, 99, terminal),
                new Field(33, 99, terminal),
                new Field(34, 99, terminal),
                new Field(35, 99, terminal),
                new Field(36, 99, terminal),
                new Field(37, 99, terminal),
                new Field(38, 99, terminal),
                new Field(39, 99, terminal),
                new Field(40, 99, terminal),
                new Field(41, 99, terminal),
                new Field(42, 99, terminal),
                new Field(43, 99, terminal),
                new Field(44, 99, terminal),
                new Field(45, 99, terminal),
                new Field(46, 99, terminal),
                new Field(155, 99, terminal),
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
                new Field(244, 99, terminal),
                new Field(245, 99, terminal),
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
                new Field(579, 99, terminal),
            };
        }

    }
}

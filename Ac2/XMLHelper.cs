using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ac2
{
    public class XMLHelper
    {
        
        public static void CreateXMLFileWithLINQ(List<ConsumAigua> list)
        {
            XDocument xmlDoc = new XDocument(
                new XElement("data",
                    from item in list
                    select new XElement("row",
                        new XElement("Any", item.Any),
                        new XElement("Codi_comarca", item.Codi_comarca),
                        new XElement("Comarca", item.Comarca),
                        new XElement("Població", item.Poblacio),
                        new XElement("Domèstic_xarxa", item.Domestic_xarxa),
                        new XElement("Activitats_econòmiques_i_fonts_pròpies", item.Activitats_economiques_i_fonts_propies),
                        new XElement("Total", item.Total),
                        new XElement("Consum_domèstic_per_càpita", item.Consum_domestic_per_capital)
                    )
                )
            );

            string xmlFilePath = "AguasXML.xml";
            xmlDoc.Save(xmlFilePath);
        }
    }
}

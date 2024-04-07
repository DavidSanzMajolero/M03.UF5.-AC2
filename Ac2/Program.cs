using System;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using CsvHelper;
namespace Ac2
{
    public class Program
    {
        public static void Main()
        {
            List<ConsumAigua> consumAiguas = new List<ConsumAigua>();
            consumAiguas = ReadCsv();
            XMLHelper.CreateXMLFileWithLINQ(consumAiguas);

            Console.WriteLine("Ex1-5");
            int ex = Convert.ToInt32(Console.ReadLine());
            switch (ex)
            {
                case 1:
                    IdenComarques();
                break;
                case 2:
                    ConsDom();
                break;
                case 3:
                    ConsDomMesAlt();
                break;
                case 4:
                    ConsDomMesBaix();
                    break;
                case 5:
                    FiltCom();
                break;
            }
        }
        public static void IdenComarques()
        {
            string xmlFilePath = "AguasXML.xml";
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var rows = from row in xmlDoc.Descendants("row")
                       select new ConsumAigua
                       {
                           Any = (int)row.Element("Any"),
                           Codi_comarca = (int)row.Element("Codi_comarca"),
                           Comarca = (string)row.Element("Comarca"),
                           Poblacio = (int)row.Element("Població"),
                           Domestic_xarxa = (double)row.Element("Domèstic_xarxa"),
                           Activitats_economiques_i_fonts_propies = (double)row.Element("Activitats_econòmiques_i_fonts_pròpies"),
                           Total = (double)row.Element("Total"),
                           Consum_domestic_per_capital = (double)row.Element("Consum_domèstic_per_càpita")
                       };

            foreach (var row in rows)
            {
                if (row.Poblacio > 200000) Console.WriteLine(row);
            }

        }
        public static void ConsDom()
        {
            string xmlFilePath = "AguasXML.xml";
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var rows = from row in xmlDoc.Descendants("row") group row by row.Element("Comarca").Value
                       into g select new { Comarca = g. Key, Consum_domestic_per_capital = g.Sum(x => (double)x.Element("Consum_domèstic_per_càpita")) / g.Count()};

            foreach (var row in rows)
            {
                Console.WriteLine($"Comarca : {row.Comarca} Mitja consum domestic: {row.Consum_domestic_per_capital:N2}");
            }
        }
        public static void ConsDomMesAlt()
        {
            string xmlFilePath = "AguasXML.xml";
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var rows = from row in xmlDoc.Descendants("row") group row by row.Element("Comarca").Value
                       into g select new { Comarca = g.Key, Consum_domestic_per_capital = g.Max(x => (double)x.Element("Consum_domèstic_per_càpita")) };

            foreach (var row in rows)
            {

                Console.WriteLine($"Comarca : {row.Comarca} Max consum domestic: {row.Consum_domestic_per_capital:N2}");
            }

            foreach (var row in rows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (row.Consum_domestic_per_capital == rows.Max(x => x.Consum_domestic_per_capital))
                {
                    Console.WriteLine($"Comarca : {row.Comarca} Max consum domestic: {row.Consum_domestic_per_capital:N2}");
                }
                Console.ResetColor();
            }
        }
        public static void ConsDomMesBaix()
        {
            string xmlFilePath = "AguasXML.xml";
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var rows = from row in xmlDoc.Descendants("row")
                       group row by row.Element("Comarca").Value
                       into g
                       select new { Comarca = g.Key, Consum_domestic_per_capital = g.Min(x => (double)x.Element("Consum_domèstic_per_càpita")) };

            foreach (var row in rows)
            {

                Console.WriteLine($"Comarca : {row.Comarca}. Min consum domestic: {row.Consum_domestic_per_capital:N2}");
            }

            foreach (var row in rows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (row.Consum_domestic_per_capital == rows.Min(x => x.Consum_domestic_per_capital))
                {
                    Console.WriteLine($"Comarca : {row.Comarca} Min consum domestic: {row.Consum_domestic_per_capital:N2}");
                }
                Console.ResetColor();
            }
        }
        public static void FiltCom()
        {
            Console.WriteLine("Introdueix el nom de la comarca");
            string input = Console.ReadLine();
            HelpFiltcom(input);
        }
        public static void HelpFiltcom(string input)
        {
            string xmlFilePath = "AguasXML.xml";
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var rows = from row in xmlDoc.Descendants("row")
                       select new ConsumAigua
                       {
                           Any = (int)row.Element("Any"),
                           Codi_comarca = (int)row.Element("Codi_comarca"),
                           Comarca = (string)row.Element("Comarca"),
                           Poblacio = (int)row.Element("Població"),
                           Domestic_xarxa = (double)row.Element("Domèstic_xarxa"),
                           Activitats_economiques_i_fonts_propies = (double)row.Element("Activitats_econòmiques_i_fonts_pròpies"),
                           Total = (double)row.Element("Total"),
                           Consum_domestic_per_capital = (double)row.Element("Consum_domèstic_per_càpita")
                       };
            bool first = true;
            foreach (var row in rows)
            {
                if (row.Comarca == input || row.Codi_comarca.ToString() == input)
                {
                    if (first)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(row.Comarca);
                        Console.ResetColor();
                        Console.WriteLine();
                        first = false;
                    }
                    Console.WriteLine(row);
                }
            }
        }

        private static List<ConsumAigua> ReadCsv()
        {
            using var reader = new StreamReader("Aguascsv.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<ConsumAigua>();
            return records.ToList();
        }
    }
}

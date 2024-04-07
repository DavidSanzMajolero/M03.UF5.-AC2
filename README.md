# Fuentes

- Jordi Sancho. Ajuda per pasar de csv a xml i utilitzar Linq
  
- Raquel Alaman - Bloc de Programació - XML Parsers. https://github.com/RaquelAlamanITB/bloc-programacio/tree/main/m03programacio/uf5/xmlparsers

# Codi utilitzat

```csharp
public static void CreateXMLFileWithLINQ()
{
    XDocument xmlDoc = new XDocument(
        new XElement("catalog",
            new XElement("book",
                new XAttribute("id", "bk101"),
                new XElement("author", "Gambardella, Matthew"),
                new XElement("title", "XML Developer's Guide"),
                new XElement("genre", "Computer"),
                new XElement("price", "44.95"),
                new XElement("publish_date", "2000-10-01"),
                new XElement("description", "An in-depth look at creating applications with XML.")
            ),
            new XElement("book",
                new XAttribute("id", "bk102"),
                new XElement("author", "Ralls, Kim"),
                new XElement("title", "Midnight Rain"),
                new XElement("genre", "Fantasy"),
                new XElement("price", "5.95"),
                new XElement("publish_date", "2000-12-16"),
                new XElement("description", "A former architect battles corporate zombies, an evil sorceress, and her own childhood to become queen of the world.")
            )
        )
    );
    string xmlFilePath = "new_book_linq.xml";
    xmlDoc.Save(xmlFilePath);

    Console.WriteLine("Documento XML creado correctamente.");
}

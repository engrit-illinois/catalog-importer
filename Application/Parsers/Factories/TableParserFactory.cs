namespace Application.Parsers.Factories;
public static class TableParserFactory
{
    public static ITableParser GetParser(HtmlNode tableNode)
    {
        bool hasArea = tableNode.HasTableArea();
        bool hasAreaSubArea = tableNode.HasTableSubArea();
        HtmlNode? firstRowNode = tableNode.GetFirstRow();


        if (firstRowNode is not null
            && firstRowNode.InnerText.StartsWith("Students choose a primary and a secondary field"))
        {
            return new CivilEngineeringFieldsTableParser();
        }
        else if (hasArea)
        {
            return new SingleAreaLevelTableParser();
        }
        else if (hasAreaSubArea)
        {
            return new MultiAreaLevelTableParser();
        }

        return new SimpleTableParser();
    }
}

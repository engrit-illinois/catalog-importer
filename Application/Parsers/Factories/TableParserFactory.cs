namespace Application.Parsers.Factories;
public static class TableParserFactory
{
    public static ITableParser GetParser(HtmlNode tableNode)
    {
        bool hasArea = tableNode.HasTableArea();
        bool hasAreaSubArea = tableNode.HasTableSubArea();

        if (hasArea)
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

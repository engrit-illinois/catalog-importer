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
        else if (firstRowNode is not null
            && firstRowNode.InnerText.StartsWith("Students are required to complete 15 hours of credit from one track area listed below."))
        {
            return new BioengineeringTracksTableParser();
        }
        else if (firstRowNode is not null
            && firstRowNode.InnerText.StartsWith("From the Departmentally Approved List"))
        {
            return new ComputerEngineeringTrackTableParser();
        }
        else if (firstRowNode is not null
            && firstRowNode.ChildNodes[0].InnerText.EndsWith("Non-CS tech electives will not be considered in focus areas."))
        {
            return new ComputerScienceTechElectTableParser();
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

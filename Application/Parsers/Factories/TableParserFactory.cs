namespace Application.Parsers.Factories;
public static class TableParserFactory
{
    public static ITableParser GetParser(HtmlNode tableNode)
    {
        bool hasArea = tableNode.HasTableArea();
        bool hasAreaSubArea = tableNode.HasTableSubArea();
        HtmlNode? firstRowNode = tableNode.GetFirstRow();
        string? DegreeTitle = tableNode.OwnerDocument.DocumentNode.SelectSingleNode("//h1[@class='page-title']")?.InnerText.Trim();
        string? sectionTitle = tableNode.ParentNode?.InnerText.Trim();

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
        //else if (firstRowNode is not null
        //    && firstRowNode.ChildNodes[0].InnerText.Equals("Course List Code Title Hours Required Computer Science Courses:"))
        //{
        //    return new ComputerSciencePlusEconTableParser();
        //}
        else if (DegreeTitle == "Computer Science + Physics, BS"
                 && (new[] { "Computer Science Core", "Physics Core" }.Contains(sectionTitle))
        )
        {
            return new ComputerSciencePlusPhysicsCoreTableParser();
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

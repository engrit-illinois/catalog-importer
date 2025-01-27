namespace Application.Parsers.SectionParsers;
public class ComputerSciencePlusXSectionParser : ISectionParser
{
    public List<DegreeRequirementSection> Parse(HtmlNode requirementsNode)
    {
        var sectionTable = requirementsNode.ChildNodes.Where(n => n.Name == "table").FirstOrDefault();

        DegreeRequirementTable? parsedTable = null;

        if (requirementsNode.OwnerDocument.DocumentNode.SelectSingleNode("//h1[@class='page-title']")?.InnerText.Trim() == "Computer Science + Economics, BSLAS")
        {
            parsedTable = new ComputerSciencePlusEconTableParser().Parse(sectionTable);
        }
        else
        {
            parsedTable = new ComputerSciencePlusXTableParser().Parse(sectionTable);
        }


        return new()
        {
            new()
            {
                DegreeRequirementTables = new List<DegreeRequirementTable> { parsedTable }
            }
        };
    }
}

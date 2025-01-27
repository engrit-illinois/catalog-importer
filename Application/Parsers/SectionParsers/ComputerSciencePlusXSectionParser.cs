namespace Application.Parsers.SectionParsers;
public class ComputerSciencePlusXSectionParser : ISectionParser
{
    public List<DegreeRequirementSection> Parse(HtmlNode requirementsNode)
    {
        var sectionTable = requirementsNode.ChildNodes.Where(n => n.Name == "table").FirstOrDefault();
        var parsedTable = new ComputerSciencePlusXTableParser().Parse(sectionTable);

        return new()
        {
            new()
            {
                DegreeRequirementTables = new List<DegreeRequirementTable> { parsedTable }
            }
        };
    }
}

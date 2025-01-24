namespace Application.Parsers.SectionParsers;
public class DefaultSectionParser : ISectionParser
{
    public List<DegreeRequirementSection> Parse(HtmlNode requirementsNode)
    {
        var tableParser = new SimpleTableParser();
        return [];
    }
}

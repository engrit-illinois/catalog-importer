namespace Application.Parsers;
public class DefaultSectionParser : ISectionParser
{
    public List<DegreeRequirementSection> Parse(HtmlNode requirementsNode)
    {

        return [];
    }

    private DegreeRequirementList ParseTable(HtmlNode tableNode)
    {
        return new();
    }
}

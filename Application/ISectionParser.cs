namespace Application;
public interface ISectionParser
{
    List<DegreeRequirementSection> Parse(HtmlNode requirementsNode);
}

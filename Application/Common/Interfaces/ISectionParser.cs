namespace Application.Common.Interfaces;
public interface ISectionParser
{
    List<DegreeRequirementSection> Parse(HtmlNode requirementsNode);
}

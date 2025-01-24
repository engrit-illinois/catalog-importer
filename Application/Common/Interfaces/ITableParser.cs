namespace Application.Common.Interfaces;
public interface ITableParser
{
    DegreeRequirementTable Parse(HtmlNode tableNode);
}

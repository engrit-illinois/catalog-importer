namespace Application;
public interface ITableParser
{
    DegreeRequirementList Parse(HtmlNode tableNode);
}

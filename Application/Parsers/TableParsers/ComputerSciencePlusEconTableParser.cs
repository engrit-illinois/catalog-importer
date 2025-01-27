namespace Application.Parsers.TableParsers;

public class ComputerSciencePlusEconTableParser : BaseTableParser, ITableParser
{
    public DegreeRequirementTable Parse(HtmlNode tableNode)
    {
        var tableHours = GetTableSummaryHours(tableNode);
        var tableRowNodes = tableNode.GetTableRows(false);
        var areas = GetTableAreas(tableRowNodes, GetHeaderType);
        var table = new DegreeRequirementTable()
        {
            Hours = tableHours,
            DegreeRequirementTableAreas = areas
        };

        return table;
    }

    private static Func<HtmlNode, int> GetHeaderType = (HtmlNode row) =>
    {
        HtmlNode commentNode = row.SelectSingleNode($".//td[1]/div/span[contains(@class, '{s_commentClass}')]")
                                ?? row.SelectSingleNode($".//td[1]/span[contains(@class, '{s_commentClass}')]");
        bool isComment = commentNode != null;
        bool isIndentedComment = commentNode != null && commentNode.GetAttributeValue("class", "").Contains(s_commentIndentClass);

        if (isComment)
        {
            return 1;
        }

        return 0;
    };
}

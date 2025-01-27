namespace Application.Parsers.TableParsers;

public class ComputerSciencePlusXTableParser : BaseTableParser, ITableParser
{
    public DegreeRequirementTable Parse(HtmlNode tableNode)
    {
        var tableHours = GetTableSummaryHours(tableNode);
        var tableRowNodes = tableNode.GetTableRows(false);
        var areas = GetTableAreas(tableRowNodes, GetHeaderType);
        var table = new DegreeRequirementTable()
        {
            Title = tableHours > 0 ? "Requirements" : string.Empty,
            Hours = tableHours,
            DegreeRequirementTableAreas = areas
        };

        return table;
    }

    private static Func<HtmlNode, int> GetHeaderType = (HtmlNode row) =>
    {
        HtmlNode commentNode = row.SelectSingleNode($".//td[1]/div/span[contains(@class, '{s_commentClass}')]")
                                ?? row.SelectSingleNode($".//td[1]/span[contains(@class, '{s_commentClass}')]");
        bool isAreaHeader = row.GetAttributeValue("class", "").Contains(s_areaHeaderClass);
        bool isComment = commentNode != null;
        bool isTotalHours = row.ChildNodes[0].InnerText.Trim() == "Total Hours"; // CS + Advertising bottom "Total Hours" row

        if (isAreaHeader && !isTotalHours)
        {
            return 1;
        }

        if (isComment && !isTotalHours)
        {
            return 2;
        }

        return 0;
    };
}

﻿namespace Application.Parsers.TableParsers;

public class BioengineeringTracksTableParser : BaseTableParser, ITableParser
{
    public DegreeRequirementTable Parse(HtmlNode tableNode)
    {
        var tableComment = GetTableComment(tableNode);
        var tableHours = GetTableSummaryHours(tableNode);
        var skipFirstRow = tableComment is not null; // skip first row if was used in table comment above
        var tableRowNodes = tableNode.GetTableRows(skipFirstRow);
        var areas = GetTableAreas(tableRowNodes, GetHeaderType);
        var table = new DegreeRequirementTable()
        {
            Comment = tableComment,
            Hours = tableHours,
            DegreeRequirementTableAreas = areas
        };

        return table;
    }

    protected override string? GetTableComment(HtmlNode tableNode)
    {
        var firstRow = tableNode.TableBodyNode().ChildNodes.First(node => node.NodeType == HtmlNodeType.Element);
        return firstRow.ChildNodes[0].InnerText.Trim();
    }

    private static Func<HtmlNode, int> GetHeaderType = (HtmlNode row) =>
    {
        HtmlNode commentNode = row.SelectSingleNode($".//td[1]/div/span[contains(@class, '{s_commentClass}')]")
                                ?? row.SelectSingleNode($".//td[1]/span[contains(@class, '{s_commentClass}')]");
        HtmlNode hoursNode = row.SelectSingleNode($".//td[2][contains(@class, '{s_hoursColClass}')]");
        bool isHoursRow = hoursNode != null && !string.IsNullOrEmpty(hoursNode.InnerText);
        bool isFirstRow = row.GetAttributeValue("class", "").Contains(s_firstRowClass);
        bool isAreaHeader = row.GetAttributeValue("class", "").Contains(s_areaHeaderClass);
        bool isComment = commentNode != null;
        bool isIndentedComment = commentNode != null && commentNode.GetAttributeValue("class", "").Contains(s_commentIndentClass);

        if (isAreaHeader)
        {
            return 1;
        }
        else if (isComment)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    };
}

namespace Application.Parsers.TableParsers;
public class SingleAreaLevelTableParser : BaseTableParser, ITableParser
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

        bool isActuallyCourse = row.ChildNodes.Count == 2 && row.ChildNodes[0].InnerText.Trim() == "Any 400 level MATH course, excluding MATH 415, MATH 441, and MATH 442"; // Engineering Mechanics, Secondary Field Option Electives

        bool isLevel1 =
                (isAreaHeader && !isHoursRow && !isIndentedComment)
                || isAreaHeader && row.ChildNodes[0].InnerText.Trim().EndsWith(':') // skip Electrical Engineering Tech Electives, "Non-ECE courses from list below:". It should be second level
                || row.ChildNodes[0].InnerText.Trim() == "Human Factors" // Industrial Engineering, "Human Factors" row
                || (row.ChildNodes[0].InnerText.EndsWith("Electives") && !isHoursRow) // Nuclear Plasma...: Plasma & Fusion Science & Engineering, Professional Concentration Area, "Electives" rows
                || row.ChildNodes[0].InnerText.Trim().EndsWith("Primary Field"); // Env Engineering "Primary Fields" table area headers

        return isLevel1 ? 1 : default;
    };
}

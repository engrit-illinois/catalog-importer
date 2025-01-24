namespace Application.Parsers.TableParsers;

public class SimpleTableParser : BaseTableParser, ITableParser
{
    public DegreeRequirementTable Parse(HtmlNode tableNode)
    {
        var tableBodyNode = tableNode.SelectSingleNode(".//tbody");
        var tableRowNodes = tableBodyNode.ChildNodes
            .Where(node => node.NodeType == HtmlNodeType.Element && node.Name == "tr");

        var tableArea = new DegreeRequirementTableArea
        {
            DegreeRequirementCourses = new List<DegreeRequirementCourse>()
        };

        foreach (var row in tableRowNodes)
        {
            bool hasAllCourseColumns = row.SelectNodes(".//td").Count == CatalogInfo.courseRowColumnCount;
            bool hasSkippableClass = new[] { s_areaHeaderClass, s_commentClass }.Contains(row.GetAttributeValue("class", ""));

            // Skip comments and area headers in case we missed them before now
            if (!hasAllCourseColumns || hasSkippableClass)
            {
                continue;
            }

            var courseRow = CourseRowParser.Parse(row);
            tableArea.DegreeRequirementCourses.Add(courseRow);
        }

        var table = new DegreeRequirementTable
        {
            //DegreeRequirementTableAreas = new List<DegreeRequirementTableArea> { tableArea }
            Hours = GetTableSummaryHours(tableNode),
            DegreeRequirementTableAreas = [tableArea]
        };

        return table;
    }
}

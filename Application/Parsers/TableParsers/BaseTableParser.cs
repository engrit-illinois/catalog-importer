
namespace Application.Parsers.TableParsers;
public abstract class BaseTableParser
{
    internal const string s_areaHeaderClass = "areaheader";
    internal const string s_commentClass = "courselistcomment";
    internal const string s_commentIndentClass = "commentindent";
    internal const string s_orClass = "orclass";
    internal const string s_firstRowClass = "firstrow";
    internal const string s_lastRowClass = "lastrow";
    internal const string s_hoursColClass = "hourscol";
    internal const string s_summaryRowClass = "listsum";
    internal const int courseRowColumnCount = 3;
    internal static readonly string[] s_tableTitleElements = ["h3", "h4", "h5", "h6"];

    protected static byte GetTableSummaryHours(HtmlNode tableNode)
    {
        byte hours = 0;
        HtmlNode? hoursNode = null;
        var tableBody = tableNode.SelectSingleNode(".//tbody");
        var firstRow = tableBody.ChildNodes.First(node => node.NodeType == HtmlNodeType.Element);
        var lastRow = tableBody.ChildNodes.Last(node => node.NodeType == HtmlNodeType.Element);

        if (lastRow.HasClass(s_summaryRowClass))
        {
            hoursNode = lastRow.LastChild;
        }
        else if (lastRow.HasClass(s_areaHeaderClass) && lastRow.ChildNodes[1].HasClass(s_hoursColClass))
        {
            hoursNode = lastRow.ChildNodes[1];
        }
        else if (lastRow.ChildNodes[1].HasClass(s_hoursColClass))
        {
            hoursNode = lastRow.ChildNodes[1];
        }
        else if (firstRow.HasClass(s_firstRowClass) && firstRow.ChildNodes[0].FirstChild.HasClass(s_commentClass))
        {
            hoursNode = firstRow.ChildNodes[1];
        }

        //TODO: handle range, such as 0-15 from AE gen ed requirements table

        if (hoursNode != null && hoursNode.InnerText.Trim() is not "")
        {
            _ = byte.TryParse(hoursNode.InnerText.Trim(), out hours);
        }

        return hours;
    }

    protected static string? GetTableComment(HtmlNode tableNode)
    {
        var tableBody = tableNode.SelectSingleNode(".//tbody");
        var firstRow = tableBody.ChildNodes.First(node => node.NodeType == HtmlNodeType.Element);
        HtmlNode? instructionNode = null;

        // ex. Aerospace Engineering Technical Electives table with top comment and hours
        if (firstRow.HasClass(s_firstRowClass)
            && firstRow.ChildNodes[0].FirstChild.HasClass(s_commentClass)
            && firstRow.ChildNodes[1].HasClass(s_hoursColClass)
        )
        {
            instructionNode = firstRow.ChildNodes[0];
        }

        return instructionNode?.InnerText.Trim();
    }


    /* Examples to check:
     * http://catalog.illinois.edu/undergraduate/engineering/bioengineering-bs
     * http://catalog.illinois.edu/undergraduate/eng_aces/agricultural-biological-engineering-bs/sustainable-ecological-environmental-systems-engineering
     * http://catalog.illinois.edu/undergraduate/engineering/civil-engineering-bs
     * http://catalog.illinois.edu/undergraduate/engineering/computer-engineering-bs table 4
     */
    public static List<DegreeRequirementTableArea> GetTableAreas(
        HtmlNodeCollection rows, Func<HtmlNode, int> areaHeaderLevelFunc)
    {
        List<DegreeRequirementTableArea> lists = [];
        DegreeRequirementTableArea? currentLevel1 = null;
        DegreeRequirementTableArea? currentLevel2 = null;
        DegreeRequirementTableArea? currentLevel3 = null;
        DegreeRequirementTableArea? currentLevel4 = null;

        foreach (var row in rows)
        {
            DegreeRequirementTableArea list = new();
            int level = areaHeaderLevelFunc(row);

            //if (level > 0)
            //{
            //    list.Title = row.SelectSingleNode(".//td[1]")?.InnerText.Trim() ?? "";
            //    list.Hours = CourseRowParser.GetRowCreditHours(row);
            //}

            if (level == 1)
            {
                list.Title = row.SelectSingleNode(".//td[1]")?.InnerText.Trim() ?? "";
                list.Hours = CourseRowParser.GetRowCreditHours(row);
                lists.Add(currentLevel1 = list);
                currentLevel2 = currentLevel3 = currentLevel4 = null;
            }
            else if (level == 2)
            {
                //list.TypeId = ListCategory.SubArea;
                currentLevel1?.ChildAreas.Add(currentLevel2 = list);
                currentLevel3 = currentLevel4 = null;
            }
            else if (level == 3)
            {
                //list.TypeId = ListCategory.SubAreaSection;
                currentLevel2?.ChildAreas.Add(currentLevel3 = list);
                currentLevel4 = null;
            }
            else if (level == 4)
            {
                //list.TypeId = ListCategory.SubSubAreaSection;
                currentLevel3?.ChildAreas.Add(currentLevel4 = list);
            }
            // Course rows only, skip comments for now
            else if (
                row.ChildNodes.Count == courseRowColumnCount
                && row.ChildNodes[0].InnerText.Trim() != "Total Hours" // NPRE: Plasma & Fusion Science & Engineering, "Professional Concentration Area" table uses a course comment at the bottom instead of "listsum" class
            )
            {
                var course = CourseRowParser.Parse(row);
                (currentLevel4 ?? currentLevel3 ?? currentLevel2 ?? currentLevel1)?.DegreeRequirementCourses.Add(course);
            }
        }

        return lists;
    }

}

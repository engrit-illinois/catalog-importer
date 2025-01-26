namespace Application.Parsers.TableParsers;

public class CivilEngineeringFieldsTableParser : BaseTableParser, ITableParser
{
    public DegreeRequirementTable Parse(HtmlNode tableNode)
    {
        var tableComment = GetTableComment(tableNode);
        var tableHours = GetTableSummaryHours(tableNode);
        var skipFirstRow = tableComment is not null; // skip first row if was used in table comment above
        var tableRowNodes = tableNode.GetTableRows(skipFirstRow);

        // break down the table into primary, secondary, and general civil engineering option for custom handling

        var fieldRows = GetFieldRows(tableRowNodes);
        var generalCivilEngOptionRows = GetGeneralCivilEngineeringOptionRows(tableRowNodes);

        var fieldDegreeRequirements = GetTableAreas(fieldRows, GetPrimarySecondaryHeaderType);
        var generalCivilEngOptionDegreeRequirements = GetTableAreas(generalCivilEngOptionRows, GetGeneralCivilEngOptionHeaderType);

        fieldDegreeRequirements.AddRange(generalCivilEngOptionDegreeRequirements);

        var table = new DegreeRequirementTable()
        {
            Comment = tableComment,
            Hours = tableHours,
            DegreeRequirementTableAreas = fieldDegreeRequirements
        };

        return table;
    }

    private static HtmlNodeCollection GetFieldRows(HtmlNodeCollection rows)
    {
        var primaryFieldRow = rows.FirstOrDefault(x => x.InnerText.StartsWith("Primary Field"));
        var primaryFieldRows = new HtmlNodeCollection(null);
        var currentRow = primaryFieldRow;

        while (currentRow != null && !currentRow.InnerText.StartsWith("The General Civil Engineering Option"))
        {
            primaryFieldRows.Add(currentRow);
            currentRow = currentRow.NextSibling;
        }

        return primaryFieldRows;
    }

    private static HtmlNodeCollection GetGeneralCivilEngineeringOptionRows(HtmlNodeCollection rows)
    {
        var generalCivilEngineeringOptionRow = rows.FirstOrDefault(x => x.InnerText.Contains("The General Civil Engineering Option"));
        var generalCivilEngineeringOptionRows = new HtmlNodeCollection(null);
        var currentRow = generalCivilEngineeringOptionRow;

        while (currentRow != null)
        {
            generalCivilEngineeringOptionRows.Add(currentRow);
            currentRow = currentRow.NextSibling;
        }

        return generalCivilEngineeringOptionRows;
    }

    private static Func<HtmlNode, int> GetPrimarySecondaryHeaderType = (HtmlNode row) =>
    {
        HtmlNode commentNode = row.SelectSingleNode($".//td[1]/div/span[contains(@class, '{s_commentClass}')]");
        HtmlNode hoursNode = row.SelectSingleNode($".//td[2][contains(@class, '{s_hoursColClass}')]");
        bool isHoursRow = hoursNode != null && !string.IsNullOrEmpty(hoursNode.InnerText);
        bool isAreaHeader = row.GetAttributeValue("class", "").Contains(s_areaHeaderClass);
        bool isComment = commentNode != null;
        bool isIndentedComment = commentNode != null && commentNode.GetAttributeValue("class", "").Contains(s_commentIndentClass);

        if (new[] { "Primary Field", "Secondary Field" }.Any(x => row.InnerText.StartsWith(x)))
        {
            return 1;
        }
        else if (new[] { "Primary", "Secondary" }.Any(x => row.InnerText.EndsWith(x)))
        {
            return 2;
        }
        // Excluded indented comments because they are 4th level, such as "Area 1 - Facilities:"
        else if (isAreaHeader && row.InnerText.Contains("course", StringComparison.CurrentCultureIgnoreCase))
        {
            return 3;
        }
        else if (isAreaHeader || isIndentedComment)
        {
            return 4;
        }

        return default;
    };

    private static Func<HtmlNode, int> GetGeneralCivilEngOptionHeaderType = (HtmlNode row) =>
    {
        HtmlNode commentNode = row.SelectSingleNode($".//td[1]/div/span[contains(@class, '{s_commentClass}')]");
        bool isAreaHeader = row.GetAttributeValue("class", "").Contains(s_areaHeaderClass);
        bool isIndentedComment = commentNode != null && commentNode.GetAttributeValue("class", "").Contains(s_commentIndentClass);

        if (new[] { "The General Civil Engineering Option" }.Any(x => row.InnerText.StartsWith(x, StringComparison.CurrentCultureIgnoreCase)))
        {
            return 1;
        }
        else if (!isIndentedComment && new[] { "Electives", "Courses" }.Any(x => row.InnerText.Contains(x, StringComparison.CurrentCultureIgnoreCase)))
        {
            return 2;
        }
        else if (isAreaHeader || row.InnerText.Contains("courses from list below", StringComparison.CurrentCultureIgnoreCase))
        {
            return 3;
        }

        return default;
    };
}

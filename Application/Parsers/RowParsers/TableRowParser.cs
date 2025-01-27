using Application.Common.Utilities;

namespace Application.Parsers.CourseParsers;
public class TableRowParser
{

    public static DegreeRequirementCourse Parse(HtmlNode rowNode)
    {
        return ParseCourseRow(rowNode);
    }

    public static (byte minHours, byte maxHours) GetRowCreditHours(HtmlNode rowNode)
    {
        var hoursNode = rowNode.ChildNodes
                                .FirstOrDefault(node =>
                                    node.NodeType == HtmlNodeType.Element
                                    && node.Name == "td"
                                    && node.GetAttributeValue("class", "").Contains(CatalogInfo.s_hoursColClass)
                                );

        if (hoursNode != null)
        {
            var hourRange = HoursHelper.FromText(hoursNode.InnerText.Trim());

            if (hourRange.minHours == 0 && hourRange.maxHours == 0)
            {
                // Try getting from row text instead, such as for "CS + Chemistry" table
                var minHoursRegex = new Regex(@"(\d+)\s*hours(?:\s+required)?\s*$");
                var minHoursMatch = minHoursRegex.Match(rowNode.FirstChild.InnerText.Trim());

                if (minHoursMatch.Success)
                {
                    hourRange = HoursHelper.FromText(minHoursMatch.Groups[1].Value);
                }
            }

            return hourRange;
        }

        return default;
    }

    protected static DegreeRequirementCourse ParseCourseRow(HtmlNode row, HtmlNode? alternativeCourseRow = null)
    {
        var cells = row.SelectNodes(".//td");

        if (cells == null || cells.Count != 3)
        {
            throw new Exception("");
        }

        var codeCol = cells[0];
        var titleCol = cells[1];
        var hoursCol = cells[2];

        (byte minHours, byte maxHours) = HoursHelper.FromText(hoursCol.InnerText.Trim());

        var requirement = new DegreeRequirementCourse()
        {
            Description = titleCol.InnerText.Trim().Split("<br>")[0], // nuclear-plasma-radiological-engineering-bs "ME 310"
            HoursMin = minHours,
            HoursMax = maxHours,
            AltCourse1 = null,
            AltCourse2 = null,
            AltCourse3 = null,
            AltCourse4 = null
        };

        // Row may list multiple courses in the 'codecol' as links, so get them all
        var courseCodes = codeCol.SelectNodes(".//a[@href]");

        // if codeCol has one or more code links
        if (courseCodes is not null && courseCodes.Count > 0)
        {
            (string subjectCode, int courseNumber) = CourseCodeHelper.FromString(courseCodes[0].InnerText.Trim());
            requirement.SubjectCode = subjectCode;
            requirement.CourseNumber = courseNumber.ToString();

            if (courseCodes.Count > 1)
            {
                requirement.AltCourse1 = courseCodes[1].InnerText.Trim();
                requirement.AltCourse1Description = titleCol.SelectSingleNode("span[@class='blockindent'][1]")?.InnerText.Trim() ?? "";

                // determine operand
                var courseColValue = codeCol.InnerText.Trim();

                if (courseColValue.Contains("or "))
                {
                    requirement.AltQuantifier = "or";
                }
                else if (courseColValue.Contains('&'))
                {
                    requirement.AltQuantifier = "&";
                }

                // When the title includes multiple courses, only get the first (which is Text)
                requirement.Description = titleCol.SelectSingleNode(".//text()[not(parent::span[@class='blockindent'])]")?.InnerText.Trim() ?? "";
            }

            if (courseCodes.Count > 2)
            {
                requirement.AltCourse2 = courseCodes[2].InnerText.Trim();
                requirement.AltCourse1Description = titleCol.SelectSingleNode("span[@class='blockindent'][2]")?.InnerText.Trim() ?? "";
            }
        }
        else // codecol is single link or plan text
        {
            (string subjectCode, int courseNumber) = CourseCodeHelper.FromString(codeCol.InnerText.Trim());
            requirement.SubjectCode = subjectCode;
            requirement.CourseNumber = courseNumber.ToString();
        }

        var firstSibling = row.GetNextTableRow();
        //var secondSibling = row.SelectSingleNode($"following-sibling::tr[2]");
        //var thirdSibling = row.SelectSingleNode($"following-sibling::tr[2]");
        //var fourthSibling = row.SelectSingleNode($"following-sibling::tr[2]");

        if (firstSibling != null && firstSibling.HasClass(CatalogInfo.s_orClass))
        {
            string codeColPath = ".//td[contains(@class, 'codecol')]";
            string titleColPath = ".//td[2]";

            requirement.AltCourse1 = firstSibling.SelectSingleNode(codeColPath).InnerText.Trim();
            requirement.AltCourse1Description = firstSibling.SelectSingleNode(titleColPath).InnerText.Trim();

            var secondSibling = firstSibling.GetNextTableRow();

            // only use the second sibling if it's directly after another "orclass", so we know it goes with the main course
            if (secondSibling != null && secondSibling.HasClass(CatalogInfo.s_orClass))
            {
                requirement.AltCourse2 = secondSibling.SelectSingleNode(codeColPath).InnerText.Trim();
                requirement.AltCourse2Description = secondSibling.SelectSingleNode(titleColPath).InnerText.Trim();

                var thirdSibling = secondSibling.GetNextTableRow();

                // CS + Crop Science, MATH 225 has three alts
                if (thirdSibling != null && thirdSibling.HasClass(CatalogInfo.s_orClass))
                {
                    requirement.AltCourse3 = thirdSibling.SelectSingleNode(codeColPath).InnerText.Trim();
                    requirement.AltCourse3Description = thirdSibling.SelectSingleNode(titleColPath).InnerText.Trim();

                    var fourthSibling = thirdSibling.GetNextTableRow();

                    if (fourthSibling != null && fourthSibling.HasClass(CatalogInfo.s_orClass))
                    {
                        requirement.AltCourse4 = fourthSibling.SelectSingleNode(codeColPath).InnerText.Trim();
                        requirement.AltCourse4Description = fourthSibling.SelectSingleNode(titleColPath).InnerText.Trim();
                    }
                }
            }
        }

        if (alternativeCourseRow != null)
        {
            requirement.AltCourse1 = alternativeCourseRow.SelectSingleNode(".//td[contains(@class, 'codecol')]").InnerText.Trim();
        }

        // try getting alternatives from the title/description if alt courses are all in one row
        if (alternativeCourseRow == null && requirement.AltCourse1 is null)
        {
            var courseCodesFromDecription = titleCol
                .SelectNodes(".//a[@href]")
                ?.Where(x => x.InnerText != $"{requirement.SubjectCode} {requirement.CourseNumber}")
                .Select(x => x.InnerText)
                .Distinct()
                .ToArray();

            for (var i = 0; i < courseCodesFromDecription?.Count(); i++)
            {
                switch (i)
                {
                    case 0:
                        requirement.AltCourse1 = courseCodesFromDecription[i];
                        break;
                    case 1:
                        requirement.AltCourse2 = courseCodesFromDecription[i];
                        break;
                    case 2:
                        requirement.AltCourse3 = courseCodesFromDecription[i];
                        break;
                    case 3:
                        requirement.AltCourse4 = courseCodesFromDecription[i];
                        break;
                }
            }
        }

        return requirement;
    }
}

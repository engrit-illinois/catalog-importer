using Application.Importers;

namespace Application.Common.Utilities;
public static class ImporterHelper
{
    internal const string s_areaHeaderClass = "areaheader";
    internal const string s_commentClass = "courselistcomment";
    internal const string s_commentIndentClass = "commentindent";
    internal const string s_orClass = "orclass";
    internal const string s_firstRowClass = "firstrow";
    internal const string s_lastRowClass = "lastrow";
    internal const string s_hoursColClass = "hourscol";
    internal const int courseRowColumnCount = 3;
    internal static readonly string[] s_tableTitleElements = ["h3", "h4", "h5", "h6"];

    public static async Task<HtmlDocument?> LoadDocument(string url)
    {
        using HttpClient client = new();

        HttpResponseMessage response = await client.GetAsync(url);
        HtmlDocument doc = new();

        if (response.IsSuccessStatusCode)
        {
            string html = await response.Content.ReadAsStringAsync();
            html = HtmlEntity.DeEntitize(html);

            html = html.Replace("\u00A0", " ").Replace("\u0026", "&");
            doc.LoadHtml(html);
            return doc;
        }

        return null;
    }

    public static async Task<HtmlNode?> LoadRequirementsDocument(string url)
        => (await LoadDocument(url))?.DocumentNode.SelectSingleNode($"//div[@id='{CatalogInfo.HtmlDegreeContainerId}']");

    public static IDegreeImporter CreateImporter()
    {
        var type = typeof(CEE_Importer_2024);
        return CreateImporter(type);
    }

    public static IDegreeImporter CreateImporter(Type type)
    {
        return (IDegreeImporter)Activator.CreateInstance(type);
    }

    internal static IReadOnlyCollection<DegreeRequirementSection> GetDegreeSections(HtmlNode requirementsNode)
    {
        var sections = new List<DegreeRequirementSection>();

        var sectionHeadings = requirementsNode.GetHtmlSectionHeadings();

        foreach (var htmlSectionHeading in sectionHeadings)
        {
            var section = new DegreeRequirementSection
            {
                Title = htmlSectionHeading.InnerText
            };

            var sectionInstructionNodes = htmlSectionHeading.GetHtmlSectionInstructions();

            section.Instructions = string.Join(" ", sectionInstructionNodes.Select(x => x.InnerText));

            var sectionTables = htmlSectionHeading.GetHtmlSectionTables();

            if (sectionTables != null)
            {
                foreach (var table in sectionTables)
                {
                    var reqList = new DegreeRequirementList()
                    {
                        Title = "",

                    };

                    section.DegreeRequirementList.Add(new DegreeRequirementList() { Title = table.InnerText });
                    //section.DegreeRequirementList.AddRange(table.GetDegreeRequirementList());
                }
            }

            sections.Add(section);
        }

        return sections;
    }

    internal static DegreeRequirementCourse HandleCourseRow(HtmlNode row, HtmlNode? alternativeCourseRow = null)
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
            AltCourse3 = null
        };

        // Row may list multiple courses in the 'codecol' as links, so get them all
        var courseCodes = codeCol.SelectNodes(".//a[@href]");

        // if codeCol has one or more code links
        if (courseCodes is not null && courseCodes.Count != 0)
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
        else // codecol is plan text
        {
            (string subjectCode, int courseNumber) = CourseCodeHelper.FromString(codeCol.InnerText.Trim());
            requirement.SubjectCode = subjectCode;
            requirement.CourseNumber = courseNumber.ToString();
        }

        var firstSibling = row.SelectSingleNode($"following-sibling::tr[1]");
        var secondSibling = row.SelectSingleNode($"following-sibling::tr[2]");

        if (firstSibling != null && firstSibling.GetAttributeValue("class", "").Contains(s_orClass))
        {
            string codeColPath = ".//td[contains(@class, 'codecol')]";
            string titleColPath = ".//td[2]";

            requirement.AltCourse1 = firstSibling.SelectSingleNode(codeColPath).InnerText.Trim();
            requirement.AltCourse1Description = firstSibling.SelectSingleNode(titleColPath).InnerText.Trim();

            // only use the second sibling if it's directly after another "orclass", so we know it goes with the main course
            if (secondSibling != null && secondSibling.GetAttributeValue("class", "").Contains(s_orClass))
            {
                requirement.AltCourse2 = secondSibling.SelectSingleNode(codeColPath).InnerText.Trim();
                requirement.AltCourse2Description = secondSibling.SelectSingleNode(titleColPath).InnerText.Trim();
            }
        }

        if (alternativeCourseRow != null)
        {
            requirement.AltCourse1 = alternativeCourseRow.SelectSingleNode(".//td[contains(@class, 'codecol')]").InnerText.Trim();
        }

        // try getting alternatives from the title/description
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
                }
            }
        }

        return requirement;
    }

}

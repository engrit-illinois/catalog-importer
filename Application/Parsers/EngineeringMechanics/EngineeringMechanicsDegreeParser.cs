namespace Application.Parsers.EngineeringMechanics;
internal class EngineeringMechanicsDegreeParser : ISectionParser
{
    public List<DegreeRequirementSection> Parse(HtmlNode requirementsNode)
    {
        var sections = new List<DegreeRequirementSection>();
        var sectionNodes = requirementsNode.GetHtmlSectionHeadings();

        foreach (var sectionNode in sectionNodes.Where(x => x.InnerText.Trim() != "General Education Requirements"))
        {
            var instructionNodes = sectionNode.GetHtmlSectionInstructions().Select(x => x.InnerText);
            var section = new DegreeRequirementSection
            {
                Title = sectionNode.InnerText,
                Instructions = string.Join(" ", instructionNodes),
            };

            var sectionTables = sectionNode.GetHtmlSectionTables();

            foreach (var sectionTable in sectionTables)
            {
                DegreeRequirementTable? table;

                if (section.Title == "Secondary Field Option Electives")
                {
                    table = new EngineeringMechanicsFieldElectivesTableParser().Parse(sectionTable);
                }
                else
                {
                    table = TableParserFactory.GetParser(sectionTable).Parse(sectionTable);
                }

                section.DegreeRequirementTables.Add(table);
            }

            sections.Add(section);
        }

        return sections;
    }
}

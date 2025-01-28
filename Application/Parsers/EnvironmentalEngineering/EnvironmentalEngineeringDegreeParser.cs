namespace Application.Parsers.EngineeringMechanics;
internal class EnvironmentalEngineeringDegreeParser : ISectionParser
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
                var table = new SingleAreaLevelTableParser().Parse(sectionTable);
                section.DegreeRequirementTables.Add(table);
            }

            sections.Add(section);
        }

        return sections;
    }
}

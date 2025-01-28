using Application.Common.Utilities;
using Application.Parsers.EngineeringMechanics;

namespace Application;
public class RequirementProcessor(PageScraper scraper, SectionParserFactory factory)
{
    private readonly PageScraper _scraper = scraper;
    private readonly SectionParserFactory _factory = factory;

    public async Task<DegreeEntry> Process(DegreeEntry degree)
    {
        var catalogUrl = CatalogHelper.GetCurrentUgradUrl(degree.CatalogUrl);
        var html = await _scraper.GetDegreeRequirementsNode(catalogUrl);

        //degree.DegreeRequirementSections = _factory.GetParser(degree).Parse(html);

        ISectionParser degreeParser;

        if (degree.MajorCode == "0118")
        {
            degreeParser = new EngineeringMechanicsDegreeParser();
        }
        else if (degree.MajorCode == "1233")
        {
            degreeParser = new EnvironmentalEngineeringDegreeParser();
        }
        else
        {
            degreeParser = _factory.GetParser(degree);
        }

        degree.DegreeRequirementSections = degreeParser.Parse(html);


        return degree;
    }
}

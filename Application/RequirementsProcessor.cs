using Application.Common.Utilities;
using Application.Parsers;

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

        if (degree.MajorCode == "0118" && degree.CatalogYear == 2024)
        {
            var degreeParser = new EngineeringMechanicsDegreeParser();
            degree.DegreeRequirementSections = degreeParser.Parse(html);
        }
        else
        {
            degree.DegreeRequirementSections = _factory.GetParser(degree).Parse(html);
        }

        return degree;
    }
}

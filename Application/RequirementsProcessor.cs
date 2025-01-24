using Application.Common.Utilities;
using Application.Parsers.Factories;

namespace Application;
public class RequirementProcessor(PageScraper scraper, SectionParserFactory factory)
{
    private readonly PageScraper _scraper = scraper;
    private readonly SectionParserFactory _factory = factory;

    public async Task<DegreeEntry> Process(DegreeEntry degree)
    {
        var catalogUrl = CatalogHelper.GetCurrentUgradUrl(degree.CatalogUrl);
        var html = await _scraper.GetDegreeRequirementsNode(catalogUrl);
        degree.DegreeRequirementSections = _factory.GetParser(degree).Parse(html);

        return degree;
    }
}

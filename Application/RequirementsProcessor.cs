using Application.Parsers;

namespace Application;
public class RequirementProcessor(PageScraper scraper, SectionParserFactory factory)
{
    private readonly PageScraper _scraper = scraper;
    private readonly SectionParserFactory _factory = factory;

    public async Task Process(string url)
    {
        var html = await _scraper.GetDegreeRequirementsNode(url);
        var parser = _factory.GetParser(url, html);
        var sections = parser.Parse(html);
    }
}

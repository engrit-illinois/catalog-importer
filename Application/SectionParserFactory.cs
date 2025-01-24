using Application.Parsers;

namespace Application;
public class SectionParserFactory
{
    public ISectionParser GetParser(string url, HtmlNode html)
    {
        //if (url.Contains("format-a"))
        //    return new FormatASectionParser();

        //if (url.Contains("format-b"))
        //    return new FormatBSectionParser();


        return new DefaultSectionParser();
    }
}

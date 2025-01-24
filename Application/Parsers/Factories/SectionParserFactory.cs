using Application.Common.Interfaces;

namespace Application.Parsers.Factories;
public class SectionParserFactory
{
    public ISectionParser GetParser(DegreeEntry degree)
    {
        //if(degree.CatalogYear == 2024 && degree.MajorCode == "0106")
        //{
        //    return new CivilEngineering2024SectionParser();
        //}

        return new DefaultSectionParser();
    }
}

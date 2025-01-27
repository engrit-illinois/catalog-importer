namespace Application.Parsers.Factories;
public class SectionParserFactory
{

    public ISectionParser GetParser(DegreeEntry degree)
    {
        string[] csPlusXMajorCodes = ["5864", "5348", "5349", "6151", "5350", "5623", "5667",];

        if (degree.CatalogYear == 2024 && csPlusXMajorCodes.Contains(degree.MajorCode))
        {
            return new ComputerSciencePlusXSectionParser();
        }

        return new DefaultSectionParser();
    }
}

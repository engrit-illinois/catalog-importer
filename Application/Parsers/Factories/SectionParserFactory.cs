namespace Application.Parsers.Factories;
public class SectionParserFactory
{

    public ISectionParser GetParser(DegreeEntry degree)
    {
        string[] csPlusXNonEngrMajorCodes = ["5348", "5349", "5350", "5623", "5667", "5673", "5864"];

        if (degree.CatalogYear == 2024 && csPlusXNonEngrMajorCodes.Contains(degree.MajorCode))
        {
            return new ComputerSciencePlusXSectionParser();
        }

        return new DefaultSectionParser();
    }
}

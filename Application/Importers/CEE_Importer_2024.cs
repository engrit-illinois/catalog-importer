using Application.Common.Utilities;

namespace Application.Importers;

internal class CEE_Importer_2024 : IDegreeImporter
{
    public async Task<IReadOnlyCollection<DegreeRequirementSection>> ImportDegreeRequirements(DegreeEntry degreeEntry)
    {
        var catalogUrl = CatalogHelper.GetCurrentUgradUrl(degreeEntry.CatalogUrl);
        HtmlNode? requirementsTab = await ImporterHelper.LoadRequirementsDocument(catalogUrl);

        var requirementSections = ImporterHelper.GetDegreeSections(requirementsTab);

        return new List<DegreeRequirementSection>();
    }
}

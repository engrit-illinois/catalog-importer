using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Degrees;

public class DegreeModel(RequirementProcessor requirementProcessor) : PageModel
{
    private readonly RequirementProcessor _requirementProcessor = requirementProcessor;

    [BindProperty(SupportsGet = true)]
    public string AcademicLevel { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public int Year { get; set; }

    [BindProperty(SupportsGet = true)]
    public string CatalogPath { get; set; } = string.Empty;

    public DegreeEntry? DegreeEntryResult { get; set; } = new();

    public string? DegreeRequirementJsonResults
        => JsonSerializer.Serialize(DegreeEntryResult, new JsonSerializerOptions { WriteIndented = true });

    public string CatalogDegreeUrl => $"{CatalogInfo.CatalogUgradDegreePath}/{CatalogPath}";

    public async Task OnGetAsync()
    {
        var degreeEntry = DegreePrograms.AllDepartments.
            SelectMany(x => x.DegreeEntries)
            .First(d => d.CatalogUrl == CatalogPath && d.CatalogYear == Year);
        DegreeEntryResult = await _requirementProcessor.Process(degreeEntry);
    }
}

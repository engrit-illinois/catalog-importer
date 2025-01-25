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
    public string Year { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string CatalogPath { get; set; } = string.Empty;

    public DegreeEntry? DegreeEntryResult { get; set; } = new();

    public string? DegreeRequirementJsonResults
        => JsonSerializer.Serialize(DegreeEntryResult, new JsonSerializerOptions { WriteIndented = true });

    public string CatalogDegreeUrl => $"{CatalogInfo.CatalogUgradDegreePath}/{CatalogPath}";

    public async Task OnGetAsync()
    {
        var degreeEntry = new DegreeEntry
        {
            CatalogUrl = CatalogPath,
            CatalogYear = int.Parse(Year),
        };

        DegreeEntryResult = await _requirementProcessor.Process(degreeEntry);
    }
}

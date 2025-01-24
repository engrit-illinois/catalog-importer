
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entities;
public class DegreeEntry
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string MajorCode { get; set; } = string.Empty;
    public string? ConcentrationCode { get; set; }
    public int CatalogYear { get; set; }
    public string CatalogUrl { get; set; } = string.Empty;

    public virtual ICollection<DegreeRequirementSection> DegreeRequirementSections { get; set; } = [];

    [NotMapped]
    public Type? DegreeImporterType { get; internal set; } = null!;
}

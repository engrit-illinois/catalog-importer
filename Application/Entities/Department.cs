namespace Application.Entities;

public class Department
{
    public string Name { get; set; } = string.Empty;
    public string CatalogKey { get; set; } = string.Empty;
    public List<DegreeEntry> DegreeEntries { get; set; } = [];
}

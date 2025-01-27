namespace Application.Entities;

public class Department
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<DegreeEntry> DegreeEntries { get; set; } = [];
}

namespace Application.Entities;
public class DegreeRequirementSectionTable
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int TypeId { get; set; }

    public int DegreeRequirementSectionId { get; set; }

    public virtual DegreeRequirementSection DegreeRequirementSection { get; set; } = null!;
    public virtual ICollection<DegreeRequirement
}

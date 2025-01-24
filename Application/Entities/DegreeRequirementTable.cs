namespace Application.Entities;

public class DegreeRequirementTable
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public byte? Hours { get; set; }
    public int TypeId { get; set; }
    public int DegreeRequirementSectionId { get; set; }

    public virtual DegreeRequirementSection DegreeRequirementSection { get; set; } = null!;
    public virtual ICollection<DegreeRequirementTableList> DegreeRequirementTableLists { get; set; } = [];
}

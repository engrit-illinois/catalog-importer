namespace Application.Entities;

public class DegreeRequirementTableList
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public byte? Hours { get; set; }
    public int TypeId { get; set; }
    public int? ParentTableListId { get; set; }

    public int DegreeRequirementTableId { get; set; }

    public virtual DegreeRequirementTable DegreeRequirementTable { get; set; } = null!;
    public virtual DegreeRequirementTableList? ParentTableList { get; set; }
    public virtual ICollection<DegreeRequirementCourse> DegreeRequirementCourses { get; set; } = [];
    public virtual ICollection<DegreeRequirementTableList> ChildLists { get; set; } = [];
}

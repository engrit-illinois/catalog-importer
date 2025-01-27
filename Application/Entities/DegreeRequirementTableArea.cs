namespace Application.Entities;

public class DegreeRequirementTableArea
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public byte? HoursMin { get; set; }
    public byte? HoursMax { get; set; }
    public int TypeId { get; set; }
    public int? ParentTableAreaId { get; set; }

    public int DegreeRequirementTableId { get; set; }

    public virtual DegreeRequirementTable DegreeRequirementTable { get; set; } = null!;
    public virtual DegreeRequirementTableArea? ParentTableList { get; set; }
    public virtual ICollection<DegreeRequirementCourse> DegreeRequirementCourses { get; set; } = [];
    public virtual ICollection<DegreeRequirementTableArea> ChildAreas { get; set; } = [];
}

namespace Application.Entities;

public class DegreeRequirementList
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public byte? Hours { get; set; }
    public int TypeId { get; set; }
    public int? ParentId { get; set; }
    public int? GroupId { get; set; }

    public int DegreeRequirementSectionId { get; set; }

    public List<DegreeRequirementCourse> DegreeRequirementCourses { get; set; } = [];
    public List<DegreeRequirementList> ChildLists { get; set; } = [];

}

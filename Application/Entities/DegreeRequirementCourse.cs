namespace Application.Entities;

public partial class DegreeRequirementCourse
{
    public int Id { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string CourseNumber { get; set; } = null!;

    public string? Description { get; set; }

    public byte? HoursMin { get; set; }

    public byte? HoursMax { get; set; }

    public string? AltQuantifier { get; set; }

    public string? AltCourse1 { get; set; }

    public string? AltCourse1Description { get; set; }

    public string? AltCourse2 { get; set; }

    public string? AltCourse2Description { get; set; }

    public string? AltCourse3 { get; set; }

    public string? AltCourse3Description { get; set; }

    public int TableListId { get; set; }

    public virtual DegreeRequirementTableList TableList { get; set; } = null!;
}

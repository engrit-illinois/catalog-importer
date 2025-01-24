namespace Application.Entities;

public class DegreeRequirementSection
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int TypeId { get; set; }

    public int DegreeEntryId { get; set; }

    public List<DegreeRequirementList> DegreeRequirementList { get; set; } = [];
}

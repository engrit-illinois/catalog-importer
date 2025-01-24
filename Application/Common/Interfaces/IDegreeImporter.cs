namespace Application.Common.Interfaces;

public interface IDegreeImporter
{
    public Task<IReadOnlyCollection<DegreeRequirementSection>> ImportDegreeRequirements(DegreeEntry degreeEntry);
}

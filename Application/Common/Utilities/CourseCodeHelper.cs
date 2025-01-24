namespace Application.Common.Utilities;
internal static class CourseCodeHelper
{
    internal static (string subjectCode, int courseNumber) FromString(string course)
    {

        string[] parts = course.Split(' ');

        if (parts.Length == 0 || parts.Length == 1)
        {
            parts = course.Split('\u00A0');
            throw new ArgumentException("Invalid course code format");
        }

        _ = int.TryParse(parts[1], out int courseNumber);

        // Handle courses with multiple subjects, such as "CS/MATH 400"
        // TODO: handle rows with multiple subject
        string subjectCode = parts[0].Split("/")[0];

        return (subjectCode, courseNumber);
    }
}

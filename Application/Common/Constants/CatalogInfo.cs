namespace Application.Common.Constants;
internal class CatalogInfo
{
    public static string CatalogUrl => "http://catalog.illinois.edu";

    public static string CatalogArchiveUrl => "http://catalog.illinois.edu/archivedacademiccatalogs";

    public static string CatalogUgradDegreePath = $"{CatalogUrl}/undergraduate";

    public static string CatalogArchiveUgradDegreePath = $"{CatalogArchiveUrl}/undergraduate";

    public static string EngineeringGenEdUrl => "https://courses.illinois.edu/gened/####/fall";

    public static readonly string HtmlDegreeContainerId = "degreerequirementstextcontainer";


    public static Dictionary<string, string> EngineeringGenEdList = new()
    {
        {"ACP", "Advanced Composition"},
        {"COMP1", "Composition I"},
        {"CS", "Cultural Studies"},
        {"HUM", "Humanities & the Arts"},
        {"NAT", "Natural Sciences & Technology"},
        {"QR", "Quantitative Reasoning"},
        {"SBS", "Social & Behavioral Sciences"}
    };

}

namespace Application.Common.Constants;
public class CatalogInfo
{
    public static string CatalogUrl => "http://catalog.illinois.edu";

    public static string CatalogArchiveUrl => "http://catalog.illinois.edu/archivedacademiccatalogs";

    public static string CatalogUgradDegreePath = $"{CatalogUrl}/undergraduate";

    public static string CatalogArchiveUgradDegreePath = $"{CatalogArchiveUrl}/undergraduate";

    public static string EngineeringGenEdUrl => "https://courses.illinois.edu/gened/####/fall";

    public static readonly string HtmlDegreeContainerId = "degreerequirementstextcontainer";

    internal const string s_areaHeaderClass = "areaheader";
    internal const string s_commentClass = "courselistcomment";
    internal const string s_commentIndentClass = "commentindent";
    internal const string s_orClass = "orclass";
    internal const string s_firstRowClass = "firstrow";
    internal const string s_lastRowClass = "lastrow";
    internal const string s_hoursColClass = "hourscol";
    internal const int courseRowColumnCount = 3;
    internal static readonly string[] s_tableTitleElements = ["h3", "h4", "h5", "h6"];

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

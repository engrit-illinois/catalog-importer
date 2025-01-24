namespace Application.Common.Utilities;
public class CatalogHelper
{
    public static string GetCurrentUgradUrl(string uri) => $"{CatalogInfo.CatalogUgradDegreePath}/{uri}";
    public static string GetArchiveUgradUrl(string catalogYear, string uri) => $"{CatalogInfo.CatalogArchiveUrl}/{catalogYear}/undergraduate/{uri}";
}

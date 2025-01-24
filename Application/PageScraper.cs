namespace Application;
public class PageScraper
{
    public async Task<HtmlDocument?> GetPageContent(string url)
    {
        using HttpClient client = new();

        HttpResponseMessage response = await client.GetAsync(url);
        HtmlDocument doc = new();

        if (response.IsSuccessStatusCode)
        {
            string html = await response.Content.ReadAsStringAsync();
            html = HtmlEntity.DeEntitize(html);

            html = html.Replace("\u00A0", " ").Replace("\u0026", "&");
            doc.LoadHtml(html);
            return doc;
        }

        return null;
    }

    public async Task<HtmlNode?> GetDegreeRequirementsNode(string url)
        => (await GetPageContent(url))?.DocumentNode.SelectSingleNode($"//div[@id='{CatalogInfo.HtmlDegreeContainerId}']");
}


namespace Application.Common;

internal static class HtmlNodeExtensions
{
    internal static readonly string s_htmlDegreeContainerId = "degreerequirementstextcontainer";

    internal static string GetDirectInnerText(this HtmlNode node)
    {
        return string.Concat(node.ChildNodes
            .Where(n => n.NodeType == HtmlNodeType.Text)
            .Select(n => n.InnerText.Trim()));
    }

    //internal static HtmlNodeCollection GetHtmlDocumentTables(this HtmlDocument doc)
    //{
    //    var degreeRequirementsNode = doc.DocumentNode.SelectSingleNode($"//div[@id='{s_htmlDegreeContainerId}']");

    //    if (degreeRequirementsNode != null)
    //    {
    //        return degreeRequirementsNode.SelectNodes(".//table[not(contains(@class, 'sc_footnotes'))]");
    //    }
    //    else
    //    {
    //        // handle Engineering Undeclared
    //        return doc.DocumentNode.SelectSingleNode($"//div[@id='textcontainer']").SelectNodes(".//table");
    //    }
    //}

    internal static HtmlNodeCollection GetHtmlSectionHeadings(this HtmlNode htmlNode)
    {
        return htmlNode.SelectNodes(".//h3 | .//p[count(strong) = 1 and count(*) = 1]");
    }

    internal static HtmlNodeCollection GetHtmlSectionInstructions(this HtmlNode sectionNode)
    {
        var instructionNodes = new HtmlNodeCollection(null);

        if (sectionNode != null)
        {
            // Select all <p> tags directly below this section, stopping at the next section
            var paragraphs = sectionNode.SelectNodes("following-sibling::p[not(preceding-sibling::h3) and not(preceding-sibling::p[strong])]");

            if (paragraphs != null)
            {
                foreach (var p in paragraphs)
                {
                    instructionNodes.Add(p);
                }
            }
        }

        return instructionNodes;
    }

    internal static HtmlNodeCollection GetHtmlSectionTables(this HtmlNode sectionNode)
    {
        if (sectionNode != null)
        {
            var tables = new HtmlNodeCollection(null);
            var current = sectionNode.NextSibling;

            while (current != null)
            {
                if (current.Name == "h3" || (current.Name == "p" && current.SelectSingleNode("strong") != null))
                {
                    break;
                }

                if (current.Name == "table")
                {
                    tables.Add(current);
                }

                current = current.NextSibling;
            }

            return tables;

            //sections[sectionName] = tables;



            //    var siblingTables = sectionNode.SelectNodes("following-sibling::*[self::table and (not(preceding-sibling::h3) or not(self::p[strong]))]");

            //    if (siblingTables == null)
            //    {
            //        Console.WriteLine("No tables found for this section.");
            //    }

            //    return siblingTables ?? new HtmlNodeCollection(null);
            //    //return degreeRequirementsNode.SelectNodes(".//table[not(contains(@class, 'sc_footnotes'))]");
        }
        else
        {
            // handle Engineering Undeclared
            return sectionNode.SelectSingleNode($".//div[@id='textcontainer']").SelectNodes(".//table");
        }
    }
}

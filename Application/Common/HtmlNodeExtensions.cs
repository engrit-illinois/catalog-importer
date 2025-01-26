namespace Application.Common;

internal static class HtmlNodeExtensions
{
    internal const string s_htmlDegreeContainerId = "degreerequirementstextcontainer";
    internal const string s_areaHeaderClass = "areaheader";
    internal const string s_commentClass = "courselistcomment";
    internal const string s_commentIndentClass = "commentindent";
    internal const string s_orClass = "orclass";
    internal const string s_firstRowClass = "firstrow";
    internal const string s_lastRowClass = "lastrow";
    internal const string s_hoursColClass = "hourscol";
    internal const string s_summaryRowClass = "listsum";
    internal const int courseRowColumnCount = 3;
    internal static readonly string[] s_tableTitleElements = ["h3", "h4", "h5", "h6"];

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

    internal static HtmlNode TableBodyNode(this HtmlNode tableNode)
    {
        if (tableNode.Name != "table")
        {
            throw new ArgumentException("The provided HtmlNode is not a table.", nameof(tableNode));
        }

        return tableNode.SelectSingleNode(".//tbody");
    }

    internal static int TableRowCount(this HtmlNode tableNode)
    {
        return tableNode.TableBodyNode().SelectNodes(".//tr").Count;
    }

    internal static bool HasTableArea(this HtmlNode tableNode, bool skipFirstRow = false)
    {
        if (tableNode.Name != "table")
        {
            throw new ArgumentException("The provided HtmlNode is not a table.", nameof(tableNode));
        }

        return tableNode.TableBodyNode().ChildNodes.Any(c => c.HasClass(CatalogInfo.s_areaHeaderClass));
    }

    internal static bool HasTableSubArea(this HtmlNode tableNode, bool skipFirstRow = false)
    {
        if (tableNode.Name != "table")
        {
            throw new ArgumentException("The provided HtmlNode is not a table.", nameof(tableNode));
        }

        return tableNode.TableBodyNode().ChildNodes.Any(c => c.HasClass(CatalogInfo.s_commentIndentClass));
    }

    internal static HtmlNode? GetFirstRow(this HtmlNode tableNode)
    {
        return tableNode.TableBodyNode().ChildNodes.FirstOrDefault(node => node.NodeType == HtmlNodeType.Element);
    }

    internal static HtmlNodeCollection GetTableAreas(this HtmlNode tableNode, bool skipFirstRow)
    {
        if (tableNode.Name != "table")
        {
            throw new ArgumentException("The provided HtmlNode is not a table.", nameof(tableNode));
        }

        var tableAreas = new HtmlNodeCollection(null);

        HtmlNode current;

        if (skipFirstRow)
        {
            current = tableNode.TableBodyNode().ChildNodes.Where(node => node.NodeType == HtmlNodeType.Element).Skip(1).First();
        }
        else
        {
            current = tableNode.TableBodyNode().ChildNodes.First(node => node.NodeType == HtmlNodeType.Element);
        }

        while (current != null)
        {
            if (current.HasClass(s_areaHeaderClass))
            {
                tableAreas.Add(current);
            }

            current = current.NextSibling;
        }
        return tableAreas;
    }

    internal static HtmlNodeCollection GetTableRows(this HtmlNode tableNode, bool skipFirstRow)
    {
        var rows = new HtmlNodeCollection(null);
        HtmlNode current;

        if (skipFirstRow)
        {
            current = tableNode.TableBodyNode().ChildNodes.Where(node => node.NodeType == HtmlNodeType.Element).Skip(1).First();
        }
        else
        {
            current = tableNode.TableBodyNode().ChildNodes.First(node => node.NodeType == HtmlNodeType.Element);
        }

        while (current != null)
        {
            if (current.NodeType == HtmlNodeType.Element && current.Name == "tr")
            {
                rows.Add(current);
            }

            current = current.NextSibling;
        }

        return rows;
    }
}

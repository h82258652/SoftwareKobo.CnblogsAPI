using SoftwareKobo.CnblogsAPI.Model;
using SoftwareKobo.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Helper
{
    internal static class ArticleHelper
    {
        internal static IEnumerable<Article> Deserialize(XDocument document)
        {
            var root = document?.Root;
            if (root == null)
            {
                return null;
            }

            var ns = root.GetDefaultNamespace();
            var articles = from entry in root.Elements(ns + "entry")
                           where entry.HasElements
                           let temp = Deserialize(entry)
                           where temp != null
                           select temp;
            return articles;
        }

        internal static Article Deserialize(XElement element)
        {
            if (element == null)
            {
                return null;
            }

            var ns = element.GetDefaultNamespace();
            var id = element.Element(ns + "id");
            var title = element.Element(ns + "title");
            var summary = element.Element(ns + "summary");
            var published = element.Element(ns + "published");
            var updated = element.Element(ns + "updated");
            var author = element.Element(ns + "author");
            var href = element.Element(ns + "link")?.Attribute("href");
            var diggs = element.Element(ns + "diggs");
            var views = element.Element(ns + "views");
            var comments = element.Element(ns + "comments");

            if (id == null || title == null || summary == null || published == null || updated == null || author == null || href == null || diggs == null || views == null || comments == null)
            {
                return null;
            }

            return new Article
            {
                Id = int.Parse(id.Value, CultureInfo.InvariantCulture),
                Title = WebUtility.HtmlDecode(title.Value),
                Summary = WebUtility.HtmlDecode(summary.Value),
                Published = DateTime.Parse(published.Value, CultureInfo.InvariantCulture),
                Updated = DateTime.Parse(updated.Value, CultureInfo.InvariantCulture),
                Author = ArticleAuthorHelper.Deserialize(author),
                Link = new Uri(href.Value, UriKind.Absolute),
                Diggs = int.Parse(diggs.Value, CultureInfo.InvariantCulture),
                Views = int.Parse(views.Value, CultureInfo.InvariantCulture),
                Comments = int.Parse(comments.Value, CultureInfo.InvariantCulture)
            };
        }
    }
}
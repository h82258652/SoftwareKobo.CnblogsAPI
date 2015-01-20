using SoftwareKobo.CnblogsAPI.Model;
using SoftwareKobo.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Helper
{
    internal static class NewsHelper
    {
        internal static IEnumerable<News> Deserialize(XDocument document)
        {
            var root = document?.Root;
            if (root == null)
            {
                return Enumerable.Empty<News>();
            }

            var ns = root.GetDefaultNamespace();
            var news = from entry in root.Elements(ns + "entry")
                       where entry.HasElements
                       let temp = Deserialize(entry)
                       where temp != null
                       select temp;
            return news;
        }

        internal static News Deserialize(XElement element)
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
            var href = element.Element(ns + "link")?.Attribute("href");
            var diggs = element.Element(ns + "diggs");
            var views = element.Element(ns + "views");
            var comments = element.Element(ns + "comments");
            var topic = element.Element(ns + "topic");
            var topicIcon = element.Element(ns + "topicIcon");
            var sourceName = element.Element(ns + "sourceName");

            if (id == null
                || title == null
                || summary == null
                || published == null
                || updated == null
                || href == null
                || diggs == null
                || views == null
                || comments == null
                || topic == null
                || topicIcon == null
                || sourceName == null)
            {
                return null;
            }

            return new News
            {
                Id = int.Parse(id.Value, CultureInfo.InvariantCulture),
                Title = WebUtility.HtmlDecode(title.Value).Trim(),
                Summary = WebUtility.HtmlDecode(summary.Value),
                Published = DateTime.Parse(published.Value, CultureInfo.InvariantCulture),
                Updated = DateTime.Parse(updated.Value, CultureInfo.InvariantCulture),
                Link = new Uri(href.Value, UriKind.Absolute),
                Diggs = int.Parse(diggs.Value, CultureInfo.InvariantCulture),
                Views = int.Parse(views.Value, CultureInfo.InvariantCulture),
                Comments = int.Parse(comments.Value, CultureInfo.InvariantCulture),
                Topic = topic.Value,
                TopicIcon = topicIcon.IsEmpty ? null : new Uri(topicIcon.Value, UriKind.Absolute),
                SourceName = sourceName.Value
            };
        }
    }
}
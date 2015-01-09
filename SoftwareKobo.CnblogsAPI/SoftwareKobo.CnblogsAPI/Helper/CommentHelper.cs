using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Helper
{
    internal static class CommentHelper
    {
        internal static T Deserialize<T>(XElement element) where T : Comment, new()
        {
            if (element == null)
            {
                return null;
            }

            var ns = element.GetDefaultNamespace();
            var id = element.Element(ns + "id");
            var title = element.Element(ns + "title");
            var published = element.Element(ns + "published");
            var updated = element.Element(ns + "updated");
            var author = element.Element(ns + "author");
            var content = element.Element(ns + "content");

            if (id == null
                || title == null
                || published == null
                || updated == null
                || author == null || content == null)
            {
                return null;
            }

            return new T
            {
                Id = int.Parse(id.Value, CultureInfo.InvariantCulture),
                Title = title.Value,
                Published = DateTime.Parse(published.Value, CultureInfo.InvariantCulture),
                Updated = DateTime.Parse(updated.Value, CultureInfo.InvariantCulture),
                Author = CommentAuthorHelper.Deserialize(author),
                Content = content.Value
            };
        }

        internal static IEnumerable<T> Deserialize<T>(XDocument document) where T : Comment, new()
        {
            var root = document?.Root;
            if (root == null)
            {
                return Enumerable.Empty<T>();
            }

            var ns = root.GetDefaultNamespace();
            var comments = from entry in root.Elements(ns + "entry")
                           where entry.HasElements
                           let temp = Deserialize<T>(entry)
                           where temp != null
                           select temp;
            return comments;
        }
    }
}
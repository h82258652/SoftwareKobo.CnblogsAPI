using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Helper
{
    internal static class BloggerHelper
    {
        internal static IEnumerable<Blogger> Deserialize(XDocument document)
        {
            var root = document?.Root;
            if (root == null)
            {
                return Enumerable.Empty<Blogger>();
            }

            var ns = root.GetDefaultNamespace();
            var bloggers = from entry in root.Elements(ns + "entry")
                           where entry.HasElements
                           let temp = Deserialize(entry)
                           where temp != null
                           select temp;
            return bloggers;
        }

        internal static Blogger Deserialize(XElement element)
        {
            if (element == null)
            {
                return null;
            }

            var ns = element.GetDefaultNamespace();
            var id = element.Element(ns + "id");
            var title = element.Element(ns + "title");
            var updated = element.Element(ns + "updated");
            var href = element.Element(ns + "link")?.Attribute("href");
            var blogapp = element.Element(ns + "blogapp");
            var avatar = element.Element(ns + "avatar");
            var postcount = element.Element(ns + "postcount");

            if (id == null || title == null || updated == null || href == null || blogapp == null || avatar == null || postcount == null)
            {
                return null;
            }

            return new Blogger
            {
                Id = new Uri(id.Value, UriKind.Absolute),
                Title = title.Value,
                Updated = DateTime.Parse(updated.Value, CultureInfo.InvariantCulture),
                Link = new Uri(href.Value, UriKind.Absolute),
                BlogApp = blogapp.Value,
                Avatar = avatar.IsEmpty ? null : new Uri(avatar.Value, UriKind.Absolute),
                PostCount = int.Parse(postcount.Value, CultureInfo.InvariantCulture)
            };
        }
    }
}
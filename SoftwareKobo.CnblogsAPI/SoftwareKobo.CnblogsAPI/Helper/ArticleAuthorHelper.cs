using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Helper
{
    internal static class ArticleAuthorHelper
    {
        internal static ArticleAuthor Deserialize(XElement element)
        {
            if (element == null)
            {
                return null;
            }

            var ns = element.GetDefaultNamespace();
            var name = element.Element(ns + "name");
            var uri = element.Element(ns + "uri");
            var avatar = element.Element(ns + "avatar");

            if (name == null || uri == null)
            {
                return null;
            }

            var articleAuthor = new ArticleAuthor
            {
                Name = name.Value,
                Uri = new Uri(uri.Value, UriKind.Absolute)
            };
            if (avatar != null && avatar.IsEmpty == false)
            {
                articleAuthor.Avator = new Uri(avatar.Value, UriKind.Absolute);
            }
            return articleAuthor;
        }
    }
}
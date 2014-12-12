using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Helper
{
    internal static class CommentAuthorHelper
    {
        internal static CommentAuthor Deserialize(XElement element)
        {
            if (element == null)
            {
                return null;
            }

            var ns = element.GetDefaultNamespace();
            var name = element.Element(ns + "name");
            var uri = element.Element(ns + "uri");

            if (name == null || uri == null)
            {
                return null;
            }

            return new CommentAuthor
            {
                Name = name.Value,
                Uri = new Uri(uri.Value, UriKind.Absolute)
            };
        }
    }
}
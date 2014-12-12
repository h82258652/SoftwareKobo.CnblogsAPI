using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Service
{
    internal static class AuthorService
    {
        internal static IEnumerable<Author> DeserializeToAuthors(XDocument document)
        {
            if (document == null)
            {
                return null;
            }

            var root = document.Root;
            if (root == null)
            {
                return null;
            }

            var ns = root.GetDefaultNamespace();
            var authors = from entry in root.Elements(ns + "entry")
                          where entry.HasElements
                          let temp = DeserializeToAuthor(entry)
                          where temp != null
                          select temp;
            return authors;
        }

        internal static Author DeserializeToAuthor(XElement element)
        {
            if (element == null)
            {
                return null;
            }

            var ns = element.GetDefaultNamespace();
            var name = element.Element(ns + "name");
            var uri = element.Element(ns + "uri");

            if (name == null
                || uri == null)
            {
                return null;
            }

            return new Author
            {
                Name = name.Value,
                Uri = new Uri(uri.Value, UriKind.Absolute)
            };
        }
    }
}
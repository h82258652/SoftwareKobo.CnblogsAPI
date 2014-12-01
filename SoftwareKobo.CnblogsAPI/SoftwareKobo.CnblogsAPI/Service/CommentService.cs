using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Service
{
    internal static class CommentService
    {
        internal static IEnumerable<Comment> DeserializeToNewsComments(XDocument document)
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
            var comments = from entry in root.Elements(ns + "entry")
                           where entry.HasElements
                           let temp = DeserializeToNewsComment(entry)
                           where temp != null
                           select temp;
            return comments;
        }

        private static Comment DeserializeToNewsComment(XElement element)
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
                || author == null
                || content == null)
            {
                return null;
            }

            return new Comment
            {
                Id = Int32.Parse(id.Value),
                Title = title.Value,
                Published = DateTime.Parse(published.Value),
                Updated = DateTime.Parse(updated.Value),
                Author = AuthorService.DeserializeToAuthor(author),
                Content = content.Value
            };
        }
    }
}
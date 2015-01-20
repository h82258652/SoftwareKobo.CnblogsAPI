using SoftwareKobo.CnblogsAPI.Model;
using SoftwareKobo.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Helper
{
    internal static class NewsDetailHelper
    {
        internal static NewsDetail Deserialize(XDocument document)
        {
            var root = document?.Root;
            if (root == null)
            {
                return null;
            }

            var ns = root.GetDefaultNamespace();
            var title = root.Element(ns + "Title");
            var sourceName = root.Element(ns + "SourceName");
            var submitDate = root.Element(ns + "SubmitDate");
            var content = root.Element(ns + "Content");
            var imageUrl = root.Element(ns + "ImageUrl");
            var prevNews = root.Element(ns + "PrevNews");
            var nextNews = root.Element(ns + "NextNews");
            var commentCount = root.Element(ns + "CommentCount");

            if (title == null
             || sourceName == null
             || submitDate == null
             || content == null
             || imageUrl == null
             || prevNews == null
             || nextNews == null
             || commentCount == null)
            {
                return null;
            }

            var newsDetail = new NewsDetail
            {
                Title = WebUtility.HtmlDecode(title.Value).Trim(),
                SourceName = sourceName.Value,
                SubmitDate = DateTime.Parse(submitDate.Value, CultureInfo.InvariantCulture),
                Content = content.Value,
                ImageUrl = new ReadOnlyCollection<Uri>(new List<Uri>(from temp in imageUrl.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                                                     select new Uri(temp, UriKind.Absolute))),
                CommentCount = int.Parse(commentCount.Value, CultureInfo.InvariantCulture)
            };
            if (prevNews.IsEmpty == false)
            {
                newsDetail.PrevNews = int.Parse(prevNews.Value, CultureInfo.InvariantCulture);
            }
            if (nextNews.IsEmpty == false)
            {
                newsDetail.NextNews = int.Parse(nextNews.Value, CultureInfo.InvariantCulture);
            }
            return newsDetail;
        }
    }
}
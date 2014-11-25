using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Service
{
    public static class NewsService
    {
        private const string CommentUrlTemplate = "http://wcf.open.cnblogs.com/news/item/{0}/comments/{1}/{2}";

        private const string DetailUrlTemplate = "http://wcf.open.cnblogs.com/news/item/{0}";

        private const string HotUrlTemplate = "http://wcf.open.cnblogs.com/news/hot/{0}";

        private const int MinNewsId = 36848;

        private const string RecentUrlTemplate = "http://wcf.open.cnblogs.com/news/recent/paged/{0}/{1}";

        private const string RecommendUrlTemplate = "http://wcf.open.cnblogs.com/news/recommend/paged/{0}/{1}";

        /// <summary>
        /// 获取新闻评论
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async static Task<IEnumerable<NewsComment>> CommentsAsync(int newsId, int pageIndex, int pageSize)
        {
            if (newsId < MinNewsId)
            {
                throw new ArgumentOutOfRangeException("newsId");
            }
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException("pageIndex");
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }

            try
            {
                // ReSharper disable once UnusedVariable
                var forCheck = checked(pageIndex * pageSize);
            }
            catch (OverflowException exception)
            {
                throw new ArgumentOutOfRangeException("超出范围。", exception);
            }

            var url = string.Format(CommentUrlTemplate, newsId, pageIndex, pageSize);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return DeserializeToNewsComments(document);
            }
        }

        public async static Task<IEnumerable<NewsComment>> CommentsAsync(int newsId)
        {
            return await CommentsAsync(newsId, 1, int.MaxValue);
        }

        /// <summary>
        /// 获取新闻内容
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async static Task<NewsDetail> DetailAsync(int newsId)
        {
            if (newsId < MinNewsId)
            {
                throw new ArgumentOutOfRangeException("newsId");
            }

            var url = string.Format(DetailUrlTemplate, newsId);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            {
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new ArgumentOutOfRangeException("newsId");
                }
                var document = XDocument.Load(response.GetResponseStream());
                var newsDetail = DeserializeToNewsDetail(document);
                newsDetail.Id = newsId;
                return newsDetail;
            }
        }

        /// <summary>
        /// 获取热门新闻列表
        /// </summary>
        /// <param name="itemCount"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<News>> HotAsync(int itemCount)
        {
            if (itemCount < 1)
            {
                throw new ArgumentOutOfRangeException("itemCount");
            }

            var url = string.Format(HotUrlTemplate, itemCount);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return DeserializeToNews(document);
            }
        }

        /// <summary>
        /// 分页获取最新新闻列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<News>> RecentAsync(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException("pageIndex");
            }
            if (pageSize < 1 || pageSize > 100)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }

            var url = string.Format(CultureInfo.InvariantCulture, RecentUrlTemplate, pageIndex, pageSize);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);

            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return DeserializeToNews(document);
            }
        }

        public static async Task<IEnumerable<News>> RecommendAsync(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException("pageIndex");
            }
            if (pageSize < 1 || pageSize > 100)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }

            var url = string.Format(CultureInfo.InvariantCulture, RecommendUrlTemplate, pageIndex, pageSize);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return DeserializeToNews(document);
            }
        }

        private static News DeserializeToNews(XElement element)
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
            var link = element.Element(ns + "link");
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
                || link == null
                || diggs == null
                || views == null
                || comments == null
                || topic == null
                || topicIcon == null
                || sourceName == null)
            {
                return null;
            }

            var href = link.Attribute("href");

            if (href == null)
            {
                return null;
            }

            return new News
            {
                Id = int.Parse(id.Value),
                Title = WebUtility.HtmlDecode(title.Value),
                Summary = WebUtility.HtmlDecode(summary.Value),
                Published = DateTime.Parse(published.Value, CultureInfo.InvariantCulture),
                Updated = DateTime.Parse(updated.Value, CultureInfo.InvariantCulture),
                Link = new Uri(href.Value, UriKind.Absolute),
                Diggs = int.Parse(diggs.Value),
                Views = int.Parse(views.Value),
                Comments = int.Parse(comments.Value),
                Topic = topic.Value,
                TopicIcon = topicIcon.IsEmpty ? null : new Uri(topicIcon.Value, UriKind.Absolute),
                SourceName = sourceName.Value
            };
        }

        private static IEnumerable<News> DeserializeToNews(XDocument document)
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
            var news = from entry in root.Elements(ns + "entry")
                       where entry.HasElements
                       let temp = DeserializeToNews(entry)
                       where temp != null
                       select temp;
            return news;
        }

        private static NewsComment DeserializeToNewsComment(XElement element)
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

            return new NewsComment
            {
                Id = int.Parse(id.Value),
                Title = title.Value,
                Published = DateTime.Parse(published.Value),
                Updated = DateTime.Parse(updated.Value),
                Author = AuthorService.DeserializeToAuthor(author),
                Content = content.Value
            };
        }

        private static IEnumerable<NewsComment> DeserializeToNewsComments(XDocument document)
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

        private static NewsDetail DeserializeToNewsDetail(XDocument document)
        {
            var root = document.Root;
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
                Title = WebUtility.HtmlDecode(title.Value),
                SourceName = sourceName.Value,
                SubmitDate = DateTime.Parse(submitDate.Value),
                Content = content.Value,
                ImageUrl = new List<Uri>((from temp in imageUrl.Value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                          select new Uri(temp, UriKind.Absolute))),
                CommentCount = int.Parse(commentCount.Value)
            };
            if (prevNews.IsEmpty == false)
            {
                newsDetail.PrevNews = int.Parse(prevNews.Value);
            }
            if (nextNews.IsEmpty == false)
            {
                newsDetail.NextNews = int.Parse(nextNews.Value);
            }
            return newsDetail;
        }
    }
}
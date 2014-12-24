using SoftwareKobo.CnblogsAPI.Helper;
using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Service
{
    /// <summary>
    /// 新闻服务。
    /// </summary>
    public static class NewsService
    {
        /// <summary>
        /// 新闻评论 Url。
        /// </summary>
        private const string CommentUrlTemplate = "http://wcf.open.cnblogs.com/news/item/{0}/comments/{1}/{2}";

        /// <summary>
        /// 新闻内容 Url。
        /// </summary>
        private const string DetailUrlTemplate = "http://wcf.open.cnblogs.com/news/item/{0}";

        /// <summary>
        /// 热门新闻列表 Url。
        /// </summary>
        private const string HotUrlTemplate = "http://wcf.open.cnblogs.com/news/hot/{0}";

        /// <summary>
        /// 博客园新闻中最小的新闻 Id。
        /// </summary>
        private const int MinNewsId = 36848;

        /// <summary>
        /// 最新新闻列表 Url。
        /// </summary>
        private const string RecentUrlTemplate = "http://wcf.open.cnblogs.com/news/recent/paged/{0}/{1}";

        /// <summary>
        /// 推荐新闻列表 Url。
        /// </summary>
        private const string RecommendUrlTemplate = "http://wcf.open.cnblogs.com/news/recommend/paged/{0}/{1}";

        /// <summary>
        /// 获取新闻评论。
        /// </summary>
        /// <param name="newsId">新闻 Id。</param>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>评论。</returns>
        /// <exception cref="ArgumentOutOfRangeException">新闻 Id 错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">评论页数错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">评论条数错误。</exception>
        public static async Task<IEnumerable<NewsComment>> CommentAsync(int newsId, int pageIndex, int pageSize)
        {
            if (newsId < MinNewsId)
            {
                throw new ArgumentOutOfRangeException(nameof(newsId));
            }
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex));
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            try
            {
                var forCheck = checked(pageIndex * pageSize);
            }
            catch (OverflowException exception)
            {
                throw new ArgumentOutOfRangeException("超出范围。", exception);
            }

            var url = string.Format(CultureInfo.InvariantCulture, CommentUrlTemplate, newsId, pageIndex, pageSize);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                var comments = new List<NewsComment>(CommentHelper.Deserialize<NewsComment>(document));
                for (var i = 0; i < comments.Count; i++)
                {
                    comments[i].NewsId = newsId;
                }
                return comments;
            }
        }

        /// <summary>
        /// 获取新闻内容。
        /// </summary>
        /// <param name="newsId">新闻 Id。</param>
        /// <returns>新闻内容。</returns>
        /// <exception cref="ArgumentOutOfRangeException">新闻 Id 错误。</exception>
        public static async Task<NewsDetail> DetailAsync(int newsId)
        {
            if (newsId < MinNewsId)
            {
                throw new ArgumentOutOfRangeException(nameof(newsId));
            }

            var url = string.Format(CultureInfo.InvariantCulture, DetailUrlTemplate, newsId);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            {
                if (response.StatusCode.HasFlag(HttpStatusCode.InternalServerError))
                {
                    throw new ArgumentOutOfRangeException(nameof(newsId));
                }
                var document = XDocument.Load(response.GetResponseStream());
                var newsDetail = NewsDetailHelper.Deserialize(document);
                newsDetail.Id = newsId;
                return newsDetail;
            }
        }

        /// <summary>
        /// 获取热门新闻列表。
        /// </summary>
        /// <param name="itemCount">条数。</param>
        /// <returns>新闻列表。</returns>
        /// <exception cref="ArgumentOutOfRangeException">条数错误。</exception>
        public static async Task<IEnumerable<News>> HotAsync(int itemCount)
        {
            if (itemCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(itemCount));
            }

            var url = string.Format(CultureInfo.InvariantCulture, HotUrlTemplate, itemCount);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return NewsHelper.Deserialize(document);
            }
        }

        /// <summary>
        /// 分页获取最新新闻列表。
        /// </summary>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>新闻列表。</returns>
        /// <exception cref="ArgumentOutOfRangeException">新闻页数错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">新闻条数错误。</exception>
        public static async Task<IEnumerable<News>> RecentAsync(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex));
            }
            if (pageSize < 1 || pageSize > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            var url = string.Format(CultureInfo.InvariantCulture, RecentUrlTemplate, pageIndex, pageSize);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return NewsHelper.Deserialize(document);
            }
        }

        /// <summary>
        /// 分页获取推荐新闻列表。
        /// </summary>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>新闻列表。</returns>
        /// <exception cref="ArgumentOutOfRangeException">新闻页数错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">新闻条数错误。</exception>
        public static async Task<IEnumerable<News>> RecommendAsync(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex));
            }
            if (pageSize < 1 || pageSize > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            var url = string.Format(CultureInfo.InvariantCulture, RecommendUrlTemplate, pageIndex, pageSize);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return NewsHelper.Deserialize(document);
            }
        }
    }
}
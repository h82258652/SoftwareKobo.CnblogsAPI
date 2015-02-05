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
    /// 博客服务。
    /// </summary>
    public static class BlogService
    {
        /// <summary>
        /// 个人博客文章列表 Url。
        /// </summary>
        private const string ArticleUrlTemplate = "http://wcf.open.cnblogs.com/blog/u/{0}/posts/{1}/{2}";

        /// <summary>
        /// 文章内容 Url。
        /// </summary>
        private const string BodyUrlTemplate = "http://wcf.open.cnblogs.com/blog/post/body/{0}";

        /// <summary>
        /// 文章评论 Url。
        /// </summary>
        private const string CommentUrlTemplate = "http://wcf.open.cnblogs.com/blog/post/{0}/comments/{1}/{2}";

        /// <summary>
        /// 首页文章列表 Url。
        /// </summary>
        private const string RecentUrlTemplate = "http://wcf.open.cnblogs.com/blog/sitehome/paged/{0}/{1}";

        /// <summary>
        /// 博客推荐列表 Url。
        /// </summary>
        private const string RecommendUrlTemplate = "http://wcf.open.cnblogs.com/blog/bloggers/recommend/{0}/{1}";

        /// <summary>
        /// 根据作者名搜索博主 Url。
        /// </summary>
        private const string SearchBloggerUrlTemplate = "http://wcf.open.cnblogs.com/blog/bloggers/search?t={0}";

        /// <summary>
        /// 10 天内推荐排行 Url。
        /// </summary>
        private const string TenDaysTopDiggUrlTemplate = "http://wcf.open.cnblogs.com/blog/TenDaysTopDiggPosts/{0}";

        /// <summary>
        /// 48 小时阅读排行 Url。
        /// </summary>
        private const string TwoDaysTopViewUrlTemplate = "http://wcf.open.cnblogs.com/blog/48HoursTopViewPosts/{0}";

        /// <summary>
        /// 分页获取个人博客文章列表。
        /// </summary>
        /// <param name="blogapp">博主用户名。</param>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>文章列表。</returns>
        /// <exception cref="ArgumentException">博主找不到。</exception>
        /// <exception cref="ArgumentOutOfRangeException">文章页数错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">文章条数错误。</exception>
        public static async Task<IEnumerable<Article>> ArticleAsync(string blogapp, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex));
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            var url = string.Format(CultureInfo.InvariantCulture, ArticleUrlTemplate, blogapp, pageIndex, pageSize);
            url = url.WithCache();
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            {
                if (response.StatusCode.HasFlag(HttpStatusCode.InternalServerError))
                {
                    throw new ArgumentException("找不到该博主", nameof(blogapp));
                }
                var document = XDocument.Load(response.GetResponseStream());
                return ArticleHelper.Deserialize(document);
            }
        }

        /// <summary>
        /// 获取文章内容。
        /// </summary>
        /// <param name="articleId">文章 Id。</param>
        /// <returns>文章内容。</returns>
        /// <exception cref="ArgumentOutOfRangeException">文章 Id 错误。</exception>
        public static async Task<string> BodyAsync(int articleId)
        {
            if (articleId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(articleId));
            }

            var url = string.Format(CultureInfo.InvariantCulture,BodyUrlTemplate, articleId);
            url = url.WithCache();
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return document.Root?.Value ?? string.Empty;
            }
        }

        /// <summary>
        /// 获取文章评论。
        /// </summary>
        /// <param name="articleId">文章 Id。</param>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>评论列表。</returns>
        /// <exception cref="ArgumentOutOfRangeException">文章 Id 错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">评论页数错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">评论条数错误。</exception>
        public static async Task<IEnumerable<ArticleComment>> CommentAsync(int articleId, int pageIndex, int pageSize)
        {
            if (articleId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(articleId));
            }
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex));
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            var url = string.Format(CultureInfo.InvariantCulture, CommentUrlTemplate, articleId, pageIndex, pageSize);
            url = url.WithCache();
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                var comments = new List<ArticleComment>(CommentHelper.Deserialize<ArticleComment>(document));
                for (var i = 0; i < comments.Count; i++)
                {
                    comments[i].ArticleId = articleId;
                }
                return comments;
            }
        }

        /// <summary>
        /// 分页获取首页文章列表。
        /// </summary>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>文章列表。</returns>
        /// <exception cref="ArgumentOutOfRangeException">文章页数错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">文章条数错误。</exception>
        public static async Task<IEnumerable<Article>> RecentAsync(int pageIndex, int pageSize)
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
            url = url.WithCache();
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return ArticleHelper.Deserialize(document);
            }
        }

        /// <summary>
        /// 分页获取推荐博客列表。
        /// </summary>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>博主列表。</returns>
        /// <exception cref="ArgumentOutOfRangeException">博客页数错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">博客条数错误。</exception>
        public static async Task<IEnumerable<Blogger>> RecommendAsync(int pageIndex, int pageSize)
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
            url = url.WithCache();
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return BloggerHelper.Deserialize(document);
            }
        }

        /// <summary>
        /// 根据作者名搜索博主。
        /// </summary>
        /// <param name="bloggerTitle">博客标题。</param>
        /// <returns>博主列表。</returns>
        public static async Task<IEnumerable<Blogger>> SearchBloggerAsync(string bloggerTitle)
        {
            var url = string.Format(CultureInfo.InvariantCulture, SearchBloggerUrlTemplate, WebUtility.UrlEncode(bloggerTitle));
            url = url.WithCache();
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return BloggerHelper.Deserialize(document);
            }
        }

        /// <summary>
        /// 10 天内推荐排行。
        /// </summary>
        /// <param name="itemCount">条数。</param>
        /// <returns>文章列表。</returns>
        /// <exception cref="ArgumentOutOfRangeException">条数错误。</exception>
        public static async Task<IEnumerable<Article>> TenDaysTopDiggAsync(int itemCount)
        {
            if (itemCount < 1 || itemCount > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(itemCount));
            }

            var url = string.Format(CultureInfo.InvariantCulture, TenDaysTopDiggUrlTemplate, itemCount);
            url = url.WithCache();
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return ArticleHelper.Deserialize(document);
            }
        }

        /// <summary>
        /// 48 小时阅读排行。
        /// </summary>
        /// <param name="itemCount">条数。</param>
        /// <returns>文章列表。</returns>
        /// <exception cref="ArgumentOutOfRangeException">条数错误。</exception>
        public static async Task<IEnumerable<Article>> TwoDaysTopViewAsync(int itemCount)
        {
            if (itemCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(itemCount));
            }

            var url = string.Format(CultureInfo.InvariantCulture, TwoDaysTopViewUrlTemplate, itemCount);
            url = url.WithCache();
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return ArticleHelper.Deserialize(document);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using SoftwareKobo.CnblogsAPI.Model;

namespace SoftwareKobo.CnblogsAPI.Service
{
    public static class BlogService
    {
        private const string BodyUrlTemplate = "http://wcf.open.cnblogs.com/blog/post/body/{0}";

        private const string CommentUrlTemplate = "http://wcf.open.cnblogs.com/blog/post/{0}/comments/{1}/{2}";

        private const string SearchBloggerUrlTemplate = "http://wcf.open.cnblogs.com/blog/bloggers/search?t={0}";

        private const string RecommendUrlTemplate = "http://wcf.open.cnblogs.com/blog/bloggers/recommend/{0}/{1}";

        /// <summary>
        /// 获取文章内容。如果文章不存在，则返回空字符串。
        /// </summary>
        /// <param name="blogId">文章 Id。</param>
        /// <returns>文章。</returns>
        /// <exception cref="ArgumentOutOfRangeException">文章 Id 错误。</exception>
        public static async Task<string> BodyAsync(int blogId)
        {
            if (blogId < 1)
            {
                throw new ArgumentOutOfRangeException("blogId");
            }

            var url = string.Format(BodyUrlTemplate, blogId);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                var root = document.Root;
                if (root == null)
                {
                    return string.Empty;
                }
                return root.Value;
            }
        }

        /// <summary>
        /// 获取文章评论。
        /// </summary>
        /// <param name="blogId">文章 Id。</param>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。不建议过大。</param>
        /// <returns>评论。</returns>
        /// <exception cref="ArgumentOutOfRangeException">文章 Id 错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">评论页数错误。</exception>
        /// <exception cref="ArgumentOutOfRangeException">评论条数错误。</exception>
        /// <exception cref="ArgumentException">文章不存在或分页过大。</exception>
        public static async Task<IEnumerable<Comment>> CommentAsync(int blogId, int pageIndex, int pageSize)
        {
            if (blogId < 1)
            {
                throw new ArgumentOutOfRangeException("blogId");
            }
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException("pageIndex");
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }

            var url = string.Format(CommentUrlTemplate, blogId, pageIndex, pageSize);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            {
                if (response.StatusCode.HasFlag(HttpStatusCode.InternalServerError))
                {
                    throw new ArgumentException("文章不存在或分页过大");
                }
                var document = XDocument.Load(response.GetResponseStream());
                return CommentService.DeserializeToNewsComments(document);
            }
        }

        private const string TwoDaysTopViewUrlTemplate = "http://wcf.open.cnblogs.com/blog/48HoursTopViewPosts/{0}";

        /// <summary>
        /// 48 小时阅读排行。
        /// </summary>
        /// <param name="itemCount">文章数量。</param>
        /// <returns>文章</returns>
        /// <exception cref="ArgumentOutOfRangeException">文章数量小于 1。</exception>
        public static async Task<IEnumerable<Article>> TwoDaysTopViewAsync(int itemCount)
        {
            if (itemCount <1)
            {
                throw new ArgumentOutOfRangeException("itemCount");
            }

            var url = string.Format(CultureInfo.InvariantCulture,TwoDaysTopViewUrlTemplate, itemCount);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response=await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                return ArticleService.DeserializeToaArticles(document);
            }
        }

        //public static async Task<IEnumerable<Blogger>> RecommendAsync(int pageIndex,int pageSize)
        //{
        //    if (pageIndex<1)
        //    {
        //        throw new ArgumentOutOfRangeException("pageIndex");
        //    }
        //    if (pageSize<1)
        //    {
        //        throw new ArgumentOutOfRangeException("pageSize");
        //    }

        //    var url = string.Format(CultureInfo.InvariantCulture, RecommendUrlTemplate, pageIndex, pageSize);
        //    var uri = new Uri(url, UriKind.Absolute);
        //    var request = WebRequest.Create(uri);
        //    using (var response=await  request.GetResponseAsync())
        //    {
        //        var document = XDocument.Load(response.GetResponseStream());
        //        return (document);
        //    }
        //}

        //[Obsolete]
        //public static async Task<IEnumerable<Blogger>> SearchBlogger(string bloggerName)
        //{
        //    // TODO
        //    if (bloggerName == null)
        //    {
        //        throw new ArgumentNullException("bloggerName");
        //    }
        //    if (bloggerName.Length <= 0)
        //    {
        //        throw new ArgumentException("博主名不能为空", "bloggerName");
        //    }
        //    throw new NotImplementedException();
        //}
    }
}
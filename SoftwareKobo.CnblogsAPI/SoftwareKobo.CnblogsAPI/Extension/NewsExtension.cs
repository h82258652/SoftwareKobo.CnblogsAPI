using SoftwareKobo.CnblogsAPI.Model;
using SoftwareKobo.CnblogsAPI.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Extension
{
    /// <summary>
    /// 新闻扩展。
    /// </summary>
    public static class NewsExtension
    {
        /// <summary>
        /// 获取新闻评论。
        /// </summary>
        /// <param name="news">新闻。</param>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>新闻评论。</returns>
        /// <exception cref="ArgumentNullException">新闻为 null。</exception>
        public static async Task<IEnumerable<NewsComment>> CommentAsync(this News news, int pageIndex, int pageSize)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            return await NewsService.CommentAsync(news.Id, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取新闻内容。
        /// </summary>
        /// <param name="news">新闻。</param>
        /// <returns>新闻内容。</returns>
        /// <exception cref="ArgumentNullException">新闻为 null。</exception>
        public static async Task<NewsDetail> DetailAsync(this News news)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            return await NewsService.DetailAsync(news.Id);
        }

        /// <summary>
        /// 发送新闻评论。
        /// </summary>
        /// <param name="news">新闻。</param>
        /// <param name="cookie">Cookie，通过登录成功获取。</param>
        /// <param name="comment">评论内容，不少于 3 个字符。</param>
        /// <returns>一段 Html，指示是否操作成功。</returns>
        public static async Task<string> SendCommentAsync(this News news, Cookie cookie, string comment)
        {
            return await UserService.SendNewsCommentAsync(cookie, news.Id, comment, 0);
        }
    }
}
using SoftwareKobo.CnblogsAPI.Model;
using SoftwareKobo.CnblogsAPI.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Extension
{
    /// <summary>
    /// 新闻内容扩展。
    /// </summary>
    public static class NewsDetailExtension
    {
        /// <summary>
        /// 获取新闻评论。
        /// </summary>
        /// <param name="newsDetail">新闻内容。</param>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>新闻评论。</returns>
        /// <exception cref="ArgumentNullException">新闻内容为 null。</exception>
        public static async Task<IEnumerable<Comment>> CommentAsync(this NewsDetail newsDetail, int pageIndex, int pageSize)
        {
            if (newsDetail == null)
            {
                throw new ArgumentNullException(nameof(newsDetail));
            }
            return await NewsService.CommentAsync(newsDetail.Id, pageIndex, pageSize);
        }

        /// <summary>
        /// 发送新闻评论。
        /// </summary>
        /// <param name="newsDetail">新闻内容。</param>
        /// <param name="cookie">Cookie。</param>
        /// <param name="comment">评论内容。</param>
        /// <returns>一段 Html，指示是否操作成功。</returns>
        public static async Task<string> SendCommentAsync(this NewsDetail newsDetail, Cookie cookie, string comment)
        {
            return await CommentService.Send(cookie, newsDetail.Id, comment);
        }
    }
}
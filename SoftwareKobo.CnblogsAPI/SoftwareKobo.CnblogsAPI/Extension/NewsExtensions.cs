using SoftwareKobo.CnblogsAPI.Model;
using SoftwareKobo.CnblogsAPI.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Extension
{
    public static class NewsExtensions
    {
        /// <summary>
        /// 获取该新闻的评论。
        /// </summary>
        /// <param name="news">新闻。</param>
        /// <returns>评论。</returns>
        /// <exception cref="ArgumentNullException">新闻为 null。</exception>
        public static async Task<IEnumerable<Comment>> CommentAsync(this News news)
        {
            if (news == null)
            {
                throw new ArgumentNullException("news");
            }
            return await NewsService.CommentsAsync(news.Id);
        }

        /// <summary>
        /// 获取该新闻的评论。
        /// </summary>
        /// <param name="news">新闻。</param>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>评论。</returns>
        /// <exception cref="ArgumentNullException">新闻为 null。</exception>
        public static async Task<IEnumerable<Comment>> CommentAsync(this News news, int pageIndex, int pageSize)
        {
            if (news == null)
            {
                throw new ArgumentNullException("news");
            }
            return await NewsService.CommentsAsync(news.Id, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取该新闻的详细。
        /// </summary>
        /// <param name="news">新闻。</param>
        /// <returns>详细。</returns>
        /// <exception cref="ArgumentNullException">新闻为 null。</exception>
        public static async Task<NewsDetail> DetailAsync(this News news)
        {
            if (news == null)
            {
                throw new ArgumentNullException("news");
            }
            return await NewsService.DetailAsync(news.Id);
        }
    }
}
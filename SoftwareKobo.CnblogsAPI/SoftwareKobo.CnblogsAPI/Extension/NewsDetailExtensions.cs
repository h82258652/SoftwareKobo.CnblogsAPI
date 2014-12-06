using SoftwareKobo.CnblogsAPI.Model;
using SoftwareKobo.CnblogsAPI.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Extension
{
    public static class NewsDetailExtensions
    {
        /// <summary>
        /// 获取该新闻的评论。
        /// </summary>
        /// <param name="newsDetail">新闻详细。</param>
        /// <returns>评论。</returns>
        /// <exception cref="ArgumentNullException">新闻详细为 null。</exception>
        public static async Task<IEnumerable<Comment>> CommentAsync(this NewsDetail newsDetail)
        {
            if (newsDetail == null)
            {
                throw new ArgumentNullException("newsDetail");
            }
            return await NewsService.CommentsAsync(newsDetail.Id);
        }

        /// <summary>
        /// 获取该新闻的评论。
        /// </summary>
        /// <param name="newsDetail">新闻详细。</param>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>评论。</returns>
        /// <exception cref="ArgumentNullException">新闻详细为 null。</exception>
        public static async Task<IEnumerable<Comment>> CommentAsync(this NewsDetail newsDetail, int pageIndex, int pageSize)
        {
            if (newsDetail == null)
            {
                throw new ArgumentNullException("newsDetail");
            }
            return await NewsService.CommentsAsync(newsDetail.Id, pageIndex, pageSize);
        }
    }
}
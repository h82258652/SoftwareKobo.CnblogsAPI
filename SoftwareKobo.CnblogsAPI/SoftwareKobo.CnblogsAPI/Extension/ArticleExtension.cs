using SoftwareKobo.CnblogsAPI.Model;
using SoftwareKobo.CnblogsAPI.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Extension
{
    /// <summary>
    /// 文章扩展。
    /// </summary>
    public static class ArticleExtension
    {
        /// <summary>
        /// 获取文章评论。
        /// </summary>
        /// <param name="article">文章。</param>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>评论列表。</returns>
        /// <exception cref="ArgumentNullException">文章为 null。</exception>
        public static async Task<IEnumerable<ArticleComment>> CommentAsync(this Article article, int pageIndex, int pageSize)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }
            return await BlogService.CommentAsync(article.Id, pageIndex, pageSize);
        }
    }
}
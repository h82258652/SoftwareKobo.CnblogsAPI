using SoftwareKobo.CnblogsAPI.Model;
using SoftwareKobo.CnblogsAPI.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Extension
{
    /// <summary>
    /// 博主扩展。
    /// </summary>
    public static class BloggerExtension
    {
        /// <summary>
        /// 分页获取个人博客文章列表。
        /// </summary>
        /// <param name="blogger">博主。</param>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>文章列表</returns>
        /// <exception cref="ArgumentNullException">博主为 null。</exception>
        public static async Task<IEnumerable<Article>> ArticleAsync(this Blogger blogger, int pageIndex, int pageSize)
        {
            if (blogger == null)
            {
                throw new ArgumentNullException(nameof(blogger));
            }
            return await BlogService.ArticleAsync(blogger.BlogApp, pageIndex, pageSize);
        }
    }
}
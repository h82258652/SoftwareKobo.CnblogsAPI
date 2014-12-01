using SoftwareKobo.CnblogsAPI.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 新闻内容。
    /// </summary>
    public class NewsDetail
    {
        /// <summary>
        /// 评论数。
        /// </summary>
        public int CommentCount
        {
            get;
            set;
        }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 新闻 Id。
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 新闻中所用到的图片。
        /// </summary>
        public List<Uri> ImageUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 下一条新闻的 Id。
        /// </summary>
        public int? NextNews
        {
            get;
            set;
        }

        /// <summary>
        /// 上一条新闻的 Id。
        /// </summary>
        public int? PrevNews
        {
            get;
            set;
        }

        /// <summary>
        /// 转载自。
        /// </summary>
        public string SourceName
        {
            get;
            set;
        }

        /// <summary>
        /// 发布时间。
        /// </summary>
        public DateTime SubmitDate
        {
            get;
            set;
        }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 获取该新闻的评论。
        /// </summary>
        /// <returns>评论。</returns>
        public async Task<IEnumerable<Comment>> CommentsAsync()
        {
            return await NewsService.CommentsAsync(Id);
        }

        /// <summary>
        /// 获取该新闻的评论。
        /// </summary>
        /// <param name="pageIndex">第几页，从 1 开始。</param>
        /// <param name="pageSize">每页条数。</param>
        /// <returns>评论。</returns>
        public async Task<IEnumerable<Comment>> CommentsAsync(int pageIndex, int pageSize)
        {
            return await NewsService.CommentsAsync(Id, pageIndex, pageSize);
        }
    }
}
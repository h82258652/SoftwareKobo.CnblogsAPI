using System;
using System.Collections.ObjectModel;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 新闻内容。
    /// </summary>
    public class NewsDetail
    {
        /// <summary>
        /// Id。
        /// </summary>
        public int Id
        {
            get;
            internal set;
        }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title
        {
            get;
            internal set;
        }

        /// <summary>
        /// 转载自。
        /// </summary>
        public string SourceName
        {
            get;
            internal set;
        }

        /// <summary>
        /// 发表时间。
        /// </summary>
        public DateTime SubmitDate
        {
            get;
            internal set;
        }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Content
        {
            get;
            internal set;
        }

        /// <summary>
        /// 新闻中用到的图片的路径。
        /// </summary>
        public ReadOnlyCollection<Uri> ImageUrl
        {
            get;
            internal set;
        }

        /// <summary>
        /// 上一条新闻的 Id。
        /// </summary>
        public int? PrevNews
        {
            get;
            internal set;
        }

        /// <summary>
        /// 下一条新闻的 Id。
        /// </summary>
        public int? NextNews
        {
            get;
            internal set;
        }

        /// <summary>
        /// 评论数。
        /// </summary>
        public int CommentCount
        {
            get;
            internal set;
        }
    }
}
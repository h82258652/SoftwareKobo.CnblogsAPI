using System;
using System.Collections.Generic;

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
    }
}
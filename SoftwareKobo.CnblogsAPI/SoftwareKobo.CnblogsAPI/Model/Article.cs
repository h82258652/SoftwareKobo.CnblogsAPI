using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 博客文章。
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 作者。
        /// </summary>
        public Author Author
        {
            get;
            set;
        }

        /// <summary>
        /// 评论数。
        /// </summary>
        public int Comments
        {
            get;
            set;
        }

        /// <summary>
        /// 推荐数。
        /// </summary>
        public int Diggs
        {
            get;
            set;
        }

        /// <summary>
        /// Id。
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 链接。
        /// </summary>
        public Uri Link
        {
            get;
            set;
        }

        /// <summary>
        /// 发布时间。
        /// </summary>
        public DateTime Published
        {
            get;
            set;
        }

        /// <summary>
        /// 摘要。
        /// </summary>
        public string Summary
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
        /// 更新时间。
        /// </summary>
        public DateTime Updated
        {
            get;
            set;
        }

        /// <summary>
        /// 查看人数。
        /// </summary>
        public int Views
        {
            get;
            set;
        }
    }
}
using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 文章。
    /// </summary>
    public class Article
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
        /// 摘要。
        /// </summary>
        public string Summary
        {
            get;
            internal set;
        }

        /// <summary>
        /// 发表时间。
        /// </summary>
        public DateTime Published
        {
            get;
            internal set;
        }

        /// <summary>
        /// 更新时间。
        /// </summary>
        public DateTime Updated
        {
            get;
            internal set;
        }

        /// <summary>
        /// 作者。
        /// </summary>
        public ArticleAuthor Author
        {
            get;
            internal set;
        }

        /// <summary>
        /// 文章链接。
        /// </summary>
        public Uri Link
        {
            get;
            internal set;
        }

        /// <summary>
        /// 推荐数。
        /// </summary>
        public int Diggs
        {
            get;
            internal set;
        }

        /// <summary>
        /// 查看数。
        /// </summary>
        public int Views
        {
            get;
            internal set;
        }

        /// <summary>
        /// 评论数。
        /// </summary>
        public int Comments
        {
            get;
            internal set;
        }
    }
}
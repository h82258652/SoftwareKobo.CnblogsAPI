using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 评论。
    /// </summary>
    public class Comment
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
        /// 标题，通常为空。
        /// </summary>
        public string Title
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
        public CommentAuthor Author
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

        internal Comment()
        {
        }
    }
}
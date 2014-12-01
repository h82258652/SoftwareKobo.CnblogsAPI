using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 评论。
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// 评论者。
        /// </summary>
        public Author Author
        {
            get;
            set;
        }

        /// <summary>
        /// 评论内容。
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 评论 Id。
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 发表时间。
        /// </summary>
        public DateTime Published
        {
            get;
            set;
        }

        /// <summary>
        /// 评论标题，通常为空。
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
    }
}
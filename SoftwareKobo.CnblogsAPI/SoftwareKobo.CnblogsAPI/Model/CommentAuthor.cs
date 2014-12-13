using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 评论作者。
    /// </summary>
    public class CommentAuthor
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name
        {
            get;
            internal set;
        }

        /// <summary>
        /// 主页。
        /// </summary>
        public Uri Uri
        {
            get;
            internal set;
        }

        internal CommentAuthor()
        {
        }
    }
}
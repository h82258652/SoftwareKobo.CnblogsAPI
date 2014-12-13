using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 文章作者。
    /// </summary>
    public class ArticleAuthor
    {
        /// <summary>
        /// 名字。
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

        /// <summary>
        /// 头像，可能为空。
        /// </summary>
        public Uri Avator
        {
            get;
            internal set;
        }

        internal ArticleAuthor()
        {
        }
    }
}
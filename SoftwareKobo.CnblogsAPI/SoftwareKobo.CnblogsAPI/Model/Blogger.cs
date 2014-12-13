using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 博主。
    /// </summary>
    public class Blogger
    {
        /// <summary>
        /// 博客主页。
        /// </summary>
        public Uri Id
        {
            get;
            internal set;
        }

        /// <summary>
        /// 博客标题。
        /// </summary>
        public string Title
        {
            get;
            internal set;
        }

        /// <summary>
        /// 最后更新日期。
        /// </summary>
        public DateTime Updated
        {
            get;
            internal set;
        }

        /// <summary>
        /// 博客主页。
        /// </summary>
        public Uri Link
        {
            get;
            internal set;
        }

        /// <summary>
        /// 用户名。
        /// </summary>
        public string BlogApp
        {
            get;
            internal set;
        }

        /// <summary>
        /// 头像，可能为空。
        /// </summary>
        public Uri Avatar
        {
            get;
            internal set;
        }

        /// <summary>
        /// 发表文章数。
        /// </summary>
        public int PostCount
        {
            get;
            internal set;
        }

        internal Blogger()
        {
        }
    }
}
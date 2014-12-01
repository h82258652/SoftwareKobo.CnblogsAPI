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
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 博客标题。
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 最后更新时间。
        /// </summary>
        public DateTime Updated
        {
            get;
            set;
        }

        /// <summary>
        /// 博客主页。
        /// </summary>
        public string Link
        {
            get;
            set;
        }
    }
}
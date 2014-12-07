using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 评论者。
    /// </summary>
    public class Author
    {
        /// <summary>
        /// 名字。
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 个人主页。
        /// </summary>
        public Uri Uri
        {
            get;
            set;
        }
    }
}
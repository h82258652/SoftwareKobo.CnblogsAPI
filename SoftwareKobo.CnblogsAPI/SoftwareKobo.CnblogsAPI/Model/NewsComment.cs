using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 新闻评论。
    /// </summary>
    public class NewsComment : Comment
    {
        /// <summary>
        /// 该新闻评论所属的新闻 Id。
        /// </summary>
        public int NewsId
        {
            get;
            set;
        }

        /// <summary>
        /// 创建一个新闻评论对象。外部使用请勿调用。
        /// </summary>
        public NewsComment()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 文章评论。
    /// </summary>
    public class ArticleComment : Comment
    {
        /// <summary>
        /// 该文章评论所属的文章 Id。
        /// </summary>
        public int ArticleId
        {
            get;
            set;
        }

        /// <summary>
        /// 创建一个文章评论对象。外部使用请勿调用。
        /// </summary>
        public ArticleComment()
        {
        }
    }
}

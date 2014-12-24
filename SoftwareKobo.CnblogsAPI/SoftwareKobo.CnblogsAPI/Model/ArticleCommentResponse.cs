using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 发送文章评论返回的对象。
    /// </summary>
    public class ArticleCommentResponse
    {
        /// <summary>
        /// 是否发送成功。
        /// </summary>
        public bool IsSuccess
        {
            get;
            internal set;
        }

        /// <summary>
        /// 博客园服务器端执行耗时的毫秒数。
        /// </summary>
        public int Duration
        {
            get;
            internal set;
        }

        /// <summary>
        /// 一段 Html 指示发送消息。
        /// </summary>
        public string Message
        {
            get;
            internal set;
        }

        internal ArticleCommentResponse()
        {
        }
    }
}

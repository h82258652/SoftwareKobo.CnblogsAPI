using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 新闻。
    /// </summary>
    public class News
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
        /// 标题。
        /// </summary>
        public string Title
        {
            get;
            internal set;
        }

        /// <summary>
        /// 摘要。
        /// </summary>
        public string Summary
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
        /// 新闻链接。
        /// </summary>
        public Uri Link
        {
            get;
            internal set;
        }

        /// <summary>
        /// 推荐数。
        /// </summary>
        public int Diggs
        {
            get;
            internal set;
        }

        /// <summary>
        /// 查看数。
        /// </summary>
        public int Views
        {
            get;
            internal set;
        }

        /// <summary>
        /// 评论数。
        /// </summary>
        public int Comments
        {
            get;
            internal set;
        }

        /// <summary>
        /// 主题。
        /// </summary>
        public string Topic
        {
            get;
            internal set;
        }

        /// <summary>
        /// 主题图标。
        /// </summary>
        public Uri TopicIcon
        {
            get;
            internal set;
        }

        /// <summary>
        /// 转载自。
        /// </summary>
        public string SourceName
        {
            get;
            internal set;
        }
    }
}
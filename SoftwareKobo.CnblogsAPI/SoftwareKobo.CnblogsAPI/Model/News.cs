using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 新闻。
    /// </summary>
    public class News
    {
        /// <summary>
        /// 评论数。
        /// </summary>
        public int Comments
        {
            get;
            set;
        }

        /// <summary>
        /// 推荐数。
        /// </summary>
        public int Diggs
        {
            get;
            set;
        }

        /// <summary>
        /// 新闻 Id。
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 新闻链接。
        /// </summary>
        public Uri Link
        {
            get;
            set;
        }

        /// <summary>
        /// 发布时间。
        /// </summary>
        public DateTime Published
        {
            get;
            set;
        }

        /// <summary>
        /// 转载自。
        /// </summary>
        public string SourceName
        {
            get;
            set;
        }

        /// <summary>
        /// 摘要。
        /// </summary>
        public string Summary
        {
            get;
            set;
        }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 主题。
        /// </summary>
        public string Topic
        {
            get;
            set;
        }

        /// <summary>
        /// 主题图标。
        /// </summary>
        public Uri TopicIcon
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

        /// <summary>
        /// 查看人数。
        /// </summary>
        public int Views
        {
            get;
            set;
        }
    }
}
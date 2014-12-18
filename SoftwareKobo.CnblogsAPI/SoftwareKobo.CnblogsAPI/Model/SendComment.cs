using Newtonsoft.Json;
using SoftwareKobo.CnblogsAPI.Converter;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 发送的评论。
    /// </summary>
    internal class SendComment
    {
        /// <summary>
        /// 新闻 Id。
        /// </summary>
        [JsonProperty(propertyName: "ContentID")]
        internal int ContentId
        {
            get;
            set;
        }

        /// <summary>
        /// 评论内容。至少 3 个字符。
        /// </summary>
        internal string Content
        {
            get;
            set;
        }

        [JsonProperty(propertyName: "strComment")]
        internal string strComment
        {
            get;
            set;
        }
        = string.Empty;

        /// <summary>
        /// 回复的评论的 Id。0 为直接回复新闻。
        /// </summary>
        [JsonProperty(propertyName: "parentCommentId")]
        [JsonConverter(typeof(Int32StringConverter))]
        internal int ParentCommentId
        {
            get;
            set;
        }

        [JsonProperty(propertyName: "title", DefaultValueHandling = DefaultValueHandling.Include)]
        internal string Title
        {
            get;
            set;
        }
    }
}
using Newtonsoft.Json;
using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Service
{
    public static class CommentService
    {
        private static async Task<string> Send(Cookie cookie, int newsId, string comment, int replyId)
        {
            if (cookie == null)
            {
                throw new ArgumentNullException(nameof(cookie));
            }
            if (newsId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newsId));
            }
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            if (comment.Length < 3)
            {
                throw new ArgumentException("评论过短", nameof(comment));
            }

            SendComment sendComment = new SendComment
            {
                ContentId = newsId,
                Content = comment,
                ParentCommentId = replyId
            };

            StringContent postData = new StringContent(JsonConvert.SerializeObject(sendComment));
            postData.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf8");

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.CookieContainer = new CookieContainer();
            clientHandler.CookieContainer.Add(new Uri("http://m.cnblogs.com/", UriKind.Absolute), cookie);

            HttpClient client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            client.DefaultRequestHeaders.Referrer = new Uri(string.Format("http://news.cnblogs.com/n/{0}", newsId));

            var response = await client.PostAsync(new Uri("http://news.cnblogs.com/mvcajax/news/InsertComment", UriKind.Absolute), postData);

            var result = await response.Content.ReadAsStringAsync();

            client.Dispose();

            return result;
        }

        /// <summary>
        /// 回复指定评论。
        /// </summary>
        /// <param name="cookie">Cookie。</param>
        /// <param name="newsId">新闻 Id。</param>
        /// <param name="comment">评论内容。</param>
        /// <param name="replyId">回复的评论的 Id。</param>
        /// <returns>返回一段 Html 内容指示是否回复成功。</returns>
        public static async Task<string> Reply(Cookie cookie, int newsId, string comment, int replyId)
        {
            if (replyId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(replyId));
            }

            return await Send(cookie, newsId, comment, replyId);
        }

        /// <summary>
        /// 发送评论。
        /// </summary>
        /// <param name="cookie">Cookie。</param>
        /// <param name="newsId">新闻 Id。</param>
        /// <param name="comment">评论内容。</param>
        /// <returns>返回一段 Html 内容指示是否回复成功。</returns>
        public static async Task<string> Send(Cookie cookie, int newsId, string comment)
        {
            return await Send(cookie, newsId, comment, 0);
        }
    }
}
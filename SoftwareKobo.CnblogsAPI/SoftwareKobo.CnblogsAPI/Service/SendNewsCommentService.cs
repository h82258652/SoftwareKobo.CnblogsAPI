using Newtonsoft.Json;
using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Service
{
    /// <summary>
    /// 发送新闻评论服务。
    /// </summary>
    public static class SendNewsCommentService
    {
        private static async Task<string> SendAsync(Cookie cookie, int newsId, string comment, int replyId)
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

            SendNewsComment sendComment = new SendNewsComment
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
        /// 回复指定新闻评论。
        /// </summary>
        /// <param name="cookie">Cookie。</param>
        /// <param name="newsId">新闻 Id。</param>
        /// <param name="comment">新闻评论内容。</param>
        /// <param name="replyId">回复的新闻评论的 Id。</param>
        /// <returns>返回一段 Html 内容指示是否回复成功。</returns>
        public static async Task<string> ReplyAsync(Cookie cookie, int newsId, string comment, int replyId)
        {
            if (replyId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(replyId));
            }

            return await SendAsync(cookie, newsId, comment, replyId);
        }

        /// <summary>
        /// 发送新闻评论。
        /// </summary>
        /// <param name="cookie">Cookie。</param>
        /// <param name="newsId">新闻 Id。</param>
        /// <param name="comment">新闻评论内容。</param>
        /// <returns>返回一段 Html 内容指示是否回复成功。</returns>
        public static async Task<string> SendAsync(Cookie cookie, int newsId, string comment)
        {
            return await SendAsync(cookie, newsId, comment, 0);
        }

        /// <summary>
        /// 将发送新闻评论返回的 Html 进行检查，判断是否发送成功。
        /// </summary>
        /// <param name="responseHtml">发送新闻评论返回的 Html。</param>
        /// <returns>是否发送成功。</returns>
        /// <exception cref="ArgumentNullException">responseHtml 为 null。</exception>
        public static bool IsSuccess(string responseHtml)
        {
            if (responseHtml==null)
            {
                throw new ArgumentNullException(nameof(responseHtml));
            }
            if (responseHtml.Contains("<span class=\"green\">刚刚发表了评论</span>"))
            {
                return true;
            }
            return false;
        }
    }
}
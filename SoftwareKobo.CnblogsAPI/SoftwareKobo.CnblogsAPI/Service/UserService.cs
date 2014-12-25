using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Service
{
    /// <summary>
    /// 用户服务。
    /// </summary>
    public static class UserService
    {
        /// <summary>
        /// 登录表单提交的 Url。
        /// </summary>
        private const string LoginUrl = @"http://m.cnblogs.com/mobileLoginPost.aspx";

        /// <summary>
        /// 访问登录页的 Url。
        /// </summary>
        private const string LoginRefererUrl = @"http://m.cnblogs.com/mobileLogin.aspx";

        /// <summary>
        /// 发送新闻评论 Url。
        /// </summary>
        private const string SendNewsCommentUrl = @"http://news.cnblogs.com/Comment/InsertComment";

        /// <summary>
        /// 发送文章评论 Url。
        /// </summary>
        private const string SendArticleCommentUrl = @"http://www.cnblogs.com/mvc/PostComment/Add.aspx";

        /// <summary>
        /// 登录博客园。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <param name="password">密码。</param>
        /// <returns>登录成功则返回 Cookie，不成功则返回 null。成功后请缓存 Cookie，请勿重复登录，以减轻博客园的压力。</returns>
        /// <exception cref="ArgumentNullException">用户名为 null。</exception>
        /// <exception cref="ArgumentNullException">密码为 null。</exception>
        /// <exception cref="ArgumentException">用户名长度为零。</exception>
        /// <exception cref="ArgumentException">密码长度为零。</exception>
        public static async Task<Cookie> LoginAsync(string userName, string password)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (userName.Length <= 0)
            {
                throw new ArgumentException("用户名不能为空。", nameof(userName));
            }
            if (password.Length <= 0)
            {
                throw new ArgumentException("密码不能为空。", nameof(password));
            }

            using (HttpClientHandler clientHandler = new HttpClientHandler())
            {
                CookieContainer cookies = new CookieContainer();
                clientHandler.CookieContainer = cookies;

                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.DefaultRequestHeaders.Referrer = new Uri(LoginRefererUrl, UriKind.Absolute);

                    var formData = new Dictionary<string, string>
                    {
                        {
                            "tbUserName", userName
                        },
                        {
                            "tbPassword", password
                        }
                    };

                    Cookie cookie = null;

                    try
                    {
                        var response = await client.PostAsync(new Uri(LoginUrl, UriKind.Absolute), new FormUrlEncodedContent(formData));
                        cookie = cookies.GetCookies(new Uri("http://m.cnblogs.com/",UriKind.Absolute))[".DottextCookie"];
                    }
                    catch
                    {
                        cookie = null;
                    }

                    return cookie;
                }
            }
        }

        /// <summary>
        /// 发送文章评论。
        /// </summary>
        /// <param name="cookie">Cookie，由登录成功获得。</param>
        /// <param name="blogApp">博主用户名。</param>
        /// <param name="articleId">文章 Id。</param>
        /// <param name="comment">文章评论内容。</param>
        /// <param name="replyId">回复评论的 Id，0 为直接回复文章。</param>
        /// <returns>一个对象指示是否发送成功、处理时间、消息。</returns>
        /// <exception cref="ArgumentNullException">cookie 为 null。</exception>
        /// <exception cref="ArgumentNullException">blogApp 为 null。</exception>
        /// <exception cref="ArgumentException">blogApp 为空字符串。</exception>
        /// <exception cref="ArgumentOutOfRangeException">articleId 小于 0。</exception>
        /// <exception cref="ArgumentNullException">comment 为 null。</exception>
        /// <exception cref="ArgumentException">comment 为空字符串。</exception>
        /// <exception cref="ArgumentOutOfRangeException">replyId 小于 0。</exception>
        public static async Task<ArticleCommentResponse> SendArticleCommentAsync(Cookie cookie, string blogApp, int articleId, string comment, int replyId)
        {
            if (cookie == null)
            {
                throw new ArgumentNullException(nameof(cookie));
            }
            if (blogApp == null)
            {
                throw new ArgumentNullException(nameof(blogApp));
            }
            if (blogApp.Length <= 0)
            {
                throw new ArgumentException("blogApp 不能为空字符串。", nameof(blogApp));
            }
            if (articleId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(articleId));
            }
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            if (comment.Length <= 0)
            {
                throw new ArgumentException("评论不能为空字符串。", nameof(comment));
            }
            if (replyId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(replyId));
            }

            var postData = new
            {
                blogApp = blogApp,
                postId = articleId,
                body = comment,
                parentCommentId = replyId
            };

            StringContent postJson = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            using (HttpClientHandler clientHandler = new HttpClientHandler())
            {
                clientHandler.CookieContainer = new CookieContainer();
                clientHandler.CookieContainer.Add(new Uri("http://m.cnblogs.com/", UriKind.Absolute), cookie);

                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                    client.DefaultRequestHeaders.Referrer = new Uri("http://www.cnblogs.com/", UriKind.Absolute);

                    var response = await client.PostAsync(new Uri(SendArticleCommentUrl, UriKind.Absolute), postJson);

                    var result = await response.Content.ReadAsStringAsync();

                    var temp = JsonConvert.DeserializeObject<JObject>(result);

                    return new ArticleCommentResponse()
                    {
                        IsSuccess = (bool)temp[nameof(ArticleCommentResponse.IsSuccess)],
                        Duration = int.Parse((string)temp[nameof(ArticleCommentResponse.Duration)]),
                        Message = (string)temp[nameof(ArticleCommentResponse.Message)]
                    };
                }
            }

        }

        /// <summary>
        /// 发送新闻评论。
        /// </summary>
        /// <param name="cookie">Cookie，由登录成功获得。</param>
        /// <param name="newsId">新闻 Id。</param>
        /// <param name="comment">新闻评论内容，至少 3 个字符。</param>
        /// <param name="replyId">回复评论的 Id，0 为直接回复新闻。</param>
        /// <returns>返回一段 Html 内容指示是否发生成功。</returns>
        /// <exception cref="ArgumentNullException">cookie 为 null。</exception>
        /// <exception cref="ArgumentOutOfRangeException">newsId 小于 0。</exception>
        /// <exception cref="ArgumentNullException">comment 为 null。</exception>
        /// <exception cref="ArgumentException">评论过短。</exception>
        /// <exception cref="ArgumentOutOfRangeException">replyId 小于 0。</exception>
        public static async Task<string> SendNewsCommentAsync(Cookie cookie, int newsId, string comment, int replyId)
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
            if (replyId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(replyId));
            }

            var postData = new
            {
                ContentID = newsId,
                Content = comment,
                strComment = string.Empty,
                parentCommentId = replyId.ToString(),
                title = string.Empty
            };

            StringContent postJson = new StringContent(JsonConvert.SerializeObject(postData));
            postJson.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=uft8");

            using (HttpClientHandler clientHandler = new HttpClientHandler())
            {
                clientHandler.CookieContainer = new CookieContainer();
                clientHandler.CookieContainer.Add(new Uri("http://m.cnblogs.com/", UriKind.Absolute), cookie);

                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                    client.DefaultRequestHeaders.Referrer = new Uri(string.Format("http://news.cnblogs.com/n/{0}", newsId));

                    var response = await client.PostAsync(new Uri(SendNewsCommentUrl, UriKind.Absolute), postJson);

                    var result = await response.Content.ReadAsStringAsync();

                    return result;
                }
            }
        }
            
        /// <summary>
        /// 将发送新闻评论返回的 Html 进行检查，判断是否发送成功。
        /// </summary>
        /// <param name="responseHtml">发送新闻评论返回的 Html。</param>
        /// <returns>是否发送成功。</returns>
        public static bool IsSendNewsCommentSuccess(string responseHtml)
        {
            if (string.IsNullOrEmpty(responseHtml))
            {
                return false;
            }
            return responseHtml.Contains("<span class=\"green\">刚刚发表了评论</span>");
        }
    }
}

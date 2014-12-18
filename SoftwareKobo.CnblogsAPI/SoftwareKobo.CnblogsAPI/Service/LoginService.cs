using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Service
{
    /// <summary>
    /// 登录服务。
    /// </summary>
    public static class LoginService
    {
        /// <summary>
        /// 登录的 Url。
        /// </summary>
        private const string LoginUrl = @"http://m.cnblogs.com/mobileLoginPost.aspx";

        /// <summary>
        /// 访问登录页的 Url。
        /// </summary>
        private const string LoginRefererUrl = @"http://m.cnblogs.com/mobileLogin.aspx";

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
                throw new ArgumentException("用户名不能为空", nameof(userName));
            }
            if (password.Length <= 0)
            {
                throw new ArgumentException("密码不能为空", nameof(password));
            }

            HttpClientHandler clientHandler = new HttpClientHandler();
            CookieContainer cookies = new CookieContainer();
            clientHandler.CookieContainer = cookies;

            HttpClient client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Referrer = new Uri(LoginRefererUrl, UriKind.Absolute);

            var formData = new Dictionary<string, string>{
                { "tbUserName",userName},
                {"tbPassword",password }
            };

            Cookie cookie = null;

            try
            {
                var response = await client.PostAsync(new Uri(LoginUrl, UriKind.Absolute), new FormUrlEncodedContent(formData));
                cookie = cookies.GetCookies(new Uri("http://m.cnblogs.com/"))[".DottextCookie"];
            }
            catch
            {
                cookie = null;
            }

            client.Dispose();

            return cookie;
        }
    }
}
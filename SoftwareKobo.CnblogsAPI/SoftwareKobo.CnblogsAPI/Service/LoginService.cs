using SoftwareKobo.CnblogsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Service
{
    /// <summary>
    /// 登录服务。
    /// </summary>
    public static class LoginService
    {
        private static Cookie Cookie;

        private const string LoginUrl = @"http://m.cnblogs.com/mobileLoginPost.aspx";

        private const string LoginRefererUrl = @"http://m.cnblogs.com/mobileLogin.aspx";

        public static async Task<Cookie> GetCookie(Action<User> setUserNameAndPassword)
        {
            if (Cookie != null)
            {
                return Cookie;
            }

            if (setUserNameAndPassword == null)
            {
                throw new ArgumentNullException(nameof(setUserNameAndPassword));
            }

            var user = new User();
            setUserNameAndPassword(user);

            Cookie = await Login(user.UserName, user.Password);
            return Cookie;
        }

        private static async Task<Cookie> Login(string userName, string password)
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
            if (password.Length < 0)
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
            return cookie;
        }
    }
}

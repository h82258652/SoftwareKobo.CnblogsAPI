using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SoftwareKobo.CnblogsAPI.Service
{
    public static class BlogService
    {
        private const string BodyUrlTemplate = "http://wcf.open.cnblogs.com/blog/post/body/{0}";

        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public static async Task<string> BodyAsync(int blogId)
        {
            if (blogId < 1)
            {
                throw new ArgumentOutOfRangeException("blogId");
            }

            var url = string.Format(BodyUrlTemplate, blogId);
            var uri = new Uri(url, UriKind.Absolute);
            var request = WebRequest.Create(uri);
            using (var response = await request.GetResponseAsync())
            {
                var document = XDocument.Load(response.GetResponseStream());
                var root = document.Root;
                if (root == null)
                {
                    return null;
                }
                return root.Value;
            }
        }
    }
}

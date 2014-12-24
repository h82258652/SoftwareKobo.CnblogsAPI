using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareKobo.CnblogsAPI.Helper
{
    internal static class UrlNoCacheHelper
    {
        internal static string WithCache(this string url)
        {
            if (url.Contains("?"))
            {
                return url + "&t=" + DateTime.Now.Ticks;
            }
            else
            {
                return url + "?t=" + DateTime.Now.Ticks;
            }
        }
    }
}

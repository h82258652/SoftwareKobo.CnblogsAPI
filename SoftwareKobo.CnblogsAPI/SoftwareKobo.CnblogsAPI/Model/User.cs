using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareKobo.CnblogsAPI.Model
{
    /// <summary>
    /// 博客园用户
    /// </summary>
    public class User
    {
        internal User()
        {
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get;
            set;
        }
    }
}

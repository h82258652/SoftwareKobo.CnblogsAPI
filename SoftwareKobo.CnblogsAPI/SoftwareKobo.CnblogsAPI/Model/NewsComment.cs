using System;

namespace SoftwareKobo.CnblogsAPI.Model
{
    public class NewsComment
    {
        public Author Author
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public DateTime Published
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public DateTime Updated
        {
            get;
            set;
        }
    }
}
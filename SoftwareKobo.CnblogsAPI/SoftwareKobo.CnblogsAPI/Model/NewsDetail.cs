using SoftwareKobo.CnblogsAPI.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Model
{
    public class NewsDetail
    {
        public int CommentCount
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

        public List<Uri> ImageUrl
        {
            get;
            set;
        }

        public int? NextNews
        {
            get;
            set;
        }

        public int? PrevNews
        {
            get;
            set;
        }

        public string SourceName
        {
            get;
            set;
        }

        public DateTime SubmitDate
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public async Task<IEnumerable<NewsComment>> CommentsAsync()
        {
            return await NewsService.CommentsAsync(Id);
        }

        public async Task<IEnumerable<NewsComment>> CommentsAsync(int pageIndex, int pageSize)
        {
            return await NewsService.CommentsAsync(Id, pageIndex, pageSize);
        }
    }
}
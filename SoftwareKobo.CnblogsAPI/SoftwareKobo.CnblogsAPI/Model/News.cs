using SoftwareKobo.CnblogsAPI.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareKobo.CnblogsAPI.Model
{
    public class News
    {
        public int Comments
        {
            get;
            set;
        }

        public int Diggs
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public Uri Link
        {
            get;
            set;
        }

        public DateTime Published
        {
            get;
            set;
        }

        public string SourceName
        {
            get;
            set;
        }

        public string Summary
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Topic
        {
            get;
            set;
        }

        public Uri TopicIcon
        {
            get;
            set;
        }

        public DateTime Updated
        {
            get;
            set;
        }

        public int Views
        {
            get;
            set;
        }

        public async Task<IEnumerable<Comment>> CommentsAsync()
        {
            return await NewsService.CommentsAsync(Id);
        }

        public async Task<IEnumerable<Comment>> CommentsAsync(int pageIndex, int pageSize)
        {
            return await NewsService.CommentsAsync(Id, pageIndex, pageSize);
        }

        public async Task<NewsDetail> DetailAsync()
        {
            return await NewsService.DetailAsync(Id);
        }
    }
}
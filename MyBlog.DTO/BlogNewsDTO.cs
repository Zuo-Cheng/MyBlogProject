using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DTO
{
    public class BlogNewsDTO
    {
        public string Title { get; set; }


        public string Content { get; set; }

        public DateTime ClearTime { get; set; }


        /// <summary>
        /// 浏览量
        /// </summary>
        public int BrowseCount { get; set; }

        /// <summary>
        /// 点赞量
        /// </summary>
        public int LikeCount { get; set; }


        public string  TypeName { get; set; }

        public string AuthorName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace MyBlog.Model
{
    public class BlogNews:BaseId
    {
        [SqlSugar.SugarColumn(ColumnDataType ="nvarchar(30)")]
        public string Title { get; set; }


        [SqlSugar.SugarColumn(ColumnDataType ="text")]
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


        public int TypeId { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// 导航属性，IsIgnore表示不生成该字段到数据库
        /// </summary>
        [SugarColumn(IsIgnore =true)]
        public TypeInfo TypeInfo { get; set; }

        [SugarColumn(IsIgnore = true)]
        public AuthorInfo AuthorInfo { get; set; }






    }
}

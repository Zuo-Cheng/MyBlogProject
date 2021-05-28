using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Model
{
    public  class AuthorInfo:BaseId
    {
        [SqlSugar.SugarColumn(ColumnDataType ="nvarchar(12)")]
        public string Name { get; set; }

        [SqlSugar.SugarColumn(ColumnDataType ="nvarchar(16)")]
        public string UserName { get; set; }

        [SqlSugar.SugarColumn(ColumnDataType = "nvarchar(64)")]
        public string UserPassword { get; set; }
    }
}

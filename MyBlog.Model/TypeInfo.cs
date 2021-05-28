using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Model
{
    public class TypeInfo:BaseId
    {
        [SqlSugar.SugarColumn(ColumnDataType ="nvarchar(12)")]
        public string TypeName { get; set; }

    }
}

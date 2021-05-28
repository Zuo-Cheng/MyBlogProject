using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace MyBlog.Model
{
    public class BaseId
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int Id { get; set; }
    }
}

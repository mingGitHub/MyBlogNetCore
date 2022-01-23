using SqlSugar;
using System;

namespace MyBlog.Model
{
    public class BaseId
    {
        [SugarColumn(IsIdentity =true,IsPrimaryKey =true)]
           public int Id { get; set; }
    }
}

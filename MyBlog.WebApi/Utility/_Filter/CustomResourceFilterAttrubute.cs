using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Utility._Filter
{
    public class CustomResourceFilterAttrubute : Attribute, IResourceFilter
    {

        private readonly IMemoryCache _cache;
        public CustomResourceFilterAttrubute(IMemoryCache cache)
        {
            _cache = cache;
        }

        //asp.net core过滤器 方法过滤器/异常/资源/授权
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            string path = context.HttpContext.Request.Path;        //api/test/getcache
            string route = context.HttpContext.Request.QueryString.Value;  //?name=name
            string key = path + route;                 //api/test/getcache?name=name
            _cache.Set(key,context.Result);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string path = context.HttpContext.Request.Path;        //api/test/getcache
            string route = context.HttpContext.Request.QueryString.Value;  //?name=name
            string key = path + route;                 //api/test/getcache?name=name
            if (_cache.TryGetValue(key,out object value))
            {
                context.Result = value as IActionResult;
            }

        }
    }
}

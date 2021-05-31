using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Utility._Filter
{
    /// <summary>
    /// 资源过滤器
    /// </summary>
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly IMemoryCache _memoryCache;
        public CustomResourceFilterAttribute(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 访问结束时
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            string path = context.HttpContext.Request.Path;
            string param = context.HttpContext.Request.QueryString.Value;
            string key = path + param;
            _memoryCache.Set(key, context.Result);
        }

        /// <summary>
        /// 访问开始时
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string path = context.HttpContext.Request.Path;
            string param = context.HttpContext.Request.QueryString.Value;
            string key = path + param;
            if(_memoryCache.TryGetValue(key,out object value))
            {
                context.Result = value as IActionResult;
            }
        }
    }
}

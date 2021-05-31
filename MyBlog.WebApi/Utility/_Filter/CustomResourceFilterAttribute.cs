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
            //已请求路径为地址作为key，这样在分页的时候也可以明确的拿到指定页的数据
            //不然的话只能拿到第一页的数据
            //在数据更新的时候，一并更新缓存中的数据，也可以通过这种路径的方式进行更新
            string key = path + param;
            //判断缓存是否有指定的key
            if(_memoryCache.TryGetValue(key,out object value))
            {
                //有的话取出数据，返回请求结果
                context.Result = value as IActionResult;
            }
        }
    }
}

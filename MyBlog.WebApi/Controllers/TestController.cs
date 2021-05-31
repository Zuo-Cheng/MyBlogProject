using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.WebApi.Utility._Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
    /// <summary>
    /// 不是用Authorize鉴权的控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("NoAuthorize")]
        public string NoAuthorize()
        {
            return "this is NoAuthorize";
        }

        [Authorize]
        [HttpGet("Authorize")]
        public string Authorize()
        {
            return "this is Authorize";
        }

        /// <summary>
        /// 通过使用[TypeFilter(typeof(类))]这种方式，构造函数就不用传参数了并且它类里面会依赖注入
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [TypeFilter(typeof(CustomResourceFilterAttribute))]
        [HttpGet("GetCache")]
        public IActionResult GetCache(string name)
        {
            return new JsonResult(new
            {
                Name = "张三",
                Age = 18,
                Gender = "男"
            });
        }
    }
}

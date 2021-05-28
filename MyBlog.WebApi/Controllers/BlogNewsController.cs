using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Utility.ApiResult;
using MyBlog.DTO;
using MyBlog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private IBlogNewsService _blogNewsService;
        public BlogNewsController(IBlogNewsService blogNewsService)
        {
            _blogNewsService = blogNewsService;
        }


        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var blogNews  = await _blogNewsService.QueryAllAsync(t=>t.AuthorId == id);
            if (blogNews == null)
                return ApiResultHelper.Error("没有查找到数据");
            return ApiResultHelper.Success(blogNews);
        }

        [HttpPost("Creater")]
        public async Task<ActionResult<ApiResult>> Creater()
        {
            var blogNews = new Model.BlogNews
            {
                AuthorId = Convert.ToInt32(this.User.FindFirst("Id").Value),
                BrowseCount = 10,
                ClearTime = DateTime.Now,
                LikeCount = 20,
                Title = "zz",
                Content = "zzz",
                TypeId = 1
            };
          var result =  await _blogNewsService.CreaterAsync(blogNews);
            if(!result) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(blogNews);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {

            var result =  await _blogNewsService.DeleteAsync(id);
            if (!result) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(result);

        }

        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id,string title,string content,int typeid)
        {
           var blogNews = await _blogNewsService.FindAsync(id);
            if(blogNews == null) return ApiResultHelper.Error("没有找到该信息");
            blogNews.Content = content;
            blogNews.Title = title;
            blogNews.TypeId = typeid;

            var result = await _blogNewsService.EditAsync(blogNews);
            if (!result) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(blogNews);
        }

        [HttpGet("GetBlogNewsPage")]
        public async Task<ActionResult<ApiResult>> GetBlogNewsPage([FromServices] IMapper impper,int pageIndex,int pageSize)
        {
            RefAsync<int> total = 0;
            var blogNewsList = await _blogNewsService.QueryAllAsync(pageIndex, pageSize, total);
            var blogNewsListDTO =  impper.Map<List<BlogNewsDTO>>(blogNewsList);
            return ApiResultHelper.Success(blogNewsListDTO);
        }
    }
}

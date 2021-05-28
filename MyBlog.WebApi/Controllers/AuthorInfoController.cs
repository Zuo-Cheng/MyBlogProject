using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Utility._MD5;
using MyBlog.Core.Utility.ApiResult;
using MyBlog.DTO;
using MyBlog.IService;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorInfoController : ControllerBase
    {

        private readonly IAuthorInfoService _authorInfoService;
        public AuthorInfoController(IAuthorInfoService authorInfoService)
        {
            _authorInfoService = authorInfoService;
        }

        [HttpPost("Creater")]
        public async Task<ActionResult<ApiResult>> Creater(string name,string userName,string pwd)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(pwd))
                return ApiResultHelper.Error("数据不能为空");
            AuthorInfo authorInfo = new AuthorInfo()
            {
                Name = name,
                UserName = userName,
                UserPassword =  MD5Helper.MD5Encrypt32(pwd)
            };
           var author = await _authorInfoService.FindAsync(t => t.UserName == userName);
            if (author != null) return ApiResultHelper.Error("当前数据已存在");
           var result = await  _authorInfoService.CreaterAsync(authorInfo);
            if (!result) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success("添加成功");
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
           var result = await _authorInfoService.DeleteAsync(id);
            if (!result) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success("删除成功");
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var authorInfo = await _authorInfoService.FindAsync(id);
            if (authorInfo == null) return ApiResultHelper.Error("没有当前用户");
            authorInfo.Name = name;
            var result = await _authorInfoService.EditAsync(authorInfo);
            if(!result)
                return ApiResultHelper.Error("失败");
            return ApiResultHelper.Success(authorInfo);
        }

        [AllowAnonymous]//AllowAnonymous添加这个注解可以不用授权匿名访问
        [HttpGet("GetAuthorInfoById")]
        public async Task<ActionResult<ApiResult>> GetAuthorInfoById([FromServices] IMapper imapper, int id)
        {
            var authorInfo = await _authorInfoService.FindAsync(id);
            if (authorInfo == null) return ApiResultHelper.Error("没有找到");
            var authorInfoDTO = imapper.Map<AuthorInfoDTO>(authorInfo);

            return ApiResultHelper.Success(authorInfoDTO);
        }


    }
}

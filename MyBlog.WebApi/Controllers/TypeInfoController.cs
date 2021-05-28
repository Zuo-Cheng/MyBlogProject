using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Utility.ApiResult;
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
    public class TypeInfoController : ControllerBase
    {
        private readonly ITypeInfoService _typeInfoService;
        public TypeInfoController(ITypeInfoService typeInfoService)
        {
            this._typeInfoService = typeInfoService;
        }

        [HttpGet("GetAllTypeInfo")]
        public async Task<ActionResult<ApiResult>> GetAllTypeInfo()
        {
            var data = await this._typeInfoService.QueryAllAsync();
            if (data == null || data.Count == 0) return ApiResultHelper.Error("没有查找到数据");

            return ApiResultHelper.Success(data);
        }

        [HttpPost("Creater")]
        public async Task<ActionResult<ApiResult>> Creater(string typeName)
        {
            if (string.IsNullOrEmpty(typeName)) return ApiResultHelper.Error("类型不能为空");
            TypeInfo typeInfo = new TypeInfo()
            {
                TypeName = typeName
            };
            var result = await this._typeInfoService.CreaterAsync(typeInfo);
            if (!result) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(result);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id,string name)
        {
           var typeInfo =await this._typeInfoService.FindAsync(id);
            if (typeInfo == null) return ApiResultHelper.Error("查找失败");
            typeInfo.TypeName = name;
            var result = await this._typeInfoService.EditAsync(typeInfo);
            if (!result) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(typeInfo);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            var result = await this._typeInfoService.DeleteAsync(id);
            if (!result) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(result);
        }
    }
}

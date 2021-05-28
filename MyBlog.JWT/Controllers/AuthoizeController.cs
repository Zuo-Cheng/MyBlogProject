using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.Core.Utility._MD5;
using MyBlog.Core.Utility.ApiResult;
using MyBlog.DTO;
using MyBlog.IService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly IAuthorInfoService _authorInfoService;
        public AuthoizeController(IAuthorInfoService authorInfoService)
        {
            _authorInfoService = authorInfoService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ApiResult>> Login(string userName, String password)
        {
            string pwd = MD5Helper.MD5Encrypt32(password);
            var authorInfo = await _authorInfoService.FindAsync(t => t.UserName == userName && t.UserPassword == pwd);

            if (authorInfo != null)
            {
                //登陆成功
                var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, authorInfo.Name),
                new Claim("Id", authorInfo.Id.ToString()),
                new Claim("UserName", authorInfo.UserName)
                //不能放敏感信息
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF"));
                //issuer代表颁发Token的Web应用程序，audience是Token的受理者
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:6060",
                    audience: "http://localhost:5000",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHelper.Success(jwtToken);
            }
            else
            {
                return ApiResultHelper.Error("登陆失败,账号或密码错误");
            }
        }


    }
}

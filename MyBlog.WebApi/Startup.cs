using Blog.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyBlog.Core.Utility._AutoMapper;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Repository;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBlog.WebApi", Version = "v1" });

                #region Swagger使用鉴权组件
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference=new OpenApiReference
              {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
              }
            },
            new string[] {}
          }
        });
                #endregion
            });

            #region SqlSugarIOC
            services.AddSqlSugar(new IocConfig()
            {
                //ConnectionString = Configuration["SqlConn"],
                //ConnectionString = Configuration.GetValue<string>("SqlConn"),
                ConnectionString = "server=112.74.53.97;uid=sa;pwd=ACxiaozu123;Database=MyBlogDB;",
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
            });
            #endregion

            #region 依赖注入
            services.AddCustomIOC();
            #endregion

            #region JWT鉴权
            services.AddCustomJWT();
            #endregion


            #region AutoMapper
            services.AddAutoMapper(typeof(CustomAutoMapperProfile));
            #endregion


            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBlog.WebApi v1"));
            }

            app.UseRouting();
            
            //添加到管道中，鉴权
            app.UseAuthentication();

            //这个是授权的
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }

    /// <summary>
    /// IOC扩展类
    /// </summary>
    public  static class IOCExtend
    {
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
            services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
            services.AddScoped<IAuthorInfoRepository, AuthorInfoRepository>();
            services.AddScoped<IBlogNewsService, BlogNewsService>();
            services.AddScoped<ITypeInfoService, TypeInfoService>();
            services.AddScoped<IAuthorInfoService, AuthorInfoService>();
            return services;
        }


       public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),
                   ValidateIssuer = true,
                   ValidIssuer = "http://localhost:6060",
                   ValidateAudience = true,
                   ValidAudience = "http://localhost:5000",
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.FromMinutes(60)
               };
           });
            return services;
        }
    }
}

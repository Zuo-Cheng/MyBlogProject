using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class BlogNewsRepository:BaseRepository<BlogNews>,IBlogNewsRepository
    {

        public async override Task<List<BlogNews>> QueryAllAsync()
        {
            //导航查询
            return await base.Context.Queryable<BlogNews>()
                 .Mapper(t => t.TypeInfo, t => t.TypeId, t => t.TypeInfo.Id)
                 .Mapper(t=>t.AuthorInfo,t=>t.AuthorId,t=>t.AuthorInfo.Id)
                 .ToListAsync();
        }

        public async override Task<List<BlogNews>> QueryAllAsync(Expression<Func<BlogNews, bool>> func)
        {
            //导航查询
            return await base.Context.Queryable<BlogNews>()
                .Where(func)
                 .Mapper(t => t.TypeInfo, t => t.TypeId, t => t.TypeInfo.Id)
                 .Mapper(t => t.AuthorInfo, t => t.AuthorId, t => t.AuthorInfo.Id)
                 .ToListAsync();
        }

        public async override Task<List<BlogNews>> QueryAllAsync(int pageIndex, int pageSize, RefAsync<int> total)
        {
            //导航查询
            return await base.Context.Queryable<BlogNews>()
                .Mapper(t => t.AuthorInfo, t => t.AuthorId, t => t.AuthorInfo.Id)
                .Mapper(t => t.TypeInfo, t => t.TypeId, t => t.TypeInfo.Id)
                .ToPageListAsync(pageIndex, pageSize, total);
        }

        public async override Task<List<BlogNews>> QueryAllAsync(Expression<Func<BlogNews, bool>> func, int pageIndex, int pageSize, RefAsync<int> total)
        {
            //导航查询
            return await this.Context.Queryable<BlogNews>()
                .Where(func)
                .Mapper(t => t.AuthorInfo, t => t.AuthorId, t => t.AuthorInfo.Id)
                .Mapper(t => t.TypeInfo, t => t.TypeId, t => t.TypeInfo.Id)
                .ToPageListAsync(pageIndex, pageSize, total);
        }
    }
}

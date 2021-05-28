using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using SqlSugar.IOC;

namespace MyBlog.Repository
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>,
        IBaseRepository<TEntity> where TEntity: class,new()
    {

        public BaseRepository(ISqlSugarClient context = null)
            :base(context)
        {
            //必须要在StartUp.cs文件中注册SqlSugarIOC服务，这里才能拿到值
            base.Context = DbScoped.Sugar;
            ////创建数据库
            //base.Context.DbMaintenance.CreateDatabase();
            ////创建表
            //base.Context.CodeFirst.InitTables(
            //    typeof(BlogNews),
            //    typeof(AuthorInfo),
            //    typeof(TypeInfo)
            //    );
        }



        public async Task<bool> CreaterAsync(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await base.DeleteByIdAsync(id);
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }

        public virtual async Task<List<TEntity>> QueryAllAsync()
        {
            return await base.GetListAsync();
        }

        public virtual async Task<List<TEntity>> QueryAllAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }

        public virtual async Task<List<TEntity>> QueryAllAsync(int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>()
                .ToPageListAsync(pageIndex, pageSize,total);
        }

        public virtual async Task<List<TEntity>> QueryAllAsync(Expression<Func<TEntity, bool>> func, int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>()
                .Where(func)
                .ToPageListAsync(pageIndex, pageSize, total);
        }
    }
}

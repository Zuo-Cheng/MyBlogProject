using MyBlog.IRepository;
using MyBlog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected IBaseRepository<TEntity> iBaseRepository;

        public BaseService()
        {

        }

        public async Task<bool> CreaterAsync(TEntity entity)
        {
            return await iBaseRepository.CreaterAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await iBaseRepository.DeleteAsync(id);
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            return await iBaseRepository.EditAsync(entity);
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await iBaseRepository.FindAsync(id);
        }

        /// <summary>
        /// 按照条件查找数据，存在返回查找到数据，否则返回null
        /// </summary>
        /// <param name="func">查找条件</param>
        /// <returns></returns>
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await iBaseRepository.FindAsync(func);
        }

        public async Task<List<TEntity>> QueryAllAsync()
        {
            return await iBaseRepository.QueryAllAsync();
        }

        public  async Task<List<TEntity>> QueryAllAsync(Expression<Func<TEntity, bool>> func)
        {
            return await iBaseRepository.QueryAllAsync(func);
        }

        public async Task<List<TEntity>> QueryAllAsync(int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await iBaseRepository.QueryAllAsync(pageIndex, pageSize, total);
        }

        public async Task<List<TEntity>> QueryAllAsync(Expression<Func<TEntity, bool>> func, int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await iBaseRepository.QueryAllAsync(func, pageIndex, pageSize, total);
        }
    }
}

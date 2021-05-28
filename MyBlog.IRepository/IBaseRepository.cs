using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
    public  interface IBaseRepository<TEntity> where TEntity :class,new()
    {

        Task<bool> CreaterAsync(TEntity entity);

        Task<bool> DeleteAsync(int id);

        Task<bool> EditAsync(TEntity entity);

        Task<TEntity> FindAsync(int id);

        /// <summary>
        /// 按照条件查找数据，存在返回查找到数据，否则返回null
        /// </summary>
        /// <param name="func">查找条件</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> QueryAllAsync();

        /// <summary>
        /// 自定义条件查询
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAllAsync(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">索引</param>
        /// <param name="pageSize">每一页的数量</param>
        /// <param name="total">总数量</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAllAsync(int pageIndex, int pageSize, RefAsync<int> total);

        /// <summary>
        /// 自定义条件分页查询
        /// </summary>
        /// <param name="func"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAllAsync(Expression<Func<TEntity, bool>> func,int pageIndex, 
            int pageSize, RefAsync<int> total);




    }
}

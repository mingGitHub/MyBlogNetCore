using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        //增加
        Task<bool> CreateAsync(TEntity entity);

        //删除
        Task<bool> DeleteAsync(int id);

        //修改
        Task<bool> EditAsync(TEntity entity);

        //查询
        Task<TEntity> FindAsync(int id);

        //自定义条件查询返回单个实体
        Task<TEntity> FindAsync(Expression<Func<TEntity,bool>> func);

        /// <summary>
        /// 查询全部的数据
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync();


        /// <summary>
        /// 自定义条件查询
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity,bool>> func);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(int page,int size,RefAsync<int> total);


        /// <summary>
        /// 自定义条件分页查询
        /// </summary>
        /// <param name="func"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func,int page, int size, RefAsync<int> total);

    }
}

using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        #region COUNT
        Task<DatabaseResult<int>> CountAsync(Expression<Func<TEntity, bool>> where);
        #endregion

        #region DELETE
        Task<DatabaseResult> DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<DatabaseResult> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region FIND_MANY
        Task<DatabaseManyResult<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, int limit = 50, int skip = 0);
        Task<DatabaseManyResult<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null, int limit = 50, int skip = 0);
        #endregion

        #region FIND_ONE
        Task<DatabaseOneResult<TEntity>> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<DatabaseOneResult<TEntity>> FindOneAsync(string id);
        Task<DatabaseOneResult<TEntity>> FindOneAsync(string id, Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null);
        Task<DatabaseOneResult<TEntity>> FindOneAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null);
        #endregion

        #region GET_ALL
        Task<DatabaseManyResult<TEntity>> GetAllAsync(int limit = 50, int skip = 0);

        Task<DatabaseManyResult<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null, int limit = 50, int skip = 0);

        Task<DatabaseManyResult<TEntity>> GetAllWithOrderAscAsync(Expression<Func<TEntity, object>> predicate, int limit = 50, int skip = 0);

        Task<DatabaseManyResult<TEntity>> GetAllWithOrderAscAsync(Expression<Func<TEntity, object>> predicate, Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null, int limit = 50, int skip = 0);


        Task<DatabaseManyResult<TEntity>> GetAllWithOrderDescAsync(Expression<Func<TEntity, object>> predicate, int limit = 50, int skip = 0);

        Task<DatabaseManyResult<TEntity>> GetAllWithOrderDescAsync(Expression<Func<TEntity, object>> predicate, Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null, int limit = 50, int skip = 0);
        #endregion

        #region INSERT
        Task<DatabaseResult> InsertManyAsync(List<TEntity> entities);

        Task<DatabaseResult> InsertOneAsync(TEntity entity);
        #endregion

        #region UPDATE
        Task<DatabaseResult> UpdateOneAsync(TEntity entity);
        #endregion

        #region SQL_QUERY
        Task<DatabaseResult> SqlQueryAsync(string query);
        Task<DatabaseResult<int>> SqlQueryIntAsync(string query);

        Task<DatabaseResult<string>> SqlQueryStringAsync(string query);

        Task<DatabaseOneResult<TEntity>> SqlQueryEntityAsync(string query);

        Task<DatabaseManyResult<TEntity>> SqlQueryManyEntitiesAsync(string query);
        #endregion
    }
}

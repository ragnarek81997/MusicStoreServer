using MongoDB.Driver;
using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class //IBaseEntity що б програміст не допустив помилку було
    {
        Task<List<TEntity>> GetAllAsync(int limit = 50, int skip = 0);
        Task<List<TResult>> GetAllAsync<TResult>(ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0);

        Task<List<TEntity>> GetAllWithOrderDescAsync(Expression<Func<TEntity, object>> predicate, int limit = 50, int skip = 0);
        Task<List<TEntity>> GetAllWithOrderAscAsync(Expression<Func<TEntity, object>> predicate, int limit = 50, int skip = 0);
        Task<List<TResult>> GetAllWithOrderAscAsync<TResult>(Expression<Func<TEntity, object>> predicate, ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0);
        Task<List<TResult>> GetAllWithOrderDescAsync<TResult>(Expression<Func<TEntity, object>> predicate, ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0);

        Task<TEntity> FindOneAsync(string id);
        Task<TResult> FindOneAsync<TResult>(string id, ProjectionDefinition<TEntity, TResult> projection);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TResult> FindOneAsync<TResult>(Expression<Func<TEntity, bool>> predicate, ProjectionDefinition<TEntity, TResult> projection);

        Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filter);

        Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, int limit = 50, int skip = 0);
        Task<List<TResult>> FindManyAsync<TResult>(Expression<Func<TEntity, bool>> predicate, ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0);

        Task<DatabaseResult> InsertOneAsync(TEntity entity);
        Task<DatabaseResult> InsertManyAsync(List<TEntity> entities);

        Task<DatabaseResult> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<DatabaseResult> DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<DatabaseResult> UpdateOneAsync(string id, UpdateDefinition<TEntity> entity);
    }
}

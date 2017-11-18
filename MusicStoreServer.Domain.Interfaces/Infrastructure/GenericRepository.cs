using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Infrastructure
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private ApplicationDbContext _applicationDbContext = null;
        public GenericRepository(ApplicationDbContext applicationDbContext = null)
        {
            _applicationDbContext = applicationDbContext ?? new ApplicationDbContext();
        }
        #region COUNT
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            return await dbSet.CountAsync(where);
        }
        #endregion

        #region DELETE
        public async Task<DatabaseResult> DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var dbResult = new DatabaseResult();

            try
            {
                var dbSet = _applicationDbContext.Set<TEntity>();

                IEnumerable<TEntity> objects = dbSet.Where<TEntity>(predicate).AsEnumerable();

                foreach (TEntity obj in objects)
                    dbSet.Remove(obj);

                await _applicationDbContext.SaveChangesAsync();

                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Exception = ex;
                dbResult.Message = "Exception DeleteManyAsync " + typeof(TEntity).Name;
                return dbResult;
            }
        }

        public async Task<DatabaseResult> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var dbResult = new DatabaseResult();

            try
            {
                var dbSet = _applicationDbContext.Set<TEntity>();

                IEnumerable<TEntity> objects = dbSet.Where<TEntity>(predicate).AsEnumerable();

                foreach (TEntity obj in objects)
                {
                    dbSet.Remove(obj);
                    break;
                }

                await _applicationDbContext.SaveChangesAsync();

                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Exception = ex;
                dbResult.Message = "Exception DeleteManyAsync " + typeof(TEntity).Name;
                return dbResult;
            }
        }
        #endregion

        #region FIND_MANY
        public async Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, int limit = 50, int skip = 0)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            return await dbSet.Where(predicate).Skip(skip).Take(limit).ToListAsync<TEntity>();
        }
        public async Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null, int limit = 50, int skip = 0)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            var dbQuery = dbSet.Include(include);

            dbQuery = (include2 != null) ? dbQuery.Include(include2) : dbQuery;
            dbQuery = (include3 != null) ? dbQuery.Include(include3) : dbQuery;
            dbQuery = (include4 != null) ? dbQuery.Include(include4) : dbQuery;

            return await dbQuery.Where(predicate).Skip(skip).Take(limit).ToListAsync<TEntity>();
        }
        #endregion

        #region FIND_ONE
        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            return await dbSet.FirstOrDefaultAsync(predicate);
        }
        public async Task<TEntity> FindOneAsync(string id)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            return await dbSet.FindAsync(id);
        }
        public async Task<TEntity> FindOneAsync(string id, Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null)
        {
            return await FindOneAsync((entity)=> entity.Id == id, include, include2, include3, include4);
        }
        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            var dbQuery = dbSet.Include(include);

            dbQuery = (include2 != null) ? dbQuery.Include(include2) : dbQuery;
            dbQuery = (include3 != null) ? dbQuery.Include(include3) : dbQuery;
            dbQuery = (include4 != null) ? dbQuery.Include(include4) : dbQuery;

            return await dbQuery.FirstOrDefaultAsync(where);
        }
        #endregion

        #region GET_ALL
        public async Task<List<TEntity>> GetAllAsync(int limit = 50, int skip = 0)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            return await dbSet.Skip(skip).Take(limit).ToListAsync<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null, int limit = 50, int skip = 0)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();

            var dbQuery = dbSet.Include(include);
            dbQuery = (include2 != null) ? dbQuery.Include(include2) : dbQuery;
            dbQuery = (include3 != null) ? dbQuery.Include(include3) : dbQuery;
            dbQuery = (include4 != null) ? dbQuery.Include(include4) : dbQuery;

            return await dbQuery.Skip(skip).Take(limit).ToListAsync<TEntity>();
        }

        public async Task<List<TEntity>> GetAllWithOrderAscAsync(Expression<Func<TEntity, object>> predicate, int limit = 50, int skip = 0)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            return await dbSet.OrderBy(predicate).Skip(skip).Take(limit).ToListAsync<TEntity>();
        }

        public async Task<List<TEntity>> GetAllWithOrderAscAsync(Expression<Func<TEntity, object>> predicate, Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null, int limit = 50, int skip = 0)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            var dbQuery = dbSet.Include(include);

            dbQuery = (include2 != null) ? dbQuery.Include(include2) : dbQuery;
            dbQuery = (include3 != null) ? dbQuery.Include(include3) : dbQuery;
            dbQuery = (include4 != null) ? dbQuery.Include(include4) : dbQuery;

            return await dbQuery.OrderBy(predicate).Skip(skip).Take(limit).ToListAsync<TEntity>();
        }


        public async Task<List<TEntity>> GetAllWithOrderDescAsync(Expression<Func<TEntity, object>> predicate, int limit = 50, int skip = 0)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            return await dbSet.OrderByDescending(predicate).Skip(skip).Take(limit).ToListAsync<TEntity>();
        }

        public async Task<List<TEntity>> GetAllWithOrderDescAsync(Expression<Func<TEntity, object>> predicate, Expression<Func<TEntity, object>> include, Expression<Func<TEntity, object>> include2 = null, Expression<Func<TEntity, object>> include3 = null, Expression<Func<TEntity, object>> include4 = null, int limit = 50, int skip = 0)
        {
            var dbSet = _applicationDbContext.Set<TEntity>();
            var dbQuery = dbSet.Include(include);

            dbQuery = (include2 != null) ? dbQuery.Include(include2) : dbQuery;
            dbQuery = (include3 != null) ? dbQuery.Include(include3) : dbQuery;
            dbQuery = (include4 != null) ? dbQuery.Include(include4) : dbQuery;

            return await dbQuery.OrderByDescending(predicate).Skip(skip).Take(limit).ToListAsync<TEntity>();
        }
        #endregion

        #region INSERT
        public async Task<DatabaseResult> InsertManyAsync(List<TEntity> entities)
        {
            var dbResult = new DatabaseResult();

            try
            {
                _applicationDbContext.Set<TEntity>().AddRange(entities);

                await _applicationDbContext.SaveChangesAsync();

                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Exception = ex;
                dbResult.Message = "Exception InsertManyAsync " + typeof(TEntity).Name;
                return dbResult;
            }
        }

        public async Task<DatabaseResult> InsertOneAsync(TEntity entity)
        {
            var dbResult = new DatabaseResult();

            try
            {
                _applicationDbContext.Set<TEntity>().Add(entity);

                await _applicationDbContext.SaveChangesAsync();

                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Exception = ex;
                dbResult.Message = "Exception InsertOneAsync " + typeof(TEntity).Name;
                return dbResult;
            }
        }
        #endregion

        #region UPDATE
        public async Task<DatabaseResult> UpdateOneAsync(TEntity entity)
        {
            var dbResult = new DatabaseResult();

            try
            {
                var dbSet = _applicationDbContext.Set<TEntity>();
                dbSet.Attach(entity);
                _applicationDbContext.Entry(entity).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();

                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Exception = ex;
                dbResult.Message = "Exception UpdateOneAsync " + typeof(TEntity).Name;
                return dbResult;
            }
        }
        #endregion
    }
}

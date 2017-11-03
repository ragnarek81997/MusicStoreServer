using MongoDB.Bson;
using MongoDB.Driver;
using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Infrastructure
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class //IBaseEntity
    {
        private DatabaseFactory _mongoDbContext = null;
        public GenericRepository(DatabaseFactory mongoDbContext = null)
        {
            _mongoDbContext = mongoDbContext != null ? mongoDbContext : new DatabaseFactory();
        }

        private IMongoCollection<TEntity> collection
        {
            get { return _mongoDbContext.GetCollection<TEntity>(); }
        }

        #region FIND

        public virtual async Task<List<TEntity>> GetAllAsync(int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(new BsonDocument()).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TResult>> GetAllAsync<TResult>(ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(new BsonDocument()).Project(projection).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TEntity>> GetAllWithOrderDescAsync(Expression<Func<TEntity, object>> predicate, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(new BsonDocument()).Sort(Builders<TEntity>.Sort.Descending(predicate)).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TEntity>> GetAllWithOrderAscAsync(Expression<Func<TEntity, object>> predicate, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(new BsonDocument()).Sort(Builders<TEntity>.Sort.Ascending(predicate)).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TResult>> GetAllWithOrderDescAsync<TResult>(Expression<Func<TEntity, object>> predicate, ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(new BsonDocument()).Project(projection).Sort(Builders<TEntity>.Sort.Descending(predicate)).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TResult>> GetAllWithOrderAscAsync<TResult>(Expression<Func<TEntity, object>> predicate, ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(new BsonDocument()).Project(projection).Sort(Builders<TEntity>.Sort.Ascending(predicate)).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<TEntity> FindOneAndUpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition, FindOneAndUpdateOptions<TEntity, TEntity> options)
        {
            var entity = await collection.FindOneAndUpdateAsync(filter, updateDefinition, options);
            return entity;
        }

        public virtual async Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(predicate).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int limit = 50, int skip = 0, bool asc = true)
        {
            var entities = asc ? await collection.Find(predicate).Sort(Builders<TEntity>.Sort.Ascending(order)).Limit(limit).Skip(skip).ToListAsync() : await collection.Find(predicate).Sort(Builders<TEntity>.Sort.Descending(order)).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TResult>> FindManyAsync<TResult>(Expression<Func<TEntity, bool>> predicate, ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(predicate).Project(projection).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TResult>> FindManyDistinctAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> field, int limit = 50, int skip = 0)
        {
            var entities = await collection.Distinct(field, predicate).ToListAsync();

            return entities;
        }

        public virtual async Task<List<TResult>> FindManyOrderDescAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(predicate).Project(projection).Sort(Builders<TEntity>.Sort.Descending(order)).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TResult>> FindManyOrderAscAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(predicate).Project(projection).Sort(Builders<TEntity>.Sort.Ascending(order)).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TEntity>> FindManyWithOrderDescAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(predicate).Sort(Builders<TEntity>.Sort.Descending(order)).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TEntity>> FindManyWithOrderAscAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(predicate).Sort(Builders<TEntity>.Sort.Ascending(order)).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TEntity>> FindManyWithMultiOrderAscAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> predicateSort, Expression<Func<TEntity, object>> predicateSortSecondary, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(predicate).Sort(Builders<TEntity>.Sort.Ascending(predicateSort).Ascending(predicateSortSecondary)).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<List<TResult>> FindManyWithMultiOrderAscAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> predicateSort, Expression<Func<TEntity, object>> predicateSortSecondary, ProjectionDefinition<TEntity, TResult> projection, int limit = 50, int skip = 0)
        {
            var entities = await collection.Find(predicate).Sort(Builders<TEntity>.Sort.Ascending(predicateSort).Ascending(predicateSortSecondary)).Project(projection).Limit(limit).Skip(skip).ToListAsync();
            return entities;
        }

        public virtual async Task<TEntity> FindOneAsync(string id)
        {
            var entity = await collection.Find(new BsonDocument("_id", new ObjectId(id))).SingleOrDefaultAsync();
            return entity;
        }

        public virtual async Task<TResult> FindOneAsync<TResult>(string id, ProjectionDefinition<TEntity, TResult> projection)
        {
            var entity = await collection.Find(new BsonDocument("_id", new ObjectId(id))).Project(projection).SingleOrDefaultAsync();
            return entity;
        }

        public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await collection.Find(predicate).SingleOrDefaultAsync();
            return entity;
        }

        public virtual async Task<TResult> FindOneAsync<TResult>(Expression<Func<TEntity, bool>> predicate, ProjectionDefinition<TEntity, TResult> projection)
        {
            var entity = await collection.Find(predicate).Project(projection).SingleOrDefaultAsync();
            return entity;
        }

        public virtual async Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filter)
        {
            var entity = await collection.Find(filter).SingleOrDefaultAsync();
            return entity;
        }

        public virtual async Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filter, ProjectionDefinition<TEntity, TEntity> projection)
        {
            var entity = await collection.Find(filter).Project(projection).SingleOrDefaultAsync();
            return entity;
        }

        public virtual async Task<bool> ExistsAsync(string id)
        {
            var entity = await collection.Find(new BsonDocument("_id", new ObjectId(id)))
                 .Project(new ProjectionDefinitionBuilder<TEntity>().Include("_id"))
                 .SingleOrDefaultAsync();
            return !entity.IsBsonNull;
        }

        #endregion

        #region INSERT

        public virtual async Task<DatabaseResult> InsertOneAsync(TEntity entity)
        {
            var dbResult = new DatabaseResult();

            try
            {
                await collection.InsertOneAsync(entity);
                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Message = "Exception InsertOneAsync " + typeof(TEntity).Name;
                dbResult.Exception = ex;
                return dbResult;
            }
        }

        public virtual async Task<DatabaseResult> InsertManyAsync(List<TEntity> entities)
        {
            var dbResult = new DatabaseResult();

            try
            {
                await collection.InsertManyAsync(entities);
                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Message = "Exception InsertManyAsync " + typeof(TEntity).Name;
                dbResult.Exception = ex;
                return dbResult;
            }
        }

        #endregion

        #region DELETE

        public virtual async Task<DatabaseResult> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var dbResult = new DatabaseResult();

            try
            {
                var res = await collection.DeleteOneAsync(predicate);
                if (res.DeletedCount < 1)
                {
                    dbResult.Message = "ERROR: DeletedCount < 1 for entity: " + typeof(TEntity).Name;
                    return dbResult;
                }

                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Message = "Exception DeleteOneAsync " + typeof(TEntity).Name;
                dbResult.Exception = ex;
                return dbResult;
            }
        }
        public virtual async Task<DatabaseResult> DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var dbResult = new DatabaseResult();
            try
            {
                var res = await collection.DeleteManyAsync(predicate);
                if (res.DeletedCount < 1)
                {
                    dbResult.Message = "Some " + typeof(TEntity).Name + "s could not be deleted.";
                    return dbResult;
                }
                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Message = "Some " + typeof(TEntity).Name + "s could not be deleted.";
                dbResult.Exception = ex;
                return dbResult;
            }
        }

        #endregion

        #region UPDATE

        public virtual async Task<DatabaseResult> UpdateOneAsync(string id, UpdateDefinition<TEntity> entity)
        {
            var dbResult = new DatabaseResult();
            try
            {
                var updateRes = await collection.UpdateOneAsync(new BsonDocument("_id", new ObjectId(id)), entity);

                if (updateRes.ModifiedCount < 1)
                {
                    dbResult.Message = "ERROR: updateRes.ModifiedCount < 1 for entity: " + typeof(TEntity).Name;
                    return dbResult;
                }

                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Message = "Exception updating entity: " + typeof(TEntity).Name;
                dbResult.Exception = ex;
                return dbResult;
            }
        }

        public virtual async Task<DatabaseResult> UpdateManyAsync(Expression<Func<TEntity, bool>> predicate, UpdateDefinition<TEntity> entity)
        {
            var dbResult = new DatabaseResult();
            try
            {
                var updateRes = await collection.UpdateManyAsync(predicate, entity);

                if (updateRes.ModifiedCount < 1)
                {
                    dbResult.Message = "ERROR: updateRes.ModifiedCount < 1 for entity: " + typeof(TEntity).Name;
                    return dbResult;
                }

                dbResult.Success = true;
                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Message = "Exception updating entity: " + typeof(TEntity).Name;
                dbResult.Exception = ex;
                return dbResult;
            }
        }

        public virtual async Task<DatabaseResult> ReplaceOneAsync(string id, TEntity entity)
        {
            var dbResult = new DatabaseResult();
            try
            {
                var updateRes = await collection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(id)), entity);
                if (updateRes.MatchedCount < 1)
                {
                    dbResult.Type = Entities.Enums.DatabaseResultType.NotMatch;
                    dbResult.Message = "ERROR: updateRes.ModifiedCount < 1 for entity: " + typeof(TEntity).Name;
                    dbResult.Success = false;
                }
                else if (updateRes.MatchedCount > 0 && updateRes.ModifiedCount != updateRes.MatchedCount)
                {
                    dbResult.Type = Entities.Enums.DatabaseResultType.NotModified;
                    dbResult.Success = false;
                }
                else
                {
                    dbResult.Success = true;
                }

                return dbResult;
            }
            catch (Exception ex)
            {
                dbResult.Message = "Exception updating entity: " + typeof(TEntity).Name;
                dbResult.Exception = ex;
                return dbResult;
            }
        }

        #endregion

        #region  COUNT

        public virtual async Task<long> CountAsync()
        {
            var entity = await collection.CountAsync(new BsonDocument());
            return entity;
        }

        public virtual async Task<long> CountByPatternAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await collection.Find(predicate).CountAsync();
            return result;
        }

        #endregion

        //And more more more ...

    }
}

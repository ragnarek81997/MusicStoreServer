using MongoDB.Driver;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Infrastructure.Data
{
    public class GenreRepository : GenericRepository<GenreModel>, IGenreRepository
    {
        public GenreRepository() : base()
        {
        }

        public async Task<DatabaseResult> Add(GenreModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Delete(string id)
        {
            return await base.DeleteOneAsync((_) => _.Id == id);
        }

        public async Task<GenreModel> Get(string id)
        {
            return await base.FindOneAsync(id);
        }

        public async Task<List<GenreModel>> GetMany(int skip, int take)
        {
            return await base.GetAllAsync(take, skip);
        }

        public async Task<List<GenreModel>> GetMany(string searchQuery, int skip, int take)
        {
            string searchLowerQuery = searchQuery.ToLower();
            return await base.FindManyAsync((_) => searchLowerQuery.Contains(_.Name.ToLower()), take, skip);
        }

        public async Task<List<GenreModel>> GetMany(List<string> ids, int skip, int take)
        {
            string idsQuery = string.Join("|", ids);
            return await base.FindManyAsync((_) => idsQuery.Contains(_.Id), take, skip);
        }

        public async Task<List<GenreModel>> GetMany(List<string> ids, string searchQuery, int skip, int take)
        {
            string idsQuery = string.Join("|", ids);
            string searchLowerQuery = searchQuery.ToLower();
            return await base.FindManyAsync((_) => idsQuery.Contains(_.Id) && searchLowerQuery.Contains(_.Name.ToLower()), take, skip);
        }

        public async Task<DatabaseResult> Update(GenreModel model)
        {
            var updateDefinition = Builders<GenreModel>.Update
                .Set(x => x.Name, model.Name);

            return await base.UpdateOneAsync(model.Id, updateDefinition);
        }
    }
}

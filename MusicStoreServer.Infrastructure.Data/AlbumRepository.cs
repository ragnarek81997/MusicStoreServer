using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Infrastructure;

namespace MusicStoreServer.Infrastructure.Data
{
    public class AlbumRepository : GenericRepository<AlbumModel>, IAlbumRepository
    {
        public AlbumRepository() : base()
        {
        }

        public async Task<DatabaseResult> Add(AlbumModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Delete(string id)
        {
            return await base.DeleteOneAsync((_)=> _.Id == id);
        }

        public async Task<DatabaseOneResult<AlbumModel>> Get(string id)
        {
            return await base.FindOneAsync(id);
        }

        public async Task<DatabaseManyResult<AlbumModel>> GetMany(int skip, int take)
        {
            return await base.GetAllAsync(take, skip);
        }

        public async Task<DatabaseManyResult<AlbumModel>> GetMany(string searchQuery, int skip, int take)
        {
            string searchLowerQuery = searchQuery.ToLower();
            return await base.FindManyAsync((_) => searchLowerQuery.Contains(_.Name.ToLower()), take, skip);
        }

        public async Task<DatabaseResult> Update(AlbumModel model)
        {
            return await base.UpdateOneAsync(model);
        }
    }
}

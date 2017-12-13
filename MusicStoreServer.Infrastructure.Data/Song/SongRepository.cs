using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Song;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using MusicStoreServer.Domain.Interfaces.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Infrastructure.Data.Song
{
    public class SongRepository : GenericRepository<SongModel>, ISongRepository
    {
        public SongRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<DatabaseResult> Add(SongModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Delete(string id)
        {
            return await base.DeleteOneAsync((_) => _.Id == id);
        }

        public async Task<DatabaseOneResult<SongModel>> Get(string id)
        {
            return await base.FindOneAsync(id, (_)=>_.Artists, (_) => _.Genres, (_) => _.Links);
        }

        public async Task<DatabaseManyResult<SongModel>> GetMany(int skip, int take)
        {
            return await base.GetAllAsync((_) => _.Artists, (_) => _.Genres, (_) => _.Links, limit: take, skip: skip);
        }

        public async Task<DatabaseManyResult<SongModel>> GetMany(string searchQuery, int skip, int take)
        {
            string searchLowerQuery = searchQuery.ToLower();
            return await base.FindManyAsync((_) => _.Name.ToLower().Contains(searchLowerQuery), (_) => _.Artists, (_) => _.Genres, (_) => _.Links, limit: take, skip: skip);
        }

        public async Task<DatabaseManyResult<SongModel>> GetMany(ICollection<string> ids, int skip, int take)
        {
            return await base.FindManyAsync((_) => ids.Contains(_.Id), take, skip);
        }

        public async Task<DatabaseResult> Update(SongModel model)
        {
            return await base.UpdateOneAsync(model);
        }
    }
}

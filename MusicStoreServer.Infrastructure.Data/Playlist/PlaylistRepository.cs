using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Playlist;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using MusicStoreServer.Domain.Interfaces.Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Infrastructure.Data.Playlist
{
    public class PlaylistRepository : GenericRepository<PlaylistModel>, IPlaylistRepository
    {
        public PlaylistRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<DatabaseResult> Add(PlaylistModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Delete(string id)
        {
            return await base.DeleteOneAsync((_) => _.Id == id);
        }

        public async Task<DatabaseOneResult<PlaylistModel>> Get(string id)
        {
            return await base.FindOneAsync(id, (_) => _.Owner, (_) => _.Songs);
        }

        public async Task<DatabaseManyResult<PlaylistModel>> GetMany(int skip, int take)
        {
            return await base.GetAllAsync((_) => _.Owner, (_) => _.Songs, limit: take, skip: skip);
        }

        public async Task<DatabaseManyResult<PlaylistModel>> GetMany(string searchQuery, int skip, int take)
        {
            string searchLowerQuery = searchQuery.ToLower();
            return await base.FindManyAsync((_) => _.Name.ToLower().Contains(searchLowerQuery), (_) => _.Owner, (_) => _.Songs, limit: take, skip: skip);
        }

        public async Task<DatabaseResult> Update(PlaylistModel model)
        {
            return await base.UpdateOneAsync(model);
        }
    }
}

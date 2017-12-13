using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Interfaces.Album;

namespace MusicStoreServer.Infrastructure.Data.Album
{
    public class AlbumRepository : GenericRepository<AlbumModel>, IAlbumRepository
    {
        public AlbumRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
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
            return await base.FindOneAsync(id, _=>_.Songs, _ => _.Artists, _ => _.Genres);
        }

        public async Task<DatabaseManyResult<AlbumModel>> GetMany(int skip, int take)
        {
            return await base.GetAllAsync(_ => _.Songs, _ => _.Artists, _ => _.Genres, limit: take, skip: skip);
        }

        public async Task<DatabaseManyResult<AlbumModel>> GetMany(string searchQuery, int skip, int take)
        {
            string searchLowerQuery = searchQuery.ToLower();
            return await base.FindManyAsync((_) => _.Name.ToLower().Contains(searchLowerQuery), _ => _.Songs, _ => _.Artists, _ => _.Genres, limit: take, skip: skip);
        }

        public async Task<DatabaseResult> Update(AlbumModel model)
        {
            return await base.UpdateOneAsync(model);
        }
    }
}

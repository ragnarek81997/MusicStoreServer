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
    public class ArtistRepository : GenericRepository<ArtistModel>, IArtistRepository
    {
        public ArtistRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<DatabaseResult> Add(ArtistModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Delete(string id)
        {
            return await base.DeleteOneAsync((_) => _.Id == id);
        }

        public async Task<DatabaseOneResult<ArtistModel>> Get(string id)
        {
            return await base.FindOneAsync(id);
        }

        public async Task<DatabaseManyResult<ArtistModel>> GetMany(int skip, int take)
        {
            return await base.GetAllAsync(take, skip);
        }

        public async Task<DatabaseManyResult<ArtistModel>> GetMany(string searchQuery, int skip, int take)
        {
            string searchLowerQuery = searchQuery.ToLower();
            return await base.FindManyAsync((_) => _.Name.ToLower().Contains(searchLowerQuery), take, skip);
        }

        public async Task<DatabaseManyResult<ArtistModel>> GetMany(ICollection<string> ids, int skip, int take)
        {
            return await base.FindManyAsync((_) => ids.Contains(_.Id), take, skip);
        }

        public async Task<DatabaseResult> Update(ArtistModel model)
        {
            return await base.UpdateOneAsync(model);
        }
    }
}

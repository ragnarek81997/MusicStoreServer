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
    public class LinkRepository : GenericRepository<LinkModel>, ILinkRepository
    {
        public LinkRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<DatabaseResult> Add(LinkModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Delete(string id)
        {
            return await base.DeleteOneAsync((_) => _.Id == id);
        }

        public async Task<DatabaseOneResult<LinkModel>> Get(string id)
        {
            return await base.FindOneAsync(id);
        }

        public async Task<DatabaseManyResult<LinkModel>> GetMany(string songId, int skip, int take)
        {
            return await base.GetAllAsync(take, skip);
        }

        public async Task<DatabaseManyResult<LinkModel>> GetMany(ICollection<string> ids, int skip, int take)
        {
            return await base.FindManyAsync((_) => ids.Contains(_.Id), take, skip);
        }

        public async Task<DatabaseResult> Update(LinkModel model)
        {
            return await base.UpdateOneAsync(model);
        }
    }
}

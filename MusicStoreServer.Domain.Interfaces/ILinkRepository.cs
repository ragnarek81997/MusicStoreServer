using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces
{
    public interface ILinkRepository : IRepository<LinkModel>
    {
        Task<DatabaseOneResult<LinkModel>> Get(string id);
        Task<DatabaseManyResult<LinkModel>> GetMany(ICollection<string> ids, int skip, int take);
        Task<DatabaseManyResult<LinkModel>> GetMany(string songId, int skip, int take);

        Task<DatabaseResult> Add(LinkModel model);
        Task<DatabaseResult> Update(LinkModel model);
        Task<DatabaseResult> Delete(string id);
    }
}

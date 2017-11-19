using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces
{
    public interface ISongRepository : IRepository<SongModel>
    {
        Task<DatabaseOneResult<SongModel>> Get(string id);

        Task<DatabaseManyResult<SongModel>> GetMany(int skip, int take);
        Task<DatabaseManyResult<SongModel>> GetMany(string searchQuery, int skip, int take);

        Task<DatabaseResult> Add(SongModel model);
        Task<DatabaseResult> Update(SongModel model);
        Task<DatabaseResult> Delete(string id);
    }
}

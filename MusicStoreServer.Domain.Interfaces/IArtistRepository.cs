using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces
{
    public interface IArtistRepository : IRepository<ArtistModel>
    {
        Task<DatabaseOneResult<ArtistModel>> Get(string id);

        Task<DatabaseManyResult<ArtistModel>> GetMany(int skip, int take);
        Task<DatabaseManyResult<ArtistModel>> GetMany(string searchQuery, int skip, int take);

        Task<DatabaseResult> Add(ArtistModel model);
        Task<DatabaseResult> Update(ArtistModel model);
        Task<DatabaseResult> Delete(string id);
    }
}

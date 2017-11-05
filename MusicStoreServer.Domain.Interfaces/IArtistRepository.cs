using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Link;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces
{
    public interface IArtistRepository : IRepository<ArtistModel>
    {
        Task<ArtistModel> Get(string id);

        Task<List<ArtistModel>> GetMany(int skip, int take);
        Task<List<ArtistModel>> GetMany(string searchQuery, int skip, int take);

        Task<List<ArtistModel>> GetMany(List<string> ids, int skip, int take);
        Task<List<ArtistModel>> GetMany(List<string> ids, string searchQuery, int skip, int take);

        Task<DatabaseResult> Add(ArtistModel model);
        Task<DatabaseResult> Update(ArtistModel model);
        Task<DatabaseResult> Delete(string id);
    }
}

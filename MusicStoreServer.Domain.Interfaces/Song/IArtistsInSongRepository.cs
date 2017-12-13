using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Song;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Song
{
    public interface IArtistsInSongRepository : IRepository<ArtistsInSongModel>
    {
        Task<DatabaseOneResult<ArtistsInSongModel>> Get(string id);

        Task<DatabaseManyResult<ArtistsInSongModel>> GetMany(string songId, int skip, int take);

        Task<DatabaseResult> Add(ArtistsInSongModel model);
        Task<DatabaseResult> Add(ICollection<ArtistsInSongModel> model);
        Task<DatabaseResult> Delete(string id);
    }
}

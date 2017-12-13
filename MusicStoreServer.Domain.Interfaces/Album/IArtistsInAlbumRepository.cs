using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Album
{
    public interface IArtistsInAlbumRepository : IRepository<ArtistsInAlbumModel>
    {
        Task<DatabaseOneResult<ArtistsInAlbumModel>> Get(string id);

        Task<DatabaseManyResult<ArtistsInAlbumModel>> GetMany(string albumId, int skip, int take);

        Task<DatabaseResult> Add(ArtistsInAlbumModel model);
        Task<DatabaseResult> Add(ICollection<ArtistsInAlbumModel> model);
        Task<DatabaseResult> Delete(string id);
    }
}

using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Album
{
    public interface ISongsInAlbumRepository : IRepository<SongsInAlbumModel>
    {
        Task<DatabaseOneResult<SongsInAlbumModel>> Get(string id);

        Task<DatabaseManyResult<SongsInAlbumModel>> GetMany(string albumId, int skip, int take);

        Task<DatabaseResult> Add(SongsInAlbumModel model);
        Task<DatabaseResult> Add(ICollection<SongsInAlbumModel> model);
        Task<DatabaseResult> Delete(string id);
    }
}

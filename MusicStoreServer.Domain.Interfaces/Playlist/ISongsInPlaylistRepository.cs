using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Playlist;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Playlist
{
    public interface ISongsInPlaylistRepository : IRepository<SongsInPlaylistModel>
    {
        Task<DatabaseOneResult<SongsInPlaylistModel>> Get(string id);

        Task<DatabaseManyResult<SongsInPlaylistModel>> GetMany(string playlistId, int skip, int take);

        Task<DatabaseResult> Add(SongsInPlaylistModel model);
        Task<DatabaseResult> Add(ICollection<SongsInPlaylistModel> model);
        Task<DatabaseResult> Delete(string id);
    }
}

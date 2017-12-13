using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Playlist;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Playlist
{
    public interface IPlaylistRepository : IRepository<PlaylistModel>
    {
        Task<DatabaseOneResult<PlaylistModel>> Get(string id);

        Task<DatabaseManyResult<PlaylistModel>> GetMany(int skip, int take);
        Task<DatabaseManyResult<PlaylistModel>> GetMany(string searchQuery, int skip, int take);

        Task<DatabaseResult> Add(PlaylistModel model);
        Task<DatabaseResult> Update(PlaylistModel model);
        Task<DatabaseResult> Delete(string id);
    }
}

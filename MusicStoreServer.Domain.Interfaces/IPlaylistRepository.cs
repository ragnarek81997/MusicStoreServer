using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces
{
    public interface IPlaylistRepository : IRepository<PlaylistModel>
    {
        Task<PlaylistModel> Get(string id);

        Task<List<PlaylistModel>> GetMany(int skip, int take);
        Task<List<PlaylistModel>> GetMany(string searchQuery, int skip, int take);

        Task<List<PlaylistModel>> GetMany(List<string> ids, int skip, int take);
        Task<List<PlaylistModel>> GetMany(List<string> ids, string searchQuery, int skip, int take);

        Task<DatabaseResult> Add(PlaylistModel model);
        Task<DatabaseResult> Update(PlaylistModel model);
        Task<DatabaseResult> Delete(string id);
    }
}

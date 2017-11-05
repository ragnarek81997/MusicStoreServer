using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Link;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces
{
    public interface ISongRepository : IRepository<SongModel>
    {
        Task<SongModel> Get(string id);

        Task<List<SongModel>> GetMany(int skip, int take);
        Task<List<SongModel>> GetMany(string searchQuery, int skip, int take);

        Task<List<SongModel>> GetMany(List<string> ids, int skip, int take);
        Task<List<SongModel>> GetMany(List<string> ids, string searchQuery, int skip, int take);

        Task<DatabaseResult> Add(SongModel model);
        Task<DatabaseResult> Update(SongModel model);
        Task<DatabaseResult> Delete(string id);
    }
}

using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces
{
    public interface IAlbumRepository : IRepository<AlbumModel>
    {
        Task<AlbumModel> Get(string id);

        Task<List<AlbumModel>> GetMany(int skip, int take);
        Task<List<AlbumModel>> GetMany(string searchQuery, int skip, int take);

        Task<List<AlbumModel>> GetMany(List<string> ids, int skip, int take);
        Task<List<AlbumModel>> GetMany(List<string> ids, string searchQuery, int skip, int take);

        Task<DatabaseResult> Add(AlbumModel model);
        Task<DatabaseResult> Update(AlbumModel model);
        Task<DatabaseResult> Delete(string id);
    }
}

using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Album
{
    public interface IAlbumRepository : IRepository<AlbumModel>
    {
        Task<DatabaseOneResult<AlbumModel>> Get(string id);

        Task<DatabaseManyResult<AlbumModel>> GetMany(int skip, int take);
        Task<DatabaseManyResult<AlbumModel>> GetMany(string searchQuery, int skip, int take);

        Task<DatabaseResult> Add(AlbumModel model);
        Task<DatabaseResult> Update(AlbumModel model);
        Task<DatabaseResult> Delete(string id);
    }
}

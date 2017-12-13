using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Album
{
    public interface IGenresInAlbumRepository : IRepository<GenresInAlbumModel>
    {
        Task<DatabaseOneResult<GenresInAlbumModel>> Get(string id);

        Task<DatabaseManyResult<GenresInAlbumModel>> GetMany(string albumId, int skip, int take);

        Task<DatabaseResult> Add(GenresInAlbumModel model);
        Task<DatabaseResult> Add(ICollection<GenresInAlbumModel> model);
        Task<DatabaseResult> Delete(string id);
    }
}

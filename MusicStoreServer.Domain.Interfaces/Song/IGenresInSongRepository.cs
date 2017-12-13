using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Song;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Song
{
    public interface IGenresInSongRepository : IRepository<GenresInSongModel>
    {
        Task<DatabaseOneResult<GenresInSongModel>> Get(string id);

        Task<DatabaseManyResult<GenresInSongModel>> GetMany(string songId, int skip, int take);

        Task<DatabaseResult> Add(GenresInSongModel model);
        Task<DatabaseResult> Add(ICollection<GenresInSongModel> model);
        Task<DatabaseResult> Delete(string id);
    }
}
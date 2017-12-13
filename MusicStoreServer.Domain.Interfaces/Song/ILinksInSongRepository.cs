using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Song;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces.Song
{
    public interface ILinksInSongRepository : IRepository<LinksInSongModel>
    {
        Task<DatabaseOneResult<LinksInSongModel>> Get(string id);

        Task<DatabaseManyResult<LinksInSongModel>> GetMany(string songId, int skip, int take);

        Task<DatabaseResult> Add(LinksInSongModel model);
        Task<DatabaseResult> Add(ICollection<LinksInSongModel> model);
        Task<DatabaseResult> Delete(string id);
    }
}
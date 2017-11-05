using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Link;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Interfaces
{
    public interface IGenreRepository : IRepository<GenreModel>
    {
        Task<GenreModel> Get(string id);

        Task<List<GenreModel>> GetMany(int skip, int take);
        Task<List<GenreModel>> GetMany(string searchQuery, int skip, int take);

        Task<List<GenreModel>> GetMany(List<string> ids, int skip, int take);
        Task<List<GenreModel>> GetMany(List<string> ids, string searchQuery, int skip, int take);

        Task<DatabaseResult> Add(GenreModel model);
        Task<DatabaseResult> Update(GenreModel model);
        Task<DatabaseResult> Delete(string id);
    }
}

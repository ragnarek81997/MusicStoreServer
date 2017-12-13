using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Song;
using MusicStoreServer.Domain.Interfaces.Song;
using System.Collections.Generic;

namespace MusicStoreServer.Infrastructure.Data.Song
{
    public class GenresInSongRepository : GenericRepository<GenresInSongModel>, IGenresInSongRepository
    {
        public async Task<DatabaseResult> Add(GenresInSongModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Add(ICollection<GenresInSongModel> model)
        {
            var dbResult = new DatabaseResult();

            model = model ?? new List<GenresInSongModel>();

            if (model.Count > 0)
            {
                var queryString = "INSERT INTO GenresInSongModels (GenreId, SongId) VALUES ";
                foreach (var item in model)
                {
                    queryString += "('" + item.GenreId + "', '" + item.SongId + "'), ";
                }
                queryString = queryString.Substring(0, queryString.Length - 2);

                dbResult = await base.SqlQueryAsync(queryString);
            }
            else
            {
                dbResult.Success = true;
            }

            return dbResult;
        }

        public async Task<DatabaseResult> Delete(string id)
        {
            return await base.DeleteOneAsync((_) => _.Id == id);
        }

        public async Task<DatabaseOneResult<GenresInSongModel>> Get(string id)
        {
            return await base.FindOneAsync(id);
        }

        public async Task<DatabaseManyResult<GenresInSongModel>> GetMany(string songId, int skip, int take)
        {
            return await base.FindManyAsync((_) => _.SongId == songId, take, skip);
        }
    }
}

using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Models.Song;
using MusicStoreServer.Domain.Interfaces.Song;
using System.Collections.Generic;

namespace MusicStoreServer.Infrastructure.Data.Song
{
    public class ArtistsInSongRepository : GenericRepository<ArtistsInSongModel>, IArtistsInSongRepository
    {
        public async Task<DatabaseResult> Add(ArtistsInSongModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Add(ICollection<ArtistsInSongModel> model)
        {
            var dbResult = new DatabaseResult();

            model = model ?? new List<ArtistsInSongModel>();

            if (model.Count > 0)
            {
                var queryString = "INSERT INTO ArtistsInSongModels (ArtistId, SongId) VALUES ";
                foreach (var item in model)
                {
                    queryString += "('" + item.ArtistId + "', '" + item.SongId + "'), ";
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

        public async Task<DatabaseOneResult<ArtistsInSongModel>> Get(string id)
        {
            return await base.FindOneAsync(id);
        }

        public async Task<DatabaseManyResult<ArtistsInSongModel>> GetMany(string songId, int skip, int take)
        {
            return await base.FindManyAsync((_) => _.SongId == songId, take, skip);
        }
    }
}

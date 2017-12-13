using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Interfaces.Playlist;
using MusicStoreServer.Domain.Entities.Models.Playlist;
using System.Collections.Generic;

namespace MusicStoreServer.Infrastructure.Data.Playlist
{
    public class SongsInPlaylistRepository : GenericRepository<SongsInPlaylistModel>, ISongsInPlaylistRepository
    {
        public async Task<DatabaseResult> Add(SongsInPlaylistModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Add(ICollection<SongsInPlaylistModel> model)
        {
            var dbResult = new DatabaseResult();

            model = model ?? new List<SongsInPlaylistModel>();

            if (model.Count > 0)
            {
                var queryString = "INSERT INTO SongsInPlaylistModels (SongId, PlaylistId) VALUES ";
                foreach (var item in model)
                {
                    queryString += "('" + item.SongId + "', '" + item.PlaylistId + "'), ";
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

        public async Task<DatabaseOneResult<SongsInPlaylistModel>> Get(string id)
        {
            return await base.FindOneAsync(id);
        }

        public async Task<DatabaseManyResult<SongsInPlaylistModel>> GetMany(string playlistId, int skip, int take)
        {
            return await base.FindManyAsync((_) => _.PlaylistId == playlistId, take, skip);
        }
    }
}

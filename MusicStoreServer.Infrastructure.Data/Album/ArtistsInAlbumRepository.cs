using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Interfaces.Album;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Infrastructure;
using System.Collections.Generic;

namespace MusicStoreServer.Infrastructure.Data.Album
{
    public class ArtistsInAlbumRepository : GenericRepository<ArtistsInAlbumModel>, IArtistsInAlbumRepository
    {
        public async Task<DatabaseResult> Add(ArtistsInAlbumModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Add(ICollection<ArtistsInAlbumModel> model)
        {
            var dbResult = new DatabaseResult();

            model = model ?? new List<ArtistsInAlbumModel>();

            if(model.Count > 0)
            {
                var queryString = "INSERT INTO ArtistsInAlbumModels (AlbumId, ArtistId) VALUES ";
                foreach (var item in model)
                {
                    queryString += "('" + item.AlbumId + "', '" + item.ArtistId + "'), ";
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

        public async Task<DatabaseOneResult<ArtistsInAlbumModel>> Get(string id)
        {
            return await base.FindOneAsync(id);
        }

        public async Task<DatabaseManyResult<ArtistsInAlbumModel>> GetMany(string albumId, int skip, int take)
        {
            return await base.FindManyAsync((_) => _.AlbumId == albumId, take, skip);
        }
    }
}

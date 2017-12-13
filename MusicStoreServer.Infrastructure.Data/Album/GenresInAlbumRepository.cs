using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Interfaces.Album;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Infrastructure;
using System.Collections.Generic;

namespace MusicStoreServer.Infrastructure.Data.Album
{
    public class GenresInAlbumRepository : GenericRepository<GenresInAlbumModel>, IGenresInAlbumRepository
    {
        public async Task<DatabaseResult> Add(GenresInAlbumModel model)
        {
            return await base.InsertOneAsync(model);
        }

        public async Task<DatabaseResult> Add(ICollection<GenresInAlbumModel> model)
        {
            var dbResult = new DatabaseResult();

            model = model ?? new List<GenresInAlbumModel>();

            if (model.Count > 0)
            {
                var queryString = "INSERT INTO GenresInAlbumModels (AlbumId, GenreId) VALUES ";
                foreach (var item in model)
                {
                    queryString += "('" + item.AlbumId + "', '" + item.GenreId + "'), ";
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

        public async Task<DatabaseOneResult<GenresInAlbumModel>> Get(string id)
        {
            return await base.FindOneAsync(id);
        }

        public async Task<DatabaseManyResult<GenresInAlbumModel>> GetMany(string albumId, int skip, int take)
        {
            return await base.FindManyAsync((_) => _.AlbumId == albumId, take, skip);
        }
    }
}

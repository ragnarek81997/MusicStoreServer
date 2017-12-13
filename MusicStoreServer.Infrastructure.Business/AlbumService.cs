using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Services.Interfaces;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Domain.Entities.Struct;
using Microsoft.AspNet.Identity;
using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using MusicStoreServer.Domain.Entities.Enums;
using System.Web.Http;
using MusicStoreServer.Infrastructure.Data.Utility.AzureBlob;
using MusicStoreServer.Domain.Entities.Dictionaries;
using System.Linq;
using System.Collections.Generic;
using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Interfaces.Album;
using MusicStoreServer.Domain.Entities.ResultModels;
using MusicStoreServer.Infrastructure.Data.Album;
using MusicStoreServer.Domain.Interfaces.Song;
using MusicStoreServer.Infrastructure.Data.Song;
using MusicStoreServer.Infrastructure.Data;
using MusicStoreServer.Domain.Entities.Models.Song;

namespace MusicStoreServer.Infrastructure.Business
{
    public class AlbumService : IAlbumService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly IAlbumRepository _albumRepository;
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IGenreRepository _genreRepository;

        public AlbumService(IAlbumRepository albumRepository, ISongRepository songRepository, IArtistRepository artistRepository, IGenreRepository genreRepository)
        {
            _applicationDbContext = new ApplicationDbContext();

            _albumRepository = new AlbumRepository(_applicationDbContext);
            _songRepository = new SongRepository(_applicationDbContext);
            _artistRepository = new ArtistRepository(_applicationDbContext);
            _genreRepository = new GenreRepository(_applicationDbContext);
        }

        public async Task<ServiceResult> Add(AlbumResultModel model)
        {
            var serviceResult = new ServiceResult();

            var songsResult = model.Songs == null || model.Songs.Count == 0 ? new DatabaseManyResult<SongModel>() { Success = true, Entities = null } : await _songRepository.GetMany(model.Songs, 0, 50);
            if (!songsResult.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = songsResult.Message;
                return serviceResult;
            }
            var artistsResult = model.Artists == null || model.Artists.Count == 0 ? new DatabaseManyResult<ArtistModel>() { Success = true, Entities = null } : await _artistRepository.GetMany(model.Artists, 0, 50);
            if (!artistsResult.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = artistsResult.Message;
                return serviceResult;
            }
            var genresResult = model.Genres == null || model.Genres.Count == 0 ? new DatabaseManyResult<GenreModel>() { Success = true, Entities = null } : await _genreRepository.GetMany(model.Genres, 0, 50);
            if (!genresResult.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = genresResult.Message;
                return serviceResult;
            }


            var modelObject = new AlbumModel()
            {
                Id = model.Id,
                Name = model.Name,
                ArtId = model.ArtId,
                Songs = songsResult.Entities,
                Artists = artistsResult.Entities,
                Genres = genresResult.Entities
            };

            var result = await _albumRepository.Add(modelObject);
            serviceResult.Success = result.Success;
            if (!result.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }

            return serviceResult;
        }

        public async Task<ServiceResult> Delete(string id)
        {
            var serviceResult = new ServiceResult();
            var result = await _albumRepository.Delete(id);
            serviceResult.Success = result.Success;
            if (!result.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult<AlbumModel>> Get(string id)
        {
            var serviceResult = new ServiceResult<AlbumModel>();
            var result = await _albumRepository.Get(id);
            if (result.Success)
            {
                serviceResult.Success = true;
                serviceResult.Result = result.Entity;
            }
            else
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<AlbumModel>>> GetMany(int skip, int take)
        {
            var serviceResult = new ServiceResult<List<AlbumModel>>();
            var result = await _albumRepository.GetMany(skip, take);
            if (result.Success)
            {
                serviceResult.Success = true;
                serviceResult.Result = result.Entities;
            }
            else
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<AlbumModel>>> GetMany(string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<AlbumModel>>();
            var result = await _albumRepository.GetMany(searchQuery, skip, take);
            if (result.Success)
            {
                serviceResult.Success = true;
                serviceResult.Result = result.Entities;
            }
            else
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult> Update(AlbumResultModel model)
        {
            var serviceResult = new ServiceResult();

            var albumResult = await this.Get(model.Id);
            if (!albumResult.Success)
            {
                serviceResult.Error = albumResult.Error;
                return serviceResult;
            }

            var songsResult = model.Songs == null || model.Songs.Count == 0 ? new DatabaseManyResult<SongModel>() { Success = true, Entities = null } : await _songRepository.GetMany(model.Songs, 0, 50);
            if (!songsResult.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = songsResult.Message;
                return serviceResult;
            }
            var artistsResult = model.Artists == null || model.Artists.Count == 0 ? new DatabaseManyResult<ArtistModel>() { Success = true, Entities = null } : await _artistRepository.GetMany(model.Artists, 0, 50);
            if (!artistsResult.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = artistsResult.Message;
                return serviceResult;
            }
            var genresResult = model.Genres == null || model.Genres.Count == 0 ? new DatabaseManyResult<GenreModel>() { Success = true, Entities = null } : await _genreRepository.GetMany(model.Genres, 0, 50);
            if (!genresResult.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = genresResult.Message;
                return serviceResult;
            }

            albumResult.Result.Name = model.Name;
            albumResult.Result.ArtId = model.ArtId;
            albumResult.Result.Songs = songsResult.Entities;
            albumResult.Result.Artists = artistsResult.Entities;
            albumResult.Result.Genres = genresResult.Entities;

            var result = await _albumRepository.Update(albumResult.Result);
            serviceResult.Success = result.Success;
            if (!result.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }
    }
}

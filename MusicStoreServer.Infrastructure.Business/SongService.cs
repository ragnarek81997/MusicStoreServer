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
using MusicStoreServer.Domain.Entities.Models.Song;
using MusicStoreServer.Domain.Interfaces.Song;
using MusicStoreServer.Domain.Entities.ResultModels;
using MusicStoreServer.Infrastructure.Data.Song;
using MusicStoreServer.Infrastructure.Data;

namespace MusicStoreServer.Infrastructure.Business
{
    public class SongService : ISongService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly ISongRepository _songRepository;
        private readonly ILinkRepository _linkRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IGenreRepository _genreRepository;

        public SongService(ISongRepository songRepository, ILinkRepository linkRepository, IArtistRepository artistRepository, IGenreRepository genreRepository)
        {
            _applicationDbContext = new ApplicationDbContext();

            _songRepository = new SongRepository(_applicationDbContext);
            _linkRepository = new LinkRepository(_applicationDbContext);
            _artistRepository = new ArtistRepository(_applicationDbContext);
            _genreRepository = new GenreRepository(_applicationDbContext);
        }

        public async Task<ServiceResult> Add(SongResultModel model)
        {
            var serviceResult = new ServiceResult();

            var linksResult = model.Links == null || model.Links.Count == 0 ? new DatabaseManyResult<LinkModel>() {  Success = true, Entities = null } : await _linkRepository.GetMany(model.Links, 0, 50);
            if (!linksResult.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = linksResult.Message;
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

            var modelObject = new SongModel()
            {
                Id = model.Id,
                Name = model.Name,
                ArtId = model.ArtId,
                Links = linksResult.Entities,
                Artists = artistsResult.Entities,
                Genres = genresResult.Entities
            };

            var result = await _songRepository.Add(modelObject);
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
            var result = await _songRepository.Delete(id);
            serviceResult.Success = result.Success;
            if (!result.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult<SongModel>> Get(string id)
        {
            var serviceResult = new ServiceResult<SongModel>();
            var result = await _songRepository.Get(id);
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

        public async Task<ServiceResult<List<SongModel>>> GetMany(int skip, int take)
        {
            var serviceResult = new ServiceResult<List<SongModel>>();
            var result = await _songRepository.GetMany(skip, take);
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

        public async Task<ServiceResult<List<SongModel>>> GetMany(string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<SongModel>>();
            var result = await _songRepository.GetMany(searchQuery, skip, take);
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

        public async Task<ServiceResult> Update(SongResultModel model)
        {
            var serviceResult = new ServiceResult();

            var songResult = await this.Get(model.Id);
            if (!songResult.Success)
            {
                serviceResult.Error = songResult.Error;
                return serviceResult;
            }

            var linksResult = model.Links == null || model.Links.Count == 0 ? new DatabaseManyResult<LinkModel>() { Success = true, Entities = null } : await _linkRepository.GetMany(model.Links, 0, 50);
            if (!linksResult.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = linksResult.Message;
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

            songResult.Result.Name = model.Name;
            songResult.Result.ArtId = model.ArtId;
            songResult.Result.Links = linksResult.Entities;
            songResult.Result.Artists = artistsResult.Entities;
            songResult.Result.Genres = genresResult.Entities;

            var result = await _songRepository.Update(songResult.Result);
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

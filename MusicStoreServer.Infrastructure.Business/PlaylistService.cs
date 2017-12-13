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
using MusicStoreServer.Domain.Entities.Models.Playlist;
using MusicStoreServer.Domain.Interfaces.Playlist;
using MusicStoreServer.Domain.Entities.ResultModels;
using MusicStoreServer.Infrastructure.Data.Playlist;
using MusicStoreServer.Domain.Interfaces.Song;
using MusicStoreServer.Infrastructure.Data.Song;
using MusicStoreServer.Domain.Entities.Models.Song;

namespace MusicStoreServer.Infrastructure.Business
{
    public class PlaylistService : IPlaylistService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly IPlaylistRepository _playlistRepository;
        private readonly ISongRepository _songRepository;

        public PlaylistService(IPlaylistRepository playlistRepository, ISongRepository songRepository)
        {
            _applicationDbContext = new ApplicationDbContext();

            _playlistRepository = new PlaylistRepository(_applicationDbContext);
            _songRepository = new SongRepository(_applicationDbContext);
        }

        public async Task<ServiceResult> Add(PlaylistResultModel model)
        {
            var serviceResult = new ServiceResult();

            var songsResult = model.Songs == null || model.Songs.Count == 0 ? new DatabaseManyResult<SongModel>() { Success = true, Entities = null } : await _songRepository.GetMany(model.Songs, 0, 50);
            if (!songsResult.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = songsResult.Message;
                return serviceResult;
            }

            var modelObject = new PlaylistModel()
            {
                Id = model.Id,
                Name = model.Name,
                ArtId = model.ArtId,
                OwnerId = model.OwnerId,
                Songs = songsResult.Entities
            };

            var result = await _playlistRepository.Add(modelObject);
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
            var result = await _playlistRepository.Delete(id);
            serviceResult.Success = result.Success;
            if (!result.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult<PlaylistModel>> Get(string id)
        {
            var serviceResult = new ServiceResult<PlaylistModel>();
            var result = await _playlistRepository.Get(id);
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

        public async Task<ServiceResult<List<PlaylistModel>>> GetMany(int skip, int take)
        {
            var serviceResult = new ServiceResult<List<PlaylistModel>>();
            var result = await _playlistRepository.GetMany(skip, take);
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

        public async Task<ServiceResult<List<PlaylistModel>>> GetMany(string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<PlaylistModel>>();
            var result = await _playlistRepository.GetMany(searchQuery, skip, take);
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

        public async Task<ServiceResult> Update(PlaylistResultModel model)
        {
            var serviceResult = new ServiceResult();

            var playlistResult = await this.Get(model.Id);
            if (!playlistResult.Success)
            {
                serviceResult.Error = playlistResult.Error;
                return serviceResult;
            }

            var songsResult = model.Songs == null || model.Songs.Count == 0 ? new DatabaseManyResult<SongModel>() { Success = true, Entities = null } : await _songRepository.GetMany(model.Songs, 0, 50);
            if (!songsResult.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = songsResult.Message;
                return serviceResult;
            }

            playlistResult.Result.Name = model.Name;
            playlistResult.Result.ArtId = model.ArtId;
            playlistResult.Result.Songs = songsResult.Entities;

            var result = await _playlistRepository.Update(playlistResult.Result);
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

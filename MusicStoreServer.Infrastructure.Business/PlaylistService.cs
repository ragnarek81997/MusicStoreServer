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

namespace MusicStoreServer.Infrastructure.Business
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistService(IPlaylistRepository playlistRepository)
        {
            this._playlistRepository = playlistRepository;
        }

        public async Task<ServiceResult> Add(PlaylistModel model)
        {
            var serviceResult = new ServiceResult();
            var result = await _playlistRepository.Add(model);
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

        public async Task<ServiceResult> Update(PlaylistModel model)
        {
            var serviceResult = new ServiceResult();
            var result = await _playlistRepository.Update(model);
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

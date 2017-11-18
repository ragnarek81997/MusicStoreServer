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
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            this._albumRepository = albumRepository;
        }

        public async Task<ServiceResult> Add(AlbumModel model)
        {
            var serviceResult = new ServiceResult();
            var result = await _albumRepository.Add(model);
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
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<AlbumModel>>> GetMany(int skip, int take)
        {
            var serviceResult = new ServiceResult<List<AlbumModel>>();
            var result = await _albumRepository.GetMany(skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<AlbumModel>>> GetMany(string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<AlbumModel>>();
            var result = await _albumRepository.GetMany(searchQuery, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<AlbumModel>>> GetMany(List<string> ids, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<AlbumModel>>();
            var result = await _albumRepository.GetMany(ids, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<AlbumModel>>> GetMany(List<string> ids, string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<AlbumModel>>();
            var result = await _albumRepository.GetMany(ids, searchQuery, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult> Update(AlbumModel model)
        {
            var serviceResult = new ServiceResult();
            var result = await _albumRepository.Update(model);
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

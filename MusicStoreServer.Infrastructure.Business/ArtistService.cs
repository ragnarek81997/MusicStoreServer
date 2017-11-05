﻿using MusicStoreServer.Domain.Entities.Enums;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Struct;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStoreServer.Infrastructure.Business
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            this._artistRepository = artistRepository;
        }

        public async Task<ServiceResult> Add(ArtistModel model)
        {
            var serviceResult = new ServiceResult();
            var result = await _artistRepository.Add(model);
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
            var result = await _artistRepository.Delete(id);
            serviceResult.Success = result.Success;
            if (!result.Success)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult<ArtistModel>> Get(string id)
        {
            var serviceResult = new ServiceResult<ArtistModel>();
            var result = await _artistRepository.Get(id);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<ArtistModel>>> GetMany(int skip, int take)
        {
            var serviceResult = new ServiceResult<List<ArtistModel>>();
            var result = await _artistRepository.GetMany(skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<ArtistModel>>> GetMany(string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<ArtistModel>>();
            var result = await _artistRepository.GetMany(searchQuery, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<ArtistModel>>> GetMany(List<string> ids, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<ArtistModel>>();
            var result = await _artistRepository.GetMany(ids, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<ArtistModel>>> GetMany(List<string> ids, string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<ArtistModel>>();
            var result = await _artistRepository.GetMany(ids, searchQuery, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult> Update(ArtistModel model)
        {
            var serviceResult = new ServiceResult();
            var result = await _artistRepository.Update(model);
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
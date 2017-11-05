﻿using MongoDB.Driver;
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
using AspNet.Identity.MongoDB;
using System.Collections.Generic;

namespace MusicStoreServer.Infrastructure.Business
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            this._songRepository = songRepository;
        }

        public async Task<ServiceResult> Add(SongModel model)
        {
            var serviceResult = new ServiceResult();
            var result = await _songRepository.Add(model);
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
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<SongModel>>> GetMany(int skip, int take)
        {
            var serviceResult = new ServiceResult<List<SongModel>>();
            var result = await _songRepository.GetMany(skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<SongModel>>> GetMany(string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<SongModel>>();
            var result = await _songRepository.GetMany(searchQuery, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<SongModel>>> GetMany(List<string> ids, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<SongModel>>();
            var result = await _songRepository.GetMany(ids, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<List<SongModel>>> GetMany(List<string> ids, string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<SongModel>>();
            var result = await _songRepository.GetMany(ids, searchQuery, skip, take);
            serviceResult.Success = result != null;
            if (result == null)
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = "Object is empty.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult> Update(SongModel model)
        {
            var serviceResult = new ServiceResult();
            var result = await _songRepository.Update(model);
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
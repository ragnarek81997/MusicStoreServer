using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Domain.Entities.Enums;
using System.Web;
using MusicStoreServer.Infrastructure.Data.Utility.AzureBlob;
using MusicStoreServer.Domain.Entities.Struct;
using System;
using MusicStoreServer.Domain.Entities.Dictionaries;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Services.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.ResultModels;
using MusicStoreServer.Domain.Entities.Models.Playlist;

namespace MusicStoreServer.Web.Controllers.ApiControllers.V1
{
    [Authorize]
    [RoutePrefix("v1/playlist")]
    public class PlaylistController : CustomApiController
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            this._playlistService = playlistService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(string id)
        {
            var result = await _playlistService.Get(id);
            return ServiceResult(result);
        }

        [HttpGet]
        [Route("GetMany")]
        public async Task<IHttpActionResult> GetMany(int skip, int take)
        {
            var result = await _playlistService.GetMany(skip, take);
            return ServiceResult(result);
        }

        [HttpGet]
        [Route("GetMany")]
        public async Task<IHttpActionResult> GetMany(string searchQuery, int skip, int take)
        {
            var result = await _playlistService.GetMany(searchQuery, skip, take);
            return ServiceResult(result);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(PlaylistResultModel model)
        {
            var serviceResult = new ServiceResult<PlaylistModel>();

            ModelState.Remove("model.Id");

            if (!ModelState.IsValid)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = "Model is not valid.";
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                return ServiceResult(serviceResult);
            }

            if(model.OwnerId != User.Identity.GetUserId())
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = "Id of owner and id of current user not equals.";
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                return ServiceResult(serviceResult);
            }

            model.Id = System.Guid.NewGuid().ToString("N").Substring(0, 24);

            var result = await _playlistService.Add(model);
            serviceResult.Success = result.Success;
            if (result.Success)
            {
                serviceResult.Result = model;
            }
            else
            {
                serviceResult.Error = result.Error;
            }
            return ServiceResult(serviceResult);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(PlaylistResultModel model)
        {
            var serviceResult = new ServiceResult<PlaylistModel>();

            if (!ModelState.IsValid)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = "Model is not valid.";
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                return ServiceResult(serviceResult);
            }

            if (model.OwnerId != User.Identity.GetUserId())
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = "Id of owner and id of current user not equals.";
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                return ServiceResult(serviceResult);
            }

            var result = await _playlistService.Update(model);
            serviceResult.Success = result.Success;
            if (result.Success)
            {
                serviceResult.Result = model;
            }
            else
            {
                serviceResult.Error = result.Error;
            }

            return ServiceResult(serviceResult);
        }
    }
}
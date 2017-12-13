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
using MusicStoreServer.Domain.Entities.Models.Album;

namespace MusicStoreServer.Web.Controllers.ApiControllers.V1
{
    [Authorize]
    [RoutePrefix("v1/album")]
    public class AlbumController : CustomApiController
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            this._albumService = albumService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(string id)
        {
            var result = await _albumService.Get(id);
            return ServiceResult(result);
        }

        [HttpGet]
        [Route("GetMany")]
        public async Task<IHttpActionResult> GetMany(int skip, int take)
        {
            var result = await _albumService.GetMany(skip, take);
            return ServiceResult(result);
        }

        [HttpGet]
        [Route("GetMany")]
        public async Task<IHttpActionResult> GetMany(string searchQuery, int skip, int take)
        {
            var result = await _albumService.GetMany(searchQuery, skip, take);
            return ServiceResult(result);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(AlbumResultModel model)
        {
            var serviceResult = new ServiceResult<AlbumModel>();

            ModelState.Remove("model.Id");

            if (!ModelState.IsValid)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = "Model is not valid.";
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                return ServiceResult(serviceResult);
            }

            model.Id = System.Guid.NewGuid().ToString("N").Substring(0, 24);

            var result = await _albumService.Add(model);
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
        public async Task<IHttpActionResult> Update(AlbumResultModel model)
        {
            var serviceResult = new ServiceResult<AlbumModel>();

            if (!ModelState.IsValid)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = "Model is not valid.";
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                return ServiceResult(serviceResult);
            }

            var result = await _albumService.Update(model);
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
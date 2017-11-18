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

namespace MusicStoreServer.Web.Controllers.ApiControllers.V1
{
    [Authorize]
    [RoutePrefix("v1/song")]
    public class SongController : CustomApiController
    {
        private readonly ISongService _songService;
        private readonly IUserService _userService;


        public SongController(ISongService songService, IUserService userService)
        {
            this._songService = songService;
            this._userService = userService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(string id)
        {
            var result = await _songService.Get(id);
            return ServiceResult(result);
        }

        [HttpGet]
        [Route("GetMany")]
        public async Task<IHttpActionResult> GetMany(int skip, int take)
        {
            var result = await _songService.GetMany(skip, take);
            return ServiceResult(result);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(SongModel model)
        {
            var serviceResult = new ServiceResult<SongModel>();

            if (!ModelState.IsValid)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = "Model is not valid.";
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                return ServiceResult(serviceResult);
            }

            //TODO: replace
            LinkModel link = null;
            foreach(var item in model.Links)
            {
                link = item;
                break;
            }

            if (link == null)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = "Temp url link not found.";
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                return ServiceResult(serviceResult);
            }

            UploadSong upload = new UploadSong();

            string inputPath = UploadSongProperties.TemporaryFolder + link.Id + UploadSongProperties.FileType;
            string sourcePath = string.Empty;
            var moveSongResult = upload.MoveFile(inputPath, sourcePath);

            if (!String.IsNullOrEmpty(moveSongResult.Error))
            {
                ModelState.AddModelError("Move", moveSongResult.Error);
                return BadRequest(ModelState);
            }

            model.Id = System.Guid.NewGuid().ToString("N").Substring(0, 10);
            model.Links = new List<LinkModel>()
            {
                link//,
                //torrentLink
            };

            var result = await _songService.Add(model);
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
        [Route("upload")]
        public async Task<IHttpActionResult> Upload()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            string path = string.Empty;

            var upload = new UploadSong();
            if (file != null)
            {
                UploadSongResult result = await upload.UploadFileSong(file, path: path);

                if (!String.IsNullOrEmpty(result.Error))
                {
                    ModelState.AddModelError("Upload", result.Error);
                    return BadRequest(ModelState);
                }

                var currentUser = await _userService.GetApplicationUser(User.Identity.GetUserId());

                var link = new LinkModel
                {
                    Id = result.Id,
                    Owner = currentUser
                };
                return Ok(link);
            }
            else
            {
                ModelState.AddModelError("FileError", "File was not received by the server");
                return BadRequest(ModelState);
            }
        }
    }
}
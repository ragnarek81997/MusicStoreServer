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
using MongoDB.Driver;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Link;

namespace MusicStoreServer.Web.Controllers.ApiControllers.V1
{
    [Authorize]
    [RoutePrefix("v1/song")]
    public class SongController : CustomApiController
    {
        private readonly ISongService _songService;
        private readonly ITorrentService _torrentService;

        public SongController(ISongService songService, ITorrentService torrentService)
        {
            this._songService = songService;
            this._torrentService = torrentService;
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

            UrlLinkModel link = null;
            foreach (var item in model.Links)
            {
                if (item is UrlLinkModel)
                {
                    link = item as UrlLinkModel;
                    if(!string.IsNullOrWhiteSpace(link.Id) && !string.IsNullOrWhiteSpace(link.OwnerId) && link.OwnerId == User.Identity.GetUserId() && string.IsNullOrWhiteSpace(link.Url))
                    {
                        break;
                    }
                    link = null;
                }
            }

            if (link == null)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = "Temp url link not found.";
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                return ServiceResult(serviceResult);
            }

            UploadSong upload = new UploadSong();

            string inputPath = UploadSongProperties.TemporaryFolder + link.Id + UploadSongProperties.SongsFolder;
            string sourcePath = UploadSongProperties.SongsFolder + link.Id + UploadSongProperties.SongsFolder;
            var moveSongResult = upload.MoveFile(inputPath, sourcePath);

            if (!String.IsNullOrEmpty(moveSongResult.Error))
            {
                ModelState.AddModelError("Move", moveSongResult.Error);
                return BadRequest(ModelState);
            }

            link.Url = UploadSongProperties.BlobAdress + moveSongResult.PathFile;

            //var downloadSongResult = await upload.DownloadFileToStreamAsync(link.Url);
            //if(downloadSongResult == null)
            //{
            //    serviceResult.Success = false;
            //    serviceResult.Error.Description = "Error of download file from blob.";
            //    serviceResult.Error.Code = ErrorStatusCode.BudRequest;
            //    return ServiceResult(serviceResult);
            //}

            //var torrentServiceResult = await _torrentService.Create(link.Url, downloadSongResult.GetBuffer(), link.Id);
            //if (!torrentServiceResult.Success)
            //{
            //    return ServiceResult(torrentServiceResult);
            //}

            //var torrentLink = new TorrentLinkModel()
            //{
            //    Id = System.Guid.NewGuid().ToString("N").Substring(0, 10),
            //    OwnerId = User.Identity.GetUserId(),
            //    Torrent = torrentServiceResult.Result
            //};

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

                var link = new UrlLinkModel
                {
                    Id = result.Id,
                    OwnerId = User.Identity.GetUserId()
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
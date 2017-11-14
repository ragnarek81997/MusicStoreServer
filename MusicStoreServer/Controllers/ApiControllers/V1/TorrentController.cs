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
using System.Security.Cryptography;
using MusicStoreServer.Web.Extensions;
using System.IO;

namespace MusicStoreServer.Web.Controllers.ApiControllers.V1
{
    [AllowAnonymous]
    [RoutePrefix("v1/torrent")]
    public class TorrentController : CustomApiController
    {
        private readonly ITorrentService _torrentService;

        public TorrentController(ITorrentService torrentService)
        {
            this._torrentService = torrentService;
        }

        [HttpGet]
        [Route("{url}")]
        public async Task<IHttpActionResult> Get(string url)
        {
            //url = "https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3";
            var result = await _torrentService.Create(url);
            //File.WriteAllBytes(@"D:\1.torrent", result.Result);
            return ServiceResult(result);
        }
    }
}
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

namespace MusicStoreServer.Web.Controllers.ApiControllers.V1
{
    [Authorize]
    [RoutePrefix("api/v1/profile")]
    public class ProfileController : ApiController
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            this._userService = userService;
        }

        #region Initialization
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion


        // POST api/Profile/upload/photo
        /// <remarks>
        ///     <para>
        ///       attach photo to you request
        ///     </para>
        /// </remarks>
        [HttpPost]
        [Route("upload/photo")]
        public async Task<IHttpActionResult> UploadPhoto(bool saveToTemporary = true)
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            string path = string.Empty;

            string userId = User.Identity.GetUserId();
            UploadImage uploadImage = new UploadImage(userId);
            if (file != null)
            {
                if (!saveToTemporary)
                    path = UploadImageProperties.ImageUserFolder;

                UploadImageResult result = await uploadImage.ImageCropAndResize(file, w: UploadImageProperties.ImageSmallResize, h: UploadImageProperties.ImageSmallResize, path: path);

                if (!String.IsNullOrEmpty(result.Error))
                {
                    ModelState.AddModelError("Upload", result.Error);
                    return BadRequest(ModelState);
                }

                var adress = UploadImageProperties.BlobAdress + result.PathFile;
                return Ok(adress);
            }
            else
            {
                ModelState.AddModelError("FileError", "File was not received by the server");
                return BadRequest(ModelState);
            }
        }


        // POST api/Profile/me
        /// <remarks>
        ///     <para>
        ///       Authorize. Return Info for current user
        ///     </para>
        /// </remarks>
        [HttpGet]
        [Route("me")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            var userId = User.Identity.GetUserId();
            var result = await _userService.GetCurrentUser(userId);
            return Ok(result);
        }


        /// <remarks>
        /// api/v1/athlete/info
        /// Only coaches can use this method
        ///        <para>
        ///        "userId": "string", //Athlete Id. 
        ///        </para>
        /// </remarks>
        /// 

        // [Authorize(Roles = "Coach")]
        [HttpGet]
        [Route("{userId}")]
        public async Task<IHttpActionResult> GetUser(string userId)
        {
            var result = await _userService.GetCurrentUser(userId);

            return Ok(result);
        }
    }
}
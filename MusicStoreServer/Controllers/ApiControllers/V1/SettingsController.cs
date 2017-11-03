using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MusicStoreServer.Domain.Entities.Struct;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Services.Interfaces;

namespace MusicStoreServer.Web.Controllers.ApiControllers.V1
{
    [Authorize]
    [RoutePrefix("api/v1/settings")]
    public class SettingsController : CustomApiController
    {
        private readonly IUserService _userServise;
        private readonly ISettingsService _settingsService;

        public SettingsController(IUserService _userServise, ISettingsService _settingsService)
        {
            this._userServise = _userServise;
            this._settingsService = _settingsService;
        }

        // POST api/v1/settings/info
        /// <remarks>
        /// /info
        ///       Only for authorize users. Use this method for get  basic and sport info for current user .
        ///      <para>
        ///       without params
        ///        </para>
        /// </remarks>
        [HttpGet]
        [Route("info")]
        public async Task<IHttpActionResult> GetInfo()
        {
            var userId = User.Identity.GetUserId();
            var result = await _userServise.GetCurrentUser(userId);
            return Ok(result);
        }



        [HttpPost]
        [Route("changepassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            var userId = User.Identity.GetUserId();
            var result = await _settingsService.ChangePassword(userId, model);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }



        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("Identity", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }



    }
}

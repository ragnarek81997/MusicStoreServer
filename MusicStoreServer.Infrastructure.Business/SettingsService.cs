using Microsoft.AspNet.Identity;
using MusicStoreServer.Domain.Entities;
using MusicStoreServer.Domain.Entities.ViewModels;
using MusicStoreServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using MusicStoreServer.Domain.Interfaces;
using System.Net;
using MusicStoreServer.Domain.Entities.Models;
using System.Configuration;
using MusicStoreServer.Domain.Entities.Infrastructure;
using System.Net.Http;

namespace MusicStoreServer.Infrastructure.Business
{
    public class SettingsService : ISettingsService
    {

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

        public async Task<IdentityResult> ChangePassword(string userId, ChangePasswordBindingModel model)
        {
            IdentityResult result = await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

            return result;
        }


    }
}

using MongoDB.Driver;
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

namespace MusicStoreServer.Infrastructure.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private ServiceResult<ApplicationUser> _serviceResult;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        #region Initialization
        private ApplicationUserManager _userManager;
        //private ApplicationRoleManager _roleManager;

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

        //public ApplicationRoleManager RoleManager
        //{
        //    get
        //    {
        //        return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
        //    }
        //    private set
        //    {
        //        _roleManager = value;
        //    }
        //}
        #endregion

        public async Task<ShortUser> GetCurrentUser(string id)
        {
            var result = await _userRepository.GetCurrentUser(id);
            result.PhotoPath = UploadImageProperties.BlobAdress + result.PhotoPath;
            return result;
        }
    }
}

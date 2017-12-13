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
using MusicStoreServer.Infrastructure.Data;

namespace MusicStoreServer.Infrastructure.Business
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _applicationDbContext = new ApplicationDbContext();

            _userRepository = new UserRepository(_applicationDbContext);
        }

        #region initialization
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

        public async Task<ServiceResult<ApplicationUser>> GetApplicationUser(string userId)
        {
            var serviceResult = new ServiceResult<ApplicationUser>();
            var result = await _userRepository.Get(userId);
            if (result.Success)
            {
                serviceResult.Success = true;
                serviceResult.Result = result.Entity;
            }
            else
            {
                serviceResult.Error.Code = ErrorStatusCode.BudRequest;
                serviceResult.Error.Description = result.Message;
            }
            return serviceResult;
        }

        public async Task<ServiceResult<ShortUser>> GetShortUser(string userId)
        {
            var serviceResult = new ServiceResult<ShortUser>();

            var result = await this.GetApplicationUser(userId);
            if (result.Success)
            {
                serviceResult.Success = true;
                serviceResult.Result = new ShortUser()
                {
                    Id = result.Result.Id,
                    Email = result.Result.Email,
                    FirstName = result.Result.FirstName,
                    LastName = result.Result.LastName,
                    PhotoId = result.Result.PhotoId
                };
            }
            else
            {
                serviceResult.Error = result.Error;
            }
            return serviceResult;
        }
    }
}

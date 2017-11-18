using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Interfaces;
using MusicStoreServer.Domain.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.ViewModels;
using Microsoft.AspNet.Identity;
using MusicStoreServer.Domain.Entities.Enums;

namespace MusicStoreServer.Infrastructure.Data
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public async Task<ApplicationUser> GetApplicationUser(string userId)
        {
            return await base.FindOneAsync(userId);
        }

        public async Task<ShortUser> GetShortUser(string userId)
        {
            var resultObject = await this.GetApplicationUser(userId);
            var userObject = new ShortUser()
            {
                Id = resultObject.Id,
                Email = resultObject.Email,
                FirstName = resultObject.FirstName,
                LastName = resultObject.LastName,
                PhotoId = resultObject.PhotoId
            };
            return userObject;
        }
    }
}

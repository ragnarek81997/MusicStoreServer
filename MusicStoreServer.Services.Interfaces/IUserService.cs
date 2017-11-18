using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreServer.Domain.Entities.Infrastructure;
using MusicStoreServer.Domain.Entities.Struct;
using MusicStoreServer.Domain.Entities.ViewModels;

namespace MusicStoreServer.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetApplicationUser(string userId);
        Task<ShortUser> GetShortUser(string userId);
    }
}

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
        public async Task<DatabaseOneResult<ApplicationUser>> Get(string id)
        {
            return await base.FindOneAsync(id);
        }
    }
}

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
using MongoDB.Driver;
using Microsoft.AspNet.Identity;
using MusicStoreServer.Domain.Entities.Enums;

namespace MusicStoreServer.Infrastructure.Data
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly ProjectionDefinition<ApplicationUser, ShortUser> ShortUserProjection = Builders<ApplicationUser>.Projection.Expression(x => new ShortUser()
        {
            Id = x.Id,
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Image = x.Image,
            PhoneNumber = x.PhoneNumber,
            Roles = x.Roles
        });

        public async Task<ShortUser> GetCurrentUser(string userId)
        {
            return await base.FindOneAsync(userId, ShortUserProjection);
        }


    }
}

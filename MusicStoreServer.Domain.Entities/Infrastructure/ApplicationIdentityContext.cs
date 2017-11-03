using AspNet.Identity.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Infrastructure
{
    public class ApplicationIdentityContext : IDisposable
    {
        public static ApplicationIdentityContext Create()
        {
            var collections = DatabaseFactory.GetIdentityCollections();
            return new ApplicationIdentityContext(collections.Item1, collections.Item2);
        }

        private ApplicationIdentityContext(IMongoCollection<ApplicationUser> users, IMongoCollection<IdentityRole> roles)
        {
            ApplicationUsers = users;
            IdentityRoles = roles;
        }

        public IMongoCollection<IdentityRole> IdentityRoles { get; set; }

        public IMongoCollection<ApplicationUser> ApplicationUsers { get; set; }

        public Task<List<IdentityRole>> AllRolesAsync()
        {
            return IdentityRoles.Find(r => true).ToListAsync();
        }

        public void Dispose()
        {
        }
    }
}

using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStoreServer.Domain.Entities.Infrastructure
{
    public class ApplicationUser : IdentityUser, IBaseEntity
    {
        [StringLength(24, MinimumLength = 24)]
        public string PhotoId { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

    }
}
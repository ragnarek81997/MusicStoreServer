using Microsoft.AspNet.Identity.EntityFramework;
using MusicStoreServer.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SQLDatabaseConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<AlbumModel> Albums { get; set; }
        public DbSet<ArtistModel> Artists { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public DbSet<LinkModel> Links { get; set; }
        public DbSet<PlaylistModel> Playlists { get; set; }
        public DbSet<SongModel> Songs { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

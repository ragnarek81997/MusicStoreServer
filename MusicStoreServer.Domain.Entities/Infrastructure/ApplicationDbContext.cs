using Microsoft.AspNet.Identity.EntityFramework;
using MusicStoreServer.Domain.Entities.Models;
using MusicStoreServer.Domain.Entities.Models.Album;
using MusicStoreServer.Domain.Entities.Models.Playlist;
using MusicStoreServer.Domain.Entities.Models.Song;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");

            modelBuilder.Entity<AlbumModel>()
                .HasMany(p => p.Songs)
                .WithMany(s => s.Albums)
                .Map(c =>
                {
                    c.MapLeftKey("AlbumId");
                    c.MapRightKey("SongId");
                    c.ToTable("SongsInAlbums");
                });
            modelBuilder.Entity<AlbumModel>()
                .HasMany(p => p.Artists)
                .WithMany(s => s.Albums)
                .Map(c =>
                {
                    c.MapLeftKey("AlbumId");
                    c.MapRightKey("ArtistId");
                    c.ToTable("ArtistsInAlbums");
                });
            modelBuilder.Entity<AlbumModel>()
                .HasMany(p => p.Genres)
                .WithMany(s => s.Albums)
                .Map(c =>
                {
                    c.MapLeftKey("AlbumId");
                    c.MapRightKey("GenreId");
                    c.ToTable("GenresInAlbums");
                });

            modelBuilder.Entity<PlaylistModel>()
                .HasMany(p => p.Songs)
                .WithMany(s => s.Playlists)
                .Map(c =>
                {
                    c.MapLeftKey("PlaylistId");
                    c.MapRightKey("SongId");
                    c.ToTable("SongsInPlaylists");
                });

            modelBuilder.Entity<SongModel>()
                .HasMany(p => p.Artists)
                .WithMany(s => s.Songs)
                .Map(c =>
                {
                    c.MapLeftKey("SongId");
                    c.MapRightKey("ArtistId");
                    c.ToTable("ArtistsInSongs");
                });
            modelBuilder.Entity<SongModel>()
                .HasMany(p => p.Genres)
                .WithMany(s => s.Songs)
                .Map(c =>
                {
                    c.MapLeftKey("SongId");
                    c.MapRightKey("GenreId");
                    c.ToTable("GenresInSongs");
                });
            modelBuilder.Entity<SongModel>()
                .HasMany(p => p.Links)
                .WithMany(s => s.Songs)
                .Map(c =>
                {
                    c.MapLeftKey("SongId");
                    c.MapRightKey("LinkId");
                    c.ToTable("LinksInSongs");
                });
        }
    }
}

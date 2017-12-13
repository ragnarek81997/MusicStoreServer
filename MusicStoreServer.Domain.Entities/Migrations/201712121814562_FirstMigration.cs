namespace MusicStoreServer.Domain.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 24),
                        Name = c.String(nullable: false, maxLength: 40),
                        ArtId = c.String(maxLength: 24),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 24),
                        Name = c.String(nullable: false, maxLength: 40),
                        ArtId = c.String(maxLength: 24),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 24),
                        Name = c.String(nullable: false, maxLength: 40),
                        ArtId = c.String(maxLength: 24),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 24),
                        Name = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 24),
                        OwnerId = c.String(nullable: false, maxLength: 128),
                        MimeType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PhotoId = c.String(maxLength: 24),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 40),
                        BirthDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 24),
                        Name = c.String(nullable: false, maxLength: 40),
                        OwnerId = c.String(nullable: false, maxLength: 128),
                        ArtId = c.String(maxLength: 24),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ArtistsInSongs",
                c => new
                    {
                        SongId = c.String(nullable: false, maxLength: 24),
                        ArtistId = c.String(nullable: false, maxLength: 24),
                    })
                .PrimaryKey(t => new { t.SongId, t.ArtistId })
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.GenresInSongs",
                c => new
                    {
                        SongId = c.String(nullable: false, maxLength: 24),
                        GenreId = c.String(nullable: false, maxLength: 24),
                    })
                .PrimaryKey(t => new { t.SongId, t.GenreId })
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.LinksInSongs",
                c => new
                    {
                        SongId = c.String(nullable: false, maxLength: 24),
                        LinkId = c.String(nullable: false, maxLength: 24),
                    })
                .PrimaryKey(t => new { t.SongId, t.LinkId })
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .ForeignKey("dbo.Links", t => t.LinkId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.LinkId);
            
            CreateTable(
                "dbo.SongsInPlaylists",
                c => new
                    {
                        PlaylistId = c.String(nullable: false, maxLength: 24),
                        SongId = c.String(nullable: false, maxLength: 24),
                    })
                .PrimaryKey(t => new { t.PlaylistId, t.SongId })
                .ForeignKey("dbo.Playlists", t => t.PlaylistId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.PlaylistId)
                .Index(t => t.SongId);
            
            CreateTable(
                "dbo.ArtistsInAlbums",
                c => new
                    {
                        AlbumId = c.String(nullable: false, maxLength: 24),
                        ArtistId = c.String(nullable: false, maxLength: 24),
                    })
                .PrimaryKey(t => new { t.AlbumId, t.ArtistId })
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.AlbumId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.GenresInAlbums",
                c => new
                    {
                        AlbumId = c.String(nullable: false, maxLength: 24),
                        GenreId = c.String(nullable: false, maxLength: 24),
                    })
                .PrimaryKey(t => new { t.AlbumId, t.GenreId })
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.AlbumId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.SongsInAlbums",
                c => new
                    {
                        AlbumId = c.String(nullable: false, maxLength: 24),
                        SongId = c.String(nullable: false, maxLength: 24),
                    })
                .PrimaryKey(t => new { t.AlbumId, t.SongId })
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.AlbumId)
                .Index(t => t.SongId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SongsInAlbums", "SongId", "dbo.Songs");
            DropForeignKey("dbo.SongsInAlbums", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.GenresInAlbums", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.GenresInAlbums", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.ArtistsInAlbums", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.ArtistsInAlbums", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.SongsInPlaylists", "SongId", "dbo.Songs");
            DropForeignKey("dbo.SongsInPlaylists", "PlaylistId", "dbo.Playlists");
            DropForeignKey("dbo.Playlists", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LinksInSongs", "LinkId", "dbo.Links");
            DropForeignKey("dbo.LinksInSongs", "SongId", "dbo.Songs");
            DropForeignKey("dbo.Links", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GenresInSongs", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.GenresInSongs", "SongId", "dbo.Songs");
            DropForeignKey("dbo.ArtistsInSongs", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.ArtistsInSongs", "SongId", "dbo.Songs");
            DropIndex("dbo.SongsInAlbums", new[] { "SongId" });
            DropIndex("dbo.SongsInAlbums", new[] { "AlbumId" });
            DropIndex("dbo.GenresInAlbums", new[] { "GenreId" });
            DropIndex("dbo.GenresInAlbums", new[] { "AlbumId" });
            DropIndex("dbo.ArtistsInAlbums", new[] { "ArtistId" });
            DropIndex("dbo.ArtistsInAlbums", new[] { "AlbumId" });
            DropIndex("dbo.SongsInPlaylists", new[] { "SongId" });
            DropIndex("dbo.SongsInPlaylists", new[] { "PlaylistId" });
            DropIndex("dbo.LinksInSongs", new[] { "LinkId" });
            DropIndex("dbo.LinksInSongs", new[] { "SongId" });
            DropIndex("dbo.GenresInSongs", new[] { "GenreId" });
            DropIndex("dbo.GenresInSongs", new[] { "SongId" });
            DropIndex("dbo.ArtistsInSongs", new[] { "ArtistId" });
            DropIndex("dbo.ArtistsInSongs", new[] { "SongId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Playlists", new[] { "OwnerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Links", new[] { "OwnerId" });
            DropTable("dbo.SongsInAlbums");
            DropTable("dbo.GenresInAlbums");
            DropTable("dbo.ArtistsInAlbums");
            DropTable("dbo.SongsInPlaylists");
            DropTable("dbo.LinksInSongs");
            DropTable("dbo.GenresInSongs");
            DropTable("dbo.ArtistsInSongs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Playlists");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Links");
            DropTable("dbo.Genres");
            DropTable("dbo.Songs");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}

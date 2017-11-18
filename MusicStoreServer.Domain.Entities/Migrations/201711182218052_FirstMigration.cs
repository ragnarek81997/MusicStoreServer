namespace MusicStoreServer.Domain.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 40),
                        ArtId = c.String(maxLength: 24),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArtistModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 40),
                        ArtId = c.String(maxLength: 24),
                        AlbumModel_Id = c.String(maxLength: 128),
                        SongModel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlbumModels", t => t.AlbumModel_Id)
                .ForeignKey("dbo.SongModels", t => t.SongModel_Id)
                .Index(t => t.AlbumModel_Id)
                .Index(t => t.SongModel_Id);
            
            CreateTable(
                "dbo.GenreModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 40),
                        AlbumModel_Id = c.String(maxLength: 128),
                        SongModel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlbumModels", t => t.AlbumModel_Id)
                .ForeignKey("dbo.SongModels", t => t.SongModel_Id)
                .Index(t => t.AlbumModel_Id)
                .Index(t => t.SongModel_Id);
            
            CreateTable(
                "dbo.SongModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 40),
                        ArtId = c.String(maxLength: 24),
                        Album_Id = c.String(nullable: false, maxLength: 128),
                        PlaylistModel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlbumModels", t => t.Album_Id, cascadeDelete: true)
                .ForeignKey("dbo.PlaylistModels", t => t.PlaylistModel_Id)
                .Index(t => t.Album_Id)
                .Index(t => t.PlaylistModel_Id);
            
            CreateTable(
                "dbo.LinkModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MimeType = c.String(nullable: false),
                        Owner_Id = c.String(nullable: false, maxLength: 128),
                        SongModel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id, cascadeDelete: true)
                .ForeignKey("dbo.SongModels", t => t.SongModel_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.SongModel_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PhotoId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(),
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
                "dbo.PlaylistModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 40),
                        ArtId = c.String(maxLength: 24),
                        Owner_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SongModels", "PlaylistModel_Id", "dbo.PlaylistModels");
            DropForeignKey("dbo.PlaylistModels", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LinkModels", "SongModel_Id", "dbo.SongModels");
            DropForeignKey("dbo.LinkModels", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GenreModels", "SongModel_Id", "dbo.SongModels");
            DropForeignKey("dbo.ArtistModels", "SongModel_Id", "dbo.SongModels");
            DropForeignKey("dbo.SongModels", "Album_Id", "dbo.AlbumModels");
            DropForeignKey("dbo.GenreModels", "AlbumModel_Id", "dbo.AlbumModels");
            DropForeignKey("dbo.ArtistModels", "AlbumModel_Id", "dbo.AlbumModels");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PlaylistModels", new[] { "Owner_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.LinkModels", new[] { "SongModel_Id" });
            DropIndex("dbo.LinkModels", new[] { "Owner_Id" });
            DropIndex("dbo.SongModels", new[] { "PlaylistModel_Id" });
            DropIndex("dbo.SongModels", new[] { "Album_Id" });
            DropIndex("dbo.GenreModels", new[] { "SongModel_Id" });
            DropIndex("dbo.GenreModels", new[] { "AlbumModel_Id" });
            DropIndex("dbo.ArtistModels", new[] { "SongModel_Id" });
            DropIndex("dbo.ArtistModels", new[] { "AlbumModel_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PlaylistModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.LinkModels");
            DropTable("dbo.SongModels");
            DropTable("dbo.GenreModels");
            DropTable("dbo.ArtistModels");
            DropTable("dbo.AlbumModels");
        }
    }
}

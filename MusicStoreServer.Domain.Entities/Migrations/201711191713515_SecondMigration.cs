namespace MusicStoreServer.Domain.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistModels", "AlbumModel_Id", "dbo.AlbumModels");
            DropForeignKey("dbo.GenreModels", "AlbumModel_Id", "dbo.AlbumModels");
            DropForeignKey("dbo.SongModels", "Album_Id", "dbo.AlbumModels");
            DropForeignKey("dbo.ArtistModels", "SongModel_Id", "dbo.SongModels");
            DropForeignKey("dbo.GenreModels", "SongModel_Id", "dbo.SongModels");
            DropForeignKey("dbo.LinkModels", "SongModel_Id", "dbo.SongModels");
            DropForeignKey("dbo.SongModels", "PlaylistModel_Id", "dbo.PlaylistModels");
            DropIndex("dbo.ArtistModels", new[] { "AlbumModel_Id" });
            DropIndex("dbo.ArtistModels", new[] { "SongModel_Id" });
            DropIndex("dbo.GenreModels", new[] { "AlbumModel_Id" });
            DropIndex("dbo.GenreModels", new[] { "SongModel_Id" });
            DropIndex("dbo.SongModels", new[] { "Album_Id" });
            DropIndex("dbo.SongModels", new[] { "PlaylistModel_Id" });
            DropIndex("dbo.LinkModels", new[] { "SongModel_Id" });
            DropPrimaryKey("dbo.AlbumModels");
            DropPrimaryKey("dbo.ArtistModels");
            DropPrimaryKey("dbo.GenreModels");
            DropPrimaryKey("dbo.SongModels");
            DropPrimaryKey("dbo.LinkModels");
            DropPrimaryKey("dbo.PlaylistModels");
            AlterColumn("dbo.AlbumModels", "Id", c => c.String(nullable: false, maxLength: 24));
            AlterColumn("dbo.ArtistModels", "Id", c => c.String(nullable: false, maxLength: 24));
            AlterColumn("dbo.ArtistModels", "AlbumModel_Id", c => c.String(maxLength: 24));
            AlterColumn("dbo.ArtistModels", "SongModel_Id", c => c.String(maxLength: 24));
            AlterColumn("dbo.GenreModels", "Id", c => c.String(nullable: false, maxLength: 24));
            AlterColumn("dbo.GenreModels", "AlbumModel_Id", c => c.String(maxLength: 24));
            AlterColumn("dbo.GenreModels", "SongModel_Id", c => c.String(maxLength: 24));
            AlterColumn("dbo.SongModels", "Id", c => c.String(nullable: false, maxLength: 24));
            AlterColumn("dbo.SongModels", "Album_Id", c => c.String(nullable: false, maxLength: 24));
            AlterColumn("dbo.SongModels", "PlaylistModel_Id", c => c.String(maxLength: 24));
            AlterColumn("dbo.LinkModels", "Id", c => c.String(nullable: false, maxLength: 24));
            AlterColumn("dbo.LinkModels", "SongModel_Id", c => c.String(maxLength: 24));
            AlterColumn("dbo.AspNetUsers", "PhotoId", c => c.String(maxLength: 24));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PlaylistModels", "Id", c => c.String(nullable: false, maxLength: 24));
            AddPrimaryKey("dbo.AlbumModels", "Id");
            AddPrimaryKey("dbo.ArtistModels", "Id");
            AddPrimaryKey("dbo.GenreModels", "Id");
            AddPrimaryKey("dbo.SongModels", "Id");
            AddPrimaryKey("dbo.LinkModels", "Id");
            AddPrimaryKey("dbo.PlaylistModels", "Id");
            CreateIndex("dbo.ArtistModels", "AlbumModel_Id");
            CreateIndex("dbo.ArtistModels", "SongModel_Id");
            CreateIndex("dbo.GenreModels", "AlbumModel_Id");
            CreateIndex("dbo.GenreModels", "SongModel_Id");
            CreateIndex("dbo.SongModels", "Album_Id");
            CreateIndex("dbo.SongModels", "PlaylistModel_Id");
            CreateIndex("dbo.LinkModels", "SongModel_Id");
            AddForeignKey("dbo.ArtistModels", "AlbumModel_Id", "dbo.AlbumModels", "Id");
            AddForeignKey("dbo.GenreModels", "AlbumModel_Id", "dbo.AlbumModels", "Id");
            AddForeignKey("dbo.SongModels", "Album_Id", "dbo.AlbumModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ArtistModels", "SongModel_Id", "dbo.SongModels", "Id");
            AddForeignKey("dbo.GenreModels", "SongModel_Id", "dbo.SongModels", "Id");
            AddForeignKey("dbo.LinkModels", "SongModel_Id", "dbo.SongModels", "Id");
            AddForeignKey("dbo.SongModels", "PlaylistModel_Id", "dbo.PlaylistModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SongModels", "PlaylistModel_Id", "dbo.PlaylistModels");
            DropForeignKey("dbo.LinkModels", "SongModel_Id", "dbo.SongModels");
            DropForeignKey("dbo.GenreModels", "SongModel_Id", "dbo.SongModels");
            DropForeignKey("dbo.ArtistModels", "SongModel_Id", "dbo.SongModels");
            DropForeignKey("dbo.SongModels", "Album_Id", "dbo.AlbumModels");
            DropForeignKey("dbo.GenreModels", "AlbumModel_Id", "dbo.AlbumModels");
            DropForeignKey("dbo.ArtistModels", "AlbumModel_Id", "dbo.AlbumModels");
            DropIndex("dbo.LinkModels", new[] { "SongModel_Id" });
            DropIndex("dbo.SongModels", new[] { "PlaylistModel_Id" });
            DropIndex("dbo.SongModels", new[] { "Album_Id" });
            DropIndex("dbo.GenreModels", new[] { "SongModel_Id" });
            DropIndex("dbo.GenreModels", new[] { "AlbumModel_Id" });
            DropIndex("dbo.ArtistModels", new[] { "SongModel_Id" });
            DropIndex("dbo.ArtistModels", new[] { "AlbumModel_Id" });
            DropPrimaryKey("dbo.PlaylistModels");
            DropPrimaryKey("dbo.LinkModels");
            DropPrimaryKey("dbo.SongModels");
            DropPrimaryKey("dbo.GenreModels");
            DropPrimaryKey("dbo.ArtistModels");
            DropPrimaryKey("dbo.AlbumModels");
            AlterColumn("dbo.PlaylistModels", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "PhotoId", c => c.String());
            AlterColumn("dbo.LinkModels", "SongModel_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.LinkModels", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.SongModels", "PlaylistModel_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.SongModels", "Album_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.SongModels", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.GenreModels", "SongModel_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.GenreModels", "AlbumModel_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.GenreModels", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ArtistModels", "SongModel_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.ArtistModels", "AlbumModel_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.ArtistModels", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AlbumModels", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PlaylistModels", "Id");
            AddPrimaryKey("dbo.LinkModels", "Id");
            AddPrimaryKey("dbo.SongModels", "Id");
            AddPrimaryKey("dbo.GenreModels", "Id");
            AddPrimaryKey("dbo.ArtistModels", "Id");
            AddPrimaryKey("dbo.AlbumModels", "Id");
            CreateIndex("dbo.LinkModels", "SongModel_Id");
            CreateIndex("dbo.SongModels", "PlaylistModel_Id");
            CreateIndex("dbo.SongModels", "Album_Id");
            CreateIndex("dbo.GenreModels", "SongModel_Id");
            CreateIndex("dbo.GenreModels", "AlbumModel_Id");
            CreateIndex("dbo.ArtistModels", "SongModel_Id");
            CreateIndex("dbo.ArtistModels", "AlbumModel_Id");
            AddForeignKey("dbo.SongModels", "PlaylistModel_Id", "dbo.PlaylistModels", "Id");
            AddForeignKey("dbo.LinkModels", "SongModel_Id", "dbo.SongModels", "Id");
            AddForeignKey("dbo.GenreModels", "SongModel_Id", "dbo.SongModels", "Id");
            AddForeignKey("dbo.ArtistModels", "SongModel_Id", "dbo.SongModels", "Id");
            AddForeignKey("dbo.SongModels", "Album_Id", "dbo.AlbumModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreModels", "AlbumModel_Id", "dbo.AlbumModels", "Id");
            AddForeignKey("dbo.ArtistModels", "AlbumModel_Id", "dbo.AlbumModels", "Id");
        }
    }
}

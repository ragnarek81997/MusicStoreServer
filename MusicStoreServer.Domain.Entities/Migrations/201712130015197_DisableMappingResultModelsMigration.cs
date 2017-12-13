namespace MusicStoreServer.Domain.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisableMappingResultModelsMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "Discriminator");
            DropColumn("dbo.Songs", "Discriminator");
            DropColumn("dbo.Playlists", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Playlists", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Songs", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Albums", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}

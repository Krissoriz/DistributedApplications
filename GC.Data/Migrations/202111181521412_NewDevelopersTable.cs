namespace GC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDevelopersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Founder = c.String(maxLength: 100),
                        Founded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.VideoGames", "DevelopersId", c => c.Int());
            CreateIndex("dbo.VideoGames", "DevelopersId");
            AddForeignKey("dbo.VideoGames", "DevelopersId", "dbo.Developers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoGames", "DevelopersId", "dbo.Developers");
            DropIndex("dbo.VideoGames", new[] { "DevelopersId" });
            DropColumn("dbo.VideoGames", "DevelopersId");
            DropTable("dbo.Developers");
        }
    }
}

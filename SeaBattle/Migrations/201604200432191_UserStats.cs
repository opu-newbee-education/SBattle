namespace SeaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserStats : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        GamesPlayed = c.Int(nullable: false),
                        GamesWon = c.Int(nullable: false),
                        TotalPlayTime = c.Int(nullable: false),
                        LongestTimePlayed = c.Int(nullable: false),
                        ShortestTimePlayed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserStats", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserStats", new[] { "UserId" });
            DropTable("dbo.UserStats");
        }
    }
}

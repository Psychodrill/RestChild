namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearOfRestInBoutMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Party", "YearOfRestId", c => c.Long());
            CreateIndex("dbo.Party", "YearOfRestId");
            AddForeignKey("dbo.Party", "YearOfRestId", "dbo.YearOfRest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Party", "YearOfRestId", "dbo.YearOfRest");
            DropIndex("dbo.Party", new[] { "YearOfRestId" });
            DropColumn("dbo.Party", "YearOfRestId");
        }
    }
}

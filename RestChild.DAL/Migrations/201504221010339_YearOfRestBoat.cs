namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearOfRestBoat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bout", "YearOfRestId", c => c.Long());
            CreateIndex("dbo.Bout", "YearOfRestId");
            AddForeignKey("dbo.Bout", "YearOfRestId", "dbo.YearOfRest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bout", "YearOfRestId", "dbo.YearOfRest");
            DropIndex("dbo.Bout", new[] { "YearOfRestId" });
            DropColumn("dbo.Bout", "YearOfRestId");
        }
    }
}

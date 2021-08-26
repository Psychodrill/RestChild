namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrpphanBlockFormOfRestMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormOfRest", "TypeOfRestId", c => c.Long());
            CreateIndex("dbo.FormOfRest", "TypeOfRestId");
            AddForeignKey("dbo.FormOfRest", "TypeOfRestId", "dbo.TypeOfRest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormOfRest", "TypeOfRestId", "dbo.TypeOfRest");
            DropIndex("dbo.FormOfRest", new[] { "TypeOfRestId" });
            DropColumn("dbo.FormOfRest", "TypeOfRestId");
        }
    }
}

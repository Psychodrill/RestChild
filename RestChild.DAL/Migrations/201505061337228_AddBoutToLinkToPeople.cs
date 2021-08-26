namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBoutToLinkToPeople : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LinkToPeople", "BoutId", c => c.Long());
            CreateIndex("dbo.LinkToPeople", "BoutId");
            AddForeignKey("dbo.LinkToPeople", "BoutId", "dbo.Bout", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LinkToPeople", "BoutId", "dbo.Bout");
            DropIndex("dbo.LinkToPeople", new[] { "BoutId" });
            DropColumn("dbo.LinkToPeople", "BoutId");
        }
    }
}

namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok20178StatusByChildMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agent", "StatusByChildId", c => c.Long());
            AddColumn("dbo.StatusByChild", "ForAgent", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Agent", "StatusByChildId");
            AddForeignKey("dbo.Agent", "StatusByChildId", "dbo.StatusByChild", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agent", "StatusByChildId", "dbo.StatusByChild");
            DropIndex("dbo.Agent", new[] { "StatusByChildId" });
            DropColumn("dbo.StatusByChild", "ForAgent");
            DropColumn("dbo.Agent", "StatusByChildId");
        }
    }
}

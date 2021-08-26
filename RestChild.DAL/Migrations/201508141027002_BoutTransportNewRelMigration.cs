namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoutTransportNewRelMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransportInfo", "BoutId", c => c.Long());
            CreateIndex("dbo.TransportInfo", "BoutId");
            AddForeignKey("dbo.TransportInfo", "BoutId", "dbo.Bout", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransportInfo", "BoutId", "dbo.Bout");
            DropIndex("dbo.TransportInfo", new[] { "BoutId" });
            DropColumn("dbo.TransportInfo", "BoutId");
        }
    }
}

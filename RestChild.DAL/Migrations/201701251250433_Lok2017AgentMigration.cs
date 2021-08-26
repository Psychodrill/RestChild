namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok2017AgentMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agent", "Male", c => c.Boolean());
            AddColumn("dbo.Agent", "DateOfBirth", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Agent", "DocumentCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.Agent", "PlaceOfBirth", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agent", "PlaceOfBirth");
            DropColumn("dbo.Agent", "DocumentCode");
            DropColumn("dbo.Agent", "DateOfBirth");
            DropColumn("dbo.Agent", "Male");
        }
    }
}

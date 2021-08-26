namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkToPeopleDateDeparture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LinkToPeople", "DateDeparture", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LinkToPeople", "DateDeparture");
        }
    }
}

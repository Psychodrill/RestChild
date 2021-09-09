namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task139451_2Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOfRest", "NeedTypeOfTransport", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.PlaceOfRest", "NeedTypeOfTransport", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlaceOfRest", "NeedTypeOfTransport");
            DropColumn("dbo.TypeOfRest", "NeedTypeOfTransport");
        }
    }
}

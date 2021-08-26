namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelAddonFieldMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "ContactPerson", c => c.String(maxLength: 1000));
            AddColumn("dbo.Hotels", "ContactPhone", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "ContactPhone");
            DropColumn("dbo.Hotels", "ContactPerson");
        }
    }
}

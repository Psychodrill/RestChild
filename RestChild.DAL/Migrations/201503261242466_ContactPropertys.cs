namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactPropertys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Child", "ContactLastName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "ContactFirstName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "ContactMiddleName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "ContactHaveMiddleName", c => c.Boolean(nullable: false));
            DropColumn("dbo.Child", "ContactPerson");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Child", "ContactPerson", c => c.String(maxLength: 1000));
            DropColumn("dbo.Child", "ContactHaveMiddleName");
            DropColumn("dbo.Child", "ContactMiddleName");
            DropColumn("dbo.Child", "ContactFirstName");
            DropColumn("dbo.Child", "ContactLastName");
        }
    }
}

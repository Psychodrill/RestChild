namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegistryNumberAnChilds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Child", "IsIncludeInInteragency", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "IsApprovedInInteragency", c => c.Boolean());
            AddColumn("dbo.Request", "RequestNumberMpgu", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "RequestNumberMpgu");
            DropColumn("dbo.Child", "IsApprovedInInteragency");
            DropColumn("dbo.Child", "IsIncludeInInteragency");
        }
    }
}

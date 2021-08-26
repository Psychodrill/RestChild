namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileToRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Child", "DocumentFileUrl", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "DocumentFileTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Child", "DocumentFileTitle");
            DropColumn("dbo.Child", "DocumentFileUrl");
        }
    }
}

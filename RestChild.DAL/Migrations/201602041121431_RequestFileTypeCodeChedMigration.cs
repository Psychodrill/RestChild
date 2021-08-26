namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestFileTypeCodeChedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestFileType", "CodeChed", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestFileType", "CodeChed");
        }
    }
}

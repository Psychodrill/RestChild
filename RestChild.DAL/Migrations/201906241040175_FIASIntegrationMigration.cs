namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FIASIntegrationMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "FiasId", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "FiasId");
        }
    }
}

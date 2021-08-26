namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FailExchangeUts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExchangeUTS", "IsError", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExchangeUTS", "IsError");
        }
    }
}

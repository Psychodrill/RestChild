namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UrlToRulesRestMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeOfRest", "UrlToRulesOfRest", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeOfRest", "UrlToRulesOfRest");
        }
    }
}

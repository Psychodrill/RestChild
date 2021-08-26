namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PartyNumberMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Party", "PartyNumber", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Party", "PartyNumber");
        }
    }
}

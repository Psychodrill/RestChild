namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok2017_1Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StatusAction", "IsFirstCompany", c => c.Boolean());
            AddColumn("dbo.StatusAction", "RequestOnMoney", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StatusAction", "RequestOnMoney");
            DropColumn("dbo.StatusAction", "IsFirstCompany");
        }
    }
}

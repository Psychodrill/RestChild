namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok20174FlagInChainMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "NeedSendForBenefit", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "NeedSendToRelative", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestStatusChainForMpgu", "RequestOnMoney", c => c.Boolean());
            AddColumn("dbo.RequestStatusChainForMpgu", "IsFirstCompany", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestStatusChainForMpgu", "IsFirstCompany");
            DropColumn("dbo.RequestStatusChainForMpgu", "RequestOnMoney");
            DropColumn("dbo.Request", "NeedSendToRelative");
            DropColumn("dbo.Request", "NeedSendForBenefit");
        }
    }
}

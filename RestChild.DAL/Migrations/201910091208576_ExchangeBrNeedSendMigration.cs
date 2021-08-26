namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExchangeBrNeedSendMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "NeedSendForSnils", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "NeedSendForCPMPK", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "NeedSendForParent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "NeedSendForPassport", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "NeedSendForPassport");
            DropColumn("dbo.Request", "NeedSendForParent");
            DropColumn("dbo.Request", "NeedSendForCPMPK");
            DropColumn("dbo.Request", "NeedSendForSnils");
        }
    }
}

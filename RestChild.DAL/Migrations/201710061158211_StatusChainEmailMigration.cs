namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusChainEmailMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestStatusForMpgu", "SendEmail", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestStatusForMpgu", "SendEmail");
        }
    }
}

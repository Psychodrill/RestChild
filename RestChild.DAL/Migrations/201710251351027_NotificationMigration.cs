namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeclineReason", "ValidReasons", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "RefusalOfAdmission", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestStatusForMpgu", "NotificationToSend", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestStatusForMpgu", "NotificationToSend");
            DropColumn("dbo.Request", "RefusalOfAdmission");
            DropColumn("dbo.DeclineReason", "ValidReasons");
        }
    }
}

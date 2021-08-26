namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotNeedReasonMigratgion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotNeedTicketReason",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AddonServices", "HotelsId", c => c.Long());
            AddColumn("dbo.LinkToPeople", "NotNeedTicketReasonId", c => c.Long());
            CreateIndex("dbo.LinkToPeople", "NotNeedTicketReasonId");
            CreateIndex("dbo.AddonServices", "HotelsId");
            AddForeignKey("dbo.LinkToPeople", "NotNeedTicketReasonId", "dbo.NotNeedTicketReason", "Id");
            AddForeignKey("dbo.AddonServices", "HotelsId", "dbo.Hotels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddonServices", "HotelsId", "dbo.Hotels");
            DropForeignKey("dbo.LinkToPeople", "NotNeedTicketReasonId", "dbo.NotNeedTicketReason");
            DropIndex("dbo.AddonServices", new[] { "HotelsId" });
            DropIndex("dbo.LinkToPeople", new[] { "NotNeedTicketReasonId" });
            DropColumn("dbo.LinkToPeople", "NotNeedTicketReasonId");
            DropColumn("dbo.AddonServices", "HotelsId");
            DropTable("dbo.NotNeedTicketReason");
        }
    }
}

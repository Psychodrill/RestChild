namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmsAndEmailSend : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SendEmailAndSms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(maxLength: 1000),
                        Phone = c.String(maxLength: 1000),
                        EmailMessage = c.String(),
                        EmailTitle = c.String(maxLength: 1000),
                        SmsMessage = c.String(maxLength: 1000),
                        IsEmailSended = c.Boolean(nullable: false),
                        IsSmsSended = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateEmail = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateSms = c.DateTime(precision: 7, storeType: "datetime2"),
                        RequestId = c.Long(),
                        StatusRequestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.Status", t => t.StatusRequestId)
                .Index(t => t.RequestId)
                .Index(t => t.StatusRequestId);
            
            AddColumn("dbo.Status", "SmsMessage", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SendEmailAndSms", "StatusRequestId", "dbo.Status");
            DropForeignKey("dbo.SendEmailAndSms", "RequestId", "dbo.Request");
            DropIndex("dbo.SendEmailAndSms", new[] { "StatusRequestId" });
            DropIndex("dbo.SendEmailAndSms", new[] { "RequestId" });
            DropColumn("dbo.Status", "SmsMessage");
            DropTable("dbo.SendEmailAndSms");
        }
    }
}

namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationFiledsAndUrlToRulesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SendEmailAndSmsAttachment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        UrlToDownload = c.String(nullable: false, maxLength: 1000),
                        SendEmailAndSmsId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SendEmailAndSms", t => t.SendEmailAndSmsId)
                .Index(t => t.SendEmailAndSmsId);
            
            AddColumn("dbo.Organization", "LatinName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Organization", "Ownership", c => c.String(maxLength: 1000));
            AddColumn("dbo.Organization", "PostAdderss", c => c.String(maxLength: 1000));
            AddColumn("dbo.Organization", "HeadPerson", c => c.String(maxLength: 1000));
            AddColumn("dbo.TypeOfRest", "UrlToListRestriction", c => c.String(maxLength: 1000));
            AddColumn("dbo.TypeOfRest", "UrlToRoolAttendant", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Organization", "Address", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SendEmailAndSmsAttachment", "SendEmailAndSmsId", "dbo.SendEmailAndSms");
            DropIndex("dbo.SendEmailAndSmsAttachment", new[] { "SendEmailAndSmsId" });
            AlterColumn("dbo.Organization", "Address", c => c.String());
            DropColumn("dbo.TypeOfRest", "UrlToRoolAttendant");
            DropColumn("dbo.TypeOfRest", "UrlToListRestriction");
            DropColumn("dbo.Organization", "HeadPerson");
            DropColumn("dbo.Organization", "PostAdderss");
            DropColumn("dbo.Organization", "Ownership");
            DropColumn("dbo.Organization", "LatinName");
            DropTable("dbo.SendEmailAndSmsAttachment");
        }
    }
}

namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationFileldsMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organization", "Address", c => c.String());
            AddColumn("dbo.Organization", "Email", c => c.String());
            AddColumn("dbo.Organization", "ContactPerson", c => c.String());
            AddColumn("dbo.Organization", "StateDistrictId", c => c.Long());
            AddColumn("dbo.DeclineReason", "ForPreferential", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeclineReason", "ForCommerce", c => c.Boolean(nullable: false));
            AddColumn("dbo.Status", "ForPreferential", c => c.Boolean(nullable: false));
            AddColumn("dbo.Status", "ForCommerce", c => c.Boolean(nullable: false));
            AddColumn("dbo.Status", "CommerceName", c => c.String(maxLength: 1000));
            AlterColumn("dbo.CounselorPractice", "Camp", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.CounselorPractice", "Year", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.CounselorPractice", "Tour", c => c.String(maxLength: 1000));
            AlterColumn("dbo.CounselorPractice", "Party", c => c.String(maxLength: 1000));
            CreateIndex("dbo.Organization", "StateDistrictId");
            AddForeignKey("dbo.Organization", "StateDistrictId", "dbo.StateDistrict", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Organization", "StateDistrictId", "dbo.StateDistrict");
            DropIndex("dbo.Organization", new[] { "StateDistrictId" });
            AlterColumn("dbo.CounselorPractice", "Party", c => c.String());
            AlterColumn("dbo.CounselorPractice", "Tour", c => c.String());
            AlterColumn("dbo.CounselorPractice", "Year", c => c.String(nullable: false));
            AlterColumn("dbo.CounselorPractice", "Camp", c => c.String(nullable: false));
            DropColumn("dbo.Status", "CommerceName");
            DropColumn("dbo.Status", "ForCommerce");
            DropColumn("dbo.Status", "ForPreferential");
            DropColumn("dbo.DeclineReason", "ForCommerce");
            DropColumn("dbo.DeclineReason", "ForPreferential");
            DropColumn("dbo.Organization", "StateDistrictId");
            DropColumn("dbo.Organization", "ContactPerson");
            DropColumn("dbo.Organization", "Email");
            DropColumn("dbo.Organization", "Address");
        }
    }
}

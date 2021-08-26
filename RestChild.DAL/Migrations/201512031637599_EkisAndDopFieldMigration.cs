namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EkisAndDopFieldMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LimitOnOrganizationRequest", "VolumeAttendant", c => c.Int());
            AddColumn("dbo.LimitOnOrganizationRequest", "VolumeCounselor", c => c.Int());
            AddColumn("dbo.LimitOnOrganizationRequest", "TimeOfRestId", c => c.Long());
            AddColumn("dbo.LimitOnOrganizationRequest", "ListOfChildsCategoryId", c => c.Long());
            AddColumn("dbo.LimitOnOrganizationRequest", "GroupedTimeOfRestId", c => c.Long());
            AddColumn("dbo.Bout", "Comment", c => c.String());
            AddColumn("dbo.Child", "ContingentGuid", c => c.Guid());
            CreateIndex("dbo.LimitOnOrganizationRequest", "TimeOfRestId");
            CreateIndex("dbo.LimitOnOrganizationRequest", "ListOfChildsCategoryId");
            CreateIndex("dbo.LimitOnOrganizationRequest", "GroupedTimeOfRestId");
            AddForeignKey("dbo.LimitOnOrganizationRequest", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest", "Id");
            AddForeignKey("dbo.LimitOnOrganizationRequest", "ListOfChildsCategoryId", "dbo.ListOfChildsCategory", "Id");
            AddForeignKey("dbo.LimitOnOrganizationRequest", "TimeOfRestId", "dbo.TimeOfRest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LimitOnOrganizationRequest", "TimeOfRestId", "dbo.TimeOfRest");
            DropForeignKey("dbo.LimitOnOrganizationRequest", "ListOfChildsCategoryId", "dbo.ListOfChildsCategory");
            DropForeignKey("dbo.LimitOnOrganizationRequest", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest");
            DropIndex("dbo.LimitOnOrganizationRequest", new[] { "GroupedTimeOfRestId" });
            DropIndex("dbo.LimitOnOrganizationRequest", new[] { "ListOfChildsCategoryId" });
            DropIndex("dbo.LimitOnOrganizationRequest", new[] { "TimeOfRestId" });
            DropColumn("dbo.Child", "ContingentGuid");
            DropColumn("dbo.Bout", "Comment");
            DropColumn("dbo.LimitOnOrganizationRequest", "GroupedTimeOfRestId");
            DropColumn("dbo.LimitOnOrganizationRequest", "ListOfChildsCategoryId");
            DropColumn("dbo.LimitOnOrganizationRequest", "TimeOfRestId");
            DropColumn("dbo.LimitOnOrganizationRequest", "VolumeCounselor");
            DropColumn("dbo.LimitOnOrganizationRequest", "VolumeAttendant");
        }
    }
}

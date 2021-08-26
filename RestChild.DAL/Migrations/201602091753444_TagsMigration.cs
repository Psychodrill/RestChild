namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagTour",
                c => new
                    {
                        Tag_Id = c.Long(nullable: false),
                        Tour_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Tour_Id })
                .ForeignKey("dbo.Tag", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.Tour_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Tour_Id);
            
            CreateTable(
                "dbo.AddonServicesTag",
                c => new
                    {
                        AddonServices_Id = c.Long(nullable: false),
                        Tag_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AddonServices_Id, t.Tag_Id })
                .ForeignKey("dbo.AddonServices", t => t.AddonServices_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.AddonServices_Id)
                .Index(t => t.Tag_Id);
            
            AddColumn("dbo.TypeOfService", "NeedSize", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "NeedConditions", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "NeedAnnouncement", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "NeedName", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "NeedDescription", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "NeedDurationHour", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "NeedDurationDay", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "NeedDurationMonth", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "NeedDurationYear", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "MayByDefault", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "MayRequared", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "MayWithAccomodation", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfService", "MayMustApprove", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "PartnerId", c => c.Long());
            AddColumn("dbo.AddonServices", "DurationHour", c => c.Int());
            AddColumn("dbo.AddonServices", "DurationDay", c => c.Int());
            AddColumn("dbo.AddonServices", "DurationMonth", c => c.Int());
            AddColumn("dbo.AddonServices", "DurationYear", c => c.Int());
            AddColumn("dbo.AddonServices", "IsGroup", c => c.Boolean(nullable: false));
            AddColumn("dbo.AddonServices", "PartnerId", c => c.Long());
            CreateIndex("dbo.Tour", "PartnerId");
            CreateIndex("dbo.AddonServices", "PartnerId");
            AddForeignKey("dbo.AddonServices", "PartnerId", "dbo.Organization", "Id");
            AddForeignKey("dbo.Tour", "PartnerId", "dbo.Organization", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "PartnerId", "dbo.Organization");
            DropForeignKey("dbo.AddonServicesTag", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.AddonServicesTag", "AddonServices_Id", "dbo.AddonServices");
            DropForeignKey("dbo.TagTour", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.TagTour", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.AddonServices", "PartnerId", "dbo.Organization");
            DropIndex("dbo.AddonServicesTag", new[] { "Tag_Id" });
            DropIndex("dbo.AddonServicesTag", new[] { "AddonServices_Id" });
            DropIndex("dbo.TagTour", new[] { "Tour_Id" });
            DropIndex("dbo.TagTour", new[] { "Tag_Id" });
            DropIndex("dbo.AddonServices", new[] { "PartnerId" });
            DropIndex("dbo.Tour", new[] { "PartnerId" });
            DropColumn("dbo.AddonServices", "PartnerId");
            DropColumn("dbo.AddonServices", "IsGroup");
            DropColumn("dbo.AddonServices", "DurationYear");
            DropColumn("dbo.AddonServices", "DurationMonth");
            DropColumn("dbo.AddonServices", "DurationDay");
            DropColumn("dbo.AddonServices", "DurationHour");
            DropColumn("dbo.Tour", "PartnerId");
            DropColumn("dbo.TypeOfService", "MayMustApprove");
            DropColumn("dbo.TypeOfService", "MayWithAccomodation");
            DropColumn("dbo.TypeOfService", "MayRequared");
            DropColumn("dbo.TypeOfService", "MayByDefault");
            DropColumn("dbo.TypeOfService", "NeedDurationYear");
            DropColumn("dbo.TypeOfService", "NeedDurationMonth");
            DropColumn("dbo.TypeOfService", "NeedDurationDay");
            DropColumn("dbo.TypeOfService", "NeedDurationHour");
            DropColumn("dbo.TypeOfService", "NeedDescription");
            DropColumn("dbo.TypeOfService", "NeedName");
            DropColumn("dbo.TypeOfService", "NeedAnnouncement");
            DropColumn("dbo.TypeOfService", "NeedConditions");
            DropColumn("dbo.TypeOfService", "NeedSize");
            DropTable("dbo.AddonServicesTag");
            DropTable("dbo.TagTour");
            DropTable("dbo.Tag");
        }
    }
}

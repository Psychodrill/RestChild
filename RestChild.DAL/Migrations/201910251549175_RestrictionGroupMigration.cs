namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestrictionGroupMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tour", "TypeOfRestrictionId", "dbo.TypeOfRestriction");
            DropForeignKey("dbo.Tour", "TypeOfSubRestrictionId", "dbo.TypeOfSubRestriction");
            DropIndex("dbo.Tour", new[] { "TypeOfRestrictionId" });
            DropIndex("dbo.Tour", new[] { "TypeOfSubRestrictionId" });
            CreateTable(
                "dbo.RestrictionGroup",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        Number = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.Tour", "RestrictionGroupId", c => c.Long());
            AddColumn("dbo.TypeOfRestriction", "RestrictionGroupId", c => c.Long());
            AddColumn("dbo.TypeOfSubRestriction", "RestrictionGroupId", c => c.Long());
            CreateIndex("dbo.Tour", "RestrictionGroupId");
            CreateIndex("dbo.TypeOfRestriction", "RestrictionGroupId");
            CreateIndex("dbo.TypeOfSubRestriction", "RestrictionGroupId");
            AddForeignKey("dbo.TypeOfRestriction", "RestrictionGroupId", "dbo.RestrictionGroup", "Id");
            AddForeignKey("dbo.TypeOfSubRestriction", "RestrictionGroupId", "dbo.RestrictionGroup", "Id");
            AddForeignKey("dbo.Tour", "RestrictionGroupId", "dbo.RestrictionGroup", "Id");
            DropColumn("dbo.Tour", "TypeOfRestrictionId");
            DropColumn("dbo.Tour", "TypeOfSubRestrictionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tour", "TypeOfSubRestrictionId", c => c.Long());
            AddColumn("dbo.Tour", "TypeOfRestrictionId", c => c.Long());
            DropForeignKey("dbo.Tour", "RestrictionGroupId", "dbo.RestrictionGroup");
            DropForeignKey("dbo.TypeOfSubRestriction", "RestrictionGroupId", "dbo.RestrictionGroup");
            DropForeignKey("dbo.TypeOfRestriction", "RestrictionGroupId", "dbo.RestrictionGroup");
            DropIndex("dbo.TypeOfSubRestriction", new[] { "RestrictionGroupId" });
            DropIndex("dbo.RestrictionGroup", new[] { "EidSendStatus" });
            DropIndex("dbo.RestrictionGroup", new[] { "Eid" });
            DropIndex("dbo.TypeOfRestriction", new[] { "RestrictionGroupId" });
            DropIndex("dbo.Tour", new[] { "RestrictionGroupId" });
            DropColumn("dbo.TypeOfSubRestriction", "RestrictionGroupId");
            DropColumn("dbo.TypeOfRestriction", "RestrictionGroupId");
            DropColumn("dbo.Tour", "RestrictionGroupId");
            DropTable("dbo.RestrictionGroup");
            CreateIndex("dbo.Tour", "TypeOfSubRestrictionId");
            CreateIndex("dbo.Tour", "TypeOfRestrictionId");
            AddForeignKey("dbo.Tour", "TypeOfSubRestrictionId", "dbo.TypeOfSubRestriction", "Id");
            AddForeignKey("dbo.Tour", "TypeOfRestrictionId", "dbo.TypeOfRestriction", "Id");
        }
    }
}

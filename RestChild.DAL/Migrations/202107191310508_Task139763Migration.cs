namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Task139763Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfCamp",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);

            AddColumn("dbo.Hotels", "IsCamping", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Request", "TypeOfCampAddonId", c => c.Long());
            AddColumn("dbo.Request", "TypeOfCampId", c => c.Long());
            CreateIndex("dbo.Request", "TypeOfCampAddonId");
            CreateIndex("dbo.Request", "TypeOfCampId");
            AddForeignKey("dbo.Request", "TypeOfCampId", "dbo.TypeOfCamp", "Id");
            AddForeignKey("dbo.Request", "TypeOfCampAddonId", "dbo.TypeOfCamp", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Request", "TypeOfCampAddonId", "dbo.TypeOfCamp");
            DropForeignKey("dbo.Request", "TypeOfCampId", "dbo.TypeOfCamp");
            DropIndex("dbo.TypeOfCamp", new[] { "EidSendStatus" });
            DropIndex("dbo.TypeOfCamp", new[] { "Eid" });
            DropIndex("dbo.Request", new[] { "TypeOfCampId" });
            DropIndex("dbo.Request", new[] { "TypeOfCampAddonId" });
            DropColumn("dbo.Request", "TypeOfCampId");
            DropColumn("dbo.Request", "TypeOfCampAddonId");
            DropColumn("dbo.Hotels", "IsCamping");
            DropTable("dbo.TypeOfCamp");
        }
    }
}

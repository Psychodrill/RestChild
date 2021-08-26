namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok2017ListOfChildMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfLimitList",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.LimitOnOrganization", "TypeOfLimitListId", c => c.Long());
            AddColumn("dbo.LimitOnVedomstvo", "TypeOfLimitListId", c => c.Long());
            AddColumn("dbo.ListOfChilds", "TypeOfLimitListId", c => c.Long());
            CreateIndex("dbo.LimitOnOrganization", "TypeOfLimitListId");
            CreateIndex("dbo.LimitOnVedomstvo", "TypeOfLimitListId");
            CreateIndex("dbo.ListOfChilds", "TypeOfLimitListId");
            AddForeignKey("dbo.ListOfChilds", "TypeOfLimitListId", "dbo.TypeOfLimitList", "Id");
            AddForeignKey("dbo.LimitOnVedomstvo", "TypeOfLimitListId", "dbo.TypeOfLimitList", "Id");
            AddForeignKey("dbo.LimitOnOrganization", "TypeOfLimitListId", "dbo.TypeOfLimitList", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LimitOnOrganization", "TypeOfLimitListId", "dbo.TypeOfLimitList");
            DropForeignKey("dbo.LimitOnVedomstvo", "TypeOfLimitListId", "dbo.TypeOfLimitList");
            DropForeignKey("dbo.ListOfChilds", "TypeOfLimitListId", "dbo.TypeOfLimitList");
            DropIndex("dbo.TypeOfLimitList", new[] { "EidSendStatus" });
            DropIndex("dbo.TypeOfLimitList", new[] { "Eid" });
            DropIndex("dbo.ListOfChilds", new[] { "TypeOfLimitListId" });
            DropIndex("dbo.LimitOnVedomstvo", new[] { "TypeOfLimitListId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "TypeOfLimitListId" });
            DropColumn("dbo.ListOfChilds", "TypeOfLimitListId");
            DropColumn("dbo.LimitOnVedomstvo", "TypeOfLimitListId");
            DropColumn("dbo.LimitOnOrganization", "TypeOfLimitListId");
            DropTable("dbo.TypeOfLimitList");
        }
    }
}

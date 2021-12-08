namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTheDifference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MGTVisitTargetMGTWorkingDayWindow", "MGTVisitTarget_Id", "dbo.MGTVisitTarget");
            DropForeignKey("dbo.MGTBookingVisit", "TargetId", "dbo.MGTVisitTarget");
            DropPrimaryKey("dbo.MGTVisitTarget");
            CreateTable(
                "dbo.MGTDepartment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.MGTWorkingDay", "DepartmentId", c => c.Long());
            AddColumn("dbo.MGTBookingVisit", "DepartmentId", c => c.Long());
            AddColumn("dbo.MGTVisitTarget", "DepartmentId", c => c.Long());
            AlterColumn("dbo.MGTVisitTarget", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.MGTVisitTarget", "Id");
            CreateIndex("dbo.MGTWorkingDay", "DepartmentId");
            CreateIndex("dbo.MGTBookingVisit", "DepartmentId");
            CreateIndex("dbo.MGTVisitTarget", "DepartmentId");
            AddForeignKey("dbo.MGTWorkingDay", "DepartmentId", "dbo.MGTDepartment", "Id");
            AddForeignKey("dbo.MGTBookingVisit", "DepartmentId", "dbo.MGTDepartment", "Id");
            AddForeignKey("dbo.MGTVisitTarget", "DepartmentId", "dbo.MGTDepartment", "Id");
            AddForeignKey("dbo.MGTVisitTargetMGTWorkingDayWindow", "MGTVisitTarget_Id", "dbo.MGTVisitTarget", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MGTBookingVisit", "TargetId", "dbo.MGTVisitTarget", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MGTBookingVisit", "TargetId", "dbo.MGTVisitTarget");
            DropForeignKey("dbo.MGTVisitTargetMGTWorkingDayWindow", "MGTVisitTarget_Id", "dbo.MGTVisitTarget");
            DropForeignKey("dbo.MGTVisitTarget", "DepartmentId", "dbo.MGTDepartment");
            DropForeignKey("dbo.MGTBookingVisit", "DepartmentId", "dbo.MGTDepartment");
            DropForeignKey("dbo.MGTWorkingDay", "DepartmentId", "dbo.MGTDepartment");
            DropIndex("dbo.MGTVisitTarget", new[] { "DepartmentId" });
            DropIndex("dbo.MGTBookingVisit", new[] { "DepartmentId" });
            DropIndex("dbo.MGTDepartment", new[] { "EidSendStatus" });
            DropIndex("dbo.MGTDepartment", new[] { "Eid" });
            DropIndex("dbo.MGTWorkingDay", new[] { "DepartmentId" });
            DropPrimaryKey("dbo.MGTVisitTarget");
            AlterColumn("dbo.MGTVisitTarget", "Id", c => c.Long(nullable: false));
            DropColumn("dbo.MGTVisitTarget", "DepartmentId");
            DropColumn("dbo.MGTBookingVisit", "DepartmentId");
            DropColumn("dbo.MGTWorkingDay", "DepartmentId");
            DropTable("dbo.MGTDepartment");
            AddPrimaryKey("dbo.MGTVisitTarget", "Id");
            AddForeignKey("dbo.MGTBookingVisit", "TargetId", "dbo.MGTVisitTarget", "Id");
            AddForeignKey("dbo.MGTVisitTargetMGTWorkingDayWindow", "MGTVisitTarget_Id", "dbo.MGTVisitTarget", "Id", cascadeDelete: true);
        }
    }
}

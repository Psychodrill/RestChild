namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoutsMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Party", "TourId", "dbo.Tour");
            DropIndex("dbo.Party", new[] { "TourId" });
            CreateTable(
                "dbo.Bout",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdministratorId = c.Long(),
                        TimeOfRestId = c.Long(),
                        HotelsId = c.Long(),
                        SeniorCounselorsId = c.Long(),
                        StateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counselors", t => t.AdministratorId)
                .ForeignKey("dbo.Hotels", t => t.HotelsId)
                .ForeignKey("dbo.Counselors", t => t.SeniorCounselorsId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.TimeOfRest", t => t.TimeOfRestId)
                .Index(t => t.AdministratorId)
                .Index(t => t.TimeOfRestId)
                .Index(t => t.HotelsId)
                .Index(t => t.SeniorCounselorsId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.CounselorsParty",
                c => new
                    {
                        Counselors_Id = c.Long(nullable: false),
                        Party_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Counselors_Id, t.Party_Id })
                .ForeignKey("dbo.Counselors", t => t.Counselors_Id, cascadeDelete: true)
                .ForeignKey("dbo.Party", t => t.Party_Id, cascadeDelete: true)
                .Index(t => t.Counselors_Id)
                .Index(t => t.Party_Id);
            
            AddColumn("dbo.Tour", "BoutId", c => c.Long());
            AddColumn("dbo.Party", "BoutsId", c => c.Long());
            AlterColumn("dbo.Tour", "StartBooking", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tour", "EndBooking", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.Tour", "BoutId");
            CreateIndex("dbo.Party", "BoutsId");
            AddForeignKey("dbo.Party", "BoutsId", "dbo.Bout", "Id");
            AddForeignKey("dbo.Tour", "BoutId", "dbo.Bout", "Id");
            DropColumn("dbo.Party", "TourId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Party", "TourId", c => c.Long());
            DropForeignKey("dbo.Tour", "BoutId", "dbo.Bout");
            DropForeignKey("dbo.Bout", "TimeOfRestId", "dbo.TimeOfRest");
            DropForeignKey("dbo.Bout", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.Bout", "SeniorCounselorsId", "dbo.Counselors");
            DropForeignKey("dbo.Party", "BoutsId", "dbo.Bout");
            DropForeignKey("dbo.Bout", "HotelsId", "dbo.Hotels");
            DropForeignKey("dbo.Bout", "AdministratorId", "dbo.Counselors");
            DropForeignKey("dbo.CounselorsParty", "Party_Id", "dbo.Party");
            DropForeignKey("dbo.CounselorsParty", "Counselors_Id", "dbo.Counselors");
            DropIndex("dbo.CounselorsParty", new[] { "Party_Id" });
            DropIndex("dbo.CounselorsParty", new[] { "Counselors_Id" });
            DropIndex("dbo.Party", new[] { "BoutsId" });
            DropIndex("dbo.Bout", new[] { "StateId" });
            DropIndex("dbo.Bout", new[] { "SeniorCounselorsId" });
            DropIndex("dbo.Bout", new[] { "HotelsId" });
            DropIndex("dbo.Bout", new[] { "TimeOfRestId" });
            DropIndex("dbo.Bout", new[] { "AdministratorId" });
            DropIndex("dbo.Tour", new[] { "BoutId" });
            AlterColumn("dbo.Tour", "EndBooking", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tour", "StartBooking", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Party", "BoutsId");
            DropColumn("dbo.Tour", "BoutId");
            DropTable("dbo.CounselorsParty");
            DropTable("dbo.Bout");
            CreateIndex("dbo.Party", "TourId");
            AddForeignKey("dbo.Party", "TourId", "dbo.Tour", "Id");
        }
    }
}

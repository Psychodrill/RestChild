namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PartysMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        HaveAero = c.Boolean(nullable: false),
                        HaveRailway = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PartyTour",
                c => new
                    {
                        Party_Id = c.Long(nullable: false),
                        Tour_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Party_Id, t.Tour_Id })
                .ForeignKey("dbo.Party", t => t.Party_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.Tour_Id, cascadeDelete: true)
                .Index(t => t.Party_Id)
                .Index(t => t.Tour_Id);
            
            AddColumn("dbo.Tour", "SurrogateKey", c => c.String(maxLength: 400));
            AddColumn("dbo.Party", "TimeOfRestId", c => c.Long());
            AddColumn("dbo.Party", "HotelsId", c => c.Long());
            AddColumn("dbo.Party", "StateId", c => c.Long());
            AddColumn("dbo.Hotels", "CityId", c => c.Long());
            AddColumn("dbo.Applicant", "NotNeedTicketForward", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicant", "NotNeedTicketBackward", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "NotNeedTicketForward", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "NotNeedTicketBackward", c => c.Boolean(nullable: false));
			CreateIndex("dbo.Tour", "SurrogateKey", false, "IND_SEARCH_KEY");
			CreateIndex("dbo.Party", "TimeOfRestId");
            CreateIndex("dbo.Party", "HotelsId");
            CreateIndex("dbo.Party", "StateId");
            CreateIndex("dbo.Hotels", "CityId");
            AddForeignKey("dbo.Hotels", "CityId", "dbo.City", "Id");
            AddForeignKey("dbo.Party", "HotelsId", "dbo.Hotels", "Id");
            AddForeignKey("dbo.Party", "StateId", "dbo.StateMachineState", "Id");
            AddForeignKey("dbo.Party", "TimeOfRestId", "dbo.TimeOfRest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartyTour", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.PartyTour", "Party_Id", "dbo.Party");
            DropForeignKey("dbo.Party", "TimeOfRestId", "dbo.TimeOfRest");
            DropForeignKey("dbo.Party", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.Party", "HotelsId", "dbo.Hotels");
            DropForeignKey("dbo.Hotels", "CityId", "dbo.City");
            DropIndex("dbo.PartyTour", new[] { "Tour_Id" });
            DropIndex("dbo.PartyTour", new[] { "Party_Id" });
            DropIndex("dbo.Hotels", new[] { "CityId" });
            DropIndex("dbo.Party", new[] { "StateId" });
            DropIndex("dbo.Party", new[] { "HotelsId" });
            DropIndex("dbo.Party", new[] { "TimeOfRestId" });
			DropIndex("dbo.Tour", new[] { "SurrogateKey" });

            DropColumn("dbo.Child", "NotNeedTicketBackward");
            DropColumn("dbo.Child", "NotNeedTicketForward");
            DropColumn("dbo.Applicant", "NotNeedTicketBackward");
            DropColumn("dbo.Applicant", "NotNeedTicketForward");
            DropColumn("dbo.Hotels", "CityId");
            DropColumn("dbo.Party", "StateId");
            DropColumn("dbo.Party", "HotelsId");
            DropColumn("dbo.Party", "TimeOfRestId");
            DropColumn("dbo.Tour", "SurrogateKey");
            DropTable("dbo.PartyTour");
            DropTable("dbo.City");
        }
    }
}

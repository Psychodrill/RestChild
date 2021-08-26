namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransportFirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DirectoryFlights",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 1000),
                        FilightNumber = c.String(nullable: false, maxLength: 1000),
                        TimeOfDeparture = c.DateTime(precision: 7, storeType: "datetime2"),
                        TimeOfArrival = c.DateTime(precision: 7, storeType: "datetime2"),
                        CodeDeparture = c.String(maxLength: 1000),
                        CodeArrival = c.String(maxLength: 1000),
                        TypeOfTransportId = c.Long(),
                        YearOfRestId = c.Long(),
                        HistoryLinkId = c.Long(),
                        ArrivalId = c.Long(),
                        StateId = c.Long(),
                        DepartureId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.ArrivalId)
                .ForeignKey("dbo.City", t => t.DepartureId)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.TypeOfTransport", t => t.TypeOfTransportId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.TypeOfTransportId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.ArrivalId)
                .Index(t => t.StateId)
                .Index(t => t.DepartureId);
            
            CreateTable(
                "dbo.TypeOfTransport",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Code = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LinkToPeople",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Wagon = c.String(maxLength: 1000),
                        PlaceNumber = c.String(maxLength: 1000),
                        NeedTicket = c.Boolean(nullable: false),
                        AdministratorTourId = c.Long(),
                        TypeOfLinkPeopleId = c.Long(),
                        CounselorsId = c.Long(),
                        ApplicantId = c.Long(),
                        RequestId = c.Long(),
                        PartyId = c.Long(),
                        ChildId = c.Long(),
                        DirectoryFlightsId = c.Long(),
                        ListOfChildsId = c.Long(),
                        TransportId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdministratorTour", t => t.AdministratorTourId)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .ForeignKey("dbo.Counselors", t => t.CounselorsId)
                .ForeignKey("dbo.DirectoryFlights", t => t.DirectoryFlightsId)
                .ForeignKey("dbo.ListOfChilds", t => t.ListOfChildsId)
                .ForeignKey("dbo.Party", t => t.PartyId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.TransportInfo", t => t.TransportId)
                .ForeignKey("dbo.TypeOfLinkPeople", t => t.TypeOfLinkPeopleId)
                .Index(t => t.AdministratorTourId)
                .Index(t => t.TypeOfLinkPeopleId)
                .Index(t => t.CounselorsId)
                .Index(t => t.ApplicantId)
                .Index(t => t.RequestId)
                .Index(t => t.PartyId)
                .Index(t => t.ChildId)
                .Index(t => t.DirectoryFlightsId)
                .Index(t => t.ListOfChildsId)
                .Index(t => t.TransportId);
            
            CreateTable(
                "dbo.TransportInfo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateOfDeparture = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DepartureId = c.Long(),
                        YearOfRestId = c.Long(),
                        HistoryLinkId = c.Long(),
                        ArrivalId = c.Long(),
                        StateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.ArrivalId)
                .ForeignKey("dbo.City", t => t.DepartureId)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.DepartureId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.ArrivalId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.TypeOfLinkPeople",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.City", "HistoryLinkId", c => c.Long());
            AddColumn("dbo.City", "StateId", c => c.Long());
            CreateIndex("dbo.City", "HistoryLinkId");
            CreateIndex("dbo.City", "StateId");
            AddForeignKey("dbo.City", "HistoryLinkId", "dbo.HistoryLink", "Id");
            AddForeignKey("dbo.City", "StateId", "dbo.StateMachineState", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LinkToPeople", "TypeOfLinkPeopleId", "dbo.TypeOfLinkPeople");
            DropForeignKey("dbo.LinkToPeople", "TransportId", "dbo.TransportInfo");
            DropForeignKey("dbo.TransportInfo", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.TransportInfo", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.TransportInfo", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.TransportInfo", "DepartureId", "dbo.City");
            DropForeignKey("dbo.TransportInfo", "ArrivalId", "dbo.City");
            DropForeignKey("dbo.LinkToPeople", "RequestId", "dbo.Request");
            DropForeignKey("dbo.LinkToPeople", "PartyId", "dbo.Party");
            DropForeignKey("dbo.LinkToPeople", "ListOfChildsId", "dbo.ListOfChilds");
            DropForeignKey("dbo.LinkToPeople", "DirectoryFlightsId", "dbo.DirectoryFlights");
            DropForeignKey("dbo.LinkToPeople", "CounselorsId", "dbo.Counselors");
            DropForeignKey("dbo.LinkToPeople", "ChildId", "dbo.Child");
            DropForeignKey("dbo.LinkToPeople", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.LinkToPeople", "AdministratorTourId", "dbo.AdministratorTour");
            DropForeignKey("dbo.DirectoryFlights", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.DirectoryFlights", "TypeOfTransportId", "dbo.TypeOfTransport");
            DropForeignKey("dbo.DirectoryFlights", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.DirectoryFlights", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.DirectoryFlights", "DepartureId", "dbo.City");
            DropForeignKey("dbo.DirectoryFlights", "ArrivalId", "dbo.City");
            DropForeignKey("dbo.City", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.City", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.TransportInfo", new[] { "StateId" });
            DropIndex("dbo.TransportInfo", new[] { "ArrivalId" });
            DropIndex("dbo.TransportInfo", new[] { "HistoryLinkId" });
            DropIndex("dbo.TransportInfo", new[] { "YearOfRestId" });
            DropIndex("dbo.TransportInfo", new[] { "DepartureId" });
            DropIndex("dbo.LinkToPeople", new[] { "TransportId" });
            DropIndex("dbo.LinkToPeople", new[] { "ListOfChildsId" });
            DropIndex("dbo.LinkToPeople", new[] { "DirectoryFlightsId" });
            DropIndex("dbo.LinkToPeople", new[] { "ChildId" });
            DropIndex("dbo.LinkToPeople", new[] { "PartyId" });
            DropIndex("dbo.LinkToPeople", new[] { "RequestId" });
            DropIndex("dbo.LinkToPeople", new[] { "ApplicantId" });
            DropIndex("dbo.LinkToPeople", new[] { "CounselorsId" });
            DropIndex("dbo.LinkToPeople", new[] { "TypeOfLinkPeopleId" });
            DropIndex("dbo.LinkToPeople", new[] { "AdministratorTourId" });
            DropIndex("dbo.DirectoryFlights", new[] { "DepartureId" });
            DropIndex("dbo.DirectoryFlights", new[] { "StateId" });
            DropIndex("dbo.DirectoryFlights", new[] { "ArrivalId" });
            DropIndex("dbo.DirectoryFlights", new[] { "HistoryLinkId" });
            DropIndex("dbo.DirectoryFlights", new[] { "YearOfRestId" });
            DropIndex("dbo.DirectoryFlights", new[] { "TypeOfTransportId" });
            DropIndex("dbo.City", new[] { "StateId" });
            DropIndex("dbo.City", new[] { "HistoryLinkId" });
            DropColumn("dbo.City", "StateId");
            DropColumn("dbo.City", "HistoryLinkId");
            DropTable("dbo.TypeOfLinkPeople");
            DropTable("dbo.TransportInfo");
            DropTable("dbo.LinkToPeople");
            DropTable("dbo.TypeOfTransport");
            DropTable("dbo.DirectoryFlights");
        }
    }
}

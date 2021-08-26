namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RequestInformationVaucherMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomRatesPrice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 38, scale: 4),
                        DateFrom = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RoomRatesId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomRates", t => t.RoomRatesId)
                .Index(t => t.RoomRatesId);

            CreateTable(
                "dbo.RequestInformationVoucher",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrganizationName = c.String(maxLength: 1000),
                        DateFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        Price = c.Decimal(precision: 38, scale: 4),
                        CostOfRide = c.Decimal(precision: 38, scale: 4),
                        CountPeople = c.Int(nullable: false),
                        TypeId = c.Long(),
                        RequestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeRequestInformationVoucher", t => t.TypeId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .Index(t => t.TypeId)
                .Index(t => t.RequestId);

            CreateTable(
                "dbo.TypeRequestInformationVoucher",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Applicant", "ForeginName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "ForeginLastName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "ForeginName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "ForeginLastName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "AmountOfCompensation", c => c.Decimal(precision: 38, scale: 4));
            AddColumn("dbo.Child", "CostOfRide", c => c.Decimal(precision: 38, scale: 4));
            AddColumn("dbo.Child", "RequestInformationVoucherId", c => c.Long());
            AddColumn("dbo.Request", "BankName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "BankBik", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "BankInn", c => c.String(maxLength: 1000));
            AddColumn("dbo.Request", "BankAccount", c => c.String(maxLength: 1000));
            AddColumn("dbo.ForeginPassport", "ForeginName", c => c.String(maxLength: 1000));
            AddColumn("dbo.ForeginPassport", "ForeginLastName", c => c.String(maxLength: 1000));
            AddColumn("dbo.ForeginPassport", "AdministratorTourId", c => c.Long());
            CreateIndex("dbo.ForeginPassport", "AdministratorTourId");
            CreateIndex("dbo.Child", "RequestInformationVoucherId");
            AddForeignKey("dbo.Child", "RequestInformationVoucherId", "dbo.RequestInformationVoucher", "Id");
            AddForeignKey("dbo.ForeginPassport", "AdministratorTourId", "dbo.AdministratorTour", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.ForeginPassport", "AdministratorTourId", "dbo.AdministratorTour");
            DropForeignKey("dbo.Child", "RequestInformationVoucherId", "dbo.RequestInformationVoucher");
            DropForeignKey("dbo.RequestInformationVoucher", "RequestId", "dbo.Request");
            DropForeignKey("dbo.RequestInformationVoucher", "TypeId", "dbo.TypeRequestInformationVoucher");
            DropForeignKey("dbo.RoomRatesPrice", "RoomRatesId", "dbo.RoomRates");
            DropIndex("dbo.RequestInformationVoucher", new[] { "RequestId" });
            DropIndex("dbo.RequestInformationVoucher", new[] { "TypeId" });
            DropIndex("dbo.Child", new[] { "RequestInformationVoucherId" });
            DropIndex("dbo.RoomRatesPrice", new[] { "RoomRatesId" });
            DropIndex("dbo.ForeginPassport", new[] { "AdministratorTourId" });
            DropColumn("dbo.ForeginPassport", "AdministratorTourId");
            DropColumn("dbo.ForeginPassport", "ForeginLastName");
            DropColumn("dbo.ForeginPassport", "ForeginName");
            DropColumn("dbo.Request", "BankAccount");
            DropColumn("dbo.Request", "BankInn");
            DropColumn("dbo.Request", "BankBik");
            DropColumn("dbo.Request", "BankName");
            DropColumn("dbo.Child", "RequestInformationVoucherId");
            DropColumn("dbo.Child", "CostOfRide");
            DropColumn("dbo.Child", "AmountOfCompensation");
            DropColumn("dbo.Child", "ForeginLastName");
            DropColumn("dbo.Child", "ForeginName");
            DropColumn("dbo.Applicant", "ForeginLastName");
            DropColumn("dbo.Applicant", "ForeginName");
            DropTable("dbo.TypeRequestInformationVoucher");
            DropTable("dbo.RequestInformationVoucher");
            DropTable("dbo.RoomRatesPrice");
        }
    }
}

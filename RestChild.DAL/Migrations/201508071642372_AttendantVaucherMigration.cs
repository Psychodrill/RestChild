namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AttendantVaucherMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestInformationVoucherAttendant",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Price = c.Decimal(precision: 38, scale: 4),
                        CostOfRide = c.Decimal(precision: 38, scale: 4),
                        ApplicantId = c.Long(),
                        RequestInformationVoucherId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.RequestInformationVoucher", t => t.RequestInformationVoucherId)
                .Index(t => t.ApplicantId)
                .Index(t => t.RequestInformationVoucherId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.RequestInformationVoucherAttendant", "RequestInformationVoucherId", "dbo.RequestInformationVoucher");
            DropForeignKey("dbo.RequestInformationVoucherAttendant", "ApplicantId", "dbo.Applicant");
            DropIndex("dbo.RequestInformationVoucherAttendant", new[] { "RequestInformationVoucherId" });
            DropIndex("dbo.RequestInformationVoucherAttendant", new[] { "ApplicantId" });
            DropTable("dbo.RequestInformationVoucherAttendant");
        }
    }
}

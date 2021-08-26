namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AmountInChildMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Child", "CostOfTour", c => c.Decimal(precision: 38, scale: 4));
            AddColumn("dbo.RequestInformationVoucherAttendant", "AmountOfCompensation", c => c.Decimal(precision: 38, scale: 4));
        }

        public override void Down()
        {
            DropColumn("dbo.RequestInformationVoucherAttendant", "AmountOfCompensation");
            DropColumn("dbo.Child", "CostOfTour");
        }
    }
}

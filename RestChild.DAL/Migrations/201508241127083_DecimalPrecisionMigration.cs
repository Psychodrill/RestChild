namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class DecimalPrecisionMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlaceOfRest", "PriceBasePlace", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.PlaceOfRest", "PriceAddonPlace", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.TimeOfRest", "FactorDependence", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Tour", "TourPrice", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Tour", "TourPriceAttendant", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Tour", "PaymentForAdult", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Tour", "PaymentForChild", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Counselors", "Rating", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Calculation", "Summa", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.AddonServicesLink", "Price", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.AddonServicesLink", "CountService", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.AddonServices", "Price", c => c.Decimal(nullable: false, precision: 32, scale: 4));
            AlterColumn("dbo.Hotels", "Squere", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Hotels", "Latitude", c => c.Decimal(precision: 32, scale: 10));
            AlterColumn("dbo.Hotels", "Longitude", c => c.Decimal(precision: 32, scale: 10));
            AlterColumn("dbo.Hotels", "DistanceFromCenter", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Hotels", "DistanceFromBeach", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.TypeOfRooms", "RoomSize", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.TypeOfRooms", "RoomSizePerPerson", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.RoomRates", "Price", c => c.Decimal(nullable: false, precision: 32, scale: 4));
            AlterColumn("dbo.RoomRatesPrice", "Price", c => c.Decimal(nullable: false, precision: 32, scale: 4));
            AlterColumn("dbo.Child", "AmountOfCompensation", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Child", "CostOfRide", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Child", "CostOfTour", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Address", "Latitude", c => c.Decimal(precision: 32, scale: 10));
            AlterColumn("dbo.Address", "Longitude", c => c.Decimal(precision: 32, scale: 10));
            //AlterColumn("dbo.Request", "Price", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.RequestInformationVoucher", "Price", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.RequestInformationVoucher", "CostOfRide", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.RequestInformationVoucherAttendant", "Price", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.RequestInformationVoucherAttendant", "CostOfRide", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.RequestInformationVoucherAttendant", "AmountOfCompensation", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Payment", "PaymentSumm", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.TieColor", "Raiting", c => c.Decimal(precision: 32, scale: 4));
            AlterColumn("dbo.Contract", "Summa", c => c.Decimal(nullable: false, precision: 32, scale: 4));
            AlterColumn("dbo.TourPrice", "Price", c => c.Decimal(precision: 32, scale: 4));
        }

        public override void Down()
        {
            AlterColumn("dbo.TourPrice", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Contract", "Summa", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TieColor", "Raiting", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Payment", "PaymentSumm", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.RequestInformationVoucherAttendant", "AmountOfCompensation", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.RequestInformationVoucherAttendant", "CostOfRide", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.RequestInformationVoucherAttendant", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.RequestInformationVoucher", "CostOfRide", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.RequestInformationVoucher", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Request", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Address", "Longitude", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Address", "Latitude", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Child", "CostOfTour", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Child", "CostOfRide", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Child", "AmountOfCompensation", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.RoomRatesPrice", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.RoomRates", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TypeOfRooms", "RoomSizePerPerson", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TypeOfRooms", "RoomSize", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Hotels", "DistanceFromBeach", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Hotels", "DistanceFromCenter", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Hotels", "Longitude", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Hotels", "Latitude", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Hotels", "Squere", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.AddonServices", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AddonServicesLink", "CountService", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.AddonServicesLink", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Calculation", "Summa", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Counselors", "Rating", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Tour", "PaymentForChild", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Tour", "PaymentForAdult", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Tour", "TourPriceAttendant", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Tour", "TourPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TimeOfRest", "FactorDependence", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PlaceOfRest", "PriceAddonPlace", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PlaceOfRest", "PriceBasePlace", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}

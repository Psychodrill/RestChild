namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CounselorsAndOtherMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClothingSize",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.ForeginPassport", "FirstName", c => c.String(maxLength: 1000));
            AddColumn("dbo.ForeginPassport", "LastName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "ClothingSizeId", c => c.Long());
            AddColumn("dbo.FileHotel", "TypeOfRoomsId", c => c.Long());
            AddColumn("dbo.CounselorTask", "DateUpdate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Hotels", "DistanceFromCenter", c => c.Decimal(precision: 38, scale: 4));
            AlterColumn("dbo.Hotels", "DistanceFromBeach", c => c.Decimal(precision: 38, scale: 4));
            CreateIndex("dbo.Counselors", "ClothingSizeId");
            CreateIndex("dbo.FileHotel", "TypeOfRoomsId");
            AddForeignKey("dbo.Counselors", "ClothingSizeId", "dbo.ClothingSize", "Id");
            AddForeignKey("dbo.FileHotel", "TypeOfRoomsId", "dbo.TypeOfRooms", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.FileHotel", "TypeOfRoomsId", "dbo.TypeOfRooms");
            DropForeignKey("dbo.Counselors", "ClothingSizeId", "dbo.ClothingSize");
            DropIndex("dbo.FileHotel", new[] { "TypeOfRoomsId" });
            DropIndex("dbo.Counselors", new[] { "ClothingSizeId" });
            AlterColumn("dbo.Hotels", "DistanceFromBeach", c => c.Int());
            AlterColumn("dbo.Hotels", "DistanceFromCenter", c => c.Int());
            DropColumn("dbo.CounselorTask", "DateUpdate");
            DropColumn("dbo.FileHotel", "TypeOfRoomsId");
            DropColumn("dbo.Counselors", "ClothingSizeId");
            DropColumn("dbo.ForeginPassport", "LastName");
            DropColumn("dbo.ForeginPassport", "FirstName");
            DropTable("dbo.ClothingSize");
        }
    }
}

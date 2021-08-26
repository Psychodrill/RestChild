namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChildListAndFilesMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Child", "LimitOnOrganizationId", "dbo.LimitOnOrganization");
            DropIndex("dbo.Child", new[] { "LimitOnOrganizationId" });
            CreateTable(
                "dbo.ListOfChilds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsLast = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateChange = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LimitOnOrganizationId = c.Long(),
                        PlaceOfRestId = c.Long(),
                        TimeOfRestId = c.Long(),
                        StatusListChildsId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LimitOnOrganization", t => t.LimitOnOrganizationId)
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRestId)
                .ForeignKey("dbo.StatusListChilds", t => t.StatusListChildsId)
                .ForeignKey("dbo.TimeOfRest", t => t.TimeOfRestId)
                .Index(t => t.LimitOnOrganizationId)
                .Index(t => t.PlaceOfRestId)
                .Index(t => t.TimeOfRestId)
                .Index(t => t.StatusListChildsId);
            
            CreateTable(
                "dbo.StatusListChilds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileHotel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileUrl = c.String(nullable: false, maxLength: 1000),
                        HotelId = c.Long(),
                        FileTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileType", t => t.FileTypeId)
                .ForeignKey("dbo.Hotels", t => t.HotelId)
                .Index(t => t.HotelId)
                .Index(t => t.FileTypeId);
            
            CreateTable(
                "dbo.FileType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileOfTour",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileUrl = c.String(nullable: false, maxLength: 1000),
                        HotelId = c.Long(),
                        FileTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileType", t => t.FileTypeId)
                .ForeignKey("dbo.Tour", t => t.HotelId)
                .Index(t => t.HotelId)
                .Index(t => t.FileTypeId);
            
            AddColumn("dbo.Applicant", "ChildListId", c => c.Long());
            AddColumn("dbo.Child", "ChildListId", c => c.Long());
            CreateIndex("dbo.Applicant", "ChildListId");
            CreateIndex("dbo.Child", "ChildListId");
            AddForeignKey("dbo.Child", "ChildListId", "dbo.ListOfChilds", "Id");
            AddForeignKey("dbo.Applicant", "ChildListId", "dbo.ListOfChilds", "Id");
            DropColumn("dbo.Child", "LimitOnOrganizationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Child", "LimitOnOrganizationId", c => c.Long());
            DropForeignKey("dbo.Applicant", "ChildListId", "dbo.ListOfChilds");
            DropForeignKey("dbo.FileOfTour", "HotelId", "dbo.Tour");
            DropForeignKey("dbo.FileOfTour", "FileTypeId", "dbo.FileType");
            DropForeignKey("dbo.FileHotel", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.FileHotel", "FileTypeId", "dbo.FileType");
            DropForeignKey("dbo.Child", "ChildListId", "dbo.ListOfChilds");
            DropForeignKey("dbo.ListOfChilds", "TimeOfRestId", "dbo.TimeOfRest");
            DropForeignKey("dbo.ListOfChilds", "StatusListChildsId", "dbo.StatusListChilds");
            DropForeignKey("dbo.ListOfChilds", "PlaceOfRestId", "dbo.PlaceOfRest");
            DropForeignKey("dbo.ListOfChilds", "LimitOnOrganizationId", "dbo.LimitOnOrganization");
            DropIndex("dbo.FileOfTour", new[] { "FileTypeId" });
            DropIndex("dbo.FileOfTour", new[] { "HotelId" });
            DropIndex("dbo.FileHotel", new[] { "FileTypeId" });
            DropIndex("dbo.FileHotel", new[] { "HotelId" });
            DropIndex("dbo.ListOfChilds", new[] { "StatusListChildsId" });
            DropIndex("dbo.ListOfChilds", new[] { "TimeOfRestId" });
            DropIndex("dbo.ListOfChilds", new[] { "PlaceOfRestId" });
            DropIndex("dbo.ListOfChilds", new[] { "LimitOnOrganizationId" });
            DropIndex("dbo.Child", new[] { "ChildListId" });
            DropIndex("dbo.Applicant", new[] { "ChildListId" });
            DropColumn("dbo.Child", "ChildListId");
            DropColumn("dbo.Applicant", "ChildListId");
            DropTable("dbo.FileOfTour");
            DropTable("dbo.FileType");
            DropTable("dbo.FileHotel");
            DropTable("dbo.StatusListChilds");
            DropTable("dbo.ListOfChilds");
            CreateIndex("dbo.Child", "LimitOnOrganizationId");
            AddForeignKey("dbo.Child", "LimitOnOrganizationId", "dbo.LimitOnOrganization", "Id");
        }
    }
}

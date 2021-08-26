namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusAndSignInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LimitOnOrganization", "LimitStatusId", "dbo.LimitStatus");
            DropForeignKey("dbo.LimitOnVedomstvo", "LimitStatusId", "dbo.LimitStatus");
            DropForeignKey("dbo.ListOfChilds", "StatusListChildsId", "dbo.StatusListChilds");
            DropForeignKey("dbo.FileHotel", "FileTypeId", "dbo.FileType");
            DropForeignKey("dbo.FileOfTour", "FileTypeId", "dbo.FileType");
            DropIndex("dbo.LimitOnOrganization", new[] { "LimitStatusId" });
            DropPrimaryKey("dbo.LimitStatus");
            DropPrimaryKey("dbo.StatusListChilds");
            DropPrimaryKey("dbo.FileType");
            CreateTable(
                "dbo.SignInfo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SignDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FileUrl = c.String(nullable: false, maxLength: 1000),
                        Title = c.String(nullable: false, maxLength: 1000),
                        AccountId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.LimitStatusYearOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LimitStatusOrganization",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.LimitOnOrganization", "LimitStatusOrganizationId", c => c.Long());
            AddColumn("dbo.LimitOnOrganization", "SignInfoId", c => c.Long());
            AddColumn("dbo.LimitOnVedomstvo", "SignInfoId", c => c.Long());
            AddColumn("dbo.YearOfRest", "SignInfoId", c => c.Long());
            AddColumn("dbo.YearOfRest", "LimitStatusYearOfRestId", c => c.Long());
            AddColumn("dbo.Child", "PaymentFileUrl", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "PaymentFileTitle", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tour", "SignInfoId", c => c.Long());
//            AlterColumn("dbo.LimitStatus", "Id", c => c.Long(nullable: false));
//            AlterColumn("dbo.StatusListChilds", "Id", c => c.Long(nullable: false));
//            AlterColumn("dbo.FileType", "Id", c => c.Long(nullable: false));

			RenameColumn("dbo.LimitStatus", "Id", "Id2");
			RenameColumn("dbo.StatusListChilds", "Id", "Id2");
			RenameColumn("dbo.FileType", "Id", "Id2");

			AddColumn("dbo.LimitStatus", "Id", c => c.Long(nullable: false));
			AddColumn("dbo.StatusListChilds", "Id", c => c.Long(nullable: false));
			AddColumn("dbo.FileType", "Id", c => c.Long(nullable: false));

			Sql("update dbo.LimitStatus set Id=Id2");
			Sql("update dbo.StatusListChilds set Id=Id2");
			Sql("update dbo.FileType set Id=Id2");

			DropColumn("dbo.LimitStatus", "Id2");
			DropColumn("dbo.StatusListChilds", "Id2");
			DropColumn("dbo.FileType", "Id2");

            AddPrimaryKey("dbo.LimitStatus", "Id");
            AddPrimaryKey("dbo.StatusListChilds", "Id");
            AddPrimaryKey("dbo.FileType", "Id");
            CreateIndex("dbo.LimitOnOrganization", "LimitStatusOrganizationId");
            CreateIndex("dbo.LimitOnOrganization", "SignInfoId");
            CreateIndex("dbo.LimitOnVedomstvo", "SignInfoId");
            CreateIndex("dbo.YearOfRest", "SignInfoId");
            CreateIndex("dbo.YearOfRest", "LimitStatusYearOfRestId");
            CreateIndex("dbo.Tour", "SignInfoId");
            AddForeignKey("dbo.LimitOnVedomstvo", "SignInfoId", "dbo.SignInfo", "Id");
            AddForeignKey("dbo.YearOfRest", "LimitStatusYearOfRestId", "dbo.LimitStatusYearOfRest", "Id");
            AddForeignKey("dbo.YearOfRest", "SignInfoId", "dbo.SignInfo", "Id");
            AddForeignKey("dbo.LimitOnOrganization", "LimitStatusOrganizationId", "dbo.LimitStatusOrganization", "Id");
            AddForeignKey("dbo.LimitOnOrganization", "SignInfoId", "dbo.SignInfo", "Id");
            AddForeignKey("dbo.Tour", "SignInfoId", "dbo.SignInfo", "Id");
            AddForeignKey("dbo.LimitOnVedomstvo", "LimitStatusId", "dbo.LimitStatus", "Id");
            AddForeignKey("dbo.ListOfChilds", "StatusListChildsId", "dbo.StatusListChilds", "Id");
            AddForeignKey("dbo.FileHotel", "FileTypeId", "dbo.FileType", "Id");
            AddForeignKey("dbo.FileOfTour", "FileTypeId", "dbo.FileType", "Id");
            DropColumn("dbo.LimitOnOrganization", "LimitStatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LimitOnOrganization", "LimitStatusId", c => c.Long());
            DropForeignKey("dbo.FileOfTour", "FileTypeId", "dbo.FileType");
            DropForeignKey("dbo.FileHotel", "FileTypeId", "dbo.FileType");
            DropForeignKey("dbo.ListOfChilds", "StatusListChildsId", "dbo.StatusListChilds");
            DropForeignKey("dbo.LimitOnVedomstvo", "LimitStatusId", "dbo.LimitStatus");
            DropForeignKey("dbo.Tour", "SignInfoId", "dbo.SignInfo");
            DropForeignKey("dbo.LimitOnOrganization", "SignInfoId", "dbo.SignInfo");
            DropForeignKey("dbo.LimitOnOrganization", "LimitStatusOrganizationId", "dbo.LimitStatusOrganization");
            DropForeignKey("dbo.YearOfRest", "SignInfoId", "dbo.SignInfo");
            DropForeignKey("dbo.YearOfRest", "LimitStatusYearOfRestId", "dbo.LimitStatusYearOfRest");
            DropForeignKey("dbo.LimitOnVedomstvo", "SignInfoId", "dbo.SignInfo");
            DropForeignKey("dbo.SignInfo", "AccountId", "dbo.Account");
            DropIndex("dbo.Tour", new[] { "SignInfoId" });
            DropIndex("dbo.YearOfRest", new[] { "LimitStatusYearOfRestId" });
            DropIndex("dbo.YearOfRest", new[] { "SignInfoId" });
            DropIndex("dbo.SignInfo", new[] { "AccountId" });
            DropIndex("dbo.LimitOnVedomstvo", new[] { "SignInfoId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "SignInfoId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "LimitStatusOrganizationId" });
            DropPrimaryKey("dbo.FileType");
            DropPrimaryKey("dbo.StatusListChilds");
            DropPrimaryKey("dbo.LimitStatus");
            AlterColumn("dbo.FileType", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.StatusListChilds", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.LimitStatus", "Id", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.Tour", "SignInfoId");
            DropColumn("dbo.Child", "PaymentFileTitle");
            DropColumn("dbo.Child", "PaymentFileUrl");
            DropColumn("dbo.YearOfRest", "LimitStatusYearOfRestId");
            DropColumn("dbo.YearOfRest", "SignInfoId");
            DropColumn("dbo.LimitOnVedomstvo", "SignInfoId");
            DropColumn("dbo.LimitOnOrganization", "SignInfoId");
            DropColumn("dbo.LimitOnOrganization", "LimitStatusOrganizationId");
            DropTable("dbo.LimitStatusOrganization");
            DropTable("dbo.LimitStatusYearOfRest");
            DropTable("dbo.SignInfo");
            AddPrimaryKey("dbo.FileType", "Id");
            AddPrimaryKey("dbo.StatusListChilds", "Id");
            AddPrimaryKey("dbo.LimitStatus", "Id");
            CreateIndex("dbo.LimitOnOrganization", "LimitStatusId");
            AddForeignKey("dbo.FileOfTour", "FileTypeId", "dbo.FileType", "Id");
            AddForeignKey("dbo.FileHotel", "FileTypeId", "dbo.FileType", "Id");
            AddForeignKey("dbo.ListOfChilds", "StatusListChildsId", "dbo.StatusListChilds", "Id");
            AddForeignKey("dbo.LimitOnVedomstvo", "LimitStatusId", "dbo.LimitStatus", "Id");
            AddForeignKey("dbo.LimitOnOrganization", "LimitStatusId", "dbo.LimitStatus", "Id");
        }
    }
}

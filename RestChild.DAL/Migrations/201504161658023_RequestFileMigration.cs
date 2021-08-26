namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestFileMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileTitle = c.String(nullable: false, maxLength: 1000),
                        DataCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreateUserId = c.Long(),
                        RequestFileTypeId = c.Long(),
                        RequestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .ForeignKey("dbo.RequestFileType", t => t.RequestFileTypeId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .Index(t => t.CreateUserId)
                .Index(t => t.RequestFileTypeId)
                .Index(t => t.RequestId);
            
            CreateTable(
                "dbo.RequestFileType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                        ForMpgu = c.Boolean(nullable: false),
                        ForOperator = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestFile", "RequestId", "dbo.Request");
            DropForeignKey("dbo.RequestFile", "RequestFileTypeId", "dbo.RequestFileType");
            DropForeignKey("dbo.RequestFile", "CreateUserId", "dbo.Account");
            DropIndex("dbo.RequestFile", new[] { "RequestId" });
            DropIndex("dbo.RequestFile", new[] { "RequestFileTypeId" });
            DropIndex("dbo.RequestFile", new[] { "CreateUserId" });
            DropTable("dbo.RequestFileType");
            DropTable("dbo.RequestFile");
        }
    }
}

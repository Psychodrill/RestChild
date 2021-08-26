namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelsTypeAndOther : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TypeOfRooms", name: "HotelsId", newName: "HotelId");
            RenameIndex(table: "dbo.TypeOfRooms", name: "IX_HotelsId", newName: "IX_HotelId");
            CreateTable(
                "dbo.HotelType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Hotels", "HotelTypeId", c => c.Long());
            AddColumn("dbo.Hotels", "StateMachineStateId", c => c.Long());
            AddColumn("dbo.FileType", "IsPhoto", c => c.Boolean(nullable: false));
            AddColumn("dbo.FileType", "ParentId", c => c.Long());
            CreateIndex("dbo.Hotels", "HotelTypeId");
            CreateIndex("dbo.Hotels", "StateMachineStateId");
            CreateIndex("dbo.FileType", "ParentId");
            AddForeignKey("dbo.FileType", "ParentId", "dbo.FileType", "Id");
            AddForeignKey("dbo.Hotels", "HotelTypeId", "dbo.HotelType", "Id");
            AddForeignKey("dbo.Hotels", "StateMachineStateId", "dbo.StateMachineState", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotels", "StateMachineStateId", "dbo.StateMachineState");
            DropForeignKey("dbo.Hotels", "HotelTypeId", "dbo.HotelType");
            DropForeignKey("dbo.FileType", "ParentId", "dbo.FileType");
            DropIndex("dbo.FileType", new[] { "ParentId" });
            DropIndex("dbo.Hotels", new[] { "StateMachineStateId" });
            DropIndex("dbo.Hotels", new[] { "HotelTypeId" });
            DropColumn("dbo.FileType", "ParentId");
            DropColumn("dbo.FileType", "IsPhoto");
            DropColumn("dbo.Hotels", "StateMachineStateId");
            DropColumn("dbo.Hotels", "HotelTypeId");
            DropTable("dbo.HotelType");
            RenameIndex(table: "dbo.TypeOfRooms", name: "IX_HotelId", newName: "IX_HotelsId");
            RenameColumn(table: "dbo.TypeOfRooms", name: "HotelId", newName: "HotelsId");
        }
    }
}

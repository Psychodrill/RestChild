namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class partysMigrationFirst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Party",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        TourId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .Index(t => t.TourId);
            
            AddColumn("dbo.Child", "PartyId", c => c.Long());
            CreateIndex("dbo.Child", "PartyId");
            AddForeignKey("dbo.Child", "PartyId", "dbo.Party", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Child", "PartyId", "dbo.Party");
            DropForeignKey("dbo.Party", "TourId", "dbo.Tour");
            DropIndex("dbo.Party", new[] { "TourId" });
            DropIndex("dbo.Child", new[] { "PartyId" });
            DropColumn("dbo.Child", "PartyId");
            DropTable("dbo.Party");
        }
    }
}

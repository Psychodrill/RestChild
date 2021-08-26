namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoutJournalFilesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoutJournalFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(maxLength: 1000),
                        FileLink = c.String(maxLength: 1000),
                        IsPhoto = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        BoutJournalId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BoutJournal", t => t.BoutJournalId)
                .Index(t => t.BoutJournalId);
            
            AddColumn("dbo.BoutJournal", "AdministratorTourId", c => c.Long());
            AddColumn("dbo.BoutJournal", "PartyId", c => c.Long());
            CreateIndex("dbo.BoutJournal", "AdministratorTourId");
            CreateIndex("dbo.BoutJournal", "PartyId");
            AddForeignKey("dbo.BoutJournal", "AdministratorTourId", "dbo.AdministratorTour", "Id");
            AddForeignKey("dbo.BoutJournal", "PartyId", "dbo.Party", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoutJournal", "PartyId", "dbo.Party");
            DropForeignKey("dbo.BoutJournalFile", "BoutJournalId", "dbo.BoutJournal");
            DropForeignKey("dbo.BoutJournal", "AdministratorTourId", "dbo.AdministratorTour");
            DropIndex("dbo.BoutJournalFile", new[] { "BoutJournalId" });
            DropIndex("dbo.BoutJournal", new[] { "PartyId" });
            DropIndex("dbo.BoutJournal", new[] { "AdministratorTourId" });
            DropColumn("dbo.BoutJournal", "PartyId");
            DropColumn("dbo.BoutJournal", "AdministratorTourId");
            DropTable("dbo.BoutJournalFile");
        }
    }
}

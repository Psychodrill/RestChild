namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SetDefaultBoutJournalFields : DbMigration
    {
        public override void Up()
        {
			Sql(@"Insert into BoutJournalType (Id, Name, IsActive, LastUpdateTick) Values(1, '-', 1, 0);");
			Sql(@"Insert into BoutJournalType (Id, Name, IsActive, LastUpdateTick) Values(2, '-', 1, 0);");
			Sql(@"Insert into BoutJournalType (Id, Name, IsActive, LastUpdateTick) Values(3, '-', 1, 0);");

			Sql(@"Update BoutJournal SET BoutJournalTypeId = 1");
			Sql(@"Update BoutJournal SET ForSite = 1");
        }

        public override void Down()
        {
			Sql(@"Update BoutJournal SET BoutJournalTypeId = NULL");
			Sql(@"Update BoutJournal SET ForSite = 0");

			Sql(@"delete from BoutJournalType");
		}
    }
}

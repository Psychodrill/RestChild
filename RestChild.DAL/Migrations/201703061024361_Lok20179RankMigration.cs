namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok20179RankMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "MayFinalSend", c => c.Boolean(nullable: false));
            AddColumn("dbo.ListTravelersRequest", "Rank", c => c.Int());
            AddColumn("dbo.ListTravelersRequest", "DateRequest", c => c.DateTime(precision: 7, storeType: "datetime2"));
			CreateIndex("dbo.ListTravelersRequest", new[] { "Rank" }, false, "IND_Rank");
			CreateIndex("dbo.ListTravelersRequest", new[] { "Rank", "DateRequest", "RequestId" }, false, "IND_RankDateRequest");
			CreateIndex("dbo.ListTravelersRequest", new[] { "Rank", "DateRequest", "IsIncluded", "RequestId" }, false, "IND_RankDateIsIncludedRequest");
		}

		public override void Down()
        {
			DropIndex("dbo.ListTravelersRequest", "IND_Rank");
			DropIndex("dbo.ListTravelersRequest", "IND_RankDateRequest");
			DropIndex("dbo.ListTravelersRequest", "IND_RankDateIsIncludedRequest");
			DropColumn("dbo.ListTravelersRequest", "DateRequest");
            DropColumn("dbo.ListTravelersRequest", "Rank");
            DropColumn("dbo.Request", "MayFinalSend");
        }
    }
}

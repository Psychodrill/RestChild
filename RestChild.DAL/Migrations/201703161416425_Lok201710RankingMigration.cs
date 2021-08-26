namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok201710RankingMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExchangeUTS", "DateToSend", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ListTravelers", "Point", c => c.String(maxLength: 1000));
            AddColumn("dbo.ListTravelers", "Limit", c => c.Long());
            AddColumn("dbo.ListTravelers", "ParentId", c => c.Long());
            CreateIndex("dbo.ListTravelers", "ParentId");
            AddForeignKey("dbo.ListTravelers", "ParentId", "dbo.ListTravelers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListTravelers", "ParentId", "dbo.ListTravelers");
            DropIndex("dbo.ListTravelers", new[] { "ParentId" });
            DropColumn("dbo.ListTravelers", "ParentId");
            DropColumn("dbo.ListTravelers", "Limit");
            DropColumn("dbo.ListTravelers", "Point");
            DropColumn("dbo.ExchangeUTS", "DateToSend");
        }
    }
}

namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok2019ToursUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "TypeOfRestrictionId", c => c.Long());
            AddColumn("dbo.Tour", "TypeOfSubRestrictionId", c => c.Long());
            AddColumn("dbo.ExchangeUTS", "IsSigned", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Tour", "TypeOfRestrictionId");
            CreateIndex("dbo.Tour", "TypeOfSubRestrictionId");
            AddForeignKey("dbo.Tour", "TypeOfRestrictionId", "dbo.TypeOfRestriction", "Id");
            AddForeignKey("dbo.Tour", "TypeOfSubRestrictionId", "dbo.TypeOfSubRestriction", "Id");
            Sql("Update [dbo].[Tour] Set [TypeOfRestrictionId] = 7, [TypeOfSubRestrictionId] = 1 Where [ForInvalid] = 1");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "TypeOfSubRestrictionId", "dbo.TypeOfSubRestriction");
            DropForeignKey("dbo.Tour", "TypeOfRestrictionId", "dbo.TypeOfRestriction");
            DropIndex("dbo.Tour", new[] { "TypeOfSubRestrictionId" });
            DropIndex("dbo.Tour", new[] { "TypeOfRestrictionId" });
            DropColumn("dbo.ExchangeUTS", "IsSigned");
            DropColumn("dbo.Tour", "TypeOfSubRestrictionId");
            DropColumn("dbo.Tour", "TypeOfRestrictionId");
        }
    }
}

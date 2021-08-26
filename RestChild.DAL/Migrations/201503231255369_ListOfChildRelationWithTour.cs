namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListOfChildRelationWithTour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StateMachineAction", "NeedSign", c => c.Boolean(nullable: false));
            AddColumn("dbo.StateMachineAction", "Attribute_420", c => c.Int());
            AddColumn("dbo.Child", "IsExcluded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "IsIncluded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "ReasonInclude", c => c.String(maxLength: 1000));
            AddColumn("dbo.Child", "ReasonExclude", c => c.String(maxLength: 1000));
            AddColumn("dbo.ListOfChilds", "TourId", c => c.Long());
            CreateIndex("dbo.ListOfChilds", "TourId");
            AddForeignKey("dbo.ListOfChilds", "TourId", "dbo.Tour", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListOfChilds", "TourId", "dbo.Tour");
            DropIndex("dbo.ListOfChilds", new[] { "TourId" });
            DropColumn("dbo.ListOfChilds", "TourId");
            DropColumn("dbo.Child", "ReasonExclude");
            DropColumn("dbo.Child", "ReasonInclude");
            DropColumn("dbo.Child", "IsIncluded");
            DropColumn("dbo.Child", "IsExcluded");
            DropColumn("dbo.StateMachineAction", "Attribute_420");
            DropColumn("dbo.StateMachineAction", "NeedSign");
        }
    }
}

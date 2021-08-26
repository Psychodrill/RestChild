namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupTypeRestForDoubleMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfGroupCheck",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Period = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TypeOfRest", "TypeOfGroupCheckId", c => c.Long());
            AddColumn("dbo.Child", "YearOfCompany", c => c.Int());
            AddColumn("dbo.Child", "TypeOfGroupCheckId", c => c.Long());
            CreateIndex("dbo.TypeOfRest", "TypeOfGroupCheckId");
            CreateIndex("dbo.Child", "TypeOfGroupCheckId");
            AddForeignKey("dbo.TypeOfRest", "TypeOfGroupCheckId", "dbo.TypeOfGroupCheck", "Id");
            AddForeignKey("dbo.Child", "TypeOfGroupCheckId", "dbo.TypeOfGroupCheck", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Child", "TypeOfGroupCheckId", "dbo.TypeOfGroupCheck");
            DropForeignKey("dbo.TypeOfRest", "TypeOfGroupCheckId", "dbo.TypeOfGroupCheck");
            DropIndex("dbo.Child", new[] { "TypeOfGroupCheckId" });
            DropIndex("dbo.TypeOfRest", new[] { "TypeOfGroupCheckId" });
            DropColumn("dbo.Child", "TypeOfGroupCheckId");
            DropColumn("dbo.Child", "YearOfCompany");
            DropColumn("dbo.TypeOfRest", "TypeOfGroupCheckId");
            DropTable("dbo.TypeOfGroupCheck");
        }
    }
}

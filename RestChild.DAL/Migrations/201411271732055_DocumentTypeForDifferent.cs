namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentTypeForDifferent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocumentType", "ForApplicant", c => c.Boolean(nullable: false));
            AddColumn("dbo.DocumentType", "ForAgent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocumentType", "ForAgent");
            DropColumn("dbo.DocumentType", "ForApplicant");
        }
    }
}

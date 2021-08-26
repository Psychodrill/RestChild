namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLimitOrganizationName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListOfChilds", "Name", c => c.String(maxLength: 1000));
            AddColumn("dbo.ListOfChilds", "CountChild", c => c.Int(nullable: false));
            AddColumn("dbo.ListOfChilds", "CountAttendants", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ListOfChilds", "CountAttendants");
            DropColumn("dbo.ListOfChilds", "CountChild");
            DropColumn("dbo.ListOfChilds", "Name");
        }
    }
}

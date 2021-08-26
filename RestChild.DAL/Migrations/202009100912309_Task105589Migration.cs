namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task105589Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListOfChilds", "RulesAgreement", c => c.Boolean(defaultValue: true, nullable: false));
            AddColumn("dbo.ListOfChilds", "PupilsRulesAgreement", c => c.Boolean(defaultValue: true, nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ListOfChilds", "PupilsRulesAgreement");
            DropColumn("dbo.ListOfChilds", "RulesAgreement");
        }
    }
}

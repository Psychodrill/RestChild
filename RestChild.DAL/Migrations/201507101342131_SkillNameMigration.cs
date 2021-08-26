namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkillNameMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Skill", "Name", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skill", "Name", c => c.String(nullable: false));
        }
    }
}

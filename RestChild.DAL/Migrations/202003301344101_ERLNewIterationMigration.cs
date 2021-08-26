namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ERLNewIterationMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ERLBenefitStatus", "ERLMessageId", c => c.Guid());
            AddColumn("dbo.ERLPersonStatus", "ERLMessageId", c => c.Guid());
            DropColumn("dbo.ERLBenefitStatus", "IsSent");
            DropColumn("dbo.ERLBenefitStatus", "AnswerRecived");
            DropColumn("dbo.ERLPersonStatus", "IsSent");
            DropColumn("dbo.ERLPersonStatus", "AnswerRecived");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ERLPersonStatus", "AnswerRecived", c => c.Boolean(nullable: false));
            AddColumn("dbo.ERLPersonStatus", "IsSent", c => c.Boolean(nullable: false));
            AddColumn("dbo.ERLBenefitStatus", "AnswerRecived", c => c.Boolean(nullable: false));
            AddColumn("dbo.ERLBenefitStatus", "IsSent", c => c.Boolean(nullable: false));
            DropColumn("dbo.ERLPersonStatus", "ERLMessageId");
            DropColumn("dbo.ERLBenefitStatus", "ERLMessageId");
        }
    }
}

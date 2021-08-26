namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task99506Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BtiDistrict", "Okato", c => c.Int());
            AddColumn("dbo.Request", "NeedSendForRegistrationByPassport", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Request", "NeedSendForAisoLegalRepresentation", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "NeedSendForAisoLegalRepresentation");
            DropColumn("dbo.Request", "NeedSendForRegistrationByPassport");
            DropColumn("dbo.BtiDistrict", "Okato");
        }
    }
}

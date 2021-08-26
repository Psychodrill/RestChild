namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task115934Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PupilGroupListCollaborator", "OrganisatonAddresId", c => c.Long());
            AddColumn("dbo.PupilGroupListMember", "OrganisatonAddresId", c => c.Long());
            CreateIndex("dbo.PupilGroupListCollaborator", "OrganisatonAddresId");
            CreateIndex("dbo.PupilGroupListMember", "OrganisatonAddresId");
            AddForeignKey("dbo.PupilGroupListCollaborator", "OrganisatonAddresId", "dbo.OrphanageAddress", "Id");
            AddForeignKey("dbo.PupilGroupListMember", "OrganisatonAddresId", "dbo.OrphanageAddress", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PupilGroupListMember", "OrganisatonAddresId", "dbo.OrphanageAddress");
            DropForeignKey("dbo.PupilGroupListCollaborator", "OrganisatonAddresId", "dbo.OrphanageAddress");
            DropIndex("dbo.PupilGroupListMember", new[] { "OrganisatonAddresId" });
            DropIndex("dbo.PupilGroupListCollaborator", new[] { "OrganisatonAddresId" });
            DropColumn("dbo.PupilGroupListMember", "OrganisatonAddresId");
            DropColumn("dbo.PupilGroupListCollaborator", "OrganisatonAddresId");
        }
    }
}

namespace YandexMapsOrganizationParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnonUsers", "MinFreeReq", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnonUsers", "MinFreeReq");
        }
    }
}

namespace YandexMapsOrganizationParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnonUsers", "RequstsLeft", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "RequestsLeft", c => c.Int(nullable: false));
            DropColumn("dbo.AnonUsers", "RequestCount");
            DropColumn("dbo.AnonUsers", "MinFreeReq");
            DropColumn("dbo.AspNetUsers", "AvailableReqCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AvailableReqCount", c => c.Int(nullable: false));
            AddColumn("dbo.AnonUsers", "MinFreeReq", c => c.Int(nullable: false));
            AddColumn("dbo.AnonUsers", "RequestCount", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "RequestsLeft");
            DropColumn("dbo.AnonUsers", "RequstsLeft");
        }
    }
}

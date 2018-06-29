namespace YandexMapsOrganizationParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        notification_type = c.String(),
                        operation_id = c.String(),
                        label = c.String(),
                        datetime = c.String(),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        withdraw_amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        sender = c.String(),
                        sha1_hash = c.String(),
                        currency = c.String(),
                        codepro = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "AvailableReqCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentNotifications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PaymentNotifications", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "AvailableReqCount");
            DropTable("dbo.PaymentNotifications");
        }
    }
}

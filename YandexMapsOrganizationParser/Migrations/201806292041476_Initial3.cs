namespace YandexMapsOrganizationParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sender = c.String(),
                        Operation_Id = c.String(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        WithdrawAmount = c.Decimal(precision: 18, scale: 2),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}

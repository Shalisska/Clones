namespace Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Moneys",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        BuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Moneys", "StockId", "dbo.Stocks");
            DropIndex("dbo.Moneys", new[] { "StockId" });
            DropTable("dbo.Moneys");
        }
    }
}

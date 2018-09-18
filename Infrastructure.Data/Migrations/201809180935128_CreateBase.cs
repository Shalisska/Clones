namespace Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PriceBase = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 4),
                        StockId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Moneys", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Resources", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Accounts", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.Resources", new[] { "StockId" });
            DropIndex("dbo.Moneys", new[] { "StockId" });
            DropIndex("dbo.Accounts", new[] { "ProfileId" });
            DropTable("dbo.Resources");
            DropTable("dbo.Stocks");
            DropTable("dbo.Moneys");
            DropTable("dbo.Profiles");
            DropTable("dbo.Accounts");
        }
    }
}

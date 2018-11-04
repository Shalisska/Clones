namespace Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _181105 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Moneys", newName: "Currencies");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Currencies", newName: "Moneys");
        }
    }
}

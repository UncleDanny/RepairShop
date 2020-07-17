namespace RepairShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvailablePartActivity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AvailableParts", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AvailableParts", "isActive");
        }
    }
}

namespace RepairShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EndMe : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "FirstName", c => c.String());
            AlterColumn("dbo.Customers", "LastName", c => c.String());
            AlterColumn("dbo.Repairmen", "FirstName", c => c.String());
            AlterColumn("dbo.Repairmen", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Repairmen", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Repairmen", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "FirstName", c => c.String(nullable: false));
        }
    }
}

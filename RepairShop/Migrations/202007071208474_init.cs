namespace RepairShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Type = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RepairOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RepairOrders", t => t.RepairOrder_ID)
                .Index(t => t.RepairOrder_ID);
            
            CreateTable(
                "dbo.RepairOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Customer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID)
                .Index(t => t.Customer_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parts", "RepairOrder_ID", "dbo.RepairOrders");
            DropForeignKey("dbo.RepairOrders", "Customer_ID", "dbo.Customers");
            DropIndex("dbo.RepairOrders", new[] { "Customer_ID" });
            DropIndex("dbo.Parts", new[] { "RepairOrder_ID" });
            DropTable("dbo.RepairOrders");
            DropTable("dbo.Parts");
            DropTable("dbo.Customers");
        }
    }
}

namespace RepairShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailableParts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false),
                        Type = c.String(nullable: false),
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
                        RepairDescription = c.String(),
                        Customer_ID = c.Int(),
                        Repairman_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID)
                .ForeignKey("dbo.Repairmen", t => t.Repairman_ID)
                .Index(t => t.Customer_ID)
                .Index(t => t.Repairman_ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Repairmen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Wage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RepairOrders", "Repairman_ID", "dbo.Repairmen");
            DropForeignKey("dbo.AvailableParts", "RepairOrder_ID", "dbo.RepairOrders");
            DropForeignKey("dbo.RepairOrders", "Customer_ID", "dbo.Customers");
            DropIndex("dbo.RepairOrders", new[] { "Repairman_ID" });
            DropIndex("dbo.RepairOrders", new[] { "Customer_ID" });
            DropIndex("dbo.AvailableParts", new[] { "RepairOrder_ID" });
            DropTable("dbo.Parts");
            DropTable("dbo.Repairmen");
            DropTable("dbo.Customers");
            DropTable("dbo.RepairOrders");
            DropTable("dbo.AvailableParts");
        }
    }
}

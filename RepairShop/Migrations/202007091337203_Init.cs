namespace RepairShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.Repairmen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Wage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RepairOrderParts",
                c => new
                    {
                        RepairOrder_ID = c.Int(nullable: false),
                        Part_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RepairOrder_ID, t.Part_ID })
                .ForeignKey("dbo.RepairOrders", t => t.RepairOrder_ID, cascadeDelete: true)
                .ForeignKey("dbo.Parts", t => t.Part_ID, cascadeDelete: true)
                .Index(t => t.RepairOrder_ID)
                .Index(t => t.Part_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RepairOrders", "Repairman_ID", "dbo.Repairmen");
            DropForeignKey("dbo.RepairOrderParts", "Part_ID", "dbo.Parts");
            DropForeignKey("dbo.RepairOrderParts", "RepairOrder_ID", "dbo.RepairOrders");
            DropForeignKey("dbo.RepairOrders", "Customer_ID", "dbo.Customers");
            DropIndex("dbo.RepairOrderParts", new[] { "Part_ID" });
            DropIndex("dbo.RepairOrderParts", new[] { "RepairOrder_ID" });
            DropIndex("dbo.RepairOrders", new[] { "Repairman_ID" });
            DropIndex("dbo.RepairOrders", new[] { "Customer_ID" });
            DropTable("dbo.RepairOrderParts");
            DropTable("dbo.Repairmen");
            DropTable("dbo.RepairOrders");
            DropTable("dbo.Parts");
            DropTable("dbo.Customers");
        }
    }
}

namespace PizzaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderPizzas",
                c => new
                    {
                        OrderPizzaId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderRefId = c.Int(nullable: false),
                        PizzaRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderPizzaId)
                .ForeignKey("dbo.Orders", t => t.OrderRefId, cascadeDelete: true)
                .ForeignKey("dbo.Pizzas", t => t.PizzaRefId, cascadeDelete: true)
                .Index(t => t.OrderRefId)
                .Index(t => t.PizzaRefId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.Pizzas",
                c => new
                    {
                        PizzaId = c.Int(nullable: false, identity: true),
                        PizzaName = c.String(),
                        SmallPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MediumPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LargePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PizzaId);
            
            CreateTable(
                "dbo.Toppings",
                c => new
                    {
                        ToppingsId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ToppingsId);
            
            CreateTable(
                "dbo.PlacedOrders",
                c => new
                    {
                        PlacedOrdersId = c.Int(nullable: false, identity: true),
                        OrderRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlacedOrdersId)
                .ForeignKey("dbo.Orders", t => t.OrderRefId, cascadeDelete: true)
                .Index(t => t.OrderRefId);
            
            CreateTable(
                "dbo.ToppingsPizzas",
                c => new
                    {
                        Toppings_ToppingsId = c.Int(nullable: false),
                        Pizza_PizzaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Toppings_ToppingsId, t.Pizza_PizzaId })
                .ForeignKey("dbo.Toppings", t => t.Toppings_ToppingsId, cascadeDelete: true)
                .ForeignKey("dbo.Pizzas", t => t.Pizza_PizzaId, cascadeDelete: true)
                .Index(t => t.Toppings_ToppingsId)
                .Index(t => t.Pizza_PizzaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlacedOrders", "OrderRefId", "dbo.Orders");
            DropForeignKey("dbo.OrderPizzas", "PizzaRefId", "dbo.Pizzas");
            DropForeignKey("dbo.ToppingsPizzas", "Pizza_PizzaId", "dbo.Pizzas");
            DropForeignKey("dbo.ToppingsPizzas", "Toppings_ToppingsId", "dbo.Toppings");
            DropForeignKey("dbo.OrderPizzas", "OrderRefId", "dbo.Orders");
            DropIndex("dbo.ToppingsPizzas", new[] { "Pizza_PizzaId" });
            DropIndex("dbo.ToppingsPizzas", new[] { "Toppings_ToppingsId" });
            DropIndex("dbo.PlacedOrders", new[] { "OrderRefId" });
            DropIndex("dbo.OrderPizzas", new[] { "PizzaRefId" });
            DropIndex("dbo.OrderPizzas", new[] { "OrderRefId" });
            DropTable("dbo.ToppingsPizzas");
            DropTable("dbo.PlacedOrders");
            DropTable("dbo.Toppings");
            DropTable("dbo.Pizzas");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderPizzas");
        }
    }
}

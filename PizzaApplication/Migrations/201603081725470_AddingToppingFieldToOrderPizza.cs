namespace PizzaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingToppingFieldToOrderPizza : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderPizzas", "Toppings", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderPizzas", "Toppings");
        }
    }
}

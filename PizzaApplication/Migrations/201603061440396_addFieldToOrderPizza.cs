namespace PizzaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldToOrderPizza : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderPizzas", "PizzaSize", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderPizzas", "PizzaSize");
        }
    }
}

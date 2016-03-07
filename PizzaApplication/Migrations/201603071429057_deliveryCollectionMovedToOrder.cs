namespace PizzaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deliveryCollectionMovedToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "deliveryCollection", c => c.String());
            DropColumn("dbo.PlacedOrders", "deliveryCollection");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlacedOrders", "deliveryCollection", c => c.String());
            DropColumn("dbo.Orders", "deliveryCollection");
        }
    }
}

namespace PizzaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deliveryCollectionFieldAddedToPlacedOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlacedOrders", "deliveryCollection", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlacedOrders", "deliveryCollection");
        }
    }
}

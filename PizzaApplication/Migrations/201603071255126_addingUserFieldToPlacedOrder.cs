namespace PizzaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUserFieldToPlacedOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlacedOrders", "userId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlacedOrders", "userId");
        }
    }
}

namespace PizzaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUserFieldToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "userId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "userId");
        }
    }
}

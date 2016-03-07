namespace PizzaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVouchersTableAndSeed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        VoucherId = c.Int(nullable: false, identity: true),
                        VoucherCode = c.String(),
                        VoucherName = c.String(),
                        DayValid = c.String(),
                        numberOfPizzas = c.Int(nullable: false),
                        SizeOfPizza = c.String(),
                        VoucherCost = c.String(),
                        CollectionDelivery = c.String(),
                    })
                .PrimaryKey(t => t.VoucherId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vouchers");
        }
    }
}

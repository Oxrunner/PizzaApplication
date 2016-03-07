namespace PizzaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkOrderAndVoucherTogeather : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VoucherOrders",
                c => new
                    {
                        Voucher_VoucherId = c.Int(nullable: false),
                        Order_OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Voucher_VoucherId, t.Order_OrderId })
                .ForeignKey("dbo.Vouchers", t => t.Voucher_VoucherId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId, cascadeDelete: true)
                .Index(t => t.Voucher_VoucherId)
                .Index(t => t.Order_OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoucherOrders", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.VoucherOrders", "Voucher_VoucherId", "dbo.Vouchers");
            DropIndex("dbo.VoucherOrders", new[] { "Order_OrderId" });
            DropIndex("dbo.VoucherOrders", new[] { "Voucher_VoucherId" });
            DropTable("dbo.VoucherOrders");
        }
    }
}

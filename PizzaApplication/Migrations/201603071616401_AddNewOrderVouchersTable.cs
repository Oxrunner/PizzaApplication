namespace PizzaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewOrderVouchersTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VoucherOrders", "Voucher_VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.VoucherOrders", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.VoucherOrders", new[] { "Voucher_VoucherId" });
            DropIndex("dbo.VoucherOrders", new[] { "Order_OrderId" });
            CreateTable(
                "dbo.OrderVouchers",
                c => new
                    {
                        OrderVoucherId = c.Int(nullable: false, identity: true),
                        DiscountApplied = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(nullable: false),
                        VoucherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderVoucherId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Vouchers", t => t.VoucherId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.VoucherId);
            
            DropTable("dbo.VoucherOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VoucherOrders",
                c => new
                    {
                        Voucher_VoucherId = c.Int(nullable: false),
                        Order_OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Voucher_VoucherId, t.Order_OrderId });
            
            DropForeignKey("dbo.OrderVouchers", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.OrderVouchers", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderVouchers", new[] { "VoucherId" });
            DropIndex("dbo.OrderVouchers", new[] { "OrderId" });
            DropTable("dbo.OrderVouchers");
            CreateIndex("dbo.VoucherOrders", "Order_OrderId");
            CreateIndex("dbo.VoucherOrders", "Voucher_VoucherId");
            AddForeignKey("dbo.VoucherOrders", "Order_OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            AddForeignKey("dbo.VoucherOrders", "Voucher_VoucherId", "dbo.Vouchers", "VoucherId", cascadeDelete: true);
        }
    }
}

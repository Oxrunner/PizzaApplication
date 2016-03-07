using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PizzaApplication.Models
{
    public class PizzaStoreContext : DbContext
    {
        public PizzaStoreContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Toppings> Toppings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPizza> OrderPizzas { get; set; }
        public DbSet<PlacedOrders> PlacedOrders { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<OrderVoucher> OrderVouchers { get; set; }
    }
}
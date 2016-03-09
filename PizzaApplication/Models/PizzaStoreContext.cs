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

        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Toppings> Toppings { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderPizza> OrderPizzas { get; set; }
        public virtual DbSet<PlacedOrders> PlacedOrders { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<OrderVoucher> OrderVouchers { get; set; }
    }
}
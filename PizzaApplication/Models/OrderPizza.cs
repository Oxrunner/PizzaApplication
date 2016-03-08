using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzaApplication.Models
{
    public class OrderPizza
    {
        public int OrderPizzaId { get; set; }
        public decimal Price { get; set; }

        public String PizzaSize { get; set; }

        public String Toppings { get; set; }


        [ForeignKey("Order")]
        public int OrderRefId { get; set; }
        public Order Order { get; set; }


        [ForeignKey("Pizza")]
        public int PizzaRefId { get; set; }
        public Pizza Pizza { get; set; }
    }
}
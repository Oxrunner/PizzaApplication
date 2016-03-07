using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaApplication.Models
{
    public class Pizza
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public decimal SmallPrice { get; set; }
        public decimal MediumPrice { get; set; }
        public decimal LargePrice { get; set; }
        public virtual List<Toppings> Toppings { get; set; }
    }
}
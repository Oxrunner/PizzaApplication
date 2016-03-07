using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaApplication.Models
{
    public class Toppings
    {
        public int ToppingsId { get; set; }
        public string Name { get; set; }
        public virtual List<Pizza> Pizza { get; set; }
    }
}
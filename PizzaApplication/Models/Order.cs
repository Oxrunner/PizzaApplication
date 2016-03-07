using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaApplication.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public String userId { get; set; }

        public String deliveryCollection { get; set; }

    }
}
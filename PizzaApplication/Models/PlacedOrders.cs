using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzaApplication.Models
{
    public class PlacedOrders
    {
        public int PlacedOrdersId { get; set; }

        public String userId { get; set; }

        [ForeignKey("Order")]
        public int OrderRefId { get; set; }
        public Order Order { get; set; }
    }
}
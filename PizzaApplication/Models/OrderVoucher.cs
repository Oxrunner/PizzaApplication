using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzaApplication.Models
{
    public class OrderVoucher
    {
        public int OrderVoucherId { get; set; }

        public decimal DiscountApplied { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Voucher")]
        public int VoucherId { get; set; }
        public Voucher Voucher { get; set; }
    }
}
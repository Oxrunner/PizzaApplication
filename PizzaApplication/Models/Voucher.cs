using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaApplication.Models
{
    public class Voucher
    {
        public int VoucherId { get; set; }

        public String VoucherCode { get; set; }

        public String VoucherName { get; set; }

        public String DayValid { get; set; }

        public int numberOfPizzas { get; set; }

        public String SizeOfPizza { get; set; }

        public String VoucherCost { get; set; }

        public String CollectionDelivery { get; set; }

    }
}
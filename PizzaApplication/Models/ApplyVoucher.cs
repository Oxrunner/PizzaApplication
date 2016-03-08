using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaApplication.Models
{
    public class ApplyVoucher
    {
        PizzaStoreContext context = new PizzaStoreContext();
        private int orderId;
        public List<OrderPizza> pizzasInBasket;

        public ApplyVoucher(int passedOrderId, List<OrderPizza> passedPizzasInBasket)
        {
            orderId = passedOrderId;
            pizzasInBasket = passedPizzasInBasket;
        }
        
        public void applyDiscount(Voucher voucherDetails)
        {
            clearVouchersAlreadyOnOrder(voucherDetails.VoucherId);
            checkDay(voucherDetails.DayValid);
            if (pizzasInBasket.Count >= voucherDetails.numberOfPizzas)
            {
                Dictionary<String, List<OrderPizza>> pizzasGroupedBySize = groupPizzasBySize();
                String[] sizes = voucherDetails.SizeOfPizza.Split('/');
                foreach (String size in sizes)
                {
                    if (pizzasGroupedBySize.ContainsKey(size))
                    {
                        while (pizzasGroupedBySize[size].Count >= voucherDetails.numberOfPizzas)
                        {
                            OrderVoucher newOrderVoucher = new OrderVoucher();
                            newOrderVoucher.VoucherId = voucherDetails.VoucherId;
                            newOrderVoucher.OrderId = orderId;
                            List<OrderPizza> itemsToDiscount = new List<OrderPizza>();
                            int itemToGet = pizzasGroupedBySize[size].Count - 1;
                            for (int i = 0; i < voucherDetails.numberOfPizzas; i++)
                            {
                                itemsToDiscount.Add(pizzasGroupedBySize[size][itemToGet]);
                                pizzasGroupedBySize[size].RemoveAt(itemToGet);
                                itemToGet--;
                            }
                            newOrderVoucher.DiscountApplied = calculateDiscountAmmount(voucherDetails.VoucherCost, itemsToDiscount);
                            context.OrderVouchers.Add(newOrderVoucher);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        private void clearVouchersAlreadyOnOrder(int voucherId)
        {
            List<OrderVoucher> vouchersFound = context.OrderVouchers.Where(op => op.VoucherId == voucherId).Where(op => op.OrderId == orderId).ToList();
            foreach (OrderVoucher voucher in vouchersFound)
            {
                context.OrderVouchers.Remove(voucher);
                context.SaveChanges();
            }
        }

        private decimal calculateDiscountAmmount(String voucherCost, List<OrderPizza> items)
        {
            decimal discountAmount = 0.00m;
            decimal voucherCostDecimal;

            if (decimal.TryParse(voucherCost, out voucherCostDecimal))
            {

                discountAmount = getCostOfPizza(items) - voucherCostDecimal;
            }
            else
            {
                String[] sizes = voucherCost.Split(' ');
                if (sizes.Length == 2)
                {
                    if (decimal.TryParse(voucherCost, out voucherCostDecimal))
                    {
                        discountAmount = getCostOfPizza(items) - figureOutDiscountAmmount(items, sizes[0], voucherCostDecimal);
                    }
                }
            }
            return discountAmount;
        }

        private decimal figureOutDiscountAmmount(List<OrderPizza> items, String highestLowest, decimal numberOfPizzas)
        {
            decimal discountAmmount = 0.00m;

            if (highestLowest == "highest")
            {
                List<decimal> pizzaPrices = new List<decimal>();
                int count = 0;
                foreach (OrderPizza item in items)
                {
                    pizzaPrices.Add(item.Price);
                    count++;
                }
                pizzaPrices.Sort();
                for (int i = 0; i < numberOfPizzas; i++)
                {
                    pizzaPrices.RemoveAt(pizzaPrices.Count-1);
                }
                foreach (Decimal pizzaPrice in pizzaPrices)
                {
                    discountAmmount += pizzaPrice;
                }
            } 

            return discountAmmount;
        }

        private decimal getCostOfPizza(List<OrderPizza> items)
        {
            decimal costOfPizzas = 0.00m;
            foreach (OrderPizza item in items)
            {
                costOfPizzas += item.Price;
            }
            return costOfPizzas;
        }

        private Dictionary<String, List<OrderPizza>> groupPizzasBySize()
        {
            Dictionary<String, List<OrderPizza>> pizzasGroupedBySize = new Dictionary<String, List<OrderPizza>>();
            foreach (OrderPizza item in pizzasInBasket)
            {
                if (!pizzasGroupedBySize.ContainsKey(item.PizzaSize))
                {
                    pizzasGroupedBySize.Add(item.PizzaSize, new List<OrderPizza>());
                }
                pizzasGroupedBySize[item.PizzaSize].Add(item);
            }
            return pizzasGroupedBySize;
        }


        private Boolean checkDay(String validDay)
        {
            if (validDay != "")
            {
                String currentDay = System.DateTime.Now.DayOfWeek.ToString();
                if (currentDay != validDay)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
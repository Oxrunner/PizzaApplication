using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Data.Entity;
using System.Diagnostics;
using System.Web.Security;


namespace PizzaApplication.Models
{
    public class Basket
    {
        PizzaStoreContext context = new PizzaStoreContext();
        private String cacheKey = "PizzaOrder";
        private static MemoryCache cache = new MemoryCache("PizzaCache");
        private int orderId;
        private Order thisOrder;

        public List<OrderPizza> pizzasInBasket;
        public decimal totalPrice = 0.00m;
        public String deliveryCollection;
        public List<OrderVoucher> orderVouchers;
        

        public Basket(int orderIdPassed = 0)
        {
            if (orderIdPassed == 0)
            {
                if (cache.Get(cacheKey) == null)
                {
                    orderId = createNewOrder();
                    cache.Set(cacheKey, orderId, new CacheItemPolicy());
                }
                else
                {
                    orderId = (Int32)cache.Get(cacheKey);
                }
            }
            else
            {
                orderId = orderIdPassed;
            }
            thisOrder = context.Orders.Find(orderId);
            deliveryCollection = thisOrder.deliveryCollection;
            getPizzasInBasket(orderId);
            getOrderVouchers();
        }

        private void getOrderVouchers()
        {
            List<OrderVoucher> vouchersToCheck = context.OrderVouchers.Include(op => op.Voucher).Where(op => op.OrderId == orderId).ToList();
            ApplyVoucher applyVoucher = new ApplyVoucher(orderId, pizzasInBasket);
            foreach (OrderVoucher voucherDetails in vouchersToCheck)
            {
                applyVoucher.applyDiscount(voucherDetails.Voucher);
            }

            orderVouchers = context.OrderVouchers.Include(op => op.Voucher).Where(op => op.OrderId == orderId).ToList();
            decimal totalDiscount = 0.00m;
            foreach (OrderVoucher discount in orderVouchers)
            {
                totalDiscount += discount.DiscountApplied;
            }
            totalPrice = totalPrice - totalDiscount;
        }

        public void applyVoucher(String voucherCode)
        {
            List<Voucher> vouchersFound = context.Vouchers.Where(op => op.VoucherCode == voucherCode).ToList();
            if (vouchersFound.Count == 1)
            {
                Voucher voucherDetails = vouchersFound[0];
                ApplyVoucher applyVoucher = new ApplyVoucher(orderId, pizzasInBasket);
                applyVoucher.applyDiscount(voucherDetails);
            }
        }

        public void setDeliveryCollection(String chosen)
        {
            thisOrder.deliveryCollection = chosen;
            deliveryCollection = chosen;
            context.Entry(thisOrder).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void addToBasket(String pizzaSize, int pizzaId, String toppings="")
        {
            createNewItem(pizzaId, orderId, pizzaSize, toppings);
        }

        public void removeOrderFromBasket(int orderPizzaId)
        {
            OrderPizza orderPizza = context.OrderPizzas.Find(orderPizzaId);
            context.OrderPizzas.Remove(orderPizza);
            context.SaveChanges();
        }

        public void submitOrder(String userId)
        {
            PlacedOrders placedOrder = new PlacedOrders();
            placedOrder.OrderRefId = orderId;
            placedOrder.userId = userId;
            context.PlacedOrders.Add(placedOrder);
            context.SaveChanges();
            cache.Remove(cacheKey);
        }

        public void saveOrder(String userId)
        {
            int savedOrderId = 0;
            List<Order> orderList = context.Orders.Where(op => op.userId == userId).ToList();
            if (orderList.Count == 1)
            {
                savedOrderId = orderList[0].OrderId;
                removeOrderItems(savedOrderId);
            }
            else
            {
                removeMultipleSavedOrders(orderList);
                savedOrderId = createNewOrder();
            }

            List<OrderPizza> orderItems = context.OrderPizzas.Where(op => op.OrderRefId == orderId).ToList();
            foreach(OrderPizza item in orderItems){
                createNewItem(item.PizzaRefId, savedOrderId, item.PizzaSize);
            }
        }

        public void retriveOrder(String userId)
        {
            List<Order> order = context.Orders.Where(op => op.userId == userId).ToList();
            if (order.Count == 1)
            {
                int savedOrderId = order[0].OrderId;
                removeOrderItems(orderId);
                List<OrderPizza> orderItems = context.OrderPizzas.Where(op => op.OrderRefId == savedOrderId).ToList();
                foreach (OrderPizza item in orderItems)
                {
                    createNewItem(item.PizzaRefId, orderId, item.PizzaSize);
                }
            }
            else if (order.Count == 0)
            {
                return;
            }
            else
            {
                removeMultipleSavedOrders(order);
            }
        }

        private void createNewItem(int pizzaId, int orderId, String size, String toppings="")
        {
            OrderPizza newItem = new OrderPizza();
            newItem.PizzaRefId = pizzaId;
            newItem.OrderRefId = orderId;
            newItem.PizzaSize = size;
            newItem.Toppings = toppings;
            newItem.Price = getPizzaPrice(size, pizzaId) + calculateExtraToppingPrice(size, toppings);
            context.OrderPizzas.Add(newItem);
            context.SaveChanges();
        }

        private decimal calculateExtraToppingPrice(String size, String toppings)
        {
            decimal toppingPrice = 0.00m;
            if (size == "Small")
            {
                toppingPrice = 0.90m;
            }
            else if (size == "Medium")
            {
                toppingPrice = 1.00m;
            }
            else if (size == "Large")
            {
                toppingPrice = 1.15m;
            }
            if (toppings != "" && toppings != null)
            {
                String[] extraToppings = toppings.Split(',');
                return toppingPrice * extraToppings.Length;
            }
            return 0.00m;
        }

        private void removeOrderItems(int orderId)
        {
            List<OrderPizza> orderItems = context.OrderPizzas.Where(op => op.OrderRefId == orderId).ToList();
            foreach (OrderPizza item in orderItems)
            {
                context.OrderPizzas.Remove(item);
                context.SaveChanges();
            }
        }

        private void removeMultipleSavedOrders(List<Order> orderList)
        {
            foreach (Order orderFound in orderList)
            {
                orderFound.userId = "";
                context.Entry(orderFound).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private int createNewOrder()
        {
            Order newOrder = new Order();
            context.Orders.Add(newOrder);
            context.SaveChanges();
            return newOrder.OrderId;
        }

        private void getPizzasInBasket(int orderIdPassed)
        {
            pizzasInBasket = context.OrderPizzas.Include(op => op.Pizza).Where(op => op.OrderRefId == orderIdPassed).ToList();
            foreach (OrderPizza item in pizzasInBasket)
            {
                totalPrice += item.Price;
            }
        }

        private decimal getPizzaPrice(String size, int pizzaId)
        {
            Pizza pizza = context.Pizzas.Find(pizzaId);
            if (size == "Small")
            {
                return pizza.SmallPrice;
            }
            else if (size == "Medium")
            {
                return pizza.MediumPrice;
            }
            else if (size == "Large")
            {
                return pizza.LargePrice;
            }
            return 0.00m;
        }
    }
}
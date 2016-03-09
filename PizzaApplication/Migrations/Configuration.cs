namespace PizzaApplication.Migrations
{
    using PizzaApplication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PizzaApplication.Models.PizzaStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PizzaApplication.Models.PizzaStoreContext context)
        {
            context.Pizzas.AddOrUpdate(
                new Pizza { PizzaId = 1, PizzaName = "Original", SmallPrice = 8, MediumPrice = 9, LargePrice = 11 },
                new Pizza { PizzaId = 2, PizzaName = "Gimme the Meat", SmallPrice = 11, MediumPrice = 14.50m, LargePrice = 16.50m },
                new Pizza { PizzaId = 3, PizzaName = "Veggie Delight", SmallPrice = 10, MediumPrice = 13, LargePrice = 15 },
                new Pizza { PizzaId = 4, PizzaName = "Make Mine Hot", SmallPrice = 11, MediumPrice = 13, LargePrice = 15 },
                new Pizza { PizzaId = 5, PizzaName = "Create Your Own", SmallPrice = 8, MediumPrice = 9, LargePrice = 11 }
            );

            context.Toppings.AddOrUpdate(
                new Toppings { ToppingsId = 1, Name = "Cheese" },
                new Toppings { ToppingsId = 2, Name = "Tomato sauce" },
                new Toppings { ToppingsId = 3, Name = "Pepperoni" },
                new Toppings { ToppingsId = 4, Name = "Ham" },
                new Toppings { ToppingsId = 5, Name = "Chicken" },
                new Toppings { ToppingsId = 6, Name = "Minced beef" },
                new Toppings { ToppingsId = 7, Name = "Onions" },
                new Toppings { ToppingsId = 8, Name = "Green peppers" },
                new Toppings { ToppingsId = 9, Name = "Mushrooms" },
                new Toppings { ToppingsId = 10, Name = "Sweetcorn" },
                new Toppings { ToppingsId = 11, Name = "Jalapeno peppers" },
                new Toppings { ToppingsId = 12, Name = "Pineapple" },
                new Toppings { ToppingsId = 13, Name = "Sausage" },
                new Toppings { ToppingsId = 14, Name = "Bacon" }
            );

            context.Vouchers.AddOrUpdate(
                new Voucher { VoucherId = 1, VoucherCode = "2FOR1TUE", VoucherName = "Two for One Tuesdays", DayValid = "Tuesday", numberOfPizzas = 2, VoucherCost = "highest 1", CollectionDelivery = "Collection/Delivery", SizeOfPizza = "Medium/Large" },
                new Voucher { VoucherId = 2, VoucherCode = "3FOR2THUR", VoucherName = "Three for Two Thursdays", DayValid = "Thursday", numberOfPizzas = 3, VoucherCost = "highest 2", CollectionDelivery = "Collection/Delivery", SizeOfPizza = "Medium" },
                new Voucher { VoucherId = 3, VoucherCode = "FAMFRIDAYCOLL", VoucherName = "Family Friday", DayValid = "Friday", numberOfPizzas = 4, VoucherCost = "30.00", CollectionDelivery = "Collection", SizeOfPizza = "Medium" },
                new Voucher { VoucherId = 4, VoucherCode = "2LARGECOLL", VoucherName = "Two Large", DayValid = "", numberOfPizzas = 2, VoucherCost = "25.00", CollectionDelivery = "Collection", SizeOfPizza = "Large" },
                new Voucher { VoucherId = 5, VoucherCode = "2MEDIUMCOLL", VoucherName = "Two Medium", DayValid = "", numberOfPizzas = 2, VoucherCost = "18.00", CollectionDelivery = "Collection", SizeOfPizza = "Medium" },
                new Voucher { VoucherId = 6, VoucherCode = "2SMALLCOLL", VoucherName = "Two Small", DayValid = "", numberOfPizzas = 2, VoucherCost = "12.00", CollectionDelivery = "Collection", SizeOfPizza = "Small" }
            );
        }
    }
}
